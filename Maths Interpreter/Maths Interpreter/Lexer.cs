using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maths_Interpreter
{
    public class Lexer
    {
        private string input;
        private int currentIndex = -1;
        private char[] WHITESPACE = {' ','\n','\t',};
        private string[] reservedWords = { "if", "while", "for", "foreach", "switch", "break", "continue", "else" };
        Tokens T1 = new Tokens();
        List<string> tokens = new List<string>();
        public Lexer(string input)
        {
            this.input = input;
            this.Next();
        }
        //Get current character from a given input
        private char CurrentChar()
        {

            if (this.currentIndex != this.input.Length)
            {
                return this.input[this.currentIndex];

            }
            return '\0';
           }

        //Go to the next character
        private void Next()
        {
            if(this.currentIndex != this.input.Length)
            {
                this.currentIndex = this.currentIndex + 1;
            }
        }

        public List<string> GenerateTokens()
        {
            
            char currentCharacter = this.CurrentChar();
            while (!currentCharacter.Equals('\0'))
            {
                currentCharacter = this.CurrentChar();
                /*
                if (this.input.Contains("="))
                {
                    string[] temp_string = this.input.Split("=");
                    bool is_digit;
                    double variableValue;
                    if (temp_string.Length == 2)
                    {
                        is_digit = Double.TryParse(temp_string[1], out variableValue);
                        if(Regex.IsMatch(temp_string[0], "^[a-zA-Z0-9 ]"))
                        {
                            Console.WriteLine("Varable Name can only contain alphanumeric characters and not a special character");
                            return new List<string>();

                        }
                        if(is_digit==false)
                        { 
                            Console.WriteLine("Invalid Variable Value");
                            return new List<string>();

                        }
                        if (reservedWords.Contains(temp_string[0]))
                        {
                            Console.WriteLine("Invalid Varaible: Variable name cannot be a reserved word");
                            return new List<string>();
                        }
                        

                    }

                }*/
                if (WHITESPACE.Contains(currentCharacter))
                {
                    this.Next();
                }
                else if (Char.IsDigit(currentCharacter) | currentCharacter.Equals('.'))
                {
                    this.tokens.Add(T1.number.ToString() + ":" + GenerateNumbers().Trim());
                    this.Next();
                }
                else if (currentCharacter.Equals('+'))
                {
                    this.tokens.Add(T1.plus.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('-'))
                {
                    this.tokens.Add(T1.subtract.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('*'))
                {
                    this.tokens.Add(T1.multiplication.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('/'))
                {
                    this.tokens.Add(T1.division.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('('))
                {
                    this.tokens.Add(T1.leftParenthesis.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals(')'))
                {
                    this.tokens.Add(T1.rightParenthesis.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('%'))
                {
                    this.tokens.Add(T1.percentage.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('^'))
                {
                    this.tokens.Add(T1.expo.ToString());
                    this.Next();
                }
                else if (currentCharacter.Equals('\n'))
                {
                    this.Next();
                }
                else if (currentCharacter.Equals('\0'))
                {
                    break;
                }
                
                else
                {


                    return new List<string>();

                }

            }
      
            return this.tokens;
        }

        public void createTokenList(Char currentChar)
        {

        }

        //Generate numbers with multiple digits 
        public string GenerateNumbers()
        {   int decimalPointCnt = 0;
            var numberStr = this.CurrentChar().ToString();
            this.Next();
            var currentChar = this.CurrentChar();

            while (!currentChar.Equals(' ') & (currentChar.Equals('.') | Char.IsDigit(currentChar)))
            {
                
                if (currentChar.Equals('.'))
                {
                    decimalPointCnt += 1;
                }
                if (decimalPointCnt > 1)
                    break;
                numberStr += currentChar;
                
                this.Next();
                currentChar = this.CurrentChar();
            }
            if (numberStr.StartsWith('.'))
                numberStr = "0" + numberStr;
            if (numberStr.EndsWith('.'))
                numberStr = numberStr + "0";
// Console.WriteLine(this.CurrentChar());
           //Decrement current index by 1 to avoid 2222+ scenario 
            this.currentIndex = this.currentIndex - 1;
            return numberStr;
        }
    }
}

/*
 Notes:

1)Why did not we go ahead with dictionary - It was rasing duplicate key error as one dictionary cannot have multiple similar keys.
 */ 