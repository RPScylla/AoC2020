using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Days
{
    class Day02
    {
        public Day02()
        {
            string[] lines = File.ReadAllLines("Days/Input/Day02.txt");

            int validPartA = 0;
            int validPartB = 0;

            Regex regex = new Regex(@"(\d+)-(\d+) (.): (.*)", RegexOptions.Compiled);

            foreach (string line in lines)
            {
                Match match = regex.Match(line);

                int lower = int.Parse(match.Groups[1].Value);
                int upper = int.Parse(match.Groups[2].Value);

                char needle = match.Groups[3].Value[0];
                string haystack = match.Groups[4].Value;

                int count = haystack.Count(c => c.Equals(needle));

                if (count >= lower && count <= upper)
                {
                    validPartA++;
                }

                char lowerc = haystack[lower - 1];
                char upperc = haystack[upper - 1];


                if (lowerc != upperc && (lowerc == needle || upperc == needle))
                {
                    validPartB++;
                }
            }

            Console.WriteLine(validPartA);
            Console.WriteLine(validPartB);

        }
    }
}
