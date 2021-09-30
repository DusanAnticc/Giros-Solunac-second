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
    public partial class SettingsForm : Form
    {
        public Form1 MainForm { get; set; }

        public SettingsForm(Form1 mainForm)
        {
            MainForm = mainForm;
            InitializeComponent();
        }

        public void LoadSettings()
        {
            var interval = int.Parse(Properties.Settings.Default["RemovingInterval"].ToString());

            button1.Text = (interval / 60000).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {           
            //MainForm.UpdateInterval(int.Parse(button1.Text) * 60000);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.Parse(button1.Text)+1 > 2000)
            {
                MessageBox.Show("Vrijednost ne moze biti veca od 2000", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            button1.Text = (int.Parse(button1.Text) + 1).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (int.Parse(button1.Text) - 1 < 1)
            {
                MessageBox.Show("Vrijednost ne moze biti manja od 1", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            button1.Text = (int.Parse(button1.Text) - 1).ToString();
        }
    }
}

