﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace SkyCrane.NetCode
{
    class RawClient
    {
        private Thread clientThread;
        private NetworkWorker nw;
        private IPEndPoint server;
        private bool go = true;
        public enum cState { DISCONNECTED, CONNECTED, TRYCONNECT };
        cState curState = cState.DISCONNECTED;

        // Semaphore to wait on for the server info to be known
        private Semaphore ready = new Semaphore(0, 1);

        // Queue of state changes to be passed off the the UI
        private Queue<StateChange> buffer = new Queue<StateChange>();

        public RawClient()
        {
            System.Console.WriteLine("Client Started");

            // Create the thread
            clientThread = new Thread(this.ClientstartFunc);
            clientThread.Name = "mainClientThread";

            clientThread.Start();
        }

        public bool connect(string host, int port)
        {
            // Store server info
            this.server = new IPEndPoint(IPAddress.Parse(host), port);

            // Client may now try to connect
            this.curState = cState.TRYCONNECT;

            //Spawn the client reader/writer threads
            this.nw = new NetworkWorker(server);

            // Inform the client thread that the server info is ready
            ready.Release();

            // Send the handshake request to the server
            this.handshake();

            // Wait for the connection to be established
            while (this.curState == cState.TRYCONNECT) ;

            return true;
        }

        public void exit()
        {
            this.go = false;
        }

        //Main routine, this does all the processing
        private void ClientstartFunc()
        {
            // Hold here until the server information has been provided
            ready.WaitOne();

            // Event Loop
            // Pull packets out of the network layer and handle them
            while (this.go)
            {
                Packet newPacket = nw.getNext(); // This is a blocking call! 

                // Handle timeout
                if (newPacket == null)
                {
                    Console.WriteLine("Timeout on receive");
                    switch (curState)
                    {
                        case cState.TRYCONNECT:
                            // Did not receive the expected HANDSHAKE message
                            // Restart the handshake
                            this.handshake();
                            break;
                        case cState.CONNECTED:
                            // The server may have died, ping the server to find out
                            this.pingServer();
                            break;
                        case cState.DISCONNECTED:
                        default:
                            // This should not happen, die screaming!
                            Environment.Exit(1);
                            break;
                    }
                }
                else
                {
                    // Handle the new packet 
                    switch (newPacket.ptype)
                    {
                        case Packet.PacketType.CMD:
                            Console.WriteLine("Should not be getting CMD packets from the server...");
                            Environment.Exit(1);
                            break;
                        case Packet.PacketType.HANDSHAKE:
                            Console.WriteLine("Handshake received from the server");

                            switch (curState)
                            {
                                case cState.TRYCONNECT:
                                    // The connection has succeeded!
                                    this.curState = cState.CONNECTED;
                                    break;
                                case cState.CONNECTED:
                                    // Repeat? This can be ignored ( I hope...)
                                    break;
                                case cState.DISCONNECTED:
                                default:
                                    // This should not happen, die screaming!
                                    Environment.Exit(1);
                                    break;
                            }

                            break;
                        case Packet.PacketType.STC:
                            Console.WriteLine("STC received from the server");

                            switch (curState)
                            {
                                case cState.TRYCONNECT:
                                    break;
                                case cState.CONNECTED:
                                    // Marshall the state change packet into an object
                                    StateChange newSTC = new StateChange(newPacket.data);

                                    // Add the state change object to the buffer for the UI
                                    lock (this.buffer)
                                    {
                                        buffer.Enqueue(newSTC);
                                    }

                                    break;
                                case cState.DISCONNECTED:
                                default:
                                    // This should not happen, die screaming!
                                    Environment.Exit(1);
                                    break;
                            }

                            break;
                        case Packet.PacketType.SYNC:
                            Console.WriteLine("SYNC received from the server");
                            
                            switch (curState)
                            {
                                case cState.TRYCONNECT:
                                    break;
                                case cState.CONNECTED:
                                    syncServer();
                                    break;
                                case cState.DISCONNECTED:
                                default:
                                    // This should not happen, die screaming!
                                    Environment.Exit(1);
                                    break;
                            }

                            break;
                        case Packet.PacketType.PING:
                            Console.WriteLine("PING received from the server");
                            
                            switch (curState)
                            {
                                case cState.TRYCONNECT:
                                    break;
                                case cState.CONNECTED:
                                    break;
                                case cState.DISCONNECTED:
                                default:
                                    // This should not happen, die screaming!
                                    Environment.Exit(1);
                                    break;
                            }

                            break;
                        default:
                            Console.WriteLine("Unknown packet type from the server...");
                            Environment.Exit(1);
                            break;
                    }
                }
            }
        }

        private void handshake()
        {
            HandshakePacket hs = new HandshakePacket();
            hs.setDest(server);
            this.nw.commitPacket(hs);
        }

        private void pingServer()
        {
            PingPacket pp = new PingPacket();
            pp.setDest(server);
            this.nw.commitPacket(pp);
        }

        private void syncServer()
        {
            SYNCPacket ss = new SYNCPacket();
            ss.setDest(server);
            this.nw.commitPacket(ss);
        }

        //OPERATORS
        public void sendCMD(List<Command> cmds)
        {
            foreach (Command c in cmds)
            {
                // Create the CMD Packet
                CMDPacket newCMD = new CMDPacket(c);
                newCMD.Dest = server;

                // Add the CMD packet to the network worker's send queue
                this.nw.commitPacket(newCMD);
            }
        }

        // Called by the UI to acquire the latest state from the server
        public List<StateChange> rcvUPD()
        {
            List<StateChange> newStates = new List<StateChange>();

            // Acquire a the buffer lock well emptying the buffer

            lock (this.buffer)
            {
                // Iterate over the buffer of states that have been acquired from the server
                while (buffer.Count > 0)
                {
                    newStates.Add(buffer.Dequeue());
                }
            }

            return newStates;
        }
    }
}
