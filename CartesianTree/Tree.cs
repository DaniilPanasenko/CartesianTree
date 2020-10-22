using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianTree
{

   public  class Tree
    {
        private Random rand= new Random();
        public int[] ArrayPriority;
        public Node Root { get; set; }
        public Tree(Node root)
        {
            ArrayPriority = new int[0];
            Root = root;
        }
        public Tree()
        {
            ArrayPriority = new int[0];
            Root = null;
        }
        public int GetPriority()
        {
            int priority = -1;
            bool t = false;
            while (!t)
            {
                priority = rand.Next(0, (ArrayPriority.Length + 1) * 2);
                t = true;
                for (int i = 0; i < ArrayPriority.Length; i++)
                {
                    if (ArrayPriority[i] == priority)
                    {
                        t = false;
                        break;
                    }
                }
            }
            return priority;
        }
        public void Split(int key, out Tree Left, out Tree Right)
        {
            if (this == null || Root==null)
            {
                Left = null;
                Right = null;
            }
            else if (key>Root.Value)
            {
                new Tree(Root.Right).Split(key, out Left, out Right);
                if (Left == null) { Left = new Tree(); }
                Root.Right = Left.Root;
                Left = new Tree(Root);
            }
            else
            {
                new Tree(Root.Left).Split(key, out Left, out Right);
                if (Right == null) { Right = new Tree(); }
                Root.Left = Right.Root;
                Right = new Tree(Root);
            }
        }    
        public Tree Merge(Tree tree2)
        {
            if (this == null || Root == null)
            {
                return tree2;
            }
            if (tree2==null ||tree2.Root == null)
            {
                return this;
            }
            if (Root.Priority > tree2.Root.Priority)
            {
                Root.Right = new Tree(Root.Right).Merge(tree2).Root;
                return this;
            }
            else
            {
                tree2.Root.Left = this.Merge(new Tree(tree2.Root.Left)).Root;
                return tree2;
            }
        }
        public Tree Insert(Node node)
        {
            
            if (Root == null)
            {
                Array.Resize(ref ArrayPriority, ArrayPriority.Length + 1);
                ArrayPriority[ArrayPriority.Length - 1] = node.Priority;
                return new Tree(node);
            }
            if(node.Priority > Root.Priority)
            {
                Tree t1;
                Tree t2;
                Split(node.Value, out t1, out t2);
                if (t1 == null) { t1 = new Tree(); }
                if (t2 == null) { t2 = new Tree(); }
                
              
                this.Root= t1.Merge(new Tree(node)).Merge(t2).Root;
                Array.Resize(ref ArrayPriority, ArrayPriority.Length + 1);
                ArrayPriority[ArrayPriority.Length - 1] = node.Priority;
                return this;
            }
            else if (node.Value < Root.Value)
            {
                Root.Left = new Tree(Root.Left).Insert(node).Root;
                Array.Resize(ref ArrayPriority, ArrayPriority.Length + 1);
                ArrayPriority[ArrayPriority.Length - 1] = node.Priority;
                return this;
            }
            else
            {
                Root.Right = new Tree(Root.Right).Insert(node).Root;
                Array.Resize(ref ArrayPriority, ArrayPriority.Length + 1);
                ArrayPriority[ArrayPriority.Length - 1] = node.Priority;
                return this;
            }
        }
        public void Create(Node[] arr)
        {
            for(int i=0; i<arr.Length; i++)
            {
                arr[i].Priority = GetPriority();
                this.Root = this.Insert(arr[i]).Root;
            }
        }
    }
}
