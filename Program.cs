using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string calculation = "((20+10)*4)+3*4+(12*29)";
            int lastindex = 0;
            bool inParenthesis = false;
            int openParenthesisCount = 0;
            List<string> segments = new List<string>();
            for (int i = 0; i < calculation.Length; i++)
            {
                char current = calculation[i];
                if (current == '(')
                {
                    if (openParenthesisCount == 0)
                    {
                        if (i != lastindex && i != lastindex+1)
                        {
                            segments.Add(calculation.Substring(lastindex, i - lastindex - 1));
                        }
                        lastindex = i;
                    }
                    inParenthesis = true;
                    openParenthesisCount++;
                }
                else if (current == ')')
                {
                    openParenthesisCount--;
                    if (openParenthesisCount == 0)
                    {
                        inParenthesis = false;
                        segments.Add(calculation.Substring(lastindex, i - lastindex+1));
                        lastindex = i;
                    }
                }
            }
            Console.WriteLine(calculation);
            foreach (var item in segments)
            {
                Console.WriteLine(item);
            }
        }
    }
}
