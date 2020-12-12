using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2020.Days
{
    class Day12
    {
        enum Directions
        {
            East = 0,
            South = 1,
            West = 2,
            North = 3
        };

        public Day12()
        {
            string[] input = File.ReadAllLines("Days/Input/Day12.txt").ToArray();

            int partA = SolveA(input);
            int partB = SolveB(input);
            Console.WriteLine($"Part A {partA}");
            Console.WriteLine($"Part B {partB}");
        }

        public int SolveA(string[] commands)
        {
            Directions direction = Directions.East;
            int x = 0;
            int y = 0;

            foreach (string line in commands)
            {
                char command = line[0];
                int arg = int.Parse(line.Substring(1));

                if (command == 'F')
                {
                    command = direction.ToString()[0];
                }

                switch (command)
                {
                    case 'N':
                        y += arg;
                        break;
                    case 'S':
                        y -= arg;
                        break;
                    case 'E':
                        x += arg;
                        break;
                    case 'W':
                        x -= arg;
                        break;
                    case 'L':
                        direction = (Directions)(((int)direction + (4 - (arg / 90))) % 4);
                        break;
                    case 'R':
                        direction = (Directions)(((int)direction + (arg / 90)) % 4);
                        break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        public int SolveB(string[] commands)
        {
            int x = 0;
            int y = 0;

            int dx = 10;
            int dy = 1;

            foreach (string line in commands)
            {
                char command = line[0];
                int arg = int.Parse(line.Substring(1));

                switch (command)
                {
                    case 'F':
                        x += (dx * arg);
                        y += (dy * arg);
                        break;
                    case 'N':
                        dy += arg;
                        break;
                    case 'S':
                        dy -= arg;
                        break;
                    case 'E':
                        dx += arg;
                        break;
                    case 'W':
                        dx -= arg;
                        break;
                    case 'L':
                        for (int i = 0; i < arg / 90; i++)
                        {
                            int dxt = dx;
                            int dyt = dy;
                            dx = -dyt;
                            dy = dxt;
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < arg / 90; i++)
                        {
                            int dxt = dx;
                            int dyt = dy;
                            dx = dyt;
                            dy = -dxt;
                        }
                        break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }
    }
}
