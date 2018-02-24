using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Gasienica
{
    class Gasienica
    {
        private Point Glowa; 
        private int numerGasienicy; 
        private int dlugosc; 
        private int wielkosc;
        private Point[] droga; 
        private int nextPoint; 
        private Color c;
        private List<Point> punkty = new List<Point>();
        private int opoznienie = 100;
        private PunktSporny[] punktySporne;
        

        public Gasienica(Point g, int dlugosc, int wielkosc, Point[] droga, Color c, PunktSporny[] p, int numerGasienicy)
        {
            
            this.c = c;
            this.numerGasienicy = numerGasienicy;
            this.Glowa = g;
            this.dlugosc = dlugosc;
            this.wielkosc = wielkosc;
            this.droga = droga;
            nextPoint = 1;
            punktySporne = p;
           
            punkty.Add(this.Glowa);
           
            for (int i = 0; i < dlugosc - 1; i++) 
            {
                if (Glowa.X < droga[3].X)
                    punkty.Add(new Point(Glowa.X + wielkosc * i, Glowa.Y));
                else if (Glowa.X > droga[3].X)
                    punkty.Add(new Point(Glowa.X - wielkosc * i, Glowa.Y));
                else if (Glowa.Y < droga[3].Y)
                    punkty.Add(new Point(Glowa.X, Glowa.Y + wielkosc * i));
                else if (Glowa.Y > droga[3].Y)
                    punkty.Add(new Point(Glowa.X, Glowa.Y - wielkosc * i));
            }
        }

        public void Paint(Graphics g) 
        {
            for (int i = 0; i < punkty.Count; i++ )
                g.FillRectangle(new SolidBrush(c), punkty[i].X, punkty[i].Y, wielkosc, wielkosc);
        }

        public void ruszaj() 
        {
            Point poprzedni = new Point(0, 0); 
            while(true) 
            {
                if (punkty[0].X < droga[nextPoint].X) //  w prawo
                {                        
                    for (int i = 0; i < punkty.Count; i++)
                    {
                        if (i == 0) 
                        {
                            sprawdzDostep(punkty[0].X + 30, punkty[0].Y); 
                            poprzedni = punkty[0];
                            punkty[0] = new Point(punkty[0].X + 30, punkty[0].Y);
                        }
                        else
                        { 
                            Point pomocniczy = punkty[i];
                            punkty[i] = poprzedni;
                            poprzedni = pomocniczy;
                        }
                    }
                }
                else if (punkty[0].X > droga[nextPoint].X) // w lewo
                {
                    for (int i = 0; i < punkty.Count; i++)
                    {
                        if (i == 0)
                        {
                            sprawdzDostep(punkty[0].X - 30, punkty[0].Y);
                            poprzedni = punkty[0];
                            punkty[0] = new Point(punkty[0].X - 30, punkty[0].Y);
                        }
                        else
                        {
                            Point pomocniczy = punkty[i];
                            punkty[i] = poprzedni;
                            poprzedni = pomocniczy;
                        }
                    }
                }
                else if (punkty[0].Y < droga[nextPoint].Y) // W dol
                {
                    for (int i = 0; i < punkty.Count; i++)
                    {
                        if (i == 0)
                        {
                            sprawdzDostep(punkty[0].X, punkty[0].Y + 30);
                            poprzedni = punkty[0];
                            punkty[0] = new Point(punkty[0].X, punkty[0].Y + 30);
                        }
                        else
                        {
                            Point pomocniczy = punkty[i];
                            punkty[i] = poprzedni;
                            poprzedni = pomocniczy;
                        }
                    }
                }
                else if (punkty[0].Y > droga[nextPoint].Y) // W gore
                {
                    for (int i = 0; i < punkty.Count; i++)
                    {
                        if (i == 0)
                        {
                            sprawdzDostep(punkty[0].X, punkty[0].Y - 30);
                            poprzedni = punkty[0];
                            punkty[0] = new Point(punkty[0].X, punkty[0].Y - 30);
                        }
                        else
                        {
                            Point pomocniczy = punkty[i];
                            punkty[i] = poprzedni;
                            poprzedni = pomocniczy;
                        }
                    }
                }
                else
                    nextPoint = (nextPoint + 1) % 4; 

                uwolnij(punkty[punkty.Count - 1].X, punkty[punkty.Count - 1].Y);
               
                Thread.Sleep(opoznienie); 
            }
        }


       
        public void uwolnij(int X, int Y)
        {    
            for (int j = 0; j < 3; j++)
                if (X == punktySporne[j].getWspl().X && Y == punktySporne[j].getWspl().Y)
                    punktySporne[j].dostep.Release();
        }


       
        public void sprawdzDostep(int X, int Y)
        {
            if (numerGasienicy == 1) //Czerwony
            {
                if (punktySporne[0].getWspl().X == X && punktySporne[0].getWspl().Y == Y )
                
                    punktySporne[0].dostep.WaitOne();
                else if (punktySporne[1].getWspl().X == X && punktySporne[1].getWspl().Y == Y )
                
                    for (int x = 1; x < 3; x++) 
                        punktySporne[x].dostep.WaitOne();
            }
            else if (numerGasienicy == 2) //Zolty
            {
                if (punktySporne[0].getWspl().X == X && punktySporne[0].getWspl().Y == Y)
                
                     punktySporne[0].dostep.WaitOne();
                else if(punktySporne[2].getWspl().X == X && punktySporne[2].getWspl().Y == Y)
                   
                    for (int x = 1; x < 3; x++)
                        punktySporne[x].dostep.WaitOne();
            }
            else if (numerGasienicy == 3) // Niebieski
            
                if (punktySporne[2].getWspl().X == X && punktySporne[2].getWspl().Y == Y)
                    for (int x = 0; x < 3; x++)
                        punktySporne[x].dostep.WaitOne();
        }
    }
}

