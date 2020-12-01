using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Days
{
    class Day1
    {
        public Day1()
        {
            string[] lines = File.ReadAllLines("Days/Input/Day1.txt");

            int[] ints = lines.Select(int.Parse).ToArray();

            for (int i = 0; i < ints.Length; i++)
            {
                for (int j = i + 1; j < ints.Length; j++)
                {
                    if (ints[i] + ints[j] == 2020)
                    {
                        Console.WriteLine(ints[i] * ints[j]);
                    }
                }
            }

            for (int i = 0; i < ints.Length; i+=2)
            {
                for (int j = i + 1; j < ints.Length; j++)
                {
                    for (int k = j + 1; k < ints.Length; k++)
                    {
                        if (ints[i] + ints[j] + ints[k] == 2020)
                        {
                            Console.WriteLine(ints[i] * ints[j] * ints[k]);
                        }
                    }
                    
                }
            }

        }
    }
}
