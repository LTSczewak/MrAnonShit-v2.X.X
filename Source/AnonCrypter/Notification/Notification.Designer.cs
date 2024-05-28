namespace Notification
{
    partial class Notification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.NotificationBorderless = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.iconPicture = new Guna.UI2.WinForms.Guna2PictureBox();
            this.message = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)(this.iconPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // NotificationBorderless
            // 
            this.NotificationBorderless.ContainerControl = this;
            this.NotificationBorderless.DockIndicatorTransparencyValue = 0.6D;
            this.NotificationBorderless.DragForm = false;
            this.NotificationBorderless.HasFormShadow = false;
            this.NotificationBorderless.ResizeForm = false;
            this.NotificationBorderless.TransparentWhileDrag = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox1.CustomIconSize = 12F;
            this.guna2ControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(272, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(28, 75);
            this.guna2ControlBox1.TabIndex = 1;
            // 
            // iconPicture
            // 
            this.iconPicture.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconPicture.FillColor = System.Drawing.Color.Transparent;
            this.iconPicture.Image = global::AnonCrypter.Properties.Resources.success_30_blue;
            this.iconPicture.ImageRotate = 0F;
            this.iconPicture.Location = new System.Drawing.Point(12, 0);
            this.iconPicture.Name = "iconPicture";
            this.iconPicture.Size = new System.Drawing.Size(31, 75);
            this.iconPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconPicture.TabIndex = 2;
            this.iconPicture.TabStop = false;
            // 
            // message
            // 
            this.message.BackColor = System.Drawing.Color.Transparent;
            this.message.Dock = System.Windows.Forms.DockStyle.Left;
            this.message.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.ForeColor = System.Drawing.Color.Silver;
            this.message.Location = new System.Drawing.Point(43, 0);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(223, 75);
            this.message.TabIndex = 371;
            this.message.Text = "Messgae";
            this.message.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.message.Click += new System.EventHandler(this.message_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(12, 75);
            this.guna2Panel1.TabIndex = 372;
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(7)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(300, 75);
            this.Controls.Add(this.message);
            this.Controls.Add(this.iconPicture);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Notification";
            this.Text = "Notification";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.iconPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm NotificationBorderless;
        internal System.Windows.Forms.Timer timer1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2PictureBox iconPicture;
        private System.Windows.Forms.Label message;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}