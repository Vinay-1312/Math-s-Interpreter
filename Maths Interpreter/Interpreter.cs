using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace Maths_Interpreter

{
    public class Interpreter
    {

        Stack<double> Operands = new Stack<double>(); //stack for number
        Stack<Char> Operations = new Stack<Char>(); // st
                                                    // ack for operators
        List<double> div_num = new List<double>();
        //List<double> plotNumbers = new List<double>();
        //ist<double> parameters = new List<double>();
        //private Dictionary<Char, List<double>> plotValues = new Dictionary<Char, List<double>>();
        public static Dictionary<string, double> variablesStored = new Dictionary<string, double>();
        Tokens T1 = new Tokens();
        int div_count = 1;
        string variable;
        double output,div_exp,temp_num1;
        public double Calculate(Stack<double> Operands, Stack<Char> Operations)
        {
            double num1, num2;
            char operation = Operations.Pop();
            if (operation.Equals('~'))
            {
                num1 = Operands.Pop();
                return -num1;

            }
            else
            {
               

                switch (operation)
                {
                    case '+':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();
                        return num1 + num2;
                    case '-':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();
                        return num2 - num1;
                    case '*':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();
                        return num1 * num2;
                    case '/':
                       /* div_count = 2;
                        if (Operations.Peek() == '/')
                        {
                            while (Operations.Peek() == '/')
                            {
                                div_count = div_count + 1;
                                operation = Operations.Pop();

                            }
                            while(div_count!=0)
                            {
                                div_num.Add(Operands.Pop());
                                div_count = div_count - 1;
                            }
                            div_exp=div_num[div_num.Count - 1];
                            div_num.RemoveAt(div_num.Count - 1);
                            while (div_num.Count!=0)
                            {
                                temp_num1 = div_num[div_num.Count - 1];
                                if (temp_num1 == 0)
                                {
                                    Console.WriteLine("Cannot divide by zero");
                                    return 0.0;
                                }
                                div_num.RemoveAt(div_num.Count - 1);
                                div_exp = div_exp / temp_num1;
                                
                                
                            }
                            return div_exp;
                        }
                        else
                        {*/
                            num1 = Operands.Pop();
                            num2 = Operands.Pop();
                            if (num1 == 0)
                            {
                                Console.WriteLine("Cannot divide by zero");
                                return 0.0;
                            }
                            return num2 / num1;
                        //}

                        return 0.0;
                    case '%':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();
                        return num2 % num1;
                    case '^':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();
                        return Math.Pow(num2, num1);

                    case '=':
                        num1 = Operands.Pop();
                        if(Operations.Peek() == '=')
                        {
                            num2 = Operands.Pop();
                            Operations.Pop();
                            if (num1 == num2)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                            
                        }
                        else if(Operations.Peek() == '<')
                        {
                            num2 = Operands.Pop();
                            Operations.Pop();
                            if (num1 <= num2)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                            
                        }
                        else if(Operations.Peek() == '>')
                        {
                            num2 = Operands.Pop();
                            Operations.Pop();
                            if (num1 <= num2)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                            
                        }
                        Interpreter.variablesStored[variable] = num1;
                        return num1;

                    case '>':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();
                        
                            if (num2 > num1)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                        

                    case '<':
                        num1 = Operands.Pop();
                        num2 = Operands.Pop();
                    
                        if (num2 < num1)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }


                        

                    

                }
            }
            return 0.0;
        }
        public string interpret(string expr)
        {
            string number = "";
            int plotFunction = 0;
            if (Interpreter.variablesStored.ContainsKey(expr))
            {
                return Interpreter.variablesStored[expr].ToString();
                
            }
           /* if(expr.StartsWith("plot("))
                {
                plotFunction = 1;
                expr = expr.Trim().Remove(0, 4);
            }
           */
            if (!expr.StartsWith('(') && plotFunction==0)
            {
                expr = '(' + expr;
            }
            if (!expr.EndsWith(')') && plotFunction == 0)

            {
                expr = expr + ')';
            }




            for (int i = 0; i < expr.Length; i++)
            {
                Char c = expr[i];
                if (Char.IsDigit(c))
                {

                    number = "";
                    //conditions for multiple digit numbers
                    while (Char.IsDigit(c) || c.Equals('.'))
                    {

                        number = number + c;
                        i = i + 1;

                        if (i < expr.Length)
                        {
                            c = expr[i];

                        }
                        else
                        {
                            break;
                        }


                    }
                    i = i - 1; // decrementing i to avoid skipping of any character
                    Operands.Push(double.Parse(number));

                }
                else if ((Char.IsLetter(c) || c.Equals('_')) && plotFunction == 0)
                {
                    this.variable = "";
                    while (Char.IsLetterOrDigit(c) || c.Equals('_'))
                    {
                        this.variable = this.variable + c;
                        i = i + 1;
                        if (i < expr.Length)
                        {
                            c = expr[i];

                        }
                        else
                        {
                            break;
                        }
                    }
                    i = i - 1;
                    if (!Interpreter.variablesStored.ContainsKey(this.variable))
                    {
                        Interpreter.variablesStored[this.variable] = 0;
                    }
                    else
                    {
                        Operands.Push(Interpreter.variablesStored[this.variable]);
                    }
                }
               /* else if (Char.IsLetter(c)  && plotFunction == 1)
                    {
                    plotFunction = 0;
                    plotValues[c] = new List<double>();
                }
               */

                else if (c.Equals('('))
                {
                    Operations.Push(c);
                }
                else if (c.Equals(')'))
                {   /*
                    while(Operations.Peek()== '#')
                    {
                        Operations.Pop();
                        Operations.Pop();
                        parameters.Add(Operands.Pop());
                    }
                    */
                    while (Operations.Peek() != '(')//check all the operators till '(' is reached
                    {
                        output = Calculate(Operands, Operations);
                        Operands.Push(output);   //push result back to stack
                    }
                    Operations.Pop();
                }
                
                else
                {
                    Operations.Push(c);
                }

            }
            
            while (Operations.Count()!=0 && Operands.Count()!=0)
            {
                 output = Calculate(Operands, Operations);
                Operands.Push(output);   //push final result back to stack
            }
            if (expr.Contains(T1.equality) || expr.Contains(T1.lessThan) || expr.Contains(T1.lessThanOrEqual) || expr.Contains(T1.greaterThan) || expr.Contains(T1.greaterThanOrEqual))
            {
                if(Operands.Pop() == 1)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            else
            {
                return Operands.Pop().ToString();
            }
        }

        //Evaluate given expression
            
    }
        

}

