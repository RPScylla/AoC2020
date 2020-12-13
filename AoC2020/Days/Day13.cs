using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2020.Days
{
    class Day13
    {
        public Day13()
        {
            string[] input = File.ReadAllLines("Days/Input/Day13.txt").ToArray();

            int partA = SolveA(input);
            Console.WriteLine($"Part A: {partA}");

            ulong partB = SolveB(input[1]);
            Console.WriteLine($"Part B: {partB}");
        }

        int SolveA(string[] input)
        {
            int startTimestamp = int.Parse(input[0]);
            int[] busses = input[1].Split(',').Where(x => !x.Equals("x")).Select(int.Parse).ToArray();

            int waitTime = int.MaxValue;
            int busnumber = 0;
            foreach (int bus in busses)
            {
                int waitTimeBus = 0;
                while ((startTimestamp + waitTimeBus) % bus != 0)
                {
                    waitTimeBus++;
                }

                if (waitTimeBus < waitTime)
                {
                    waitTime = waitTimeBus;
                    busnumber = bus;
                }
            }

            return busnumber * waitTime;
        }

        ulong SolveB(string input)
        {
            Dictionary<uint, ulong> busses = input.Split(',')
                .Select((x, i) => (i: (ulong) i, x))
                .Where(x => x.x != "x").ToDictionary(x => uint.Parse(x.x), x => x.i);

            string wolframAlphaQuery = String.Join(", ", busses.Select(x => $"(t + {x.Value}) mod {x.Key} = 0"));
            Console.WriteLine("Super slow c# solution below, for faster answer use Wolfram Alpha");
            Console.WriteLine($"https://www.wolframalpha.com/input/?i={HttpUtility.UrlEncode(wolframAlphaQuery)}");

            ulong t = 0;

            while (!CheckBussesAtT(t, busses))
            {
                t++;
            }


            return t;
        }

        bool CheckBussesAtT(ulong t, Dictionary<uint, ulong> busses)
        {
            foreach (var bus in busses)
            {
                if ((t + bus.Value) % bus.Key != 0)
                    return false;
            }

            return true;
        }
    }
}
