using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianTree
{
    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Priority { get; set; }
        public int Value { get; set;}
        public Node (int value, int priority, Node left, Node right)
        {
            Left = left;
            Right = right;
            Value = value;
            Priority = priority;
        }
        public Node (int value, int priority)
        {
            Left = null;
            Right = null;
            Value = value;
            Priority = priority;
        }
    }
}
