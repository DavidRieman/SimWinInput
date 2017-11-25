// This file is part of the SimWinMouse project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System;

    /// <summary>Methods for gathering screen size information.</summary>
    public class ScreenSize
    {
        /// <summary>Gets the width of the physical screen.</summary>
        static public int Width()
        {
            IntPtr globalDeviceContext = InteropScreen.GetDC(IntPtr.Zero);
            int i = InteropScreen.GetDeviceCaps(globalDeviceContext, (int)InteropScreen.DeviceCaps.HORZSIZE);
            InteropScreen.ReleaseDC(IntPtr.Zero, globalDeviceContext);
            return i;
        }

        /// <summary>Gets the height of the physical screen.</summary>
        static public int Height()
        {
            IntPtr globalDeviceContext = InteropScreen.GetDC(IntPtr.Zero);
            int i = InteropScreen.GetDeviceCaps(globalDeviceContext, (int)InteropScreen.DeviceCaps.VERTSIZE);
            InteropScreen.ReleaseDC(IntPtr.Zero, globalDeviceContext);
            return i;
        }
    }
}