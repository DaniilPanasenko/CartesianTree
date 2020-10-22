using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CartesianTree
{
    public partial class InsertIntoTree : UserControl
    {
        Form1 Form{get; set;}
        public Tree CurrentTree { get; set; }
        public InsertIntoTree(Form1 frm)
        {
            Form = frm;
            InitializeComponent();
            string[] str = File.ReadAllLines("DB.txt");
            for (int i = 0; i < str.Length; i += 2)
            {
                comboBox1.Items.Add(str[i].Split(' ')[0]);
            }
            Point p = new Point(frm.panel2.Location.X, frm.panel2.Location.Y + frm.panel2.Size.Height);
            Location = p;
            Size = new Size(frm.Size.Width - p.X, frm.Size.Height - p.Y);
        }

        private void InsertIntoTree_Load(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            string[] str = File.ReadAllLines("DB.txt");
            string[] values = new string[0];
            string[] prior = new string[0];
            for(int i=0; i<str.Length; i++)
            {
                if(comboBox1.Text == str[i].Split(' ')[0])
                {
                    values = str[i].Split(' ')[1].Replace("{","").Replace("}", "").Split(',');
                    prior = str[i+1].Split(' ')[1].Replace("{", "").Replace("}", "").Split(',');
                    break;
                }
            }
            panel1.Controls.Clear();
            Tree t = new Tree();
            Node[] arr = new Node[values.Length];
     
            for (int i = 0; i < arr.Length; i++)
            {

                arr[i] = new Node(Convert.ToInt32(values[i]), int.Parse(prior[i]));
            }
            t.Create(arr);
            Form1.DrawingTree(panel1, Form1.GetArrayTree(t));
            CurrentTree = t;
        }
    }
}
