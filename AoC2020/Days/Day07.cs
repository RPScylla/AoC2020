using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Days
{
    class Day07
    {
        public Day07()
        {
            string[] input = File.ReadAllLines("Days/Input/Day07.txt");

            string target = "shiny gold";

            Regex regex = new Regex(@"(.*?) bags contain(?: (\d+ .*?) bag(?:s)?[,.])*", RegexOptions.Compiled);

            IEnumerable<Match> matches = input.Select(line => regex.Match(line));

            Dictionary<string, Dictionary<string, int>> bags = matches.ToDictionary(match => match.Groups[1].Value, match =>
            {
                Dictionary<string, int> subDictionary = new Dictionary<string, int>();
                foreach (Capture capture in match.Groups[2].Captures)
                {
                    string[] parts = capture.Value.Split(new []{' '}, 2);
                    subDictionary.Add(parts[1], int.Parse(parts[0]));
                }

                return subDictionary;
            });


            bool RecursiveCanContain(string color)
            {
                return bags[color].ContainsKey(target) || bags[color].Keys.Any(RecursiveCanContain);
            }

            int partA = bags.Keys.Count(RecursiveCanContain);
            Console.WriteLine(partA);

            int RecursiveMustContain(string color)
            {
                return bags[color].Sum(subBag => subBag.Value * (1 + RecursiveMustContain(subBag.Key)));
            }

            int partB = RecursiveMustContain(target);
            Console.WriteLine(partB);
        }
    }
}
