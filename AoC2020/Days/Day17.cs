using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Instrumentation;
using System.Web.UI.WebControls;

namespace AoC2020.Days
{
    class Day17
    {
        object _lock = new object();

        public Day17()
        {

            string[] input = File.ReadAllLines("Days/Input/Day17.txt");

            Cube cube = ParseInput(input);

            InitActiveAroundDeltaList(false);
            for (int i = 0; i < 6; i++)
            {
                ParseRound(cube, false);
            }

            int partA = cube.GetAllActive().Count();
            Console.WriteLine($"Part A: {partA}");


            cube = ParseInput(input);
            InitActiveAroundDeltaList(true);
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"starting round {i}");
                ParseRound(cube, true);
            }

            int partB = cube.GetAllActive().Count();
            Console.WriteLine($"Part B: {partB}");
        }

        
        void ParseRound(Cube cube, bool includeW)
        {
            var actives = cube.GetAllActive().ToArray();

            int minX = int.MaxValue;
            int maxX = int.MinValue;

            int minY = int.MaxValue;
            int maxY = int.MinValue;

            int minZ = int.MaxValue;
            int maxZ = int.MinValue;

            int minW = int.MaxValue;
            int maxW = int.MinValue;

            foreach (var area in actives)
            {
                minX = Math.Min(area.x, minX);
                maxX = Math.Max(area.x, maxX);
                minY = Math.Min(area.y, minY);
                maxY = Math.Max(area.y, maxY);
                minZ = Math.Min(area.z, minZ);
                maxZ = Math.Max(area.z, maxZ);
                minW = Math.Min(area.w, minW);
                maxW = Math.Max(area.w, maxW);
            }

            if (!includeW)
            {
                minW = 1;
                maxW = 0;
            }

            Parallel.For(minX - 1, maxX + 2, (int x) =>
            {
                for (int y = minY - 1; y <= maxY + 1; y++)
                    for (int z = minZ - 1; z <= maxZ + 1; z++)
                        for (int w = minW - 1; w <= maxW + 1; w++)
                        {
                            int around = GetActiveAround(x, y, z, w, actives);
                            lock (_lock)
                            {
                                if (actives.Contains((x, y, z, w)))
                                {
                                    cube.Set(x, y, z, w, (around == 2 || around == 3));
                                }
                                else
                                {
                                    cube.Set(x, y, z, w, (around == 3));
                                }
                            }
                        }
            });


        }

        Cube ParseInput(string[] input)
        {
            Cube cube = new Cube();

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    cube.Set(x, y, 0, 0, input[y][x] == '#');
                }
            }

            return cube;
        }

        public int GetActiveAround(int x, int y, int z, int w, IEnumerable<(int x, int y, int z, int w)> actives)
        {
            return _checkDeltas.Count(d => actives.Contains((x + d.dx, y + d.dy, z + d.dz, w + d.dw)));
        }

        private List<(int dx, int dy, int dz, int dw)> _checkDeltas;
        public void InitActiveAroundDeltaList(bool includeW)
        {
            _checkDeltas = new List<(int dx, int dy, int dz, int dw)>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        for (int w = (includeW ? -1 : 0); w <= (includeW ? 1 : 0); w++)
                        {
                            if (x != 0 || y != 0 || z != 0 || w != 0)
                            {
                                _checkDeltas.Add((x, y, z, w));
                            }
                        }
                    }
                }
            }
        }
        class Cube
        {
            Dictionary<(int x, int y, int z, int w), bool> cube = new Dictionary<(int x, int y, int z, int w), bool>();

            public void Set(int x, int y, int z, int w, bool value)
            {
                if (!cube.ContainsKey((x, y, z, w)))
                    cube.Add((x, y, z, w), false);

                cube[(x, y, z, w)] = value;
            }

            public IEnumerable<(int x, int y, int z, int w)> GetAllActive()
            {
                return cube.Where(area => area.Value).Select(area => area.Key);
            }

        }



    }
}
