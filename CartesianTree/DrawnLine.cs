using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CartesianTree
{
    class DrawnLine: Panel
    {
        Point P1 { get; set; }
        Point P2 { get; set; }
        bool per;
        public bool Perevernut
        {
            get
            {
                return per;
            }
            set
            {
                per = value;
                Invalidate();
            }
        }
        public DrawnLine(Point p1, Point p2)
        {
            Location = new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            Size = new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            P1 = p1;
            P2 = p2;
            Perevernut = true;
            BackColor = Color.Transparent;
        }
        public DrawnLine():this(new Point(100,100), new Point(200,200))
        {

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Graphics g = e.Graphics;
            if (P1.X > P2.X || !per)
            {
                g.DrawLine(new Pen(Color.Black, 3), new Point(0, 0), new Point(Width, Height));
            }
            else
            {
                g.DrawLine(new Pen(Color.Black, 3), new Point(0, Height), new Point(Width, 0));
            }
        }
    }
}
