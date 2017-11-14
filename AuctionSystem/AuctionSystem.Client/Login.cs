using AuctionSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuctionSystem.Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private static bool Maximized = false;
        private void loginBtn_Click(object sender, EventArgs e)
        {
            using (var db = new AuctionContext())
            {
                var t = from p in db.Users where p.Username == usernameTxtBox.Text select p;
                var b = db.Users.Where(p => p.Username == usernameTxtBox.Text).Select(p => p.Username);
                if (b.Any())
                {
                    this.Hide();
                    AuctionClient a = new AuctionClient();
                    a.Show();
                }
            }
        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void maximizeBtn_Click(object sender, EventArgs e)
        {
            if (Maximized)
            {
                WindowState = FormWindowState.Normal;
                Maximized = false;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                Maximized = true;
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
