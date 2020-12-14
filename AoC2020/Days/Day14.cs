using System;
using System.Collections;
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
using Microsoft.Win32;

namespace AoC2020.Days
{
    class Day14
    {
        string _mask = "";
        Dictionary<ulong, ulong> _memA = new Dictionary<ulong, ulong>();
        Dictionary<ulong, ulong> _memB = new Dictionary<ulong, ulong>();

        public Day14()
        {
            string[] input = File.ReadAllLines("Days/Input/Day14.txt").ToArray();

            Regex regex = new Regex(@"(?:(?:mem\[(\d+)\])|mask) = ([0-9X]+)", RegexOptions.Compiled);
            foreach (string line in input)
            {
                Match match = regex.Match(line);
                if (string.IsNullOrEmpty(match.Groups[1].ToString()))
                {
                    // Mask
                    _mask = match.Groups[2].ToString();
                }
                else
                {
                    // Mem
                    ulong address = ulong.Parse(match.Groups[1].ToString());
                    ulong value = ulong.Parse(match.Groups[2].ToString());
                    string valueS = Convert.ToString((long)value, 2);

                    ParseA(address, valueS);
                    ParseB(address, value);
                }
            }


            ulong partA = SumMemory(_memA);
            Console.WriteLine($"Part A: {partA}");

            ulong partB = SumMemory(_memB);
            Console.WriteLine($"Part B: {partB}");
        }

        void ParseA(ulong address, string value)
        {
            char[] valueArray = value.PadLeft(36, '0').ToCharArray();
            for (int i = 0; i < _mask.Length; i++)
            {
                if (_mask[i] != 'X')
                    valueArray[i] = _mask[i];
            }

            WriteToMem(_memA, address, Convert.ToUInt64(string.Join("", valueArray), 2));
        }
        void ParseB(ulong address, ulong value)
        {
            string addressS = Convert.ToString((long)address, 2);
            char[] addressArray = addressS.PadLeft(36, '0').ToCharArray();

            List<int> xindex = new List<int>();

            for (int i = 0; i < _mask.Length; i++)
            {
                if (_mask[i] == 'X')
                    xindex.Add(i);
                else if (_mask[i] == '1')
                    addressArray[i] = _mask[i];
            }

            ulong addressX = Convert.ToUInt64(string.Join("", addressArray), 2);

            int length = xindex.Count;

            foreach (string permutation in PermutateBinary(length))
            {
                for (int i = 0; i < length; i++)
                {
                    switch (permutation[i])
                    {
                        case '1':
                            addressX |= 1ul << (36 - (xindex[i] + 1));
                            break;
                        case '0':
                            addressX &= ~(1ul << (36 - (xindex[i] + 1)));
                            break;
                    }
                }
                WriteToMem(_memB, addressX, value);
            }
        }

        void WriteToMem(Dictionary<ulong, ulong> mem, ulong address, ulong value)
        {
            if (mem.ContainsKey(address))
            {
                mem.Remove(address);
            }
            mem.Add(address, value);
        }

        ulong SumMemory(Dictionary<ulong, ulong> mem)
        {
            ulong sum = 0;

            foreach (KeyValuePair<ulong, ulong> addr in mem)
            {
                sum += addr.Value;
            }

            return sum;
        }


        public static IEnumerable<string> PermutateBinary(int count)
        {
            List<string> permutations = new List<string>();

            int max = 1 << count;
            for (int i = 0; i < max; i++)
            {
                yield return ToBin(i, count);
            }
        }

        public static string ToBin(int value, int len)
        {
            return (len > 1 ? ToBin(value >> 1, len - 1) : null) + "01"[value & 1];
        }
    }
}
