using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day10
    {
        public static void Main(string[] args)
        {
            List<int> numbers = File.ReadAllLines("Day10_input").Select(int.Parse).ToList();
            numbers = numbers.Prepend(0).ToList();
            numbers.Sort();
            int[] diffs = new int[3];
            for (int i = 1; i < numbers.Count; i++)
                diffs[numbers[i] - numbers[i - 1] - 1]++;
            diffs[2] += 1;
            Console.WriteLine(diffs[0] * diffs[2]);

            long[] dp = new long[numbers.Count];
            dp[0] = 1;
            for (int i = 1; i < numbers.Count; i++)
            {
                int j = i - 1;
                while (j >= 0 && numbers[i] - numbers[j] <= 3)
                {
                    dp[i] += dp[j];
                    j--;
                }
            }
            Console.WriteLine(dp[^1]);
        }
    }
}