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
    class Day11
    {
        private const char FREE = 'L';
        private const char OCCUPIED = '#';
        private const char FLOOR = '.';

        public Day11()
        {
            char[][] input = File.ReadAllLines("Days/Input/Day11.txt").Select(line => line.ToCharArray()).ToArray();

            int partA = LoopSeats(input, false);

            input = File.ReadAllLines("Days/Input/Day11.txt").Select(line => line.ToCharArray()).ToArray();

            int partB = LoopSeats(input, true);

            Console.WriteLine($"Part A: {partA}");
            Console.WriteLine($"Part B: {partB}");
        }

        public int LoopSeats(char[][] input, bool skipFloor)
        {
            while (true)
            {
                var applied = ApplyRules(input, skipFloor);
                input = applied.seats;
                if (applied.changes == 0)
                {
                    break;
                }
            }

            int occupied = 0;

            foreach (char[] row in input)
            {
                foreach (char seat in row)
                {
                    if (seat == OCCUPIED)
                    {
                        occupied++;
                    }
                }
            }

            return occupied;
        }

        public (char[][] seats, int changes) ApplyRules(char[][] seats, bool skipFloor)
        {
            char[][] seatconfig = new char[seats.Length][];
            for (int i = 0; i < seats.Length; i++)
            {
                seatconfig[i] = new char[seats[i].Length];
                seats[i].CopyTo(seatconfig[i], 0);
            }

            int changes = 0;

            foreach (int y in Enumerable.Range(0, seats.Length))
            {
                foreach (int x in Enumerable.Range(0, seats[y].Length))
                {
                    int adjacent = GetAdjacentOccupied(seats, x, y, skipFloor);

                    if (seats[y][x] == FREE && adjacent == 0)
                    {
                        seatconfig[y][x] = OCCUPIED;
                        changes++;
                    }
                    else if (seats[y][x] == OCCUPIED && adjacent >= (skipFloor ? 5: 4))
                    {
                        seatconfig[y][x] = FREE;
                        changes++;
                    }
                }
            }

            return (seatconfig, changes);
        }

        public int GetAdjacentOccupied(char[][] seats, int x, int y, bool skipFloor)
        {
            int occupied = 0;
            int[,] directions = new[,] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dy = directions[i, 0];
                int dx = directions[i, 1];
                int m = 1;
                bool found = false;
                while (!found)
                {
                    if (y + dy * m >= 0 &&
                        y + dy * m < seats.Length &&
                        x + dx * m >= 0 &&
                        x + dx * m < seats[y + dy * m].Length)
                    {
                        char curSeat = seats[y + dy * m][x + dx * m];
                        switch (curSeat)
                        {
                            case OCCUPIED:
                                occupied++;
                                found = true;
                                break;
                            case FLOOR:
                                m++;
                                found = !skipFloor;
                                break;
                            case FREE:
                                found = true;
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return occupied;
        }

    }
}
