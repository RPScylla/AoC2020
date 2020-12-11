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
    class Day10
    {
        public Day10()
        {
            List<int> input = File.ReadAllLines("Days/Input/Day10.txt").Select(int.Parse).ToList();
            input.Add(0);
            input.Sort();
            input.Add(input.Last() + 3);

            int partA = SolveA(input);
            ulong partB = SolveB(input);

            Console.WriteLine($"Part A: {partA}");
            Console.WriteLine($"Part A: {partB}");

        }

        int SolveA(List<int> input)
        {
            int jolts = 0;
            int diff1 = 0;
            int diff3 = 0;
            foreach (int jolt in input)
            {
                int diff = jolt - jolts;
                if (diff == 1)
                {
                    diff1++;
                }
                else if (diff == 3)
                {
                    diff3++;
                }

                jolts = jolt;
            }

            return diff1 * diff3;
        }

        ulong SolveB(List<int> input)
        {
            input.Reverse();
            Dictionary<int, TreeNode> treeIndex = new Dictionary<int, TreeNode>();

            TreeNode TreeBase = new TreeNode(input[0]);
            treeIndex.Add(input[0], TreeBase);

            foreach (int jolt in input.Skip(1))
            {
                TreeNode node = new TreeNode(jolt);
                treeIndex.Add(jolt, node);

                foreach(int i in Enumerable.Range(1,3))
                {
                    if (treeIndex.ContainsKey(jolt + i))
                    {
                        treeIndex[jolt+i].Children.Add(treeIndex[jolt]);
                    }
                }

            }

            return PathToZero(TreeBase);
        }

        public Dictionary<int, ulong> CachedTreeChilds = new Dictionary<int, ulong>();
        ulong PathToZero(TreeNode node)
        {
            if (CachedTreeChilds.ContainsKey(node.Value))
            {
                return CachedTreeChilds[node.Value];
            }

            if (node.Value == 0)
            {
                CachedTreeChilds.Add(node.Value, 1);
                return 1;
            }

            ulong sum = 0;
            foreach (TreeNode child in node.Children)
            {
                sum += PathToZero(child);
            }
            CachedTreeChilds.Add(node.Value, sum);
            return sum;
        }

        class TreeNode
        {
            public int Value { get; set; }
            public List<TreeNode> Children { get; set; }

            public TreeNode(int value)
            {
                Value = value;
                Children = new List<TreeNode>();
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

    }
}
