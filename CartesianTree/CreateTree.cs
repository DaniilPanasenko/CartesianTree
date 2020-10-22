using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CartesianTree
{
    public partial class CreateTree : UserControl
    {
        public int[] LastTree { get; set; }
        public int[] LastTreePriority { get; set; }
        public Tree Tree { get; set; }
        Form1 Form { get; set; }
        public CreateTree(Form1 frm)
        {
            Form = frm;
            InitializeComponent();
            Point p = new Point(frm.panel2.Location.X, frm.panel2.Location.Y + frm.panel2.Size.Height);
            Location = p;
            Size = new Size(frm.Size.Width - p.X, frm.Size.Height - p.Y);
            dataGridView1.Rows.Add();
            dataGridView1.Size = new Size(Size.Width - 320, 160);
            panel1.Size = new Size(Size.Width-60, Size.Height-310);

        }
        private void Plus_Click(object sender, EventArgs e)
        {
            
                textBox1.Text = (int.Parse(textBox1.Text) + 1).ToString();
            
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) != 1)
            {
                textBox1.Text = (int.Parse(textBox1.Text) - 1).ToString();
            }
        }
        private void CreateTree_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) > dataGridView1.Columns.Count)
            {
                dataGridView1.Columns.Add("Column" + textBox1.Text, textBox1.Text);
            }
            if(int.Parse(textBox1.Text) < dataGridView1.Columns.Count)
            {
                dataGridView1.Columns.Remove(dataGridView1.Columns[dataGridView1.Columns.Count-1]);
            }
        }

       

        private void DataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (dataGridView1.Columns.Count > 29)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                for(int i=30;i<dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = dataGridView1.Columns[0].Width;
                }
            }
        }

        private void DataGridView1_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            if (dataGridView1.Columns.Count <= 29)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void Panel2_MouseLeave(object sender, EventArgs e)
        {
            Point p = Cursor.Position;
            if (p.X<=panel2.Location.X+Form.panel1.Width+5 || p.X+5 >=panel2.Location.X +panel2.Width + Form.panel1.Width || p.Y+5>=panel2.Location.Y+panel2.Height+Location.Y )
            {
                panel2.Visible = false;
            }
        }
        private void Panel7_MouseLeave(object sender, EventArgs e)
        {
            Point p = Cursor.Position;
            if (p.X <= panel7.Location.X + Form.panel1.Width + 5 || p.X + 5 >= panel7.Location.X + panel7.Width + Form.panel1.Width || p.Y + 5 >= panel7.Location.Y + panel7.Height + Location.Y
                || (p.Y - 5 <= Location.Y && p.X - 5 >= Form.Save.Location.X + Form.Save.Width + Form.panel2.Location.X))
            {
                panel7.Visible = false;
            }
        }
        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar>=48 && e.KeyChar<=57) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
