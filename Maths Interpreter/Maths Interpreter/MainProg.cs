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
            while(true)
            {
                Console.Write("Expr > ");
                string input = Console.ReadLine();
                Lexer lexer = new Lexer(input);
                var tokens = lexer.GenerateTokens();
                Console.WriteLine(string.Join(",",tokens));
            }
        }
    }
}
