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
    public partial class OrderSize : Form
    {
        public Form1 Main;

        public OrderSize(Form1 forma)
        {
            InitializeComponent();
            Main = forma;
            button1.BackColor = Color.FromArgb(54, 54, 54);
            button2.BackColor = Color.FromArgb(54, 54, 54);
            button3.BackColor = Color.FromArgb(54, 54, 54);

        }

        void fun(object sender)
        {
            Button btn = sender as Button;
            Main.tunaOrder = btn.Tag.ToString();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fun(sender);
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            fun(sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fun(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            this.Close();
        }
    }
}
