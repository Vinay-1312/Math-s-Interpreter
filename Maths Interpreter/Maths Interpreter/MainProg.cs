using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Maths_Interpreter
{
    public class MainProg
    {

        public static void Main(string[] args)
        {

            List<string> tokens;
            while (true)
            {
                Console.Write("Expr > ");
                string input = Console.ReadLine();
                Lexer lexer = new Lexer(input);
                Interpreter interpreter = new Interpreter();
                tokens = lexer.GenerateTokens();
                if (tokens.Count == 0)
                {
                    Console.WriteLine("Invalid Expression");
                    continue;
                }
                /*
                 foreach (string token in tokens)
                 {
                     var splittedToken = token.Split(":");

                     if (splittedToken[0].Equals("Number"))
                     {


                         Console.WriteLine($"Token ID {splittedToken[0]} Token Value {long.Parse(splittedToken[1])}");
                     }
                     else
                     {
                         Console.WriteLine($"Token ID {splittedToken[0]}");
                     }

                 }

 */             Console.WriteLine(string.Join(",", tokens));
                Parser p1 = new Parser(tokens);
                string Tree = p1.Parse();
                if (Tree.Equals("Syntax Error"))
                {
                    Console.WriteLine("Syntax Error");
                    continue;
                }
                Console.WriteLine(Tree);

               double ans = interpreter.interpret(Tree);
               Console.WriteLine(ans);

                //  DataTable dt = new DataTable();
                //var v = dt.Compute(Tree, "");


                //Console.WriteLine(v);



            }
        }
    }

}

