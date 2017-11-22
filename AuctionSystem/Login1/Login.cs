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
using AuctionSystem.Client;

namespace Login1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        private void loginBtn_Click(object sender, EventArgs e)
        {
            using (var db = new AuctionContext())
            {
                var t = from p in db.Users where p.Username == usernameTxtBox.Text select p;
                if (t.Any())
                {
                    Console.WriteLine("sss");
                    

                }
            }
        }
    }
}
