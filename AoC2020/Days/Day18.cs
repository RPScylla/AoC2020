using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Instrumentation;
using System.Web.UI.WebControls;

namespace AoC2020.Days
{
    class Day18
    {
        public Day18()
        {
            string[] input = File.ReadAllLines("Days/Input/Day18.txt");

            long partA = 0;
            long partB = 0;

            foreach (string expression in input)
            {
                var result = Parse(expression);
                partA += result.A;
                partB += result.B;
            }

            Console.WriteLine($"Part A: {partA}");
            Console.WriteLine($"Part B: {partB}");

        }

        public (long A, long B) Parse(string sum)
        {
            Stack<StringBuilder> opsA = new Stack<StringBuilder>();
            StringBuilder sbA = new StringBuilder();
            Stack<StringBuilder> opsB = new Stack<StringBuilder>();
            StringBuilder sbB = new StringBuilder();

            foreach (char c in sum)
            {
                switch (c)
                {
                    case '(':
                        opsA.Push(sbA);
                        opsB.Push(sbB);
                        sbA = new StringBuilder();
                        sbB = new StringBuilder();
                        break;
                    case ')':
                        string ansA = CalculateA(sbA.ToString()).ToString();
                        string ansB = CalculateB(sbB.ToString()).ToString();
                        sbA = opsA.Pop();
                        sbB = opsB.Pop();
                        sbA.Append(ansA);
                        sbB.Append(ansB);
                        break;
                    case '+':
                    case '*':
                    default:
                        sbA.Append(c);
                        sbB.Append(c);
                        break;
                }
            }

            long a = CalculateA(sbA.ToString());
            long b = CalculateB(sbB.ToString());
            return (a, b);
        }

        public long CalculateA(string expression)
        {
            string[] c = expression.Split(' ');
            long sum = long.Parse(c[0]);
            for (int i = 2; i < c.Length; i = i + 2)
            {
                int to = int.Parse(c[i]);
                switch (c[i - 1])
                {
                    case "+":
                        sum += to;
                        break;
                    case "*":
                        sum *= to;
                        break;
                }
            }
            return sum;
        }

        public long CalculateB(string expression)
        {
            string[] subExpressions = expression.Replace(" ", "").Split('*');
            long sum = 1;

            foreach (string c in subExpressions)
            {
                sum *= c.Split('+').Select(long.Parse).Sum();
            }
            
            return sum;
        }
    }
}
