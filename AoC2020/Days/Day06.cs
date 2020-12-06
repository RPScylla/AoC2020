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
    class Day06
    {
        public Day06()
        {
            string[] input = File.ReadAllLines("Days/Input/Day06.txt");

            int partA = 0;
            int partB = 0;
            
            List<char> partAList = new List<char>();
            List<char> partBList = new List<char>();

            bool first = true;

            foreach (string answer in input)
            {
                if (String.IsNullOrEmpty(answer))
                {
                    partA += partAList.Count();
                    partB += partBList.Count();

                    partAList.Clear();
                    partBList.Clear();
                    first = true;
                    continue;
                }

                List<char> tempList = answer.ToList();

                if (first)
                {
                    partAList.AddRange(tempList);
                    partBList.AddRange(tempList);
                    first = false;
                }
                else
                {
                    partAList = partAList.Union(tempList).Distinct().ToList();
                    partBList = partBList.Intersect(tempList).ToList();
                }
            }

            partA += partAList.Count();
            partB += partBList.Count();

            Console.WriteLine($"Part A: {partA}");
            Console.WriteLine($"Part B: {partB}");

        }
    }
}
