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
            // If the installer was included already with the running executable, prefer to run it from there.
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(assemblyLocation, "ScpDriverInstaller.exe");

            // If the file did not exist there, let's try to extract it there. (Or perhaps we should prefer system temp directory?)
            if (!File.Exists(filePath))
            {
                // Attempt to extract the executable as a resource
                File.WriteAllBytes(filePath, SimWinGamePad.Properties.Resources.ScpDriverInstaller);
            }

            var process = Process.Start(filePath, "/install /silent");
            process.WaitForExit();
        }
    }
}