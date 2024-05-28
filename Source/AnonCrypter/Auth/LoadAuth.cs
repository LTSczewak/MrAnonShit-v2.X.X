using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnonCrypter.Forms;
using KeyAuth;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AnonCrypter.Auth
{
    public class LoadAuth
    {
        public static void Alert(string msg, Notification.Notification.enmType type)
        {
            Notification.Notification frm = new Notification.Notification();
            frm.showAlert(msg, type);
        }

        public static bool SubExist(string name)
        {
            if (Program.KeyAuthApp.user_data.subscriptions.Exists(x => x.subscription == name))
                return true;
            return false;
        }
        static string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;

        }

        private void ShowResponse(string type)
        {
            //responseTimeLbl.Text = $"It took {api.responseTime} ms to {type}"; // you need to create a label called responseTimeLbl to display to a label.
            MessageBox.Show($"It took {api.responseTime} msg to {type}");
        }

        public static bool loginFunction(string username, string password)
        {
            bool result;
            Program.KeyAuthApp.login(username, password);
            if (Program.KeyAuthApp.response.success)
            {
                result = true;
                Alert(Program.KeyAuthApp.response.message, Notification.Notification.enmType.Success);
            }
            else
            {
                result = false;
                Alert(Program.KeyAuthApp.response.message, Notification.Notification.enmType.Error);
            }
            return result;
        }

        public static bool licenseFunction(string key, bool autologin)
        {
            bool result;
            Program.KeyAuthApp.license(key);
            if (Program.KeyAuthApp.response.success)
            {
                result = true;
                if (autologin)
                {
                    Alert("Automatically logged in!", Notification.Notification.enmType.Success);
                }
                else
                    Alert(Program.KeyAuthApp.response.message, Notification.Notification.enmType.Success);
            }
            else
            {
                result = false;
                if (autologin)
                {
                    Alert("Automatic login failed: " + Program.KeyAuthApp.response.message, Notification.Notification.enmType.Success);

                }
                Alert(Program.KeyAuthApp.response.message, Notification.Notification.enmType.Success);
            }
            return result;
        }

        public static void entryUpdate()
        {
            Program.KeyAuthApp.init();
            if (Program.KeyAuthApp.response.message == "invalidver")
            {
                if (!string.IsNullOrEmpty(Program.KeyAuthApp.app_data.downloadLink))
                {
                    DialogResult dialogResult = MessageBox.Show("Yes to open file in browser\nNo to download file automatically", "Auto update", MessageBoxButtons.YesNo);
                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            Process.Start(Program.KeyAuthApp.app_data.downloadLink);
                            Environment.Exit(0);
                            break;
                        case DialogResult.No:
                            WebClient webClient = new WebClient();
                            string destFile = Application.ExecutablePath;

                            string rand = random_string();

                            destFile = destFile.Replace(".exe", $"-{rand}.exe");
                            webClient.DownloadFile(Program.KeyAuthApp.app_data.downloadLink, destFile);

                            Process.Start(destFile);
                            Process.Start(new ProcessStartInfo()
                            {
                                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true,
                                FileName = "cmd.exe"
                            });
                            Environment.Exit(0);

                            break;
                        default:
                            MessageBox.Show("Invalid option");
                            Environment.Exit(0);
                            break;
                    }
                }
                MessageBox.Show("Version of this program does not match the one online. Furthermore, the download link online isn't set. You will need to manually obtain the download link from the developer");
                Environment.Exit(0);
            }

            if (!Program.KeyAuthApp.response.success)
            {
                MessageBox.Show(Program.KeyAuthApp.response.message);
                Environment.Exit(0);
            }

        }
    }
}