// This file is part of the SimWinGamePad project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    public class SimGamePad
    {
        private static SimGamePad instance = new SimGamePad();
        private static string NoDriverMessage = "The ScpVBus driver used for simulating GamePad input was not found. Would you like to install it and retry?" + Environment.NewLine + "This may prompt for administrative privileges.";

        private bool[] isPluggedIn = new bool[4];

        // Note that ScpBus expects controller Numbers while we use Index; giving bus methods an arg of 0 for identifying controller is wrong.
        // While the ScpBus interface is well-established, we can at least use sensible indices and convert to number (via +1) at our own call sites.
        private ScpBus bus;

        private SimGamePad()
        {
            this.State = new SimulatedGamePadState[4] { new SimulatedGamePadState(), new SimulatedGamePadState(), new SimulatedGamePadState(), new SimulatedGamePadState() };
        }
        
        public static SimGamePad Instance { get { return instance; } }

        public SimulatedGamePadState[] State { get; private set; }

        /// <summary>Initialize SimGamePad, including the key driver for simulation.</summary>
        /// <param name="mayAutoInstallDriver">If true and the driver could not be loaded, asks the user if they'd like to install it, and automatically recovers if they do.</param>
        public void Initialize(bool mayAutoInstallDriver = true)
        {
            if (bus != null)
            {
                return; // Already initialized.
            }

            bool retryInit = false;
            do
            {
                try
                {
                    bus = new ScpBus();
                }
                catch (IOException)
                {
                    if (mayAutoInstallDriver)
                    {
                        var result = MessageBox.Show(NoDriverMessage, "Install", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            ScpDriverInstaller.Install();
                            retryInit = true;
                        }
                        else
                        {
                            throw new UserDeclinedDriverException("ScpVBus");
                        }
                    }
                }
            } while (retryInit);
        }

        public void ShutDown()
        {
            if (bus != null)
            {
                // Automatically unplug simulated controllers that we can't drive anymore.
                for (var i = 0; i < 4; i++)
                {
                    Unplug(i);
                }

                bus.Dispose();
                bus = null;
            }
        }

        public void PlugIn(int controllerIndex = 0)
        {
            EnsureInitialized();
            int i = controllerIndex;
            if (bus != null && !isPluggedIn[i])
            {
                bus.PlugIn(i + 1);
                isPluggedIn[i] = true;
            }
        }

        public void Unplug(int controllerIndex = 0)
        {
            EnsureInitialized();
            int i = controllerIndex;
            if (bus != null && isPluggedIn[i])
            {
                bus.Unplug(i + 1);
                isPluggedIn[i] = false;
            }
        }

        /// <summary>
        /// Use the specified control and release it to default state after a moment.
        /// For example, press and release a specified button, or move and return an extreme analog stick position.
        /// </summary>
        /// <param name="control">Which control to use.</param>
        /// <param name="controllerIndex">Which controller to use.</param>
        public void Use(GamePadControl control, int controllerIndex = 0)
        {
            EnsureInitialized();
            SetControl(control, controllerIndex);
            Thread.Sleep(10);
            ReleaseControl(control, controllerIndex);
        }

        public void SetControl(GamePadControl control, int controllerIndex = 0)
        {
            EnsureInitialized();
            int i = controllerIndex;
            if (!isPluggedIn[i])
            {
                PlugIn(controllerIndex);
            }

            var flagsToSet = Enum.GetValues(typeof(GamePadControl)).Cast<GamePadControl>().Where(c => c != GamePadControl.None && (control & c) != 0);
            foreach (var flag in flagsToSet)
            {
                if (flag <= GamePadControl.Y)
                {
                    State[i].Buttons |= flag;
                }
                else
                {
                    switch (flag)
                    {
                        case GamePadControl.LeftTrigger: State[i].LeftTrigger = byte.MaxValue; break;
                        case GamePadControl.RightTrigger: State[i].RightTrigger = byte.MaxValue; break;
                        case GamePadControl.LeftStickLeft: State[i].LeftStickX = short.MinValue; break;
                        case GamePadControl.LeftStickRight: State[i].LeftStickX = short.MaxValue; break;
                        case GamePadControl.LeftStickUp: State[i].LeftStickY = short.MaxValue; break;
                        case GamePadControl.LeftStickDown: State[i].LeftStickY = short.MinValue; break;
                        case GamePadControl.RightStickLeft: State[i].RightStickX = short.MinValue; break;
                        case GamePadControl.RightStickRight: State[i].RightStickX = short.MaxValue; break;
                        case GamePadControl.RightStickUp: State[i].RightStickY = short.MaxValue; break;
                        case GamePadControl.RightStickDown: State[i].RightStickY = short.MinValue; break;
                    }
                }
            }

            Update(controllerIndex);
        }

        public void ReleaseControl(GamePadControl control, int controllerIndex = 0)
        {
            EnsureInitialized();
            int i = controllerIndex;
            if (isPluggedIn[i])
            {
                var flagsToSet = Enum.GetValues(typeof(GamePadControl)).Cast<GamePadControl>().Where(c => c != GamePadControl.None && (control & c) != 0);
                foreach (var flag in flagsToSet)
                {
                    if (flag <= GamePadControl.Y)
                    {
                        State[i].Buttons &= ~flag;
                    }
                    else
                    {
                        switch (flag)
                        {
                            case GamePadControl.LeftTrigger: State[i].LeftTrigger = 0; break;
                            case GamePadControl.RightTrigger: State[i].RightTrigger = 0; break;
                            case GamePadControl.LeftStickLeft: State[i].LeftStickX = 0; break;
                            case GamePadControl.LeftStickRight: State[i].LeftStickX = 0; break;
                            case GamePadControl.LeftStickUp: State[i].LeftStickY = 0; break;
                            case GamePadControl.LeftStickDown: State[i].LeftStickY = 0; break;
                            case GamePadControl.RightStickLeft: State[i].RightStickX = 0; break;
                            case GamePadControl.RightStickRight: State[i].RightStickX = 0; break;
                            case GamePadControl.RightStickUp: State[i].RightStickY = 0; break;
                            case GamePadControl.RightStickDown: State[i].RightStickY = 0; break;
                        }
                    }
                }

                Update(controllerIndex);
            }
        }

        public void Update(int controllerIndex = 0)
        {
            EnsureInitialized();
            int i = controllerIndex;
            if (isPluggedIn[i])
            {
                var report = State[i].GetReport();
                // ScpBus uses a 1-based index rather than 0-based index.
                bus.Report(i + 1, report);
            }
        }

        private void EnsureInitialized()
        {
            if (bus == null)
            {
                throw new InvalidOperationException("Must Initialize SimGamePad before first usage.");
            }
        }
    }
}