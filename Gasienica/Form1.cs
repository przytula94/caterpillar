using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Gasienica
{
    public partial class Form1 : Form
    {
        private List<Punkt> punkty = new List<Punkt>();
        private List<PunktSporny> punktySporne = new List<PunktSporny>();
        private List<Gasienica> gasienice = new List<Gasienica>();

        public Form1()
        {
            InitializeComponent();
            dodajPunkty();
            dodajGasienice();

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 50; // przerysowanie co 50 ms
            t.Tick += new EventHandler(rysowanie);
            t.Start();
        }

        private void dodajGasienice()
        {
            
            Point[] droga = new Point[4] { new Point(420, 200), new Point(240, 200), new Point(240, 470), new Point(420, 470) };
            Point glowa = new Point(420, 200);
            PunktSporny[] p1 = new PunktSporny[3] { pobierzPkt(1), pobierzPkt(2), pobierzPkt(3) };
      
            Gasienica g1 = new Gasienica(glowa, 9, 30, droga, Color.Red, p1, 1);
            gasienice.Add(g1);
            
            Thread watek2 = new Thread(g1.ruszaj);
            watek2.IsBackground = true;
            watek2.Start();

            droga = new Point[4] { new Point(150, -10), new Point(150, 200), new Point(330, 200), new Point(330, -10) };
            glowa = new Point(150, -10);
            PunktSporny[] p2 = new PunktSporny[3] { pobierzPkt(4), pobierzPkt(1), pobierzPkt(2) };

            Gasienica g2 = new Gasienica(glowa, 5, 30, droga, Color.Orange, p2, 2);
            gasienice.Add(g2);

            Thread watek = new Thread(g2.ruszaj);
            watek.IsBackground = true;
            watek.Start();




            droga = new Point[4] { new Point(90, 470), new Point(240, 470), new Point(240, 200), new Point(90, 200) };
            glowa = new Point(90, 470);
            PunktSporny[] p3 = new PunktSporny[3] { pobierzPkt(4), pobierzPkt(2), pobierzPkt(3) };

            Gasienica g3 = new Gasienica(glowa, 7, 30, droga, Color.Blue, p3, 3);
            gasienice.Add(g3);
            Thread watek3 = new Thread(g3.ruszaj);
            watek3.IsBackground = true;
            watek3.Start();
        }

        private PunktSporny pobierzPkt(int n)
        {
            
            foreach (PunktSporny p in punktySporne)
                if (p.pobierzNumer() == n)
                    return p;
            return null;
        }

        
        private void dodajPunkty()
        {
            int wielkosc = 30;
            int i = 0;

            int j = 470;
            int licznik = 0;
            
            // dół i środek - poziom
            for (i = 90; i < 450; i = i + wielkosc)
            {
                switch (licznik)
                {
                    case 2:
                        punkty.Add(new Punkt(i, j, wielkosc));
                        punktySporne.Add(new PunktSporny(i, 200, wielkosc, 4));
                        break;
                    case 5:
                        punktySporne.Add(new PunktSporny(i, 200, wielkosc, 2));
                        punktySporne.Add(new PunktSporny(i, j, wielkosc, 3));
                        break;
                    case 8:
                        punkty.Add(new Punkt(i, j, wielkosc));
                        punktySporne.Add(new PunktSporny(i, 200, wielkosc, 1));
                        break;
                    default:
                        punkty.Add(new Punkt(i, j, wielkosc));
                        punkty.Add(new Punkt(i, 200, wielkosc));
                        break;
                }
                licznik++;
            }

            //Gora - poziom
            for (i = 150; i < 360; i = i + wielkosc)
                punkty.Add(new Punkt(i, -10, wielkosc));

            // piony - dol
            for (j = 440; j > 200; j = j - wielkosc)
            {
                punkty.Add(new Punkt(90, j, wielkosc));
                punkty.Add(new Punkt(240, j, wielkosc));
                punkty.Add(new Punkt(420, j, wielkosc));
            }
            
            // piony - gora
            for (j = 20; j < 230; j = j + wielkosc)
            {
                punkty.Add(new Punkt(150, j, wielkosc));
                punkty.Add(new Punkt(330, j, wielkosc));
            }

        }


        void rysowanie(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (Gasienica ga in gasienice)
                ga.Paint(g);

            foreach (Punkt p in punkty)
                p.Paint(g);

            foreach (PunktSporny p in punktySporne)
                p.Paint(g);
        }
    }
}
