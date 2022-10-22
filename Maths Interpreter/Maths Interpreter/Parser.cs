using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Interpreter
{
    public class Parser
    {
        public List<string> input,div;
        public int currentIndex = -1;
        public int isEnded = 0;
        public string res, currentchar;

        Tokens T1 = new Tokens();
        Nodes N1 = new Nodes();
        public Parser(List<string> input)
        {
            this.input = input;
            this.Next();   
        }
        public void Next()
        {   
            
            if (this.currentIndex != this.input.Count - 1)
            {
                this.currentIndex = this.currentIndex + 1;
            }
            else
            {
                this.isEnded = 1;
                
            }


        }
        public string CurrentValue()
        {
            return this.input[this.currentIndex];
        }
        public string RaiseError()
        {
            return "Syntax Error";
        }
        public string Parse()
        {   if (this.CurrentValue() == "\0")
            {
                return null;
            }
        //storing the result of an expression
            res = this.Expression();

            //Console.WriteLine(this.CurrentValue());
            
            /*
            if (this.CurrentValue() != "\0")
                this.RaiseError();
            */
            return res;

        }
        public string Expression()
        {
            res = this.Term();
            if (res.Contains("Syntax Error"))
            {
                return "Syntax Error";
            }
            else
            {
                this.currentchar = this.CurrentValue();
                while (this.isEnded != 1 && (this.currentchar.Equals(T1.plus) || this.currentchar.Equals(T1.subtract)))
                {

                    if (this.currentchar.Equals(T1.plus))
                    {
                        this.Next();
                        res = N1.AddNode(res, this.Term()); // for example -  add node 2 + 3
                    }
                    else if ((this.currentchar.Equals(T1.subtract)))
                    {
                        this.Next();
                        res = N1.SubtractNode(res, this.Term());  // for example -  add node 2 - 3
                    }



                }
            }
            
            return res;
        }
        //this function checks for the multiplication and division because of the presedence 
        public string Term()
        {
            res = this.Factor();
            if(res.Equals("Syntax Error"))
            {
                return "Syntax Error";
            }
            else
            { 
            this.currentchar = this.CurrentValue();
                while (this.isEnded != 1 && (this.currentchar.Equals(T1.multiplication) || this.currentchar.Equals(T1.division) || this.currentchar.Equals(T1.expo)))
                {

                    if (this.currentchar.Equals(T1.multiplication))
                    {
                        this.Next();
                        res = N1.MultiplyNode(res, this.Term()); // for example -  add node 2 * 3
                    }
                    else if ((this.currentchar.Equals(T1.division)))
                    {
                        this.Next();
                       
                        res = N1.DivideNode(res, this.Term()); // for example -  add node 2 / 3
                    }
                    else if ((this.currentchar.Equals(T1.expo)))
                    {
                        this.Next();
                        res = N1.ExpononetNode(res, this.Term()); // for example -  add node 2 / 3
                    }


                }
            }

            return res;

        }
        //this function checks for number, left parenthesis then for the right parenthesis and then for single + or - operator
        public string Factor()

        {   try
            {

                this.currentchar = this.CurrentValue();

                if (currentchar.Equals(T1.leftParenthesis))
                {
                    this.Next();
                    res = this.Expression();

                    this.currentchar = this.CurrentValue();
                    if (!this.currentchar.Equals(T1.rightParenthesis))
                    {
                        return this.RaiseError();
                    }
                    this.Next();
                    return res;
                }

                else if (this.currentchar.Contains(T1.number))
                {
                    this.Next();


                    return N1.NumberNode(this.currentchar.Split(":")[1].Trim());

                }
                else if (this.currentchar.Equals(T1.plus))
                {
                    this.Next();


                    return N1.AdditionNode(this.Factor()); ////to add node +2

                }
                else if (this.currentchar.Equals(T1.subtract))
                {
                    this.Next();

                    return N1.SubtractionNode(this.Factor());//to add node -2
                }
                return this.RaiseError();
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Invalid Expression");
                return null;
            }
        }

    }
}
