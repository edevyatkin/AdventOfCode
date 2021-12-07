using System;
using System.IO;

namespace AdventOfCode2020
{
    public class Day3
    {
        public static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("Day3_input");

            int trees = 0;
            int i = 0;
            int j = 0;
            int rows = input.Length;
            int cols = input[0].Length;
            int right = 1;
            int down = 2;
            while (i < rows) {
                if (input[i][j] == '#')
                    trees++;
                i = i + down;
                j = (j + right) % cols;
            }
            Console.WriteLine(trees);
        }
    }
}