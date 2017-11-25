// This file is part of the SimWinMouse project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System;
    using System.Runtime.InteropServices;

    public class InteropScreen
    {
        /// <summary>Options for GetDeviceCaps. There are many more; this is all we need for SimWinMouse though.</summary>
        internal enum DeviceCaps : int
        {
            HORZSIZE = 8,
            VERTSIZE = 10,
        }

        /// <summary>Retrieve device-specific information.</summary>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd144877(v=vs.85).aspx</remarks>
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr windowHandle, int caps);

        /// <summary>Retrieve a handle to a device context.</summary>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd144871(v=vs.85).aspx</remarks>
        [DllImport("User32.dll")]
        public extern static System.IntPtr GetDC(IntPtr windowHandle);

        /// <summary>Release a handle to a device context.</summary>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd162920(v=vs.85).aspx</remarks>
        [DllImport("User32.dll")]
        public extern static int ReleaseDC(IntPtr windowHandle, IntPtr deviceContextHandle);
    }
}