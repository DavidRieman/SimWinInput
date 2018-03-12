// This file is part of the SimWinGamePad project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

namespace SimWinInput
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    public class ScpDriverInstaller
    {
        public static void Install()
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(assemblyLocation, "ScpDriverInstaller.exe");
            var process = Process.Start(filePath, "/install /silent");
            process.WaitForExit();
        }
    }
}