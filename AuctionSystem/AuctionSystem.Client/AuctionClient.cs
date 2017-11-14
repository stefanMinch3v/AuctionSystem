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
    public partial class AuctionClient : Form
    {
        public AuctionClient()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private static bool Maximized = false;
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
        private void myaccountbtn_Click(object sender, EventArgs e)
        {

        }

        private void newsBtn_Click(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void biddingBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
