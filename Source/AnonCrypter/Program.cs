using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnonCrypter.Forms;
using KeyAuth;
using AnonCrypter.Auth;
using System.Runtime.InteropServices;
using System.IO;
using AnonCrypter.Settings;
using System.Diagnostics;

namespace AnonCrypter
{
    internal static class Program
    {
        private static readonly string _binPath = AppDomain.CurrentDomain.BaseDirectory + "bin";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Security.AntiDebug.routine();

            if (!Directory.Exists(_binPath))
            {
                Directory.CreateDirectory(_binPath);
                Process.Start("https://t.me/batchcrypter");
                Process.Start("https://t.me/mranontools");
            }

            Settings.Settings.SaveDirectory = _binPath;
            SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

    }
}