using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gasienica
{
    class Punkt
    {
        protected int wielkosc;
        protected Point wspl;

        public Punkt(int x, int y, int wielkosc)
        {
            this.wielkosc = wielkosc;
            wspl = new Point(x, y);
        }

        public Point getWspl()
        {
            return this.wspl;
        }

        public void Paint(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Black), wspl.X, wspl.Y, wielkosc, wielkosc);
        }
    }
}
