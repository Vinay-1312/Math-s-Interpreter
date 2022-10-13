using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                tokens = lexer.GenerateTokens();
                /*
                foreach(string token in tokens)
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
                */
                Console.WriteLine(string.Join(",", tokens));
                Parser p1 = new Parser(tokens);
                string Tree = p1.Parse();
                Console.WriteLine(Tree);

            }
            
        }
    }
}
