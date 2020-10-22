using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CartesianTree
{
    public struct PairNodeParent
    {
        public Node Node { get; set; }
        public int IndexParent { get; set; }
        public PairNodeParent(Node n, int i)
        {
            Node = n;
            IndexParent = i;
        }
    }
    public partial class Form1 : Form
    {
        public string ArrayToString(string name, int[] arr)
        {
            string res = name + " {";
            for (int i = 0; i < arr.Length; i++)
            {
                res += arr[i] + ",";
            }
            res = res.Substring(0, res.Length - 1);
            res += "}";
            return res;
        }
        public void GetRandomArray(DataGridView dataGridView1, int max)
        {
            Random rand = new Random();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                bool t = true;
                int n = 0;
                while (t)
                {
                    t = false;
                    n = rand.Next(0, max+1);
                    for (int j = 0; j < i; j++)
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[0].Cells[j].Value) == n)
                        {
                            t = true;
                        }
                    }
                }
                dataGridView1.Rows[0].Cells[i].Value = n;
            }
        }
        public static Point SumPoints(Point p1, Point p2)
        {
            Point res = new Point(p1.X + p2.X, p1.Y + p2.Y);
            return res;
        }
        public string GetNameOfButton(Panel panel)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                if (panel.Controls[i].GetType() == typeof(Label))
                {
                    return panel.Controls[i].Text;
                }
            }
            return "";
        }
        public string GetNameOfButton(Panel p, PictureBox pb)
        {
            for (int i = 0; i < p.Controls.Count; i++)
            {
                for (int j = 0; j < p.Controls[i].Controls.Count; j++)
                {
                    if (p.Controls[i].Controls[j] == pb)
                    {
                        for (int k = 0; k < p.Controls[i].Controls.Count; k++)
                        {
                            if (p.Controls[i].Controls[k].GetType() == typeof(Label))
                            {
                                return p.Controls[i].Controls[k].Text;
                            }
                        }
                    }
                }
            }
            return "";
        }
        static void SetRoundedShape(Control control, int radius)
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
       
        public static PairNodeParent[][] GetArrayTree(Tree tree)
        {
            PairNodeParent[][] arr = new PairNodeParent[1][];
            arr[0] = new PairNodeParent[1];
            arr[0][0] = new PairNodeParent(tree.Root, -1);
            bool t = true;
            while (t)
            {
                t = false;
                Array.Resize(ref arr, arr.Length + 1);
                arr[arr.Length - 1] = new PairNodeParent[0];

                for(int i=0; i< arr[arr.Length-2].Length; i++)
                {
                    if (arr[arr.Length - 2][i].Node.Left != null)
                    {
                        t = true;
                        Array.Resize(ref arr[arr.Length - 1], arr[arr.Length - 1].Length + 1);
                        arr[arr.Length - 1][arr[arr.Length - 1].Length - 1] = new PairNodeParent(arr[arr.Length - 2][i].Node.Left, i);
                    }
                    if (arr[arr.Length - 2][i].Node.Right != null)
                    {
                        t = true;
                        Array.Resize(ref arr[arr.Length - 1], arr[arr.Length - 1].Length + 1);
                        arr[arr.Length - 1][arr[arr.Length - 1].Length - 1] = new PairNodeParent(arr[arr.Length - 2][i].Node.Right, i);
                    }
                }
            }
            Array.Resize(ref arr, arr.Length - 1);
            return arr;
        }
        public static void DrawingTree(Panel panel, PairNodeParent[][] arr)
        {
            int HDN = 70;
            int Height = panel.Height-40;
            int Width = panel.Width;
            int DefHeight = (Height - arr.Length * HDN)/Math.Max(1,(arr.Length-1));
            DrawnNode[][] mas = new DrawnNode[arr.Length][];
            DrawnLine[][] lines = new DrawnLine[arr.Length-1][];
            for (int i=0; i<arr.Length; i++)
            {
                mas[i] = new DrawnNode[arr[i].Length];
                
                for (int j=0; j<arr[i].Length; j++)
                {
                    mas[i][j] = new DrawnNode(arr[i][j].Node.Value, arr[i][j].Node.Priority);
                }
                int sum = 0;
                
                int DefWidth = (int)((Width) / (Math.Pow(2,i+1)));

                if (i != 0)
                {
                    for (int j = 0; j < arr[i].Length; j++)
                    {



                        int x = mas[i - 1][arr[i][j].IndexParent].Location.X + mas[i - 1][arr[i][j].IndexParent].Center.X;
                        if (arr[i][j].Node.Value < arr[i - 1][arr[i][j].IndexParent].Node.Value)
                        {
                            x -= DefWidth + mas[i][j].Width / 2;
                        }
                        else
                        {
                            x += DefWidth - mas[i][j].Width / 2;
                        }



                        mas[i][j].Location = new Point(x, (DefHeight + HDN) * i);
                        sum += mas[i][j].Width;
                    }
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        panel.Controls.Add(mas[i][j]);
                    }
                }
                else
                {
                    mas[0][0].Location = new Point(DefWidth - mas[0][0].Width / 2 + sum, 0);
                    panel.Controls.Add(mas[0][0]);
                }
                
            }
            for (int i = 1; i <arr.Length; i++)
            {
                lines[i - 1] = new DrawnLine[arr[i].Length];
                for(int j=0; j<arr[i].Length; j++)
                {
                    lines[i - 1][j] = new DrawnLine(
                        SumPoints(mas[i][j].Center, mas[i][j].Location), 
                        SumPoints(mas[i - 1][arr[i][j].IndexParent].Center, mas[i - 1][arr[i][j].IndexParent].Location));
                    panel.Controls.Add(lines[i - 1][j]);
                }

            }


        }
        UserControl CurrentUserControl { get; set; }
        public Form1()
        {
            InitializeComponent();
            CurrentUserControl = new CreateTree(this);
            Controls.Add(CurrentUserControl);
            CurrentUserControl.Show();
            for (int i = 0; i < panel2.Controls.Count; i++)
            {
                if (panel2.Controls[i].GetType() == typeof(Panel))
                {
                    panel2.Controls[i].Click += PanelTopClick;
                    for (int j = 0; j < panel2.Controls[i].Controls.Count; j++)
                    {
                        panel2.Controls[i].Controls[j].Click += PanelTopClick;
                    }
                }
            }
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (panel1.Controls[i].GetType() == typeof(Panel))
                {
                    panel1.Controls[i].Click += ButtonHome1_Click;
                    for (int j = 0; j < panel1.Controls[i].Controls.Count; j++)
                    {
                        panel1.Controls[i].Controls[j].Click += ButtonHome1_Click;
                    }
                }
            }
            for (int i=0; i<Save.Controls.Count; i++)
            {
                Save.Controls[i].MouseEnter += Save_MouseEnter;
                Save.Controls[i].MouseLeave += Save_MouseLeave;
            }
            for (int i = 0; i < Random.Controls.Count; i++)
            {
                Random.Controls[i].MouseEnter += Random_MouseEnter;
                Random.Controls[i].MouseLeave += Random_MouseLeave;
            }
        }
        private void ButtonHome1_Click(object sender, EventArgs e)
        {
            string name = "";
            try
            {
                Panel p = (Panel)sender;
                name = GetNameOfButton(p);
            }
            catch { }
            try
            {
                Label l = (Label)sender;
                name = l.Text;
            }
            catch { }
            try
            {
                PictureBox pb = (PictureBox)sender;
                name = GetNameOfButton(panel1, pb);
            }
            catch { }
            if (name != "")
            {
                if (name == "Create")
                {
                    Controls.Remove(CurrentUserControl);
                    CurrentUserControl = new CreateTree(this);
                    Controls.Add(CurrentUserControl);
                    CurrentUserControl.Show();
                    
                }
                if (name == "Insert")
                {
                    Controls.Remove(CurrentUserControl);
                    CurrentUserControl = new InsertIntoTree(this);
                    Controls.Add(CurrentUserControl);
                    CurrentUserControl.Show();
                    panel12.Visible = true;
                    panel4.Visible = false;
                }
            }
        }

        private void PanelTopClick(object sender, EventArgs e)
        {
            string name = "";
            try
            {
                Panel p = (Panel)sender;
                name = GetNameOfButton(p);
            }
            catch { }
            try
            {
                Label l = (Label)sender;
                name = l.Text;
            }
            catch { }
            try
            {
                PictureBox pb = (PictureBox)sender;
                name = GetNameOfButton(panel2, pb);
            }
            catch { }
            if(name == "Заполнить случайно")
            {
                if(CurrentUserControl.GetType() == typeof(CreateTree))
                {
                    int max = int.Parse(((CreateTree)CurrentUserControl).textBox2.Text);
                    DataGridView dataGridView = ((CreateTree)CurrentUserControl).dataGridView1;
                    if (max+1 >= dataGridView.Columns.Count)
                    {
                        GetRandomArray(dataGridView, max);
                    }
                    else
                    {
                        MyMessageBox mmb = new MyMessageBox(this, "Ошибка", "В массив элементов больше чем допустимых значений");
                        mmb.Show();
                    }
                }
            }
            if (name == "Выполнить")
            {
                if (CurrentUserControl.GetType() == typeof(CreateTree))
                {
                    CreateTree ct = (CreateTree)CurrentUserControl;
                    ct.panel1.Controls.Clear();
                    Tree t = new Tree();
                    Node[] arr = new Node[ct.dataGridView1.Columns.Count];
                    int[] mas = new int[arr.Length];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        mas[i] = Convert.ToInt32(ct.dataGridView1.Rows[0].Cells[i].Value);
                        int pr = t.GetPriority();
                        arr[i] = new Node(Convert.ToInt32(ct.dataGridView1.Rows[0].Cells[i].Value), pr);
                    }
                    t.Create(arr);
                    DrawingTree(ct.panel1, GetArrayTree(t));
                    ct.LastTree = mas;
                    ct.LastTreePriority = t.ArrayPriority;
                }
                if (CurrentUserControl.GetType() == typeof(InsertIntoTree))
                {
                    InsertIntoTree ct = (InsertIntoTree)CurrentUserControl;
                    ct.panel1.Controls.Clear();
                    ct.CurrentTree.Insert(new Node(int.Parse(ct.textBox0.Text), ct.CurrentTree.GetPriority()));
                    DrawingTree(ct.panel1, GetArrayTree(ct.CurrentTree));
                }
            }
            if (name == "Сохранить")
            {
                if (CurrentUserControl.GetType() == typeof(CreateTree))
                {
                    CreateTree t = (CreateTree)CurrentUserControl;
                    StreamWriter sw = new StreamWriter("DB.txt", true, Encoding.UTF8);
                    string str = ArrayToString(t.textBox3.Text, t.LastTree);
                    string str1 = ArrayToString(t.textBox3.Text, t.LastTreePriority);
                    sw.WriteLine(str);
                    sw.WriteLine(str1);
                    sw.Close();
                }
            }
        }
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Close_MouseEnter(object sender, EventArgs e)
        {
            Close.BackColor = Color.LightSalmon;
            
        }

        private void Close_MouseLeave(object sender, EventArgs e)
        {
            Close.BackColor = Color.CornflowerBlue;

        }

        private void Open_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Random_MouseEnter(object sender, EventArgs e)
        {
            Random.BackColor = Color.FromArgb(188, 228, 255);
            if(CurrentUserControl.GetType() == typeof(CreateTree))
            {
                ((CreateTree)CurrentUserControl).panel2.Visible = true;
                ((CreateTree)CurrentUserControl).panel2.Location = new Point(Random.Location.X, 0);
            }
        }
        

        private void Random_MouseLeave(object sender, EventArgs e)
        {
            if (CurrentUserControl.GetType() == typeof(CreateTree))
            {
                int x = Cursor.Position.X;
                if(x>Random.Location.X+panel2.Location.X && x< Random.Location.X + panel2.Location.X + Random.Width && panel2.Location.Y<=Cursor.Position.Y )
                {

                }
                else
                {
                    ((CreateTree)CurrentUserControl).panel2.Visible = false;
                }
            }
            Random.BackColor = Color.FromArgb(168, 208, 248);
        }
        private void Save_MouseEnter(object sender, EventArgs e)
        {
            Save.BackColor = Color.FromArgb(188, 228, 255);
            if (CurrentUserControl.GetType() == typeof(CreateTree))
            {
                ((CreateTree)CurrentUserControl).panel7.Visible = true;
                ((CreateTree)CurrentUserControl).panel7.Location = new Point(Save.Location.X, 0);
            }
        }


        private void Save_MouseLeave(object sender, EventArgs e)
        {
            if (CurrentUserControl.GetType() == typeof(CreateTree))
            {
                int x = Cursor.Position.X;
                if (x > Save.Location.X + panel2.Location.X && x < Save.Location.X + panel2.Location.X + Save.Width && panel2.Location.Y <= Cursor.Position.Y)
                {

                }
                else
                {
                    ((CreateTree)CurrentUserControl).panel7.Visible = false;
                }
            }
            Save.BackColor = Color.FromArgb(168, 208, 248);
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }
    }
}
