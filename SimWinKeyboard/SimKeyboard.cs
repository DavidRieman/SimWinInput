// This file is part of the SimWinMouse project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System.Threading;

    /// <summary>Simulate keyboard events, such as pressing a key.</summary>
    public class SimKeyboard
    {
        /// <summary>Simulates a keyboard key in the 'pressed' state.</summary>
        /// <param name="keyCode">The key code to simulate.</param>
        /// <remarks>
        /// This method is non-blocking; a key set to KeyDown state will remain so until a KeyUp state is set
        /// (either through another call to a key simulation method, or real key state changes occur).
        /// </remarks>
        public static void KeyDown(byte keyCode)
        {
            InteropKeyboard.keybd_event(keyCode, 0, (uint)InteropKeyboard.KeyboardEventFlags.KeyDown, 0);
        }

        /// <summary>Simulates a keyboard key in the 'unpressed' state.</summary>
        /// <param name="keyCode">The key code to simulate.</param>
        /// <remarks>
        /// This method is non-blocking; a key set to KeyDown state will remain so until a KeyUp state is set
        /// (either through another call to a key simulation method, or real key state changes occur).
        /// </remarks>
        public static void KeyUp(byte keyCode)
        {
            InteropKeyboard.keybd_event(keyCode, 0, (uint)InteropKeyboard.KeyboardEventFlags.KeyUp, 0);
        }

        /// <summary>Simulates a keyboard keystroke (press and release).</summary>
        /// <param name="keyCode">The key code to simulate.</param>
        /// <param name="holdKeyTime">The time, in milliseconds, to simulate holding the key down.</param>
        /// <remarks>This method is thread-blocking for the duration of the simulated key press.</remarks>
        public static void Press(byte keyCode, int holdKeyTime = 10)
        {
            KeyDown(keyCode);
            Thread.Sleep(holdKeyTime);
            KeyUp(keyCode);
        }
    }
}