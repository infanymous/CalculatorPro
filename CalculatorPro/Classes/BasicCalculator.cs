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

        public double Solve()
        {
            int digitsNumber = 0;
            double result=0;
            double x = 0, y = 0;
            

            int[] priorities = Decipher();

            for (int i = 0; i < priorities.Length; i++)
            {
                if (priorities[i]==3 || priorities[i]==4) //check for * or / expression (highest priority)
                {
                    int j = i - 1; //if we find we can check for length of number used in expression (from the left thus i-1)
                    while (priorities[j] == 0 || priorities[j]==5) //getting whole number on left from * (or /)
                    {
                        digitsNumber++;
                        priorities[j] = -1;
                        j--;
                        
                    }
                    x = Double.Parse(expression.Substring(i-digitsNumber,digitsNumber));

                    j = i + 1;
                    digitsNumber = 0;
                    while (priorities[j] == 0 || priorities[j] == 5) //getting whole number on right from * (or /)
                    {
                        digitsNumber++;
                        priorities[j] = -1;
                        j++;
                    }
                    x = Double.Parse(expression.Substring(i + 1, digitsNumber));

                    if (priorities[i] == 3) result = x * y;
                    if (priorities[i] == 4) result = x / y;//dodac opcje dzielenia przez zero!
                    priorities[i] = -1; //on the end counted expression is signed as "-1"
                    i += digitsNumber; //we dont need to check couple (min. 1) chars cus we know its a number 
                }
            }
            

            return result;
        }

        public int[] Decipher()
        {
            int[] priorities = new int[expression.Length];

            for (int i = 0; i < priorities.Length; i++)
            {
                if ((int)expression[i] >= 48 && (int)expression[i] <= 57)
                {
                    priorities[i] = 0;         //digits are zeroes
                }

                switch ((int)expression[i])
                {
                    case 43:
                        priorities[i] = 1;     // "+" means 1
                        break;
                    case 45:
                        priorities[i] = 2;     // "-" means 2
                        break;
                    case 42:
                        priorities[i] = 3;     // "*" means 3
                        break;
                    case 47:
                        priorities[i] = 4;     // "/" means 4
                        break;
                    case 46:
                        priorities[i] = 5;    // "." means 5
                        break;
                    default:
                        break;
                }

            }

            return priorities;
        }
    }
}
