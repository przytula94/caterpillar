using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace Gasienica
{
    class PunktSporny:Punkt // Dziedziczy z punktu
    {
        public Semaphore dostep = new Semaphore(1, 1); // Semafor dostępu
        int nrPunktu;

        public PunktSporny(int x, int y, int wielkosc, int nrPunktu) : base(x, y, wielkosc)
        {
            this.wspl = new Point(x, y);
            this.wielkosc = wielkosc;
            this.nrPunktu = nrPunktu;
        }

        new public void Paint(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Orange), wspl.X, wspl.Y, wielkosc, wielkosc);
        }

        public int pobierzNumer()
        {
            return this.nrPunktu;
        }
    }
}
