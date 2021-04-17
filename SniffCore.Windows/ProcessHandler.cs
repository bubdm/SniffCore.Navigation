using System.Diagnostics;

namespace SniffCore.Windows
{
    public static class ProcessHandler
    {
        public static void Restart()
        {
            var process = Process.GetCurrentProcess();
            var module = process.MainModule;
            if (module == null)
                return;

            var info = new ProcessStartInfo
            {
                Arguments = "/C ping 127.0.0.1 -n 2 && \"" + module.FileName + "\"", // Delay of 2-3 seconds by self-ping
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            Process.Start(info);
            process.Kill();
        }
    }
}