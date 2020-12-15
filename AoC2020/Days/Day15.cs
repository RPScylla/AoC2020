using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    class Day15
    {

        Dictionary<int, (int prevspoken, int lastspoken)> _spoken = new Dictionary<int, (int, int)>();
        int _lastNumber = 0;
        bool _newNumber = true;
        int _round = 0;

        public Day15()
        {
            int[] input = File.ReadAllText("Days/Input/Day15.txt").Split(',').Select(int.Parse).ToArray();

            for (; _round < input.Length; _round++)
            {
                _newNumber = AddToDict(input[_round]);
                _lastNumber = input[_round];
            }

            for (; _round < 2020; _round++)
            {
                ProcessRound();
            }

            int partA = _lastNumber;
            Console.WriteLine($"Part A: {partA}");

            for (; _round < 30000000; _round++)
            {
                ProcessRound();
            }

            int partB = _lastNumber;
            Console.WriteLine($"Part B: {partB}");

        }

        private void ProcessRound()
        {
            if (_newNumber)
            {
                _newNumber = AddToDict(0);
                _lastNumber = 0;
            }
            else
            {
                int number = _spoken[_lastNumber].lastspoken - _spoken[_lastNumber].prevspoken;
                _newNumber = AddToDict(number);
                _lastNumber = number;
            }
        }

        public bool AddToDict(int number)
        {
            if (!_spoken.ContainsKey(number))
            {
                _spoken.Add(number, (0, _round + 1));
                return true;
            }

            _spoken[number] = (_spoken[number].lastspoken, _round + 1);
            return false;
        }
    }
}
