// This file is part of the SimWinMouse project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System;
    using System.Runtime.InteropServices;

    public class InteropKeyboard
    {
        /// <summary>Keyboard key actions.</summary>
        /// <remarks>These values should match those expected by the keybd_event native method.</remarks>
        [Flags]
        public enum KeyboardEventFlags : uint
        {
            /// <summary>The keyboard key is in a 'down' or 'pressed' state.</summary>
            KeyDown = 0x0,

            /// <summary>The keyboard key is an 'extended' key (KEYEVENTF_EXTENDEDKEY).</summary>
            /// <remarks></remarks>
            KeyExtended = 0x1,

            /// <summary>The keyboard key is in an 'up' or 'unpressed' state (KEYEVENTF_KEYUP).</summary>
            KeyUp = 0x2,
        }

        /// <summary>Simulate a keyboard keystroke event.</summary>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646304(v=vs.85).aspx</remarks>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
    }
}
