using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fastfoodsolunacnew
{
    public partial class PomfritNumber : Form
    {
        public Form1 Main;

        public PomfritNumber(Form1 forma)
        {
            InitializeComponent();
            Main = forma;
        }

        private void PomfritNumber_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(156, 156, 156);
            button2.BackColor = Color.FromArgb(156, 156, 156);
            button3.BackColor = Color.FromArgb(156, 156, 156);
            button4.BackColor = Color.FromArgb(156, 156, 156);
            button5.BackColor = Color.FromArgb(156, 156, 156);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            Main.PomfritCount = 1;            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            Main.PomfritCount = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            Main.PomfritCount = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            Main.PomfritCount = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            Main.PomfritCount = 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
