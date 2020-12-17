using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoC2020.Days;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            new Day17();
            stopwatch.Stop();
            Console.WriteLine($"Total Time: {stopwatch.ElapsedMilliseconds}ms");
            Console.ReadKey();
        }
    }
}
