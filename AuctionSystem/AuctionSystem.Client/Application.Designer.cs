namespace AuctionSystem.Client
{
    partial class MainPanel
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.ebidBtn = new System.Windows.Forms.Button();
            this.leftMenuPanel = new System.Windows.Forms.Panel();
            this.selectionPanel = new System.Windows.Forms.Panel();
            this.newsBtn = new System.Windows.Forms.Button();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.myaccountbtn = new System.Windows.Forms.Button();
            this.catalogueBtn = new System.Windows.Forms.Button();
            this.biddingBtn = new System.Windows.Forms.Button();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.leftMenuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 100);
            this.panel3.TabIndex = 0;
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
            this.topPanel.Size = new System.Drawing.Size(1539, 103);
            this.topPanel.TabIndex = 2;
            this.topPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
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
            this.ebidBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // leftMenuPanel
            // 
            this.leftMenuPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.leftMenuPanel.Controls.Add(this.selectionPanel);
            this.leftMenuPanel.Controls.Add(this.newsBtn);
            this.leftMenuPanel.Controls.Add(this.settingsBtn);
            this.leftMenuPanel.Controls.Add(this.myaccountbtn);
            this.leftMenuPanel.Controls.Add(this.catalogueBtn);
            this.leftMenuPanel.Controls.Add(this.biddingBtn);
            this.leftMenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftMenuPanel.Location = new System.Drawing.Point(0, 103);
            this.leftMenuPanel.Name = "leftMenuPanel";
            this.leftMenuPanel.Size = new System.Drawing.Size(330, 800);
            this.leftMenuPanel.TabIndex = 3;
            this.leftMenuPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // selectionPanel
            // 
            this.selectionPanel.BackColor = System.Drawing.Color.DimGray;
            this.selectionPanel.Location = new System.Drawing.Point(0, 53);
            this.selectionPanel.Name = "selectionPanel";
            this.selectionPanel.Size = new System.Drawing.Size(17, 70);
            this.selectionPanel.TabIndex = 5;
            // 
            // newsBtn
            // 
            this.newsBtn.FlatAppearance.BorderSize = 0;
            this.newsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newsBtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newsBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.newsBtn.Image = global::AuctionSystem.Client.Properties.Resources.news_icon_136481;
            this.newsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newsBtn.Location = new System.Drawing.Point(16, 53);
            this.newsBtn.Name = "newsBtn";
            this.newsBtn.Size = new System.Drawing.Size(298, 70);
            this.newsBtn.TabIndex = 9;
            this.newsBtn.Text = "News";
            this.newsBtn.UseVisualStyleBackColor = true;
            // 
            // settingsBtn
            // 
            this.settingsBtn.FlatAppearance.BorderSize = 0;
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.settingsBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.settingsBtn.Image = global::AuctionSystem.Client.Properties.Resources.settings_icon_14953;
            this.settingsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.settingsBtn.Location = new System.Drawing.Point(16, 704);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(314, 70);
            this.settingsBtn.TabIndex = 8;
            this.settingsBtn.Text = "    Settings";
            this.settingsBtn.UseVisualStyleBackColor = true;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // myaccountbtn
            // 
            this.myaccountbtn.FlatAppearance.BorderSize = 0;
            this.myaccountbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.myaccountbtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.myaccountbtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.myaccountbtn.Image = global::AuctionSystem.Client.Properties.Resources.account_icon_25499;
            this.myaccountbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.myaccountbtn.Location = new System.Drawing.Point(10, 281);
            this.myaccountbtn.Name = "myaccountbtn";
            this.myaccountbtn.Size = new System.Drawing.Size(304, 70);
            this.myaccountbtn.TabIndex = 7;
            this.myaccountbtn.Text = "           My account";
            this.myaccountbtn.UseVisualStyleBackColor = true;
            // 
            // catalogueBtn
            // 
            this.catalogueBtn.FlatAppearance.BorderSize = 0;
            this.catalogueBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.catalogueBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.catalogueBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.catalogueBtn.Image = global::AuctionSystem.Client.Properties.Resources.catalog;
            this.catalogueBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.catalogueBtn.Location = new System.Drawing.Point(16, 205);
            this.catalogueBtn.Name = "catalogueBtn";
            this.catalogueBtn.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.catalogueBtn.Size = new System.Drawing.Size(298, 70);
            this.catalogueBtn.TabIndex = 6;
            this.catalogueBtn.Text = "   Catalog";
            this.catalogueBtn.UseVisualStyleBackColor = true;
            this.catalogueBtn.Click += new System.EventHandler(this.catalogueBtn_Click);
            // 
            // biddingBtn
            // 
            this.biddingBtn.FlatAppearance.BorderSize = 0;
            this.biddingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.biddingBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.biddingBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.biddingBtn.Image = global::AuctionSystem.Client.Properties.Resources.auction_icon_16281;
            this.biddingBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.biddingBtn.Location = new System.Drawing.Point(16, 129);
            this.biddingBtn.Name = "biddingBtn";
            this.biddingBtn.Size = new System.Drawing.Size(298, 70);
            this.biddingBtn.TabIndex = 5;
            this.biddingBtn.Text = "   Bidding";
            this.biddingBtn.UseVisualStyleBackColor = true;
            this.biddingBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // maximizeBtn
            // 
            this.maximizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeBtn.BackgroundImage = global::AuctionSystem.Client.Properties.Resources.expand;
            this.maximizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.maximizeBtn.FlatAppearance.BorderSize = 0;
            this.maximizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maximizeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.maximizeBtn.Location = new System.Drawing.Point(1463, 0);
            this.maximizeBtn.Name = "maximizeBtn";
            this.maximizeBtn.Size = new System.Drawing.Size(35, 35);
            this.maximizeBtn.TabIndex = 7;
            this.maximizeBtn.UseVisualStyleBackColor = true;
            this.maximizeBtn.Click += new System.EventHandler(this.maximizeBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitBtn.BackColor = System.Drawing.Color.DimGray;
            this.exitBtn.BackgroundImage = global::AuctionSystem.Client.Properties.Resources.error__2_;
            this.exitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exitBtn.FlatAppearance.BorderSize = 0;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Location = new System.Drawing.Point(1504, 0);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(35, 35);
            this.exitBtn.TabIndex = 6;
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1539, 903);
            this.Controls.Add(this.leftMenuPanel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.leftMenuPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel leftMenuPanel;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Button myaccountbtn;
        private System.Windows.Forms.Button catalogueBtn;
        private System.Windows.Forms.Button biddingBtn;
        private System.Windows.Forms.Button newsBtn;
        private System.Windows.Forms.Button ebidBtn;
        private System.Windows.Forms.Panel selectionPanel;
        private System.Windows.Forms.Button maximizeBtn;
        private System.Windows.Forms.Button exitBtn;
    }
}

