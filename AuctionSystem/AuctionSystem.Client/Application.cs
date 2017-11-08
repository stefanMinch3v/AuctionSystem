using System;
using System.Windows.Forms;

namespace AuctionSystem.Client
{
    public partial class MainPanel : Form
    {
        public MainPanel()
        {
            
            InitializeComponent();
           
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void newsBtn_Click(object sender, EventArgs e)
        {

        }

        private void catalogueBtn_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void settingsBtn_Click(object sender, EventArgs e)
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

        private void minimizeBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
