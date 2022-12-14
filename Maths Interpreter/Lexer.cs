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
        private char[] WHITESPACE = { ' ', '\n', '\t', };
        private string[] reservedWords = { "if", "while", "for", "foreach", "switch", "break", "continue", "else" ,"plot" };
        //private char[] specialCharaters = { '(', '@', ')', '-', '+','*','/','%','' };
        private int equalityCheck = 0,plotCheck=0,plotChecked=0;
        Tokens T1 = new Tokens();
        List<string> tokens = new List<string>();
        public string[] temp_string;
        bool isValidExp;
        public Lexer(string input)
        {
            this.input = input;
            plotCheck = 0;
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
            if (this.currentIndex != this.input.Length)
            {
                this.currentIndex = this.currentIndex + 1;
            }
           
        }

        
        /*
        public void checkPlotFunc()
        {
            if (this.input.Contains("plot"))
            {

                this.temp_string = this.input.Split("=");
                if (this.temp_string.Length == 2)
                {


                    this.input = this.temp_string[1];
                }
                else
                {

                }
            }

        }
        */
        public List<string> GenerateTokens()
        {

            char currentCharacter = this.CurrentChar();
           
            while (!currentCharacter.Equals('\0'))
            {
                currentCharacter = this.CurrentChar();
                //Check for variable  assignment
                if (this.input.Contains("->") && equalityCheck == 0)
                {



                        
                        equalityCheck = 1;
                        string[] temp_string = this.input.Split("->",2);
                        bool is_digit;
                        double variableValue;
                        if (temp_string.Length == 2)
                        {
                            
                        is_digit = Double.TryParse(temp_string[1], out variableValue);
                        /*
                        if (!Regex.IsMatch(temp_string[0], "[a-zA-Z0-9_]"))//check for special character in a string
                            {
                            //Console.WriteLine("Varable Name can only contain alphanumeric characters and not a special character");
                            tokens.Add("Invalid Varaible: Varable Name can only contain alphanumeric characters and not a special character");
                            return tokens;
                        }
                        */
                        if (temp_string[0].Trim().Any(ch => !char.IsLetterOrDigit(ch)))
                        {
                            tokens.Add("Invalid Varaible: Varable Name can only contain alphanumeric characters and not a special character");
                            return tokens;
                        }
                            if (is_digit == false)//if we assign expression to a variable
                            {
                                //Console.WriteLine("Invalid Variable Value");
                                //return new List<string>();
                                this.input = temp_string[1].Trim();
                                tokens.Add(T1.variable + ":" + temp_string[0].Trim());
                                tokens.Add(T1.assignment);
                                continue;

                            }
                            if (reservedWords.Contains(temp_string[0].Trim()))//check for  reserved words
                            {
                            //Console.WriteLine("Invalid Varaible: Variable name cannot be a reserved word");
                            tokens.Add("Invalid Varaible: Variable name cannot be a reserved word");
                            return tokens;
                        }
                            
                            if (Char.IsDigit(temp_string[0][0]))
                            {
                                //Console.WriteLine("Invalid Varaible: Variable name cannot begin with a number");
                            tokens.Add("Invalid Varaible: Variable name cannot begin with a number");
                            return tokens;
                            }
                            tokens.Add(T1.variable + ":" + temp_string[0].Trim());
                            tokens.Add(T1.assignment);
                            tokens.Add(T1.number + ":" + temp_string[1]);
                            //Lexer.variablesStored.Add(temp_string[0], temp_string[1]);
                            break;
                        }
                    }
                
                //Checking if request came for plot or not
                /*
                else if(this.input.Contains("plot") && plotCheck == 0 && plotChecked == 0)
                {
                    plotChecked = 1; // toexecute this if block only once
                    plotCheck = 1;
                    string varPlotString = "";
                    varPlotString += this.CurrentChar();
                    this.Next();
                    int countLetters = 1;
                    while(countLetters !=4)
                    {
                        countLetters += 1;
                        varPlotString += this.CurrentChar();
                        this.Next();
                    }
                    if(!varPlotString.Equals("plot") &&(!this.CurrentChar().Equals('(') || !this.CurrentChar().Equals(' ')))
                    {
                        tokens.Clear();
                        tokens.Add("Undefined Token:" + this.input);
                        break;
                    }
                    tokens.Add(T1.plot.ToString());
                }
                */
                else if(currentCharacter.Equals('<'))
                {
                    this.Next();
                    Char currentchar = this.CurrentChar();
                    if(currentchar.Equals('='))
                    {
                        this.tokens.Add(T1.lessThanOrEqual.ToString());
                        this.Next();
                        continue;
                    }
                    /*
                    else if (WHITESPACE.Contains(currentCharacter))
                    {
                        this.Next();
                        currentchar = this.CurrentChar();
                        if(currentchar.Equals('='))
                        {
                            this.tokens.Add(T1.lessThanOrEqual.ToString());
                            this.Next();
                            continue;
                        }
                    }
                    */
                        this.tokens.Add(T1.lessThan.ToString());
                        //this.Next();

                    

                }
                else if (currentCharacter.Equals('>'))
                {
                    this.Next();
                    Char currentchar = this.CurrentChar();
                    if (currentchar.Equals('='))
                    {
                        this.tokens.Add(T1.greaterThanOrEqual.ToString());
                        this.Next();
                        continue;
                    }
                    /*
                    else if (WHITESPACE.Contains(currentCharacter))
                    {
                        this.Next();
                        currentchar = this.CurrentChar();
                        if (currentchar.Equals('='))
                        {
                            this.tokens.Add(T1.greaterThanOrEqual.ToString());
                            this.Next();
                            continue;
                        }
                    }
                    */
                    this.tokens.Add(T1.greaterThan.ToString());
                    //this.Next();
                }
                else if (currentCharacter.Equals('='))
                {
                    this.Next();
                    Char currentchar = this.CurrentChar();
                    if (currentchar.Equals('='))
                    {
                        this.tokens.Add(T1.equality.ToString());
                        this.Next();
                        continue;
                    }
                    /*
                    else if (WHITESPACE.Contains(currentCharacter))
                    {
                        this.Next();
                        currentchar = this.CurrentChar();
                        if (currentchar.Equals(T1.assignment))
                        {
                            this.tokens.Add(T1.equality.ToString());
                            this.Next();
                            continue;
                        }
                    }
                    */
                }
                /*
                else if(this.CurrentChar().Equals(',') && plotChecked == 1)
                {
                    this.tokens.Add(T1.seperator.ToString());
                    this.Next();
                }
                */
                else if (WHITESPACE.Contains(currentCharacter))
                {
                    this.Next();
                }
                else if (Char.IsDigit(currentCharacter) || currentCharacter.Equals('.'))
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
                /*
                else if(Interpreter.variablesStored.ContainsKey(this.input))//check if variable exist or not 
                {
                    tokens.Add(T1.variable + ":" + this.input);
                    break;
                }
                */
                else if ((Char.IsLetter(currentCharacter)  || currentCharacter.Equals('_')) && plotCheck == 0)//check if variable exist or not 
                { string variableStr = this.CurrentChar().ToString();
                    this.Next();
                    Char currentChar = this.CurrentChar();
                    while (!currentChar.Equals(' ') & (( Char.IsLetterOrDigit(currentChar)) || currentChar.Equals('_')))
                        {
                        variableStr = variableStr + currentChar;
                        this.Next();
                        currentChar = this.CurrentChar();
                    }
                    //this.currentIndex = this.currentIndex - 1;
                    if(Interpreter.variablesStored.ContainsKey(variableStr))
                    {
                        tokens.Add(T1.variable + ":" + variableStr);
                    }
                    else
                    {
                        tokens.Clear();
                        tokens.Add("Undefined Token:" + variableStr);
                        break;
                    }
                }
                /*
                else if(Char.IsLetter(currentCharacter) && plotCheck == 1)
                {
                    plotCheck = 0;
                    tokens.Add(T1.variable + ":" + currentCharacter);
                    this.Next();
                }
                */
                else
                {
                    tokens.Clear();
                    tokens.Add("Undefined Token:" + this.CurrentChar());

                    

                }

            }

            return this.tokens;
        }



        //Generate numbers with multiple digits 
        public string GenerateNumbers()
            { int decimalPointCnt = 0;
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