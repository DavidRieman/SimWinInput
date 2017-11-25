// This file is part of the SimWinMouse project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinMouse

namespace SimWinMouse
{
    using Microsoft.Win32;
    using System;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>Simulate mouse events, such as moving the mouse cursor and clicking.</summary>
    public static class SimMouse
    {
        private const InteropMouse.MouseEventFlags AbsoluteMovement = InteropMouse.MouseEventFlags.Absolute | InteropMouse.MouseEventFlags.Movement;

        // Cache screen width/height as needed for mouse_event, but update them upon applicable display settings changes.
        private static int ScreenWidth, ScreenHeight;

        static SimMouse()
        {
            RecalculateScreen();
            SystemEvents.DisplaySettingsChanged += (sender, eventArgs) => RecalculateScreen();
        }
        
        /// <summary>Mouse input simulation options.</summary>
        /// <remarks>
        /// This is a subset of MouseEventFlags pairings intended for easy usage with the Simulate method.
        /// If you need finer control, consider using Interop.mouse_event directly instead.
        /// </remarks>
        public enum Action : uint
        {
            MoveOnly = AbsoluteMovement,
            LeftButtonDown = AbsoluteMovement | InteropMouse.MouseEventFlags.LeftDown,
            LeftButtonUp = AbsoluteMovement | InteropMouse.MouseEventFlags.LeftUp,
            MiddleButtonDown = AbsoluteMovement | InteropMouse.MouseEventFlags.MiddleDown,
            MiddleButtonUp = AbsoluteMovement | InteropMouse.MouseEventFlags.MiddleUp,
            RightButtonDown = AbsoluteMovement | InteropMouse.MouseEventFlags.RightDown,
            RightButtonUp = AbsoluteMovement | InteropMouse.MouseEventFlags.RightUp,
        }

        /// <summary>Simulates the specified mouse action.</summary>
        /// <param name="x">The horizontal pixel coordinate to place the mouse cursor at.</param>
        /// <param name="y">The vertical pixel coordinate to place the mouse cursor at.</param>
        /// <param name="mouseOption">The mouse state to simulate.</param>
        public static void Simulate(int x, int y, Action mouseOption)
        {
            var absX = 65535 * x / ScreenWidth;
            var absY = 65535 * y / ScreenHeight;
            InteropMouse.mouse_event((uint)mouseOption, absX, absY, 0, 0);
        }

        // TODO: Simulation of mouse scroll wheel movements. Something like:
        //public void SimulateScroll(int x, int y, scrollAmount)
        //public void SimulateScroll(scrollAmount)
        
        // TODO: Simulation of click events which omit movement, to click wherever the cursor already is.
        //public static void SimulateClick(MouseButtons mouseButton, int holdClickTime = 10)

        /// <summary>Simulates a mouse click at the specified location.</summary>
        /// <param name="x">The horizontal pixel coordinate to place the mouse cursor at.</param>
        /// <param name="y">The vertical pixel coordinate to place the mouse cursor at.</param>
        /// <param name="mouseButton">Which mouse button to simulate a click for.</param>
        /// <param name="holdClickTime">How long to simulate holding the mouse button down, in milliseconds.</param>
        /// <remarks>Basically this is just a pair of simulated mouse "down" and mouse "up" simulations in sequence.</remarks>
        public static void SimulateClick(int x, int y, MouseButtons mouseButton, int holdClickTime = 10)
        {
            Action mouseDownOption, mouseUpOption;
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    mouseDownOption = Action.LeftButtonDown;
                    mouseUpOption = Action.LeftButtonUp;
                    break;
                case MouseButtons.Middle:
                    mouseDownOption = Action.MiddleButtonDown;
                    mouseUpOption = Action.MiddleButtonUp;
                    break;
                case MouseButtons.Right:
                    mouseDownOption = Action.RightButtonDown;
                    mouseUpOption = Action.RightButtonUp;
                    break;
                default:
                    // TODO: Implement and test XButton1 and XButton2 with a fancy mouse. (Requires extra data.)
                    throw new NotSupportedException("The selected mouse button is not yet supported: " + mouseButton.ToString());
            }

            Simulate(x, y, mouseDownOption);
            Thread.Sleep(holdClickTime);
            Simulate(x, y, mouseUpOption);
        }

        private static void RecalculateScreen()
        {
            ScreenWidth = ScreenSize.Width();
            ScreenHeight = ScreenSize.Height();
        }
    }
}