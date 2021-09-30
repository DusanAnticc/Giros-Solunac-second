using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace fastfoodsolunacnew
{
    public partial class Form1 : Form
    {
        public List<SupplementData> Supplements { get; set; }
        public List<SupplementData> SupplementsExcept { get; set; }
        List<numberOdGirosInOrder> numberOfGiros { get; set; }
        public List<SupplementData> primarniPrilozi { get; set; }
        List<int> obrisaniBrojevi { get; set; }

        List<ActiveOrder> finishedOrders { get; set; }
        List<ActiveOrder> copyOffinishedOrders { get; set; }
        
        public string PrintText { get; set; }
        List<ActiveOrder> ActiveOrders { get; set; }
        Button ActiveOrdersButtonSelected = null;
        Button DoneOrdersButtonSelected = null;
        ControlData selectedButtonTAG = null;
        TableLayoutPanel selectedTablelayoutREF = null;
        TableLayoutPanel selectedTablelayoutREF1 = null;
        TableLayoutPanel mainFormTableLayoutPanel = null;
        TableLayoutPanel mainFormTableLayoutPanel1 = null;
        TableLayoutPanel mainFormTableLayoutPanel2 = null;
        TableLayoutPanel mainFormTableLayoutPanel3 = null;
        TableLayoutPanel mainFormTableLayoutPanel4 = null;
        tabNum activeOrderPanel = new tabNum();
        tabNum doneOrderPanel = new tabNum();

        tabNum activeNumberForm1 = new tabNum();
        tabNum activeNumberForm2 = new tabNum();
        tabNum activeNumberForm3 = new tabNum();
        tabNum doneNumberForm1 = new tabNum();
        tabNum doneNumberForm2 = new tabNum();
        tabNum doneNumberForm3 = new tabNum();

        public string tunaOrder = "";
        public string phoneName = "";
        public string phoneTime = "";
        public string note = "u lokalu";
        string tname = "";      //telefonska narudzba ime i vrijeme
        string ttime = "";

        public int PomfritCount = 1;
        int counter1 = 1;       //broj girosa unutar nerudzbe
        int counter=1;    //redni br narudzbe
        int girosaUPripremi = 0;
        int mainformcounter = 1; //koja je forma iscrtana
        int activeOrderNumberCount = 0;

        SoundPlayer sp;
        SecondScreen secondScreen;

        List<numberOdGirosInOrder> aktivniGirosi { get; set; }
        List<numberOdGirosInOrder> zavrseniGirosi { get; set; }


        public Form1()
        {
            InitializeComponent();
            SupplementsExcept = new List<SupplementData>();

            ActiveOrders = new List<ActiveOrder>();
            finishedOrders = new List<ActiveOrder>();
            copyOffinishedOrders = new List<ActiveOrder>();
            numberOfGiros = new List<numberOdGirosInOrder>();
            obrisaniBrojevi = new List<int>();

            aktivniGirosi = new List<numberOdGirosInOrder>();
            zavrseniGirosi = new List<numberOdGirosInOrder>();

            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            loadInputs();
            makeMainForm();
            girosiUPripremi();
            setSupplementsButtons(Supplements, tableLayoutPanel6, "1");
            setSupplementsButtons(Supplements, tableLayoutPanel15, "2");

            primarniPrilozi = Supplements.Where(x => x.Priority == "1").ToList();
            //setDate();
        }

        public static class MyGlobals
        {
            public static string crvenaPraznaSelektivana = "Resources\\crvenapraznaselektovana.png";
            public static string zelenaPraznaSelektivana = "Resources\\zelenapraznaselektovana.png";
            public static string sivaPrazna = "Resources\\sivaprazna1.png";
            public static string crvenaPrazna = "Resources\\crvenaprazna1.png";
            public static string zelenaPrazna = "Resources\\zelenaprazna1.png";
            public static string novaNarudzba = "Resources\\Nova narudzba 1.png";
            public static string prilozi = "Resources\\Prilozi.txt";            
            public static string buttonSound = "Resources\\multimedia_button_click_013.wav";
            public static string fajlAktivni = "Resources\\aktivniGirosi.txt";
            public static string brGirosaZaDrugiEkran = @"\\Solunac-second\Release\Resources\aktivniGirosi.txt";
            public static string datum = @"\\Solunac-second\Release\Resources\datum.txt";
            //public static string datum = @"\\F:\Resources\datum.txt";

            public static string azurirajPrviRacunar = "Resources\\azurirajPrviRacunar.txt";
        }

        void loadInputs()
        {
            Supplements = nameParser(MyGlobals.prilozi);
            sp = new SoundPlayer(MyGlobals.buttonSound);
        }
        
        void makeMainForm()
        {
            RemoveStaticTableLayoutForm(tableLayoutPanel3, tableLayoutPanel4);
            makeMainTableLayoutPanel();
            selectedTablelayoutREF1 = mainFormTableLayoutPanel;
            printMainTableLayoutForm(mainFormTableLayoutPanel);
        }

        void girosiUPripremi()
        {
            girosaUPripremi = 0;
            counter = 0;
            using (FileStream stream = new FileStream(MyGlobals.brGirosaZaDrugiEkran, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader tr = new StreamReader(stream))
                {
                    string line;
                    while ((line = tr.ReadLine()) != null&&line!="")
                    {
                        //counter = int.Parse(line.Split(',')[0]) + 1;
                        girosaUPripremi += int.Parse(line.Split(',')[1]);
                    }                  
                }
            }
            //trenutniCounter();
            button68.Text = girosaUPripremi.ToString();
        }

        public void buttonClickSound()
        {
            sp.Play();
        }
        
        public void setBackgroungImageSetings(Button item)
        {
            item.BackgroundImageLayout = ImageLayout.Stretch;
            item.FlatAppearance.BorderSize = 0;
            item.FlatStyle = FlatStyle.Flat;
        }
        
        void DeselectItem()
        {
            if (selectedTablelayoutREF != null)
                selectedTablelayoutREF.BackColor = SystemColors.Control;
        }

        string shortToActualString(string str)
        {
            switch (str)
            {
                case ("MP"):
                    return "MALI PILECI";
                case ("SP"):
                    return "SREDNJI PILECI";
                case ("VP"):
                    return "VELIKI PILECI";
                case ("MS"):
                    return "MALI SVINJSKI";
                case ("SS"):
                    return "SREDNJI SVINJSKI";
                case ("VS"):
                    return "VELIKI SVINJSKI";
                case ("MM"):
                    return "MALI MIJESANI";
                case ("SM"):
                    return "SREDNJI MIJESANI";
                case ("VM"):
                    return "VELIKI MIJESANI";
                case ("TUN"):
                    return "TUNA";
                case ("VEG"):
                    return "VEGETARIJANSKI";
                case ("POM"):
                    return "POMFRIT";
            }
            return str;
        }

        string imeGirosaSkraceno(string str)
        {
            switch (str)
            {
                case (" MALI PILECI"):
                    return "MP";
                case (" SREDNJI PILECI"):
                    return "SP";
                case (" VELIKI PILECI"):
                    return "VP";
                case (" MALI SVINJSKI"):
                    return "MS";
                case (" SREDNJI SVINJSKI"):
                    return "SS";
                case (" VELIKI SVINJSKI"):
                    return "VS";
                case (" MALI MIJESANI"):
                    return "MM";
                case (" SREDNJI MIJESANI"):
                    return "SM";
                case (" VELIKI MIJESANI"):
                    return "VM";
                case (" MALI TUNA"):
                    return "MT";
                case (" SREDNJI TUNA"):
                    return "ST";
                case (" VELIKI TUNA"):
                    return "VT";
                case (" MALI VEGETARIJANSKI"):
                    return "MV";
                case (" SREDNJI VEGETARIJANSKI"):
                    return "SV";
                case (" VELIKI VEGETARIJANSKI"):
                    return "VV";
                case (" POMFRIT x1"):
                    return "POM X1";
                case (" POMFRIT x2"):
                    return "POM X2";
                case (" POMFRIT x3"):
                    return "POM X3";
                case (" POMFRIT x4"):
                    return "POM X4";
                case (" POMFRIT x5"):
                    return "POM X5";
            }
            return str;
        }

        void listEventHandler(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            buttonClickSound();
            if (e.IsSelected == true)
            {
                var list = sender as ListView;
                e.Item.Remove();
                DeselectItem();

                selectedTablelayoutREF = list.Parent as TableLayoutPanel;
                selectedTablelayoutREF1 = list.Parent.Parent as TableLayoutPanel;
                selectedTablelayoutREF.BackColor = Color.SteelBlue;
                selectedButtonTAG = list.Tag as ControlData;
            }
        }
        void doubleClickRemoveOrder(object sender, EventArgs e)
        {
            ActiveOrder tmp = new ActiveOrder();
            foreach (ActiveOrder item in ActiveOrders)
            {
                if (item.form == selectedTablelayoutREF)
                {
                    tmp = item;
                    break;
                }
            }
            string i = selectedTablelayoutREF.Controls[0].Text.Split(']')[0].Split('[')[1];
            obrisaniBrojevi.Add(int.Parse(i));
            obrisaniBrojevi.Sort();
            ActiveOrders.Remove(tmp);
            removeOrder(selectedTablelayoutREF, selectedTablelayoutREF1, selectedButtonTAG); ;
            deselectEverything();
        }

        void deselectEverything()
        {
            selectedTablelayoutREF1 = null;
            selectedTablelayoutREF = null;
            selectedButtonTAG = null;
        }

        void selectedButton(object sender, EventArgs e)
        {
            buttonClickSound();
            var btn = sender as Button;
            selectedButtonTAG = btn.Tag as ControlData;
            DeselectItem();

            selectedTablelayoutREF = btn.Parent as TableLayoutPanel;
            selectedTablelayoutREF1 = btn.Parent.Parent as TableLayoutPanel;
            selectedTablelayoutREF.BackColor = Color.SteelBlue;
        }

        private void HandleItemTypeClick(object sender, EventArgs e)
        {
            buttonClickSound();

            int brojac = 0;
            if (obrisaniBrojevi.Count!=0)
            {
                brojac = obrisaniBrojevi[0];
                obrisaniBrojevi.Remove(brojac);
            }
            else
            {
                brojac = counter1;
                counter1++;
            }

            var theChosenOne = sender as Control;
            var parent = theChosenOne.Parent;
            var parentOfParent = parent.Parent as TableLayoutPanel;
            string location = (parent.Tag as ControlData).Location;

            parent.Parent.Controls.Remove(parent);

            TableLayoutPanel ctr = new TableLayoutPanel();
            ctr.Dock = DockStyle.Fill;
            ctr.ColumnCount = 1;
            ctr.RowCount = 2;
            TableLayoutRowStyleCollection styles = ctr.RowStyles;
            styles.Add(new RowStyle(SizeType.Percent, 25));
            styles.Add(new RowStyle(SizeType.Percent, 75));

            MyButton headerButton = new MyButton();
            headerButton.DoubleClick += new System.EventHandler(doubleClickRemoveOrder);
            headerButton.Click += new EventHandler(selectedButton);
            headerButton.Font = new Font(headerButton.Font.FontFamily, 15);
            headerButton.Dock = DockStyle.Fill;
            headerButton.TabStop = false;
            headerButton.Tag = parent.Tag;

            selectedButtonTAG = parent.Tag as ControlData;

            bool uslovTV = false, uslovP = false;
            if (theChosenOne.Text == "POM")
                uslovP = true;
            if (theChosenOne.Text == "TUN" || theChosenOne.Text == "VEG")
                uslovTV = true;
            headerButton.Text = "[" + brojac + "] ";

            if (uslovTV)
                headerButton.Text += tunaOrder + " ";
            headerButton.Text += shortToActualString(theChosenOne.Text);
            if (uslovP)
                headerButton.Text += " x" + PomfritCount.ToString();

            var list = new ListView();
            list.Tag = parent.Tag;
            list.BackColor = Color.FromArgb(156, 156, 156);
            list.Dock = DockStyle.Fill;
            list.View = View.Details;
            list.GridLines = true;
            list.FullRowSelect = true;
            Font ft = new Font(list.Font.Name, 14);
            list.Font = ft;

            ctr.Controls.Add(headerButton);
            ctr.Controls.Add(list);
            parentOfParent.Controls.Add(ctr);
            parentOfParent.SetCellPosition(ctr,
                new TableLayoutPanelCellPosition(int.Parse(location.Split(',')[1]), int.Parse(location.Split(',')[0])));

            list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            list.Columns.Add("Name").Width = list.Width / 2;
            list.Columns.Add("Quantity").Width = (list.Width / 2) - 4;
            list.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(listEventHandler);
            DeselectItem();
            selectedTablelayoutREF = headerButton.Parent as TableLayoutPanel;
            selectedTablelayoutREF1 = headerButton.Parent.Parent as TableLayoutPanel;
            selectedTablelayoutREF.BackColor = Color.SteelBlue;

            ActiveOrder t = new ActiveOrder();
            t.number = counter;
            t.form = selectedTablelayoutREF;
            t.time = DateTime.Now;
            ActiveOrders.Add(t);
            //counter1++;
        }

        private void eventHandlerForTuna(object sender, EventArgs e)
        {
            buttonClickSound();
            Button btn = sender as Button;
            OrderSize f2 = new OrderSize(this);

            if (f2.ShowDialog() == DialogResult.OK)
            {
                HandleItemTypeClick(btn, null);
            }
        }

        private void eventHandlerForPomfrit(object sender, EventArgs e)
        {
            buttonClickSound();
            Button btn = sender as Button;
            PomfritNumber f2 = new PomfritNumber(this);

            if (f2.ShowDialog() == DialogResult.OK)
            {
                HandleItemTypeClick(btn, null);
            }
        }

        private Button GetButton(string name, string str, Color col)
        {
            var btn = new Button();
            btn.Text = name;
            btn.Font = new Font(btn.Font.FontFamily, 15);
            btn.Dock = DockStyle.Fill;
            btn.BackColor = col;
            btn.Tag = new ControlData() { Location = str };
            if (btn.Text == "POM")
                btn.Click += new EventHandler(eventHandlerForPomfrit);
            else if (btn.Text == "TUN" || btn.Text == "VEG")
                btn.Click += new EventHandler(eventHandlerForTuna);
            else
                btn.Click += new EventHandler(HandleItemTypeClick);
            return btn;
        }

        void makeTableLayoutForm(Control parent, string str)
        {
            TableLayoutPanel prnt = parent as TableLayoutPanel;
            TableLayoutPanel baseTable = new TableLayoutPanel();
            baseTable.Tag = new ControlData() { Location = str };
            baseTable.ColumnCount = 4;
            baseTable.RowCount = 3;
            baseTable.Dock = DockStyle.Fill;
            baseTable.BackColor = Color.FromArgb(54, 54, 54);

            TableLayoutRowStyleCollection styles = baseTable.RowStyles;
            styles.Add(new RowStyle(SizeType.Percent, 33.330f));
            styles.Add(new RowStyle(SizeType.Percent, 33.330f));
            styles.Add(new RowStyle(SizeType.Percent, 33.330f));
            TableLayoutColumnStyleCollection style = baseTable.ColumnStyles;
            style.Add(new ColumnStyle(SizeType.Percent, 25));
            style.Add(new ColumnStyle(SizeType.Percent, 25));
            style.Add(new ColumnStyle(SizeType.Percent, 25));
            style.Add(new ColumnStyle(SizeType.Percent, 25));

            baseTable.Controls.Add(GetButton("MP", "0,0", Color.FromArgb(255, 255, 128)));
            baseTable.Controls.Add(GetButton("MS", "0,1", Color.FromArgb(155, 32, 32)));
            baseTable.Controls.Add(GetButton("MM", "0,2", Color.FromArgb(35, 100, 255)));
            baseTable.Controls.Add(GetButton("TUN", "0,3", Color.FromArgb(168, 150, 255)));
            baseTable.Controls.Add(GetButton("SP", "1,0", Color.FromArgb(255, 255, 128)));
            baseTable.Controls.Add(GetButton("SS", "1,1", Color.FromArgb(155, 32, 32)));
            baseTable.Controls.Add(GetButton("SM", "1,2", Color.FromArgb(35, 100, 255)));
            baseTable.Controls.Add(GetButton("VEG", "1,3", Color.FromArgb(55, 200, 55)));
            baseTable.Controls.Add(GetButton("VP", "2,0", Color.FromArgb(255, 255, 128)));
            baseTable.Controls.Add(GetButton("VS", "2,1", Color.FromArgb(155, 32, 32)));
            baseTable.Controls.Add(GetButton("VM", "2,2", Color.FromArgb(35, 100, 255)));
            baseTable.Controls.Add(GetButton("POM", "2,3", Color.FromArgb(200, 200, 100)));

            prnt.Controls.Add(baseTable);
            prnt.SetCellPosition(baseTable,
                new TableLayoutPanelCellPosition(int.Parse(str.Split(',')[1]), int.Parse(str.Split(',')[0])));
        }

        List<SupplementData> nameParser(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            List<SupplementData> supplements = new List<SupplementData>();
            foreach (string line in lines)
            {
                string[] tmp = line.Split(';');
                supplements.Add(new SupplementData(tmp[0], tmp[1], tmp[2], tmp[3]));
            }
            return supplements;
        }

        void supplementHandler(object sender, EventArgs e)
        {
            buttonClickSound();
            addToList(sender as Button);
        }

        public void setSupplementsButtons(List<SupplementData> supplements, TableLayoutPanel table, string priority)
        {
            var filteredSups = supplements.Where(x => x.Priority == priority).ToList();
            int brojac = filteredSups.Count;
            var ctr = 0;

            foreach (Control item in table.Controls)
            {
                if (brojac == ctr)
                    return;
                if (item is Button)
                {
                    item.Tag = filteredSups.ElementAt(ctr);
                    item.Click += new EventHandler(supplementHandler);
                    ctr++;
                }
            }
        }

        void secondScreenOrdersadd(bool color, int num)
        {
            Color tmp;
            if (color)
                tmp = Color.Red;
            else
                tmp = Color.FromArgb(0, 192, 0);
            secondScreen.addButton(tmp, num.ToString());
        }

        private void PrintPageEventCustomers(object sender, PrintPageEventArgs ev)
        {
            Font fontA = new Font("Arial", 10, System.Drawing.FontStyle.Regular);
            Font fontB = new Font("Arial", 12, System.Drawing.FontStyle.Regular);
            Font fontC = new Font("Arial", 30, System.Drawing.FontStyle.Bold);
            Font fontD = new Font("Arial", 12, System.Drawing.FontStyle.Bold);

            int y = 0;
            int novired = 20;

            if (ttime != "")
            {
                ev.Graphics.DrawString(tname, fontD, Brushes.Black, 0, y);
                y += novired;                
            }

            ev.Graphics.DrawString("KUPAC", fontB, Brushes.Black, 0, y);

            string redniBroj = "[" + (counter - 1) + "]";
            ev.Graphics.DrawString(redniBroj, fontC, Brushes.Black, 180, 0);

            y += novired;
            DateTime time = DateTime.Now;
            ev.Graphics.DrawString(time.Hour.ToString() + ":" + time.Minute.ToString(), fontB, Brushes.Black, 0, y);

            int i = 0;
            y += novired;

            while (i < finishedOrders.Count)
            {
                var item = finishedOrders[i++];
                y += novired;

                string[] narudzba = napraviRacun(item);
                ev.Graphics.DrawString(narudzba[0], fontB, Brushes.Black, 0, y);
                y += novired;

                ev.Graphics.DrawString(narudzba[1], fontA, Brushes.Black, 0, y);

                int skok = novired;
                if (novired > 3)
                    skok = novired - 2;
                y += broj_linija * skok;
                duzina_linije = 0;
            }
            y += novired;
            ev.Graphics.DrawString(note, fontB, Brushes.Black, 150, y);
        }

        string noteToSymbol(string note)
        {
            switch (note)
            {
                case ("u lokalu"):
                    return "O";
                case ("usput"):
                    return "U";
                case ("ponijeti"):
                    return "P";
            }
            return note;
        }

        private void PrintPageEventStaff(object sender, PrintPageEventArgs ev)
        {
            Font fontA = new Font("Arial", 12, System.Drawing.FontStyle.Bold);
            Font fontAA = new Font("Arial", 16, System.Drawing.FontStyle.Bold);

            Font fontB = new Font("Arial", 17, System.Drawing.FontStyle.Regular);
            Font fontC = new Font("Arial", 30, System.Drawing.FontStyle.Bold);
            Font fontD = new Font("Arial", 24, System.Drawing.FontStyle.Bold);
            Font fontE = new Font("Arial", 14, System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Strikeout);

            int y = 0;
            int novired = 28;

            ev.Graphics.DrawString("KUHINJA", fontB, Brushes.Black, 0, y);

            string redniBroj = "[" + (counter - 1) + "]";
            ev.Graphics.DrawString(redniBroj, fontC, Brushes.Black, 180, 0);

            y += novired;
            DateTime time = DateTime.Now;
            ev.Graphics.DrawString(time.Hour.ToString() + ":" + time.Minute.ToString(), fontB, Brushes.Black, 0, y);

            int i = 0;
            y += novired;

            while (i < finishedOrders.Count)
            {
                string[] narudzba;
                var item = finishedOrders[i++];
                y += novired;

                int br =kolikoPrimarnihPrilogaIma(item.form.Controls[1] as ListView);
                if (br > 5)
                {
                    narudzba = napraviRacunZaZaposlenePrekrizeno(item);
                    ev.Graphics.DrawString(narudzba[0], fontB, Brushes.Black, 0, y);
                    y += novired;

                    if (br == 9)
                    {
                        ev.Graphics.DrawString("SVE", fontA, Brushes.Black, 0, y);
                        y += novired;
                        string st = napraviRacunZaSveSaPrilozima(item);
                        ev.Graphics.DrawString(st, fontA, Brushes.Black, 0, y);
                    }
                    else
                    {
                        ev.Graphics.DrawString(narudzba[1], fontE, Brushes.Black, 0, y);
                        y += novired;
                        string st = napraviRacunZaSveSaPrilozima(item);
                        ev.Graphics.DrawString(st, fontA, Brushes.Black, 0, y);
                    }

                    string ostaliZacini = dodajOstaleZacine(item);
                    if(ostaliZacini!="")
                    {
                        y += novired;
                        ev.Graphics.DrawString(ostaliZacini, fontA, Brushes.Black, 0, y);
                    }

                }
                else
                {
                    narudzba = napraviRacunZaZaposlene(item);
                    ev.Graphics.DrawString(narudzba[0], fontB, Brushes.Black, 0, y);
                                    y += novired;
                    ev.Graphics.DrawString(narudzba[2], fontAA, Brushes.Black, 0, y);
                    ev.Graphics.DrawString(narudzba[1], fontA, Brushes.Black, (narudzba[2].Length)*10, y);
                }

                int skok = novired;
                if (novired > 3)
                    skok = novired - 2;
                y += broj_linija * skok;
                duzina_linije = 0;
            }
            //finishedOrders.Clear();
            y += novired;
            if (ttime != "")
            {
                ev.Graphics.DrawString("TEL: " + ttime, fontB, Brushes.Black, 0, y);
                y += novired;
                ev.Graphics.DrawString(tname, fontA, Brushes.Black, 0, y);
            }
            string n = noteToSymbol(note);
            ev.Graphics.DrawString(n, fontD, Brushes.Black, 180, y);
        }

        int kolikoPrimarnihPrilogaIma(ListView t)
        {
            int br = 0;
            Invoke(new Action(() =>
            {
                foreach (ListViewItem i in t.Items)
                {
                    foreach (var item in primarniPrilozi)
                    {
                        if (item.Name == i.Text)
                        {
                            br++;
                            break;
            }}}}));
            return br;
        }

        int duzina_linije = 0;
        int broj_linija = 1;

        string dodajOstaleZacine(ActiveOrder item)
        {
            duzina_linije = 0;
            broj_linija = 1;
            int sirina_racuna = 22;
            string narudzba = "";
            ListView t = (item.form.Controls[1] as ListView);

            Invoke(new Action(() =>
            {

                foreach (ListViewItem i in t.Items)
                {
                    int uslov = 0;
                    foreach (var p in primarniPrilozi)
                    {
                        if (i.Text == p.Name)
                            uslov++;
                    }
                    if (uslov == 0)
                    {
                        string prilog = "";
                        string tmp = prilogUSkraceniOblikZaZaposlene(i.SubItems[0].Text);
                        prilog = KolicinaPriloga(tmp, i.SubItems[1].Text);
                        narudzba += dodajULiniju(prilog, sirina_racuna);
                        narudzba += " , ";
                    }
                }
            }));
            return narudzba;
        }

        string[] napraviRacunZaZaposlenePrekrizeno(ActiveOrder item)
        {
            duzina_linije = 0;
            broj_linija = 1;
            int sirina_racuna = 22;
            string[] narudzba = new string[2];
            narudzba[0] = (item.form.Controls[0].Text.Split(']')[0] + "] " + imeGirosaSkraceno(item.form.Controls[0].Text.Split(']')[1]));
            narudzba[1] = "";
            ListView t = (item.form.Controls[1] as ListView);

            Invoke(new Action(() =>
            {
                foreach (var p in primarniPrilozi)
                {
                    int uslov = 0;
                    foreach (ListViewItem i in t.Items)
                    {
                        if (i.Text == p.Name)
                            uslov++;
                    }
                    if(uslov==0)
                    {                        
                        narudzba[1] += dodajULiniju(p.ShortName, sirina_racuna);
                        narudzba[1] += " , ";
                    }
                }
            }));
            return narudzba;
        }

        string[] napraviRacunZaZaposlene(ActiveOrder item)
        {
            broj_linija = 1;
            int sirina_racuna = 17;
            string[] narudzba = new string[3];
            narudzba[0] = (item.form.Controls[0].Text.Split(']')[0]+"] " + imeGirosaSkraceno(item.form.Controls[0].Text.Split(']')[1]));
            narudzba[1] = "";
            narudzba[2] = "";

            bool caciki = false, pom = false, cili = false;
            string ca ="",po="",ci="";
            ListView t = (item.form.Controls[1] as ListView);
            bool uslov = false;

            Invoke(new Action(() => {

                foreach (ListViewItem i in t.Items)
                {
                    if (i.SubItems[0].Text == "Caciki Sos" || i.SubItems[0].Text == "Pomfrit" || i.SubItems[0].Text == "Cili Sos")
                    {
                        switch (i.SubItems[0].Text)
                        {
                            case ("Caciki Sos"):
                                {
                                    caciki = true;
                                    ca = i.SubItems[1].Text;
                                    if (ca != "")
                                        uslov = true;
                                    break;
                                }
                            case ("Pomfrit"):
                                {
                                    pom = true;
                                    po = i.SubItems[1].Text;
                                    if (po != "")
                                        uslov = true;
                                    break;
                                }
                            case ("Cili Sos"):
                                {
                                    cili = true;
                                    ci = i.SubItems[1].Text;
                                    if (ci != "")
                                        uslov = true;
                                    break;
                                }
                        }
                    }                    
                }
                bool uslovZaPomfrit = false;
                foreach (ListViewItem i in t.Items)
                {
                    if(uslov)
                    {
                        if (i.SubItems[0].Text!="Pomfrit")
                        {
                            string prilog = "";
                            string tmp = prilogUSkraceniOblikZaZaposlene(i.SubItems[0].Text);
                            prilog = KolicinaPriloga(tmp, i.SubItems[1].Text);
                            narudzba[1] += dodajULiniju(prilog, sirina_racuna);
                            narudzba[1] += " , ";
                        }
                        else
                        {
                            uslovZaPomfrit = true;
                        }
                    }
                    else
                    {
                        if (i.SubItems[0].Text == "Caciki Sos" || i.SubItems[0].Text == "Pomfrit" || i.SubItems[0].Text == "Cili Sos")
                        {
                            if (i.SubItems[0].Text != "Pomfrit")
                            {
                                uslovZaPomfrit = true;
                            }
                        }
                        else
                        {
                            string prilog = "";
                            string tmp = prilogUSkraceniOblikZaZaposlene(i.SubItems[0].Text);
                            prilog = KolicinaPriloga(tmp, i.SubItems[1].Text);
                            narudzba[1] += dodajULiniju(prilog, sirina_racuna);
                            narudzba[1] += " , ";
                        }
                    }
                }
                string kraj="";
                if (uslov==false)
                {
                    if (caciki)
                    {
                        if (pom && cili)
                            kraj = "3+ , ";
                        else if (pom)
                            kraj = "+ , ";
                        else if (cili)
                            kraj = "2+,";
                        else
                        {
                            foreach (ListViewItem i in t.Items)
                            {
                                if (i.SubItems[0].Text == "Caciki Sos")
                                {
                                    string prilog = "";
                                    string tmp = prilogUSkraceniOblikZaZaposlene(i.SubItems[0].Text);
                                    prilog = KolicinaPriloga(tmp, i.SubItems[1].Text);
                                    narudzba[1] += dodajULiniju(prilog, sirina_racuna);
                                    narudzba[1] += " , ";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (pom || cili)
                        {
                            foreach (ListViewItem i in t.Items)
                            {
                                if (i.SubItems[0].Text == "Pomfrit" || i.SubItems[0].Text == "Cili Sos")
                                {
                                    string prilog = "";
                                    string tmp = prilogUSkraceniOblikZaZaposlene(i.SubItems[0].Text);
                                    prilog = KolicinaPriloga(tmp, i.SubItems[1].Text);
                                    narudzba[1] += dodajULiniju(prilog, sirina_racuna);
                                    narudzba[1] += " , ";
                                }
                            }
                        }
                    }
                }
                else
                {
                    if(uslovZaPomfrit)
                    {
                        string prilog = "";
                        string tmp = prilogUSkraceniOblikZaZaposlene("Pomfrit");
                        prilog = KolicinaPriloga(tmp, po);
                        narudzba[1] += dodajULiniju(prilog, sirina_racuna);
                        narudzba[1] += " , ";
                    }
                }
                narudzba[2] = kraj;
            }));

            return narudzba;
        }

        string napraviRacunZaSveSaPrilozima(ActiveOrder item)
        {
            duzina_linije = 0;
            broj_linija = 1;
            int sirina_racuna = 25;
            string narudzba = "";

            ListView t = (item.form.Controls[1] as ListView);
            Invoke(new Action(() => {

                foreach (ListViewItem i in t.Items)
                {
                    foreach (var pr in primarniPrilozi)
                    {
                        if (pr.Name == i.Text)
                        {
                            if (i.SubItems[1].Text != "")
                            {
                                string prilog = "";
                                string tmp = prilogUSkraceniOblikZaZaposlene(i.SubItems[0].Text);
                                prilog = KolicinaPriloga(tmp, i.SubItems[1].Text);
                                narudzba += dodajULiniju(prilog, sirina_racuna);
                                narudzba += " , ";
                            }
                        }
                    }
                }
            }));
            return narudzba;
        }

        private void Print()
        {
            PrintDocument pd = new PrintDocument();

            var ppeh = new PrintPageEventHandler(this.PrintPageEventCustomers);
            pd.PrintPage += ppeh;
            try
            {
                pd.Print();
                pd.PrintPage -= ppeh;
                PrintText = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Greška prilikom stampanja", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // ppeh = new PrintPageEventHandler(this.PrintPageEventCustomers);
            //pd.PrintPage += ppeh;
            //try
            //{
            //    pd.Print();
            //    pd.PrintPage -= ppeh;
            //    PrintText = null;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Greška prilikom stampanja", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            ppeh = new PrintPageEventHandler(this.PrintPageEventStaff);
            pd.PrintPage += ppeh;
            try
            {
                pd.Print();
                pd.PrintPage -= ppeh;
                PrintText = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Greška prilikom stampanja", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ppeh = new PrintPageEventHandler(this.PrintPageEventStaff);
            pd.PrintPage += ppeh;
            try
            {
                pd.Print();
                pd.PrintPage -= ppeh;
                PrintText = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Greška prilikom stampanja", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ppeh = new PrintPageEventHandler(this.PrintPageEventStaff);
            pd.PrintPage += ppeh;
            try
            {
                pd.Print();
                pd.PrintPage -= ppeh;
                PrintText = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Greška prilikom stampanja", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finishedOrders.Clear();
            ttime = "";
            tname = "";            
        }

        string[] napraviRacun(ActiveOrder item)
        {
            duzina_linije = 0;
            broj_linija = 1;
            int sirina_racuna = 25;
            string[] narudzba = new string[2];
            narudzba[0] = item.form.Controls[0].Text;
            narudzba[1] = "";

            ListView t = (item.form.Controls[1] as ListView);
            Invoke(new Action(() => {

                    foreach (ListViewItem i in t.Items)
                    {                        
                            string tmp = prilogUSkraceniOblik(i.SubItems[0].Text);
                            narudzba[1] += dodajULiniju(tmp/*prilog*/, sirina_racuna);
                            narudzba[1] += " , ";
                    }                
            }));          
            return narudzba;
        }

        string prilogUSkraceniOblik(string prilog)
        {
            foreach (var item in Supplements)
            {
                if (prilog == item.Name)
                    return item.skracenicaZaRacun;
                
            }
            return prilog;
        }

        string prilogUSkraceniOblikZaZaposlene(string prilog)
        {
            foreach (var item in Supplements)
            {
                if (prilog == item.Name)
                    return item.ShortName;

            }
            return prilog;
        }

        string KolicinaPriloga(string prilog,string velicina)
        {
            if (velicina == "")
                return prilog;
            else if (velicina == "vise")
                return prilog + "²";
            else if (velicina == "manje")
                return prilog + "¹";
            return prilog + "(odv)";
        }

        string dodajULiniju(string word,int sirina_racuna)
        {
            string line = "";
            if(duzina_linije+word.Length<sirina_racuna)
            {
                line += word;
                duzina_linije = duzina_linije + word.Length;
                return line;
            }
            else
            {
                line += "\n" + word;
                broj_linija++;
                duzina_linije =  word.Length;
                return line;
            }
        }

        void makeBill(bool i)
        {
            var thread = new Thread((text) =>
            {
                Print();
            });
            thread.Start();
        }
      
        void onClickNewButton(object sender, EventArgs e)
        {
            buttonClickSound();
            var btn = sender as Button;
            var parent = btn.Parent;
            var t = btn.Tag as ControlData;

            string str = t.Location;

            parent.Controls.Remove(btn);
            makeTableLayoutForm(parent, str);
        }

        void removeOrder(TableLayoutPanel tab, TableLayoutPanel parent,ControlData tag)
        {
            if (parent == null ||tab == null || tag == null)
                return;

            DeselectItem();
            parent.Controls.Remove(tab);

            Button btn = makeNewOrderButton(parent, tag);
            parent.Controls.Add(btn);
            parent.SetCellPosition(btn,
                new TableLayoutPanelCellPosition(int.Parse(tag.Location.Split(',')[1]), int.Parse(tag.Location.Split(',')[0])));
        }

        Button makeNewOrderButton(TableLayoutPanel parent, ControlData tag)
        {
            Button btn = new Button();
            btn.Parent = parent;
            btn.Text = "";
            btn.Tag = tag;
            btn.Dock = DockStyle.Fill;
            btn.BackgroundImage = Image.FromFile(MyGlobals.novaNarudzba);
            setBackgroungImageSetings(btn);
            btn.TabStop = false;
            btn.Click += new EventHandler(onClickNewButton);
            return btn;
        }
     
        void addToList(Button btn)
        {      
            if (selectedTablelayoutREF1 == null || selectedTablelayoutREF == null || selectedButtonTAG == null)
                return;
            var list = selectedTablelayoutREF.Controls[1] as ListView;          
            var arr = new string [2];
            var tmp = btn.Tag as SupplementData;
            bool exist = false;
            int i =0;
            foreach (ListViewItem item in list.Items)
            {
                if (tmp.Name==item.SubItems[0].Text)
                {
                    exist = true;
                    switch (item.SubItems[1].Text)
                    {
                        case (""):
                            item.SubItems[1].Text = "vise";
                            break;
                        case ("vise"):
                            item.SubItems[1].Text = "manje";
                            break;
                        case ("manje"):
                            if (item.SubItems[0].Text == "Pomfrit")
                            {
                                item.SubItems[1].Text = "odvojeno";
                            }
                            else
                                item.SubItems[1].Text = "";
                            break;
                        case ("odvojeno"):
                            item.SubItems[1].Text = "";
                            break;
                    }
                    break;
                }
                i++;
            }
            if (!exist)
            {
                arr[0] = tmp.Name ;
                arr[1] = "";                
                list.Items.Add(new ListViewItem(arr));
            }
            if (i > 0 && i<3)
               list.EnsureVisible(i-1);
            else
                list.EnsureVisible(i);
        }

        void removeAllFromList()
        {
            if (selectedTablelayoutREF1 == null || selectedTablelayoutREF == null || selectedButtonTAG == null)
                return;

            var list = selectedTablelayoutREF.Controls[1] as ListView;
            list.Items.Clear();
        }

        void printMainTableLayoutForm(TableLayoutPanel baseTable)
        {
            RemoveStaticTableLayoutForm(tableLayoutPanel3, mainFormTableLayoutPanel);
            var prnt = tableLayoutPanel3;
            prnt.Controls.Add(baseTable);
            prnt.SetCellPosition(baseTable, new TableLayoutPanelCellPosition(0, 0));
            mainFormTableLayoutPanel = baseTable;
            selectedTablelayoutREF1 = mainFormTableLayoutPanel;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    
        TableLayoutPanel makeMainTableLayoutForm(TableLayoutPanel table)
        {
            var baseTable = new TableLayoutPanel();

            baseTable.ColumnCount = 4;
            baseTable.RowCount = 2;
            baseTable.Dock = DockStyle.Fill;
            baseTable.BackColor = Color.FromArgb(54, 54, 54);

            TableLayoutRowStyleCollection styles = baseTable.RowStyles;
           // styles.Add(new RowStyle(SizeType.Percent, 33.330f));
            styles.Add(new RowStyle(SizeType.Percent, 50));
            styles.Add(new RowStyle(SizeType.Percent, 50));

            TableLayoutColumnStyleCollection style = baseTable.ColumnStyles;
            style.Add(new ColumnStyle(SizeType.Percent, 25));
            style.Add(new ColumnStyle(SizeType.Percent, 25));
            style.Add(new ColumnStyle(SizeType.Percent, 25));
            style.Add(new ColumnStyle(SizeType.Percent, 25));

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 4; j++)
                {
                    string tmps = i.ToString() + " , " + j.ToString();
                    ControlData tag = new ControlData() { Location = tmps };
                    var x = makeNewOrderButton(baseTable,tag);
                    baseTable.Controls.Add(x);
                    baseTable.SetCellPosition(x, new TableLayoutPanelCellPosition(j, i));
                }       
            return baseTable;
        }       

        void RemoveStaticTableLayoutForm(TableLayoutPanel parent, TableLayoutPanel child)
        {
            parent.Controls.Remove(child);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            copyOrder();
        }

        void copyOrder()
        {
            if (selectedTablelayoutREF1 == null || selectedTablelayoutREF == null || selectedButtonTAG == null)
                return;
          
            int br = 0;
            Control item;
            bool uslov = false;
            for (int i = 0; i <= this.selectedTablelayoutREF1.RowCount; i++)
            {
                for (int j = 0; j <= this.selectedTablelayoutREF1.ColumnCount; j++)
                {
                    if (ActiveOrders.Count == 32)
                        return;

                    if (br == 9)
                    {
                        button59_Click_1(button59, null);
                        copyOrder();
                    }
                    br++;
                    item = selectedTablelayoutREF1.GetControlFromPosition(j, i);
                    if (item is Button)
                    {
                        int brojac = 0;
                        if (obrisaniBrojevi.Count!=0)
                        {
                            brojac = obrisaniBrojevi[0];
                            obrisaniBrojevi.Remove(brojac);
                        }
                        else
                        {
                            brojac = counter1;
                            counter1++;
                        }
                        var tag = item.Tag as ControlData;
                        string location = tag.Location;

                        selectedTablelayoutREF1.Controls.Remove(item);

                        var ctr = new TableLayoutPanel();
                        ctr.Dock = DockStyle.Fill;
                        ctr.ColumnCount = 1;
                        ctr.RowCount = 2;

                        TableLayoutRowStyleCollection styles = ctr.RowStyles;
                        styles.Add(new RowStyle(SizeType.Percent, 25));
                        styles.Add(new RowStyle(SizeType.Percent, 75));

                        MyButton tmp = new MyButton();
                        tmp.Font = new Font(tmp.Font.FontFamily, 15);
                        tmp.Dock = DockStyle.Fill;

                        tmp.Text = "[" + brojac + "]" + selectedTablelayoutREF.Controls[0].Text.Split(']')[1];
                        tmp.TabStop = false;
                        tmp.Tag = item.Tag;
                        tmp.Click += new EventHandler(selectedButton);
                        tmp.DoubleClick += new System.EventHandler(doubleClickRemoveOrder);


                        var list = new ListView();
                        list.BackColor = Color.FromArgb(156, 156, 156);
                        list.Dock = DockStyle.Fill;
                        list.View = View.Details;
                        list.GridLines = true;
                        list.FullRowSelect = true;
                        list.Tag = item.Tag;

                        Font ft = new Font(list.Font.Name, 14);
                        list.Font = ft;

                        ctr.Controls.Add(tmp);
                        ctr.Controls.Add(list);

                        selectedTablelayoutREF1.Controls.Add(ctr);
                        selectedTablelayoutREF1.SetCellPosition(ctr,
                            new TableLayoutPanelCellPosition(int.Parse(location.Split(',')[1]), int.Parse(location.Split(',')[0])));

                        list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
                        list.Columns.Add("Name").Width = list.Width / 2;
                        list.Columns.Add("Quantity").Width = (list.Width / 2) -4;

                        list.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(listEventHandler);

                        ListView tt = selectedTablelayoutREF.Controls[1] as ListView;
                        foreach (ListViewItem itemm in tt.Items)
                        {
                            string[] s2 = new string[2];
                            s2[0] = itemm.SubItems[0].Text;
                            s2[1] = itemm.SubItems[1].Text;
                            ListViewItem n = new ListViewItem(s2);
                            list.Items.Add(n);
                        }

                        DeselectItem();
                        selectedTablelayoutREF = tmp.Parent as TableLayoutPanel;
                        selectedTablelayoutREF1 = tmp.Parent.Parent as TableLayoutPanel;
                        selectedButtonTAG = tmp.Tag as ControlData;
                        selectedTablelayoutREF.BackColor = Color.SteelBlue;

                        ActiveOrder t = new ActiveOrder();
                        t.number = counter;
                        t.form = selectedTablelayoutREF;
                        t.time = DateTime.Now;
                        ActiveOrders.Add(t);

                        //counter1++;
                        uslov=true;
                        break;
                    }
                }
                if (uslov)
                    break;
            }
           
        }

        void trenutniCounter()
        {
            counter = 0;
            using (FileStream stream = new FileStream(MyGlobals.datum, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader tr = new StreamReader(stream))
                {
                    string line;
                    line = tr.ReadLine();                   
                    counter = int.Parse(line.Split(';')[1])+1;  
                }
            }
        }

        private void button53_Click_1(object sender, EventArgs e) //zavrsena narudzba
        {
            buttonClickSound();            
            if (ActiveOrders.Count == 0)
                return;

            Note orderNote = new Note(this);
            int brojac = 0;
            if (orderNote.ShowDialog() == DialogResult.OK)
            {
                foreach (ActiveOrder item in ActiveOrders)
                {
                      brojac++;
                      finishedOrders.Add(item);
                }
                RemoveStaticTableLayoutForm(tableLayoutPanel3, mainFormTableLayoutPanel);
                
                makeMainTableLayoutPanel();

                selectedTablelayoutREF1 = mainFormTableLayoutPanel;
                printMainTableLayoutForm(mainFormTableLayoutPanel);
                trenutniCounter();
                izmjeniDatumFajl();
                makeBill(true);
               
                ActiveOrders.Clear();
                numberOfGiros.Add(new numberOdGirosInOrder(counter-1, brojac));

                int tmpint = int.Parse(button68.Text);
                // int kolicinaGirosaUNarudzbi = nadjiGirosPodBrojem(counter);
                int kolicinaGirosaUNarudzbi = brojac;
                button68.Text = (tmpint + kolicinaGirosaUNarudzbi).ToString();

                aktivniGirosi.Add(new numberOdGirosInOrder(counter-1, kolicinaGirosaUNarudzbi));

                dodajUFajlZaPrviRacunar(counter-1, kolicinaGirosaUNarudzbi);
                counter1 = 1;
                obrisaniBrojevi.Clear();
                              

                deselectEverything();
            }
        }

        void izmjeniDatumFajl()
        {
            using (var fs = new FileStream(MyGlobals.datum, FileMode.Truncate))
            {
            }
            if (counter > 99)
                counter = 1;
            using (FileStream stream = new FileStream(MyGlobals.datum, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (TextWriter tw = new StreamWriter(stream))
                {                    
                    DateTime date = DateTime.Now;
                    tw.Write(date.Day + "." + date.Month + ";" + counter);
                    tw.Close();
                }
            }
        }

        void dodajUFajlZaPrviRacunar(int rb,int kolicina)
        {
            
            using (FileStream stream = new FileStream(MyGlobals.azurirajPrviRacunar, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (TextWriter tw = new StreamWriter(stream))
                {
                    tw.Write(rb + "," + kolicina + '\n');
                    tw.Close();
                }
            }
        }



        int nadjiGirosPodBrojem(int co)
        {
            foreach (var item in numberOfGiros)
            {
                if (co == item.counter)
                   return item.numberOfGiros;
            }
            return 0;
        }

        private void button55_Click(object sender, EventArgs e) //svi prilozi
        {
            buttonClickSound();
            if (selectedTablelayoutREF1 == null || selectedTablelayoutREF == null || selectedButtonTAG == null)
                return;

            removeAllFromList();
            var list = selectedTablelayoutREF.Controls[1] as ListView;
            var filteredSups = Supplements.Where(x => x.Priority == "1").ToList();

            foreach (SupplementData item in filteredSups)
            {
                Button btn = new Button();
                btn.Tag = item;
                addToList(btn);
            }
        }

        private void button57_Click_1(object sender, EventArgs e)//svi prilozi osim
        {
            buttonClickSound();
            if (selectedTablelayoutREF1 == null || selectedTablelayoutREF == null || selectedButtonTAG == null)
                return;

            SupplementsExcept.Clear();
            SveOsim sveOsim = new SveOsim(this);
            if (sveOsim.ShowDialog() == DialogResult.OK)
            {
                removeAllFromList();

                foreach (SupplementData item in SupplementsExcept)
                {
                    Button btn = new Button();
                    btn.Tag = item;
                    addToList(btn);
                }
            }
        }
        
        private void button54_Click(object sender, EventArgs e)//ponisti sve
        {
            buttonClickSound();
            if (ActiveOrders.Count == 0)
                return;

            RemoveStaticTableLayoutForm(tableLayoutPanel3, mainFormTableLayoutPanel);
            makeMainTableLayoutPanel();
            selectedTablelayoutREF1 = mainFormTableLayoutPanel;
            printMainTableLayoutForm(mainFormTableLayoutPanel);

            ActiveOrders.Clear();
            counter1 = 1;
            obrisaniBrojevi.Clear();
            deselectEverything();
        }

        private void button59_Click_1(object sender, EventArgs e)
        {
            buttonClickSound();
            switch (mainformcounter)
            {
                case 1:
                    printMainTableLayoutForm(mainFormTableLayoutPanel4);
                    mainformcounter = 4;
                    break;
                case 2:
                    printMainTableLayoutForm(mainFormTableLayoutPanel1);
                    mainformcounter = 1;
                    break;
                case 3:
                    printMainTableLayoutForm(mainFormTableLayoutPanel2);
                    mainformcounter = 2;
                    break;
                case 4:
                    printMainTableLayoutForm(mainFormTableLayoutPanel3);
                    mainformcounter = 3;
                    break;
            }
        }

        private void button58_Click_1(object sender, EventArgs e)
        {
            buttonClickSound();
            switch (mainformcounter)
            {
                case 1:
                    printMainTableLayoutForm(mainFormTableLayoutPanel2);
                    mainformcounter = 2;
                    break;
                case 2:
                    printMainTableLayoutForm(mainFormTableLayoutPanel3);
                    mainformcounter = 3;
                    break;
                case 3:
                    printMainTableLayoutForm(mainFormTableLayoutPanel4);
                    mainformcounter = 4;
                    break;
                case 4:
                    printMainTableLayoutForm(mainFormTableLayoutPanel1);
                    mainformcounter = 1;
                    break;
            }
        }

        private void button70_Click(object sender, EventArgs e)// zavrsi narudzbu - telefonska 
        {
            buttonClickSound();
            if (ActiveOrders.Count == 0)
                return;          

            int brojac = 0;
            telefonskaNarudzba telefonska = new telefonskaNarudzba(this);
            if (telefonska.ShowDialog() == DialogResult.OK)
            {
                Note note = new Note(this);
                if (note.ShowDialog() == DialogResult.OK)
                {
                    foreach (ActiveOrder item in ActiveOrders)
                    {
                        brojac++;
                        finishedOrders.Add(item);
                    }
                    RemoveStaticTableLayoutForm(tableLayoutPanel3, mainFormTableLayoutPanel);
                    makeMainTableLayoutPanel();
                    selectedTablelayoutREF1 = mainFormTableLayoutPanel;
                    printMainTableLayoutForm(mainFormTableLayoutPanel);
                    tname = phoneName;
                    ttime = phoneTime;

                    trenutniCounter();
                    makeBill(false);
                    izmjeniDatumFajl();
                    ActiveOrders.Clear();

                    numberOfGiros.Add(new numberOdGirosInOrder(counter-1, brojac));
                    int tmpint = int.Parse(button68.Text);
                    //int kolicinaGirosaUNarudzbi = nadjiGirosPodBrojem(counter);
                    int kolicinaGirosaUNarudzbi = brojac;
                    button68.Text = (tmpint + kolicinaGirosaUNarudzbi).ToString();
                    aktivniGirosi.Add(new numberOdGirosInOrder(counter-1, kolicinaGirosaUNarudzbi));

                    dodajUFajlZaPrviRacunar(counter-1, kolicinaGirosaUNarudzbi);

                    counter1 = 1;
                    obrisaniBrojevi.Clear();
                    //counter++;

                    phoneTime = "";
                    phoneName = "";

                    deselectEverything();
                }
            }            
        }

        void makeMainTableLayoutPanel()
        {
            mainFormTableLayoutPanel1 = makeMainTableLayoutForm(mainFormTableLayoutPanel1);
            mainFormTableLayoutPanel2 = makeMainTableLayoutForm(mainFormTableLayoutPanel2);
            mainFormTableLayoutPanel3 = makeMainTableLayoutForm(mainFormTableLayoutPanel3);
            mainFormTableLayoutPanel4 = makeMainTableLayoutForm(mainFormTableLayoutPanel4);
            mainFormTableLayoutPanel = mainFormTableLayoutPanel1;
        }

        private void button69_Click(object sender, EventArgs e)
        {
            buttonClickSound();
            copyOrder();
        }

        void onClickNumberButtonRed(object sender, EventArgs e)
        {
            buttonClickSound();
            if (ActiveOrdersButtonSelected == null)
                return;
            if (ActiveOrdersButtonSelected.Text!="")
            {
                ActiveOrdersButtonSelected.BackgroundImage = Image.FromFile(MyGlobals.crvenaPrazna);
            }
            Button btn = sender as Button;
            ActiveOrdersButtonSelected = btn;
        }

        void onClickNumberButtonGreen(object sender, EventArgs e)
        {
            buttonClickSound();
            if (DoneOrdersButtonSelected == null)
                return;
            if (DoneOrdersButtonSelected.Text!="")
            {
                DoneOrdersButtonSelected.BackgroundImage = Image.FromFile(MyGlobals.zelenaPrazna);
            }
            Button btn = sender as Button;
            DoneOrdersButtonSelected = btn;
        }

        private Button makeNumberFormButton(string str, TableLayoutPanel tlp,bool arrow)
        {
            var btn = new Button();
            btn.Tag = new ControlData() { Location = str };
            btn.Parent = tlp;
            btn.Text = "";
            btn.Dock = DockStyle.Fill;
            btn.Font = new Font(btn.Font.FontFamily, 16);
            btn.BackgroundImage = Image.FromFile(MyGlobals.sivaPrazna);
            setBackgroungImageSetings(btn);
            btn.TabStop = false;
            if(arrow)
                btn.Click += new EventHandler(onClickNumberButtonRed);
            else
                btn.Click += new EventHandler(onClickNumberButtonGreen);
            return btn;
        }

        void printNumberTableLayoutForm(Control parent,TableLayoutPanel baseTable,int j,int i)
        {
            TableLayoutPanel prnt = parent as TableLayoutPanel;
            prnt.Controls.Add(baseTable);
            prnt.SetCellPosition(baseTable, new TableLayoutPanelCellPosition(i,j));
        }
        
        private void button71_Click(object sender, EventArgs e)
        {
            buttonClickSound();
            if (selectedTablelayoutREF1 == null || selectedTablelayoutREF == null || selectedButtonTAG == null)
                return;

            OstaliZacini f2 = new OstaliZacini(this);
            f2.ShowDialog();           
        }

        private void button68_Click(object sender, EventArgs e)
        {          
                girosiUPripremi();           
        }
    }
}

