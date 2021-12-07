using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day1
    {
        public static void Main(string[] args)
        {
            int[] input = File.ReadAllLines("Day1_input").Select(int.Parse).ToArray();

            // Part 1
            Utils.AddPart1Header();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < input.Length; i++)
            {
                int complement = 2020 - input[i];
                if (dict.ContainsKey(complement))
                {
                    Console.WriteLine($"{input[dict[complement]]}+{input[i]}=2020");
                    Console.WriteLine(input[dict[complement]] * input[i]);
                    break;
                }

                dict[input[i]] = i;
            }

            // Part 2
            Utils.AddPart2Header();
            for (int i = 0; i < input.Length - 2; i++)
            {
                for (int j = i + 1; j < input.Length - 1; j++)
                {
                    for (int k = j + 1; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            Console.WriteLine($"{input[i]}+{input[j]}+{input[k]}=2020");
                            Console.WriteLine(input[i] * input[j] * input[k]);
                            break;
                        }
                    }
                }
            }
        }
    }
}