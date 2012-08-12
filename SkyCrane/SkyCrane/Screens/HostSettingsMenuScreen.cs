#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System.Text;
#endregion

namespace SkyCrane.Screens
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class HostSettingsMenuScreen : MenuScreen
    {
        #region Initialization

        bool host;
        MenuEntry hostAddressMenuEntry;
        MenuEntry hostPortMenuEntry;

        string lastAddress = "";
        static StringBuilder hostAddress = new StringBuilder(); // Host's address (IP or hostname)
        const int MAX_ADDRESS_LENGTH = 32;
        string lastPort = "9999";
        static StringBuilder hostPort = new StringBuilder("9999"); // Host's port
        const int MAX_PORT_LENGTH = 5;
        const int MIN_PORT = 1;
        const int MAX_PORT = 65535;

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        /// <param name="host">Whether or not this player is the host.</param>
        /// <param name="multiplayer">Whether or not this game is multiplayer.</param>
        public HostSettingsMenuScreen(bool host)
            : base(host ? "Host Game" : "Join Game")
        {
            this.host = host; // Is this player the host?

            if (!host) // If we're not the host, then we need to specify an address
            {
                hostAddressMenuEntry = new MenuEntry(string.Empty);
                hostAddressMenuEntry.Selected += HostAddressMenuEntrySelected;
                hostAddressMenuEntry.Typed += HostAddressMenuEntryTyped;
                MenuEntries.Add(hostAddressMenuEntry);
            }

            // Both the host and the client should specify ports
            hostPortMenuEntry = new MenuEntry(string.Empty);
            hostPortMenuEntry.Selected += HostPortMenuEntrySelected;
            hostPortMenuEntry.Typed += HostPortMenuEntryTyped;
            MenuEntries.Add(hostPortMenuEntry);
            
            SetMenuEntryText(); // Set the initial menu text

            // Add the select and back options
            MenuEntry continueMenuEntry = new MenuEntry(host ? "Create Game" : "Connect to Game");
            continueMenuEntry.Selected += ContinueMenuEntrySelected;
            MenuEntries.Add(continueMenuEntry);

            MenuEntry backMenuEntry = new MenuEntry("Back");
            backMenuEntry.Selected += OnCancel;
            MenuEntries.Add(backMenuEntry);
            
            return;
        }

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            if (!host) // Only non-hosts should need to enter an address
            {
                hostAddressMenuEntry.Text = "Host Address: " + hostAddress;
            }
            hostPortMenuEntry.Text = "Host Port: " + hostPort;
            return;
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void HostAddressMenuEntrySelected(object sender, PlayerInputEventArgs e)
        {
            TypingInput = true;
            return;
        }

        /// <summary>
        /// Event handler for typing this into the host address.
        /// </summary>
        void HostAddressMenuEntryTyped(object sender, PlayerInputEventArgs e)
        {
            bool resetText = false;
            if (e.KeysTyped != string.Empty && hostAddress.Length < MAX_ADDRESS_LENGTH) // Add a character
            {
                hostAddress.Append(e.KeysTyped);
                resetText = true;
            }
            if (e.TypingBackspace && hostAddress.Length > 0) // Remove a character
            {
                hostAddress.Remove(hostAddress.Length - 1, 1);
                resetText = true;
            }
            if (e.TypingAccepted) // User has entered a value
            {
                lastAddress = hostAddress.ToString();
            }
            else if (e.TypingCancelled) // User has cancelled their input
            {
                hostAddress.Clear();
                hostAddress.Append(lastAddress);
                resetText = true;
            }

            if (resetText) // Update the on-screen text
            {
                SetMenuEntryText();
            }
            return;
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void HostPortMenuEntrySelected(object sender, PlayerInputEventArgs e)
        {
            TypingInput = true;
            return;
        }

        /// <summary>
        /// Event handler for typing this into the host address.
        /// </summary>
        void HostPortMenuEntryTyped(object sender, PlayerInputEventArgs e)
        {
            bool resetText = false;
            if (e.KeysTyped != string.Empty && hostPort.Length < MAX_PORT_LENGTH) // Add a character
            {
                for (int i = 0; i < e.KeysTyped.Length; i += 1)
                {
                    if (char.IsDigit(e.KeysTyped[i]))
                    {
                        hostPort.Append(e.KeysTyped[i]);
                        resetText = true;
                    }
                }
            }
            if (e.TypingBackspace && hostPort.Length > 0) // Remove a character
            {
                hostPort.Remove(hostPort.Length - 1, 1);
                resetText = true;
            }
            if (e.TypingAccepted) // User has entered a value
            {
                lastPort = hostPort.ToString();
            }
            else if (e.TypingCancelled) // User has cancelled their input
            {
                hostPort.Clear();
                hostPort.Append(lastPort);
                resetText = true;
            }
            if (resetText) // Update the on-screen text
            {
                SetMenuEntryText();
            }
            return;
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void ContinueMenuEntrySelected(object sender, PlayerInputEventArgs e)
        {
            // TODO: Connect with the host
            ScreenManager.AddScreen(new CharacterSelectMenuScreen(host, true), e.PlayerIndex);
            return;
        }

        #endregion
    }
}
