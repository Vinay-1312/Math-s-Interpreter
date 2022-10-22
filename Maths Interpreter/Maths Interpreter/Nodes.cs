using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Interpreter
{
    public class Nodes
    {
        public string Node1, Node2;



        public string NumberNode(string Node1)
        {
            this.Node1 = Node1;

            return  this.Node1 ;
        }
        public string AddNode(string Node1,string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "("+this.Node1 + "+" + this.Node2 +")";
        }
        public string SubtractNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "("+this.Node1 + "-" + this.Node2+")";
        }
        public string MultiplyNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "(" + this.Node1 + "*" + this.Node2 + ")";
        }
        public string DivideNode(string Node1, string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return this.Node1 + "/" + this.Node2;
        }
        public string ExpononetNode(string Node1,string Node2)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            return "(" + this.Node1 + "^" + this.Node2 + ")";
        }
        public string AdditionNode(string Node1)
        {
            this.Node1 = Node1;
            
            return "(" + "+" + this.Node1 + ")";
        }
        public string SubtractionNode(string Node1)
        {
            this.Node1 = Node1;

            //  return "("+"-" + this.Node1+")";
            return "(" + "~" + this.Node1 + ")";
        }
    }
}
