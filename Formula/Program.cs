using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Formula
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ConvertFormula("(22*2)+50"));
            Console.WriteLine(ConvertFormula("((2*3+12)/2)"));
            Console.WriteLine(ConvertFormula("(10-5+3/2*2)"));

            Console.ReadLine();
        }

        public static double ConvertFormula(String formula)
        {
            Stack<String> stackFormula = new Stack<String>();
            string temp = "";
            double result = 0;
            
            for (int i = 0; i < formula.Length; i++)
            {
                String s = formula.Substring(i, 1);
                char tempChar = s[0];

                if (!char.IsNumber(tempChar) && !String.IsNullOrEmpty(temp))
                {
                    stackFormula.Push(temp);
                    temp = "";
                }

                if (s == "(")
                {
                    string subFormula = "";
                    i++; 
                    int bracketNum = 0;
                    for (; i < formula.Length; i++)
                    {
                        s = formula.Substring(i, 1);

                        if (s == "(")
                            bracketNum++;

                        if (s == ")")
                        {
                            if (bracketNum == 0)
                                break;
                            else
                                bracketNum--;
                        }

                        subFormula += s;
                    }

                    stackFormula.Push(ConvertFormula(subFormula).ToString());

                }
                else if (s == "+" || s == "-")
                    stackFormula.Push(s);
                else if (s == "/" || s == "*")
                    stackFormula.Push(s);
                else if (char.IsNumber(tempChar))
                {
                    temp += s;
                    if (i == (formula.Length - 1))
                        stackFormula.Push(temp);
                }
            }
            
            while (stackFormula.Count >= 3)
            {
                double rightValue = Convert.ToDouble(stackFormula.Pop());
                string oper = stackFormula.Pop();
                double leftValue = Convert.ToDouble(stackFormula.Pop());

                if (oper == "+")
                    result = leftValue + rightValue;
                else if (oper == "-")
                    result = leftValue - rightValue;
                else if (oper == "/")
                    result = leftValue / rightValue;
                else if (oper == "*")
                    result = leftValue * rightValue;
                
                stackFormula.Push(result.ToString());
            }

            return Convert.ToDouble(stackFormula.Pop());
        }
    }
}
