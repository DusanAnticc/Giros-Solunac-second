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
    public partial class SecondScreen : Form
    {
        public List<Control> listOfControlsActive { get;set; }
        public List<Control> listOfControlsDone { get; set; }
        public List<orderInQueue> listaCekanjaCrveni { get; set; }
        public List<orderInQueue> listaCekanjaZeleni { get; set; }

        public List<orderInQueue> obrisiIzListeCrvenih { get; set; }
        public List<orderInQueue> obrisiIzListeZelenih { get; set; }


        public Form1 Main;

        public SecondScreen(Form1 forma)
        {
            InitializeComponent();
            Main = forma;
            makeListOfControls();
            setBackground(listOfControlsActive);
            setBackground(listOfControlsDone);

            this.Location = Screen.AllScreens[0].WorkingArea.Location;
            listaCekanjaCrveni = new List<orderInQueue>();
            obrisiIzListeCrvenih = new List<orderInQueue>();
            listaCekanjaZeleni = new List<orderInQueue>();
            obrisiIzListeZelenih = new List<orderInQueue>();
        }

        void makeListOfControls()
        {
            listOfControlsActive = tableLayoutPanel3.Controls.Cast<Control>().OrderBy(x => x.TabIndex).ToList();
            listOfControlsDone = tableLayoutPanel4.Controls.Cast<Control>().OrderBy(x => x.TabIndex).ToList();
        }

        void setBackground(List<Control> lista)
        {
            foreach (var item in lista)
            {
                item.BackgroundImage= Image.FromFile(Form1.MyGlobals.sivaPrazna);
                Main.setBackgroungImageSetings(item as Button);
            }
        }

        public void deleteOldOrder(bool uslov,string number)
        {
            List<Control> tmp;
            if (uslov)
                tmp = listOfControlsActive;
            else
                tmp = listOfControlsDone;
            foreach (var item in tmp)
            {
               if(item.Text==number)
               {
                    item.BackgroundImage = Image.FromFile(Form1.MyGlobals.sivaPrazna);
                    item.Text = "";
               }
            }
            if (uslov)
                sortTablePanel(tableLayoutPanel3);
            else
                sortTablePanel(tableLayoutPanel4);
        }

        void sortTablePanel(TableLayoutPanel panel)
        {
            for(int j=0;j<panel.ColumnCount;j++)
            {
                for(int i=0;i<panel.RowCount;i++)
                {
                    Control item = panel.GetControlFromPosition(j, i);
                    if(item.Text=="")
                    {
                        int col, row;
                        col = j;
                        row = i+1;
                        while (true)
                        {                           
                            if (row == panel.RowCount)
                            {
                                row = 0;
                                col++;
                                if (col == panel.ColumnCount)
                                    break;
                            }
                            Control tmp = panel.GetControlFromPosition(col, row);
                            if (tmp.Text != "")
                            {
                                item.Text = tmp.Text;
                                item.BackgroundImage = tmp.BackgroundImage;
                                tmp.Text = "";
                                tmp.BackgroundImage= Image.FromFile(Form1.MyGlobals.sivaPrazna);
                                break;
                            }
                            row++;
                        }
                    }
                }
            }
        }

        public async void addaftertime()
        {
            await Task.Delay(60000);
            foreach (var item in listaCekanjaCrveni)
            {
                if (provjeriListOfContols(listOfControlsActive))
                {
                    addButton1(item.color, item.number);
                    obrisiIzListeCrvenih.Add(item);
                }
                else
                    break;
            }
            foreach (var item in obrisiIzListeCrvenih)
            {
                listaCekanjaCrveni.Remove(item);
            }
            obrisiIzListeCrvenih.Clear();

            foreach (var item in listaCekanjaZeleni)
            {
                if (provjeriListOfContols(listOfControlsDone))
                {
                    addButton1(item.color, item.number);
                    obrisiIzListeZelenih.Add(item);
                }
                else
                    break;
            }
            foreach (var item in obrisiIzListeZelenih)
            {
                listaCekanjaZeleni.Remove(item);
            }
            obrisiIzListeZelenih.Clear();
        }


        public void addButton(Color color, string number)
        {
            addaftertime();
            addButton1(color, number);
            sortTablePanel(tableLayoutPanel3);
            sortTablePanel(tableLayoutPanel4);
        }

        bool provjeriListOfContols(List<Control> tmp)
        {
            foreach (var item in tmp)
            {
                if (item.Text == "")
                {
                    return true;
                }
            }
            return false;
        }

        bool addButton1(Color color,string number)
        {
            List<Control> tmp;
            bool other = true;
            if (color == Color.Red)
            {
                tmp = listOfControlsActive;
                other = false;
            }
            else
            {
                tmp = listOfControlsDone;
                other = true;
            }
            deleteOldOrder(other, number);
            
            foreach (var item in tmp)
            {
                if(item.Text=="")
                {
                    item.Text = number;
                    if (color == Color.Red)
                    {
                        item.ForeColor = Color.Black;
                        item.BackgroundImage = Image.FromFile(Form1.MyGlobals.crvenaPrazna);                        
                    }                        
                    else
                        item.BackgroundImage= Image.FromFile(Form1.MyGlobals.zelenaPrazna);

                    return true;
                }
            }
            if (color == Color.Red)
                listaCekanjaCrveni.Add(new orderInQueue(number, color));
            else
                listaCekanjaZeleni.Add(new orderInQueue(number, color));
            return false;
        }

    }
}
