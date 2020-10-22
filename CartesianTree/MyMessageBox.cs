using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CartesianTree
{
    public partial class MyMessageBox : Form
    {
        public MyMessageBox()
        {
            InitializeComponent();
        }
        public bool PanelMouseDown { get; set; }
        public Point PanelMouseDownLocation { get; set; }
        public string Met1 { get; set; }
        public string Met2 { get; set; }
        public Form1 Frm { get; set; }
        public MyMessageBox(Form1 frm, string name, string message, string but1, string but2, string met1, string met2)
        {
            InitializeComponent();
            Frm = frm;
            label4.Text = name;
            label1.Text = message;
            Yes.Text = but1;
            if (but2 != "")
            {
                No.Text = but2;
            }
            else
            {
                No.Visible = false;
                Yes.BackColor = Color.CornflowerBlue;
            }
            Met1 = met1;
            Met2 = met2;
        }
        public MyMessageBox(Form1 frm, string name, string message):this(frm,name,message,"OK","","OK","")
        {
            
        }
        private void MyMessageBox_Load(object sender, EventArgs e)
        {
            int lenbut = Yes.Size.Width;
            if (No.Visible)
            {
                lenbut += No.Size.Width + 50;
            }
            this.Size = new Size(130 + Math.Max(lenbut, label1.Size.Width), this.Size.Height);
            label1.Location = new Point((this.Size.Width - label1.Size.Width) / 2, label1.Location.Y);
            Yes.Location = new Point((this.Size.Width - lenbut) / 2, Yes.Location.Y);
            No.Location = new Point((this.Size.Width - lenbut) / 2 + Yes.Size.Width + 50, Yes.Location.Y);
            Size screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            this.Location = new Point((screen.Width - this.Size.Width) / 2, (screen.Height - this.Size.Height) / 2);
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            Close.BackColor = Color.Orange;
            Close.ForeColor = Color.Black;
        }
        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            Close.BackColor = Color.Transparent;
            Close.ForeColor = Color.White;
        }
        private void PanelForm_MouseDown(object sender, MouseEventArgs e)
        {
            PanelMouseDown = true;
            PanelMouseDownLocation = new Point(e.X, e.Y);
        }
        private void PanelForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (PanelMouseDown)
            {
                this.Location = new Point(this.Location.X - PanelMouseDownLocation.X + e.X, this.Location.Y - PanelMouseDownLocation.Y + e.Y);
            }
        }
        private void PanelForm_MouseUp(object sender, MouseEventArgs e)
        {
            PanelMouseDown = false;
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            if(Met1 == "OK")
            {
                Close();
            }
        }
    }
}
