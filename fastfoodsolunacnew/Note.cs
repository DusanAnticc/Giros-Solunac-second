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

    public partial class Note : Form
    {
        public Form1 Main;

        public Note(Form1 forma)
        {
            InitializeComponent();
            Main = forma;
            button1.BackColor = Color.FromArgb(156, 156, 156);
            button2.BackColor = Color.FromArgb(156, 156, 156);
            button3.BackColor = Color.FromArgb(156, 156, 156);
            button4.BackColor = Color.FromArgb(156, 156, 156);

        }

        void fun(object sender)
        {
            Main.buttonClickSound();
            Button btn = sender as Button;
            Main.note = btn.Tag.ToString();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fun(sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fun(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fun(sender);
        }
      
        private void button4_Click_1(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            this.Close();
        }
        
    }
}
