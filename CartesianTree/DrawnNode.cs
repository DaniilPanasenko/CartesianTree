using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CartesianTree
{
    class DrawnNode: Panel
    {

        private int borderThickness = 6;
        private int borderThicknessByTwo = 3;

        public Point Center { get; set; }
        Panel Panel1 { get; set; }
        static void SetRoundedShape(Panel control, int radius)
        {
          
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

                path.AddLine(radius, 0, control.Width - radius, 0);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddLine(control.Width, radius, control.Width, control.Height - radius);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddLine(control.Width - radius, control.Height, radius, control.Height);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.AddLine(0, control.Height - radius, 0, radius);
                path.AddArc(0, 0, radius, radius, 180, 90);
                control.Region = new Region(path);
            
        }
        public DrawnNode(int Value, int Preority):base()
        {
            Panel panel1 = new Panel();
            Panel panel2 = new Panel();
            panel1.Dock = DockStyle.Left;
            panel2.Dock = DockStyle.Right;
            panel1.BackColor = Color.FromArgb(168,208,248);
            panel2.BackColor = Color.CornflowerBlue;
            Controls.Add(panel1);
            Controls.Add(panel2);
            Label l1 = new Label();
            Label l2 = new Label();
            l1.Text = Value + "";
            l2.Text = Preority + "";
            l2.ForeColor = Color.White;
            l1.Font = new Font("Century Gothic", 13);
            l2.Font = new Font("Century Gothic", 13);
            l1.AutoSize = true;
            l2.AutoSize = true;
            panel1.Controls.Add(l1);
            panel2.Controls.Add(l2);
            panel1.Padding = new Padding(0, 0, 0, 0);
            l1.Location = new Point(10, 13);
            l2.Location = new Point(4, 13);
            panel1.Size = new Size(l1.Width+10, 70);
            panel2.Size = new Size(l2.Width+10, 70);
            Size = new Size(panel1.Width + panel2.Width, 70);
            Panel1 = panel1;
            SetRoundedShape(this, 70);
            Center = new Point(Size.Width/2, Size.Height/2);
            //Panel1.Paint += Panel1_Paint;
        }
        public DrawnNode() : this(0, 0)
        {

        }

        //private void Panel1_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    Brush brush = new SolidBrush(Color.Black);

        //    g.FillEllipse(brush, 0, 0, Height-1, Height - 2);
        //    g.FillEllipse(brush, Width - Height+10, 0, Height - 1, Height - 1);
        //    g.FillRectangle(brush, Height / 2, 0, Width - Height - 1, Height - 1);

        //    brush.Dispose();
        //    brush = new SolidBrush(Color.FromArgb(168,208,248));

        //    g.FillEllipse(brush, borderThicknessByTwo, borderThicknessByTwo, Height - borderThickness,
        //        Height - borderThickness - 1);
        //    g.FillEllipse(brush, (Width - Height) + borderThicknessByTwo, borderThicknessByTwo,
        //        Height - borderThickness - 1, Height - borderThickness - 1);
        //    g.FillRectangle(brush, Height / 2 + borderThicknessByTwo, borderThicknessByTwo,
        //        Width - Height - borderThickness - 1, Height - borderThickness - 1);

        //    brush.Dispose();
        //}

        
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Graphics g = e.Graphics;
        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //    Brush brush = new SolidBrush(Color.Black);

        //    g.FillEllipse(brush, 0, 0, Height, Height-1);
        //    g.FillEllipse(brush, Width - Height, 0, Height-1, Height-1);
        //    g.FillRectangle(brush, Height / 2, 0, Width - Height-1, Height-1);

        //    brush.Dispose();
        //    brush = new SolidBrush(Color.CornflowerBlue);

        //    g.FillEllipse(brush, borderThicknessByTwo, borderThicknessByTwo, Height - borderThickness,
        //        Height - borderThickness-1);
        //    g.FillEllipse(brush, (Width - Height) + borderThicknessByTwo, borderThicknessByTwo,
        //        Height - borderThickness-1, Height - borderThickness-1);
        //    g.FillRectangle(brush, Height / 2 + borderThicknessByTwo, borderThicknessByTwo,
        //        Width - Height - borderThickness-1, Height - borderThickness-1);
            
        //    brush.Dispose();
            


        //}
    }
}
