// This file is part of the SimWinGamePad project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput
// 
// For further details on the code this component was based on, see:
// https://github.com/DavidRieman/ScpDriverInterface/ (at X360Controller.cs)

namespace SimWinInput
{
    /// <summary>A simulated Xbox 360 controller.</summary>
    /// <remarks>After setting the desired values, use the GetReport() method to generate a controller report that can be used with the ScpBus Report() method.</remarks>
    public class SimulatedGamePadState
    {
        /// <summary>Generates a new SimulatedGamePad object with the default initial state (no buttons pressed, all analog inputs 0).</summary>
        public SimulatedGamePadState() { }

        /// <summary>The controller's currently pressed buttons.</summary>
        /// <remarks>These are flags. (GamePadControl.A | GamePadControl.X) would be mean both A and X are pressed. Not all values are applicable as buttons.</remarks>
        public GamePadControl Buttons { get; set; }

        /// <summary>The controller's left trigger analog input. Value can range from 0 to 255.</summary>
        public byte LeftTrigger { get; set; }

        /// <summary>The controller's right trigger analog input. Value can range from 0 to 255.</summary>
        public byte RightTrigger { get; set; }

        /// <summary>The controller's left stick X-axis. Value can range from -32,768 to 32,767.</summary>
        public short LeftStickX { get; set; }

        /// <summary>The controller's left stick Y-axis. Value can range from -32,768 to 32,767.</summary>
        public short LeftStickY { get; set; }

        /// <summary>The controller's right stick X-axis. Value can range from -32,768 to 32,767.</summary>
        public short RightStickX { get; set; }

        /// <summary>The controller's right stick Y-axis. Value can range from -32,768 to 32,767.</summary>
        public short RightStickY { get; set; }

        /// <summary>Reset all values to their default zero states.</summary>
        public void Reset()
        {
            this.Buttons = GamePadControl.None;
            this.LeftStickX = 0;
            this.LeftStickY = 0;
            this.LeftTrigger = 0;
            this.RightStickX = 0;
            this.RightStickY = 0;
            this.RightTrigger = 0;
        }

        /// <summary>Generates an Xbox 360 controller report which can be used with the ScpBus Report() method.</summary>
        /// <remarks>See http://free60.org/wiki/GamePad#Input_report for report details.</remarks>
        /// <returns>A 20-byte Xbox 360 controller report.</returns>
        public byte[] GetReport()
        {
            byte[] bytes = new byte[20];

            bytes[0] = 0x00;                                 // Message type (input report)
            bytes[1] = 0x14;                                 // Message size (20 bytes)

            bytes[2] = (byte)((ushort)Buttons & 0xFF);       // Buttons low
            bytes[3] = (byte)((ushort)Buttons >> 8 & 0xFF);  // Buttons high

            bytes[4] = LeftTrigger;                          // Left trigger
            bytes[5] = RightTrigger;                         // Right trigger

            bytes[6] = (byte)(LeftStickX & 0xFF);            // Left stick X-axis low
            bytes[7] = (byte)(LeftStickX >> 8 & 0xFF);       // Left stick X-axis high
            bytes[8] = (byte)(LeftStickY & 0xFF);            // Left stick Y-axis low
            bytes[9] = (byte)(LeftStickY >> 8 & 0xFF);       // Left stick Y-axis high

            bytes[10] = (byte)(RightStickX & 0xFF);          // Right stick X-axis low
            bytes[11] = (byte)(RightStickX >> 8 & 0xFF);     // Right stick X-axis high
            bytes[12] = (byte)(RightStickY & 0xFF);          // Right stick Y-axis low
            bytes[13] = (byte)(RightStickY >> 8 & 0xFF);     // Right stick Y-axis high

            // Remaining bytes are unused

            return bytes;
        }
    }
}