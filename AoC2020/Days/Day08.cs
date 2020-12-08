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
    class Day08
    {
        public Day08()
        {
            string[] input = File.ReadAllLines("Days/Input/Day08.txt");
            int partA = RunComputer(input).acc;
            Console.WriteLine($"Part A: {partA}");

            int partB = FindNonStuck(input);
            Console.WriteLine($"Part B: {partB}");         
            
        }

        public (bool loop, int acc) RunComputer(IEnumerable<string> instructions)
        {
            int acc = 0;
            int pointer = 0;
            HashSet<int> history = new HashSet<int>();

            while(pointer < instructions.Count())
            {
                if (history.Contains(pointer))
                {
                    return (true, acc);
                }

                history.Add(pointer);

                string[] parts = instructions.ElementAt(pointer).Split(' ');
                int arg = int.Parse(parts[1]);

                switch(parts[0])
                {
                    case "acc":
                        acc += arg;
                        pointer += 1;
                        break;
                    case "jmp":
                        pointer += arg;
                        break;
                    case "nop":
                        pointer += 1;
                        break;
                }
            }

            return (false, acc);
        }

        public int FindNonStuck(string[] instructions)
        {
            string[] copyInstructions = new string[instructions.Length];
            for (int i = 0; i < instructions.Length; i++)
            {
                instructions.CopyTo(copyInstructions, 0);
                string instruction = instructions[i];
                switch (instruction.Substring(0, 3))
                {
                    case "acc":
                        continue;
                    case "jmp":
                        instruction = instruction.Replace("jmp", "nop");
                        copyInstructions[i] = instruction;
                        break;
                    case "nop":
                        instruction = instruction.Replace("nop", "jmp");
                        copyInstructions[i] = instruction;
                        break;
                }

                var runResult = RunComputer(copyInstructions);
                if (!runResult.loop)
                    return runResult.acc;
            }

            return -1;
        }
    }
}
