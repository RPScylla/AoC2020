using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Days
{
    class Day03
    {
        public Day03()
        {
            string[] lines = File.ReadAllLines("Days/Input/Day03.txt");

            uint r1d1 = Traverse(lines, 1, 1, '#');
            uint r3d1 = Traverse(lines, 3, 1, '#');
            uint r5d1 = Traverse(lines, 5, 1, '#');
            uint r7d1 = Traverse(lines, 7, 1, '#');
            uint r1d2 = Traverse(lines, 1, 2, '#');

            Console.WriteLine($"Part 1 {r3d1}");
            Console.WriteLine($"Part 2 {r1d1 * r3d1 * r5d1 * r7d1 * r1d2}");


        }

        public uint Traverse(string[] map, int xoff, int yoff, char hit)
        {
            int maxX = map[0].Length;
            int x = 0;
            int y = 0;

            uint hits = 0;

            while (true)
            {
                y += yoff;
                x = (x + xoff) % maxX;

                if (y >= map.Length)
                    break;

                if (map[y][x] == hit)
                    hits++;
            }

            return hits;
        }
    }
}
