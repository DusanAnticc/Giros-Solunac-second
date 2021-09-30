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
    public partial class OstaliZacini : Form
    {
        public OstaliZacini(Form1 forma)
        {
            InitializeComponent();
            forma.setSupplementsButtons(forma.Supplements, tableLayoutPanel1, "3");
            tagToName(tableLayoutPanel1);
        }

        void tagToName(TableLayoutPanel table)
        {
            foreach (Control item in table.Controls)
            {
                if (item.Tag == null)
                    return;
                Button btn = item as Button;
                SupplementData tag = item.Tag as SupplementData;
                btn.Text = tag.Name;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
