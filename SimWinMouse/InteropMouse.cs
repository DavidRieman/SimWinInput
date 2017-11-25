// This file is part of the SimWinMouse project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>Exposes relevant mouse methods and data from DLLs.</summary>
    public static class InteropMouse
    {
        /// <summary>Flags for mouse_event.</summary>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646260(v=vs.85).aspx</remarks>
        [Flags]
        public enum MouseEventFlags : uint
        {
            Movement = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            WheelTilt = 0x1000,
            Absolute = 0x8000,
        }

        /// <summary>Simulate a mouse event with the system.</summary>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646260(v=vs.85).aspx</remarks>
        [DllImport("user32.dll")]
        public static extern void mouse_event(uint flags, int x, int y, int data, int extraInfo);
    }
}