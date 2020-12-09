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
    class Day09
    {
        const int PREAMBLE_SIZE = 25;

        public Day09()
        {
            ulong[] input = File.ReadAllLines("Days/Input/Day09.txt").Select(ulong.Parse).ToArray();
            Queue<ulong> XMAS = new Queue<ulong>(PREAMBLE_SIZE);

            ulong partA = 0;
            foreach (ulong stream in input)
            {
                // Preamble
                if (XMAS.Count() < PREAMBLE_SIZE)
                {
                    XMAS.Enqueue(stream);
                    continue;
                }

                if (MatchExists(stream, XMAS))
                {
                    XMAS.Dequeue();
                    XMAS.Enqueue(stream);
                    continue;
                }

                partA = stream;
                break;
            }


            ulong partB = FindWeakness(partA, input);

            Console.WriteLine($"Part A: {partA}");
            Console.WriteLine($"Part A: {partB}");

        }

        public bool MatchExists(ulong number, Queue<ulong> XMAS)
        {
            for (int i = 0; i < PREAMBLE_SIZE - 1; i++)
            {
                for (int j = i + 1; j < PREAMBLE_SIZE; j++)
                {
                    if (XMAS.ElementAt(i) + XMAS.ElementAt(j) == number)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public ulong FindWeakness(ulong target, ulong[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 1; j <= input.Length - i; j++)
                {
                    IEnumerable<ulong> range = input.Skip(i).Take(j);
                    if (range.Aggregate((a, b) => a + b) == target)
                    {
                        return range.Min() + range.Max();
                    }
                }
            }

            return 0;
        }
    }
}
