using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace AoC2020.Days
{
    class Day16
    {
        delegate void LineParser(string line);


        Dictionary<string, Range[]> rules = new Dictionary<string, Range[]>();
        List<int[]> tickets = new List<int[]>();
        Regex re = new Regex(@"(?:(\d+)-(\d+))", RegexOptions.Compiled);

        public Day16()
        {
            string[] input = File.ReadAllLines("Days/Input/Day16.txt");

            LineParser parser = ParseRule;

            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("your ticket:") || line.StartsWith("nearby tickets:"))
                {
                    parser = TicketParser;
                    continue;
                }

                parser(line);
            }

            int partA = SolveA();
            Console.WriteLine($"Part A: {partA}");

            ulong partB = SolveB();
            Console.WriteLine($"Part B: {partB}");
        }

        public int SolveA()
        {
            int ans = 0;
            for (int i = 1; i < tickets.Count; i++)
            {
                int[] ticket = tickets[i];
                bool valid = true;
                foreach (int value in ticket)
                {
                    if (!rules.Any(rule => rule.Value.Any(range => range.InRange(value))))
                    {
                        valid = false;
                        ans += value;
                        break;
                    }
                }

                if (!valid)
                {
                    tickets.RemoveAt(i);
                    i--;
                }
            }

            return ans;
        }

        public ulong SolveB()
        {
            Dictionary<string, HashSet<int>> possibilities = new Dictionary<string, HashSet<int>>();
            foreach (KeyValuePair<string, Range[]> rule in rules)
            {
                HashSet<int> options = new HashSet<int>();
                for (int i = 0; i < rules.Count; i++)
                {
                    options.Add(i);
                }
                possibilities.Add(rule.Key, options);
            }


            foreach (KeyValuePair<string, HashSet<int>> possibility in possibilities)
            {
                for (int i = 0; i < possibility.Value.Count; i++)
                {
                    int index = possibility.Value.ElementAt(i);

                    if (!tickets.All(ticket => rules[possibility.Key].Any(range => range.InRange(ticket[index]))))
                    {
                        possibility.Value.Remove(index);
                        i--;
                    }

                }
            }

            HashSet<int> freeLocations = new HashSet<int>();
            for (int i = 0; i < possibilities.Count; i++)
            {
                freeLocations.Add(i);
            }

            
            foreach (KeyValuePair<string, HashSet<int>> possibility in possibilities.OrderBy(p => p.Value.Count))
            {
                possibility.Value.RemoveWhere(location => !freeLocations.Contains(location));

                if (possibility.Value.Count == 1)
                {
                    freeLocations.Remove(possibility.Value.First());
                }
            }


            ulong sum = 1;
            foreach (KeyValuePair<string, HashSet<int>> possibility in possibilities)
            {
                if (possibility.Key.StartsWith("departure"))
                {
                    sum *= (uint)tickets[0][possibility.Value.First()];
                }
            }

            return sum;
        }

        public void ParseRule(string line)
        {
            MatchCollection matches = re.Matches(line);
            string name = line.Substring(0, line.IndexOf(':'));
            Range[] ranges = new Range[matches.Count];
            for (var i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                ranges[i] = new Range(int.Parse(match.Groups[1].ToString()), int.Parse(match.Groups[2].ToString()));
            }
            rules.Add(name, ranges);
        }

        public void TicketParser(string line)
        {
            int[] values = line.Split(',').Select(int.Parse).ToArray();
            tickets.Add(values);
        }

        struct Range
        {
            private int start;
            private int end;

            public Range(int a, int b)
            {
                start = Math.Min(a, b);
                end = Math.Max(a, b);
            }

            public bool InRange(int x)
            {
                return x >= start && x <= end;
            }
        }
    }
}
