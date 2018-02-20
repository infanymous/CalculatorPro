using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorPro.Classes
{
    public class BasicCalculator
    {
        string expression;

        public string Expression { get { return expression; } set { expression = value; } }

        public BasicCalculator (string expression)
        {
            Expression = expression;
        }

        public Queue<string> ConvertToONP()
        {

            int digitsNumber = 0;

            Stack<string> Symbols = new Stack<string>();
            Queue<string> Output = new Queue<string>();

            for (int i = 0; i < expression.Length; i++)
            {
                int j = i;
                while (((int)expression[j] >= 48 && (int)expression[j] <= 57) || (int)expression[j] == 46) //if first symbol is a number then we check for the whole number (e.g. we read 3, so we check if its not 35.5)
                {
                    digitsNumber++;
                    if (j < expression.Length - 1)
                    {
                        j++;
                    }
                    else
                    {
                        break;
                    }

                }
                if (digitsNumber > 0) Output.Enqueue(expression.Substring(i, digitsNumber)); //save read number in queue
                if (i + digitsNumber <= expression.Length) i += digitsNumber;
                digitsNumber = 0;

                if(i<expression.Length)
                switch ((int)expression[i])// >= 40 && expression[j] <= 47 && expression[j] != 46 && expression[j] != 44)
                {
                    case 40: // (
                        Symbols.Push(expression.Substring(i, 1));
                        break;

                    case 41: // )
                        do
                        {
                            Output.Enqueue(Symbols.Pop());
                        } while (Symbols.Peek() != "(");
                        Symbols.Pop();
                        break;

                    case 42: // *
                        while (Symbols.Any() && Symbols.Peek() == "/")
                        {
                            Output.Enqueue(Symbols.Pop());
                        }
                        Symbols.Push(expression.Substring(i, 1));
                        break;

                    case 43: // +
                    case 45: // - 
                        while (Symbols.Any() && Symbols.Peek() != "(")
                        {
                            Output.Enqueue(Symbols.Pop());
                        }
                        Symbols.Push(expression.Substring(i, 1));
                        break;
                        
                    case 47: // / 
                        while (Symbols.Any() && Symbols.Peek() == "*" && Symbols.Peek() != "(")
                        {
                            Output.Enqueue(Symbols.Pop());
                        }
                        Symbols.Push(expression.Substring(i, 1));
                        break;

                    default:
                        break;
                }
            }
            while (Symbols.Any())
            {
                Output.Enqueue(Symbols.Pop());
            }
            return Output;
        }

        public string CountONP()
        {
            Queue<string> Symbols = ConvertToONP();
            Stack<double> Numbers = new Stack<double>();

            foreach (string item in Symbols)
            {
                if ((int)item[0] >= 48 && (int)item[0] <= 57)
                {
                    Numbers.Push(Double.Parse(item));
                }
                else
                {
                    double a;
                    switch ((int)item[0])
                    {
                        case 42: // *
                            a = Numbers.Pop();
                            Numbers.Push(Numbers.Pop() * a);
                            break;

                        case 43: // +
                            a = Numbers.Pop();
                            Numbers.Push(Numbers.Pop() + a);
                            break;

                        case 45: // -
                            a = Numbers.Pop();
                            Numbers.Push(Numbers.Pop() - a);
                            break;

                        case 47: // / 
                            a = Numbers.Pop();
                            Numbers.Push(Numbers.Pop() / a);
                            break;

                        default:
                            break;

                    }
                }
            }
            return Numbers.Pop().ToString();
        }
    }
}
