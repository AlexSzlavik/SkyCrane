﻿using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace SkyCrane.NetCode
{
    public enum CommandType { MOVE, SHOOT, ATTACK, GOBLIN_ATTACK }

    public class Command : Marshable
    {
        public int entity_id;
        public CommandType ct;
        public Vector2 position;
        public Vector2 direction;

        public Command() { }

        public Command(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryReader br = new BinaryReader(ms);

            this.entity_id = br.ReadInt32();
            this.ct = (CommandType)br.ReadByte();
            this.position.X = (float)br.ReadDouble();
            this.position.Y = (float)br.ReadDouble();
            this.direction.X = (float)br.ReadDouble();
            this.direction.Y = (float)br.ReadDouble();
        }

        public byte[] getPacketData()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bb = new BinaryWriter(ms);
            bb.Write((int)entity_id);
            bb.Write((byte)ct);
            bb.Write((double)position.X);
            bb.Write((double)position.Y);
            bb.Write((double)direction.X);
            bb.Write((double)direction.Y);

            return ms.ToArray();
        }
    }
}
