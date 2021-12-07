using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day15
    {
        static int[] input = new int[] { 7, 14, 0, 17, 11, 1, 2 };
        //static int[] input = new int[] { 0, 3, 6 };

        public static void Main(string[] args)
        {
            //var input = new int[] { 7, 14, 0, 17, 11, 1, 2 };
            var input = new int[] { 0, 3, 6 };
            //Console.WriteLine($"Day 15 part 1: {Calculate(10)}");
            Console.WriteLine($"Day 15 part 1: {Calculate(2020)}");
            Console.WriteLine($"Day 15 part 2: {Calculate(30000000)}");
        }

        public static int Calculate(int stopTime)
        {
            var spokenDict = input.ToDictionary(x => x, x => Array.IndexOf(input, x) + 1);
            Console.WriteLine(string.Join(' ', spokenDict.Keys));
            int time = input.Length+2;
            int spoken = 0;
            int toSpeak = 0;
            while (time <= stopTime)
            {
                toSpeak = (spokenDict.ContainsKey(spoken) == true) ? (time - spokenDict[spoken] - 1) : 0;
                spokenDict[toSpeak] = ++time;
                spoken = toSpeak;
                //time++;
            }
            return spoken;
        }
    }
}
