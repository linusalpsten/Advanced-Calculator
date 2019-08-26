using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advanced_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string calculation = "((20+10)*4)+3*4+(12*29)";
            Console.WriteLine(Calculate(calculation).ToString());
        }

        static float Calculate(string calculation)
        {
            int lastindex = 0;
            int openParenthesisCount = 0;
            string simplified = "";
            List<string> testlist = new List<string>();

            for (int i = 0; i < calculation.Length; i++)
            {
                char current = calculation[i];
                if (current == '(')
                {
                    if (openParenthesisCount == 0)
                    {
                        if (i != lastindex && i != lastindex + 1)
                        {
                            simplified = new string(simplified.Concat(calculation.Substring(lastindex, i - lastindex)).ToArray());
                            //testlist.Add(calculation.Substring(lastindex + 1, i - lastindex - 1));
                        }
                        lastindex = i;
                    }
                    openParenthesisCount++;
                }
                else if (current == ')')
                {
                    openParenthesisCount--;
                    if (openParenthesisCount == 0)
                    {
                        simplified = new string(simplified.Concat(Calculate(calculation.Substring(lastindex + 1, i - lastindex - 1)).ToString()).ToArray());
                        //testlist.Add(calculation.Substring(lastindex + 1, i - lastindex - 1));
                        lastindex = i + 1;
                    }
                }
            }

            simplified = new string(simplified.Concat(calculation.Substring(lastindex, calculation.Length - lastindex)).ToArray());

            Console.WriteLine(simplified);
            var result = CalculateSimple(simplified);
            return result;
        }

        static float CalculateSimple(string simplified)
        {
            Regex tester = new Regex(@"(\d+|[+\-*\/])");
            var matches = tester.Matches(simplified).Cast<Match>().Select(m => m.Value).ToArray();
            while (matches.Length > 1)
            {
                bool matchesChanged = false;
                for (int i = 0; i < matches.Length; i++)
                {
                    if (matches[i] == "*")
                    {
                        matches = matches.Take(i - 1).ToArray().Concat(matches.Skip(i + 2).ToArray().Prepend((Convert.ToDouble(matches[i - 1]) * Convert.ToDouble(matches[i + 1])).ToString())).ToArray();
                        matchesChanged = true;
                        break;
                    }
                }

                if (matchesChanged)
                {
                    continue;
                }
                for (int i = 0; i < matches.Length; i++)
                {
                    if (matches[i] == "/")
                    {
                        matches = matches.Take(i - 1).ToArray().Concat(matches.Skip(i + 2).ToArray().Prepend((Convert.ToDouble(matches[i - 1]) / Convert.ToDouble(matches[i + 1])).ToString())).ToArray();
                        matchesChanged = true;
                        break;
                    }
                }

                if (matchesChanged)
                {
                    continue;
                }
                for (int i = 0; i < matches.Length; i++)
                {
                    if (matches[i] == "+")
                    {
                        matches = matches.Take(i - 1).ToArray().Concat(matches.Skip(i + 2).ToArray().Prepend((Convert.ToDouble(matches[i - 1]) + Convert.ToDouble(matches[i + 1])).ToString())).ToArray();
                        matchesChanged = true;
                        break;
                    }
                }

                if (matchesChanged)
                {
                    continue;
                }
                for (int i = 0; i < matches.Length; i++)
                {
                    if (matches[i] == "-")
                    {
                        matches = matches.Take(i - 1).ToArray().Concat(matches.Skip(i + 2).ToArray().Prepend((Convert.ToDouble(matches[i - 1]) - Convert.ToDouble(matches[i + 1])).ToString())).ToArray();
                        matchesChanged = true;
                        break;
                    }
                }
            }
            return (float)Convert.ToDouble(matches[0]);
        }
    }
}
