using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notification
{
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();
        }
        public enum enmAction
        {
            wait,
            start,
            close
        }

        public enum enmType
        {
            Success,
            Warning,
            Error,
            Info
        }

        private Notification.enmAction action;

        private int x, y;

        public void showAlert(string msg, enmType type)
        {
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 1; i < 10; i++)
            {
                fname = "alert" + i.ToString();
                Notification frm = (Notification)Application.OpenForms[fname];

                if (frm == null)
                {
                    Name = fname;
                    x = Screen.PrimaryScreen.WorkingArea.Width - Width + 15;
                    y = Screen.PrimaryScreen.WorkingArea.Height - Height * i - 5 * i;
                    Location = new Point(x, y);
                    break;

                }

            }
            x = Screen.PrimaryScreen.WorkingArea.Width - Width - 5;

            switch (type)
            {
                case enmType.Success:
                    iconPicture.Image = AnonCrypter.Properties.Resources.success_30_blue;
                    break;
                case enmType.Error:
                    iconPicture.Image = AnonCrypter.Properties.Resources.warning_30;
                    break;
                case enmType.Info:
                    iconPicture.Image = AnonCrypter.Properties.Resources.information_30_blue;
                    break;
                case enmType.Warning:
                    break;
            }


            message.Text = msg;

            Show();
            action = enmAction.start;
            timer1.Interval = 1;
            timer1.Start();
        }

        private void message_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case enmAction.wait:
                    timer1.Interval = 5000;
                    action = enmAction.close;
                    break;
                case Notification.enmAction.start:
                    timer1.Interval = 1;
                    Opacity += 0.1;
                    if (x < Location.X)
                    {
                        Left--;
                    }
                    else
                    {
                        if (Opacity == 1.0)
                        {
                            action = Notification.enmAction.wait;
                        }
                    }
                    break;
                case enmAction.close:
                    timer1.Interval = 1;
                    Opacity -= 0.1;

                    Left -= 3;
                    if (Opacity == 0.0)
                    {
                        Close();
                    }
                    break;
            }
        }
    }
}