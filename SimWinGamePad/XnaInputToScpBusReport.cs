// This file is part of the SimWinGamePad project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    /* TODO: Dynamic to drop MonoGame.Framework hard dependency? Else move to separate small library when needed? Also, finish testing.
    using Microsoft.Xna.Framework.Input;

    public class XnaInputToScpBusReport
    {
        /// <summary>Generate an ScpBus-compatible "report" from a captured GamePadState.</summary>
        /// <remarks>This can be used, for example, to mirror real GamePad input read through XNA/MonoGame from one controller to additional simulated ports.</remarks>
        /// <param name="gamePad">The game pad state.</param>
        /// <returns>An ScpBus-compatible "report" (see http://free60.org/wiki/GamePad#Input_report for details).</returns>
        public static byte[] Generate(GamePadState gamePad)
        {
            byte[] bytes = new byte[20];

            bytes[0] = 0x00; // Message type (input report)
            bytes[1] = 0x14; // Message size (20 bytes)

            bytes[2] = GetButtonsLow(gamePad);
            bytes[3] = GetButtonsHigh(gamePad);

            bytes[4] = ScalePositiveNormalizedFloatToByte(gamePad.Triggers.Left);
            bytes[5] = ScalePositiveNormalizedFloatToByte(gamePad.Triggers.Right);

            short leftStickX = ScaleNormalizedFloatToShort(gamePad.ThumbSticks.Left.X);
            short leftStickY = ScaleNormalizedFloatToShort(gamePad.ThumbSticks.Left.Y);
            bytes[6] = (byte)(leftStickX & 0xFF);            // Left stick X-axis low
            bytes[7] = (byte)(leftStickX >> 8 & 0xFF);       // Left stick X-axis high
            bytes[8] = (byte)(leftStickY & 0xFF);            // Left stick Y-axis low
            bytes[9] = (byte)(leftStickY >> 8 & 0xFF);       // Left stick Y-axis high

            short rightStickX = ScaleNormalizedFloatToShort(gamePad.ThumbSticks.Right.X);
            short rightStickY = ScaleNormalizedFloatToShort(gamePad.ThumbSticks.Right.Y);
            bytes[10] = (byte)(rightStickX & 0xFF);          // Right stick X-axis low
            bytes[11] = (byte)(rightStickX >> 8 & 0xFF);     // Right stick X-axis high
            bytes[12] = (byte)(rightStickY & 0xFF);          // Right stick Y-axis low
            bytes[13] = (byte)(rightStickY >> 8 & 0xFF);     // Right stick Y-axis high

            // Remaining bytes are unused

            return bytes;
        }

        private static byte ScalePositiveNormalizedFloatToByte(float f)
        {
            return (byte)(f >= 1.0 ? 255 : f <= 0.0 ? 0 : f * 256);
        }

        private static short ScaleNormalizedFloatToShort(float f)
        {
            return f <= -1 ? short.MinValue : f >= 1 ? short.MaxValue : (short)(f * short.MaxValue);
        }

        private static byte GetButtonsLow(GamePadState gamePad)
        {
            byte b = 0;
            b |= gamePad.DPad.Up == ButtonState.Pressed ? (byte)GamePadControl.DPadUp : (byte)0;
            b |= gamePad.DPad.Down == ButtonState.Pressed ? (byte)GamePadControl.DPadDown : (byte)0;
            b |= gamePad.DPad.Left == ButtonState.Pressed ? (byte)GamePadControl.DPadLeft : (byte)0;
            b |= gamePad.DPad.Right == ButtonState.Pressed ? (byte)GamePadControl.DPadRight : (byte)0;
            b |= gamePad.Buttons.Start == ButtonState.Pressed ? (byte)GamePadControl.Start : (byte)0;
            b |= gamePad.Buttons.Back == ButtonState.Pressed ? (byte)GamePadControl.Back : (byte)0;
            b |= gamePad.Buttons.LeftStick == ButtonState.Pressed ? (byte)GamePadControl.LeftStickClick : (byte)0;
            b |= gamePad.Buttons.RightStick == ButtonState.Pressed ? (byte)GamePadControl.RightStickClick : (byte)0;
            return b;
        }

        private static byte GetButtonsHigh(GamePadState gamePad)
        {
            byte b = 0;
            b |= gamePad.Buttons.LeftShoulder == ButtonState.Pressed ? (byte)((int)GamePadControl.LeftShoulder >> 8) : (byte)0;
            b |= gamePad.Buttons.RightShoulder == ButtonState.Pressed ? (byte)((int)GamePadControl.RightShoulder >> 8) : (byte)0;
            // Can't read Guide button with public GamePadState; skipping GamePadControl.Guide bit.
            b |= gamePad.Buttons.A == ButtonState.Pressed ? (byte)((int)GamePadControl.A >> 8) : (byte)0;
            b |= gamePad.Buttons.B == ButtonState.Pressed ? (byte)((int)GamePadControl.B >> 8) : (byte)0;
            b |= gamePad.Buttons.X == ButtonState.Pressed ? (byte)((int)GamePadControl.X >> 8) : (byte)0;
            b |= gamePad.Buttons.Y == ButtonState.Pressed ? (byte)((int)GamePadControl.Y >> 8) : (byte)0;
            return b;
        }
    }*/
}