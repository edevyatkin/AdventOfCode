using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day6
    {
        public static void Main(string[] args)
        {
            string[] groups = File.ReadAllText("Day6_input").Split("\n\n");
            int result = 0;
            foreach (string g in groups)
            {
                HashSet<char> hashSet = new HashSet<char>();
                foreach (string s in g.Split('\n'))
                    hashSet.UnionWith(s);
                result += hashSet.Count;
            }
            Console.WriteLine($"Day 6 part 1: {result}");

            result = 0;
            foreach (string g in groups)
            {
                HashSet<char> hashSet = new HashSet<char>();
                for (char c = 'a'; c <= 'z'; c++)
                    hashSet.Add(c);
                foreach (string s in g.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                    hashSet.IntersectWith(s);
                result += hashSet.Count;
            }
            Console.WriteLine($"Day 6 part 2: {result}");
        }
    }
}