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
    public partial class telefonskaNarudzba : Form
    {
        public Form1 Main;
        System.Diagnostics.Process p=null;
        public telefonskaNarudzba(Form1 forma)
        {
            InitializeComponent();
            Main = forma;
        }

        private void telefonskaNarudzba_Load(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            button5.Text = time.Hour.ToString();
            button2.Text = time.Minute.ToString();

            button1.BackColor = Color.FromArgb(156, 156, 156);
            button2.BackColor = Color.FromArgb(156, 156, 156);
            button3.BackColor = Color.FromArgb(156, 156, 156);
            button4.BackColor = Color.FromArgb(156, 156, 156);
            button5.BackColor = Color.FromArgb(156, 156, 156);
            button6.BackColor = Color.FromArgb(156, 156, 156);
            button7.BackColor = Color.FromArgb(156, 156, 156);
            button8.BackColor = Color.FromArgb(156, 156, 156);
            textBox1.BackColor = Color.FromArgb(156, 156, 156);


        }

        private void button7_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            Main.phoneName = textBox1.Text;
            Main.phoneTime = button5.Text + ":" + button2.Text;
            //if(p!=null)
            //    p.Kill();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            //if (p != null)
            //    p.Kill();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(e!=null)
                Main.buttonClickSound();
            int hour = int.Parse(button5.Text) + 1;
            if (hour > 23)
                hour = 0;
            button5.Text = hour.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            int hour = int.Parse(button5.Text) - 1;
            if (hour <0)
                hour = 23;
            button5.Text = hour.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            int min = int.Parse(button2.Text) + 1;
            while (min % 5 != 0)
                min++;
            if (min > 55)
            {
                min = 0;
                button1_Click(button4, null);
            }

            button2.Text = min.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main.buttonClickSound();
            int min = int.Parse(button2.Text) - 1;
            while (min % 5 != 0)
                min--;
            if (min < 0)
            {
                min = 55;
                button4_Click(button4, null);
            }
            button2.Text = min.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }        

        private void textBox1_Click(object sender, EventArgs e)
        {
          //p=System.Diagnostics.Process.Start(@"C:\windows\system32\osk.exe");
          //p.Kill();
        }
    }
}