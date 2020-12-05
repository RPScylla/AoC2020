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
    class Day05
    {
        public Day05()
        {
            string[] input = File.ReadAllLines("Days/Input/Day05.txt");
            HashSet<int> ids = new HashSet<int>();

            foreach (string pass in input)
            {
                ids.Add(DecodePass(pass));
            }

            Console.WriteLine($"Part A: {ids.Max()}");

            int seat = FindBetween(ids);
            
            Console.WriteLine($"Part B: {seat}");
        }

        public int DecodePass(string pass)
        {
            int rowLower = 0;
            int rowUpper = 127;
            int colLower = 0;
            int colUpper = 7;

            foreach (char step in pass)
            {
                int rowDiff = ((rowUpper + 1) - rowLower) / 2;
                int colDiff = ((colUpper + 1) - colLower) / 2;

                switch (step)
                {
                    case 'F':
                        rowUpper -= rowDiff;
                        break;
                    case 'B':
                        rowLower += rowDiff;
                        break;
                    case 'R':
                        colLower += colDiff;
                        break;
                    case 'L':
                        colUpper -= colDiff;
                        break;
                }
            }

            return rowLower * 8 + colLower;
        }

        public int FindBetween(HashSet<int> ids)
        {
            foreach (int id in ids)
            {
                if (ids.Contains(id - 2) && !ids.Contains(id - 1))
                {
                    return id - 1;
                }

                if (ids.Contains(id + 2) && !ids.Contains(id + 1))
                {
                    return id + 1;
                }
            }

            return -1;
        }
    }
}
