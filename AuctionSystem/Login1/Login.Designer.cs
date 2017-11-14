namespace Login1
{
    partial class Login
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.ebidBtn = new System.Windows.Forms.Button();
            this.loginLbl = new System.Windows.Forms.Label();
            this.passwordTxtBox = new System.Windows.Forms.TextBox();
            this.usernameTxtBox = new System.Windows.Forms.TextBox();
            this.passwordLbl = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginBtn = new System.Windows.Forms.Button();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.DimGray;
            this.topPanel.Controls.Add(this.maximizeBtn);
            this.topPanel.Controls.Add(this.exitBtn);
            this.topPanel.Controls.Add(this.ebidBtn);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1578, 103);
            this.topPanel.TabIndex = 10;
            // 
            // ebidBtn
            // 
            this.ebidBtn.BackColor = System.Drawing.Color.DimGray;
            this.ebidBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ebidBtn.FlatAppearance.BorderSize = 0;
            this.ebidBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ebidBtn.Font = new System.Drawing.Font("Harlow Solid Italic", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ebidBtn.ForeColor = System.Drawing.Color.Black;
            this.ebidBtn.Location = new System.Drawing.Point(0, 0);
            this.ebidBtn.Name = "ebidBtn";
            this.ebidBtn.Size = new System.Drawing.Size(330, 103);
            this.ebidBtn.TabIndex = 4;
            this.ebidBtn.Text = "E-Bid";
            this.ebidBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ebidBtn.UseVisualStyleBackColor = false;
            // 
            // loginLbl
            // 
            this.loginLbl.AutoSize = true;
            this.loginLbl.Font = new System.Drawing.Font("Century Gothic", 34F);
            this.loginLbl.ForeColor = System.Drawing.Color.White;
            this.loginLbl.Location = new System.Drawing.Point(305, -36);
            this.loginLbl.Name = "loginLbl";
            this.loginLbl.Size = new System.Drawing.Size(214, 83);
            this.loginLbl.TabIndex = 9;
            this.loginLbl.Text = "Login";
            // 
            // passwordTxtBox
            // 
            this.passwordTxtBox.Font = new System.Drawing.Font("Century Gothic", 16F);
            this.passwordTxtBox.Location = new System.Drawing.Point(686, 370);
            this.passwordTxtBox.Name = "passwordTxtBox";
            this.passwordTxtBox.Size = new System.Drawing.Size(263, 47);
            this.passwordTxtBox.TabIndex = 14;
            this.passwordTxtBox.UseSystemPasswordChar = true;
            // 
            // usernameTxtBox
            // 
            this.usernameTxtBox.Font = new System.Drawing.Font("Century Gothic", 16F);
            this.usernameTxtBox.Location = new System.Drawing.Point(686, 247);
            this.usernameTxtBox.Name = "usernameTxtBox";
            this.usernameTxtBox.Size = new System.Drawing.Size(263, 47);
            this.usernameTxtBox.TabIndex = 13;
            // 
            // passwordLbl
            // 
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.Font = new System.Drawing.Font("Century Gothic", 24F);
            this.passwordLbl.ForeColor = System.Drawing.Color.White;
            this.passwordLbl.Location = new System.Drawing.Point(300, 359);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(240, 58);
            this.passwordLbl.TabIndex = 12;
            this.passwordLbl.Text = "Password";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Font = new System.Drawing.Font("Century Gothic", 24F);
            this.usernameLbl.ForeColor = System.Drawing.Color.White;
            this.usernameLbl.Location = new System.Drawing.Point(300, 235);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(257, 58);
            this.usernameLbl.TabIndex = 11;
            this.usernameLbl.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 34F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(712, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 83);
            this.label1.TabIndex = 16;
            this.label1.Text = "Login";
            // 
            // loginBtn
            // 
            this.loginBtn.FlatAppearance.BorderSize = 0;
            this.loginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginBtn.Font = new System.Drawing.Font("Century Gothic", 18F);
            this.loginBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.loginBtn.Image = global::Login1.Properties.Resources.login;
            this.loginBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loginBtn.Location = new System.Drawing.Point(669, 473);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(292, 70);
            this.loginBtn.TabIndex = 17;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // maximizeBtn
            // 
            this.maximizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeBtn.BackgroundImage = global::Login1.Properties.Resources.expand;
            this.maximizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.maximizeBtn.FlatAppearance.BorderSize = 0;
            this.maximizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maximizeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.maximizeBtn.Location = new System.Drawing.Point(1502, 0);
            this.maximizeBtn.Name = "maximizeBtn";
            this.maximizeBtn.Size = new System.Drawing.Size(35, 35);
            this.maximizeBtn.TabIndex = 16;
            this.maximizeBtn.UseVisualStyleBackColor = true;
            // 
            // exitBtn
            // 
            this.exitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitBtn.BackColor = System.Drawing.Color.DimGray;
            this.exitBtn.BackgroundImage = global::Login1.Properties.Resources.error__2_;
            this.exitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exitBtn.FlatAppearance.BorderSize = 0;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Location = new System.Drawing.Point(1543, 0);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(35, 35);
            this.exitBtn.TabIndex = 7;
            this.exitBtn.UseVisualStyleBackColor = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1578, 844);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.loginLbl);
            this.Controls.Add(this.passwordTxtBox);
            this.Controls.Add(this.usernameTxtBox);
            this.Controls.Add(this.passwordLbl);
            this.Controls.Add(this.usernameLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "Login";
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button ebidBtn;
        private System.Windows.Forms.Label loginLbl;
        private System.Windows.Forms.TextBox passwordTxtBox;
        private System.Windows.Forms.TextBox usernameTxtBox;
        private System.Windows.Forms.Label passwordLbl;
        private System.Windows.Forms.Label usernameLbl;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button maximizeBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loginBtn;
    }
}