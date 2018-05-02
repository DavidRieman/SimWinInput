// This file is part of the SimWinGamePad project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System;
    using System.Collections.Generic;

    [Flags]
    public enum GamePadControl : Int32
    {
        None = 0,

        // This block of controls should map up with ScpBus expectations.
        DPadUp = 1 << 0,
        DPadDown = 1 << 1,
        DPadLeft = 1 << 2,
        DPadRight = 1 << 3,
        Start = 1 << 4,
        Back = 1 << 5,
        LeftStickClick = 1 << 6,
        RightStickClick = 1 << 7,
        LeftShoulder = 1 << 8,
        RightShoulder = 1 << 9,
        Guide = 1 << 10,
        A = 1 << 12,
        B = 1 << 13,
        X = 1 << 14,
        Y = 1 << 15,

        // The remaining flags are not associated with ScpBus, but can be used for other purposes.
        LeftTrigger = 1 << 16,
        RightTrigger = 1 << 17,
        LeftStickLeft = 1 << 18,
        LeftStickRight = 1 << 19,
        LeftStickUp = 1 << 20,
        LeftStickDown = 1 << 21,
        RightStickLeft = 1 << 22,
        RightStickRight = 1 << 23,
        RightStickUp = 1 << 24,
        RightStickDown = 1 << 25,
        LeftStickAsAnalog = 1 << 28,
        RightStickAsAnalog = 1 << 29,
        DPadAsAnalog = 1 << 30,
    }

    /// <summary>GamePadControls provides static helpers for handling GamePadControl information.</summary>
    public static class GamePadControls
    {
        /// <summary>Provides the set of GamePadControl entries which directly represent binary buttons which should have just on/off states.</summary>
        public static readonly GamePadControl[] BinaryControls = {
            GamePadControl.DPadUp, GamePadControl.DPadDown, GamePadControl.DPadLeft, GamePadControl.DPadRight,
            GamePadControl.Start, GamePadControl.Back,
            GamePadControl.LeftStickClick, GamePadControl.RightStickClick,
            GamePadControl.LeftShoulder, GamePadControl.RightShoulder,
            GamePadControl.Guide, GamePadControl.A, GamePadControl.B, GamePadControl.X, GamePadControl.Y
        };
    }
}
