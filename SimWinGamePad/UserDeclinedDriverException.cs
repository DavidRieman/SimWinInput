// This file is part of the SimWinGamePad project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System;

    public class UserDeclinedDriverException : Exception
    {
        public UserDeclinedDriverException(string driverName) : base("User declined to install the " + driverName + " driver.")
        { }
    }
}