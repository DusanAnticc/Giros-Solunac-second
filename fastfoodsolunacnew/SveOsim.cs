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

    public partial class SveOsim : Form
    {
        List<SupplementData> SupplementsExcept { get; set; }

        public SveOsim(Form1 forma)
        {
            InitializeComponent();
            setSupplementsButtons(forma.Supplements, tableLayoutPanel1);
            buttonActivity(tableLayoutPanel1);
            SupplementsExcept = forma.SupplementsExcept;
        }

        void buttonActivity(TableLayoutPanel table)
        {
            foreach (Control item in table.Controls)
            {
                if (item is Button)
                {
                    item.BackColor = SystemColors.Control;
                }
            }
        }

        void supplementHandler(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor == SystemColors.Control)
            {
                string imagepath = "Resources\\";
                btn.BackColor = Color.Red;
                imagepath += btn.Name + "mala1.png";
                btn.BackgroundImage = Image.FromFile(imagepath);
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.TabStop = false;
            }
            else
            {
                string imagepath = "Resources\\";
                btn.BackColor = SystemColors.Control;
                imagepath += btn.Name + ".png";
                btn.BackgroundImage = Image.FromFile(imagepath);
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.TabStop = false;
            }
        }

        public void setSupplementsButtons(List<SupplementData> supplements, TableLayoutPanel table)
        {
            var ctr = 0;

            foreach (Control item in table.Controls)
            {
                if (item is Button)
                {
                    item.Tag = supplements.ElementAt(ctr);
                    item.Click += new EventHandler(supplementHandler);
                    ctr++;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TableLayoutPanel parent = btn.Parent as TableLayoutPanel;
          
            foreach (Control item in parent.Parent.Controls)
            {
                SupplementData tag = item.Tag as SupplementData;
                if (item is Button)
                {
                    if(item.BackColor==SystemColors.Control)
                        SupplementsExcept.Add(new SupplementData(tag.Name,tag.ShortName,tag.Priority,tag.skracenicaZaRacun));
                }
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ljubicastikupus_Click(object sender, EventArgs e)
        {

        }
    }
}
