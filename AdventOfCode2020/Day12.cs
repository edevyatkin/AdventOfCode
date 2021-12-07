using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    public class Day12
    {
        public static void Main(string[] args)
        {
            var counts = new Dictionary<char, int>()
            {
                ['E'] = 0,
                ['W'] = 0,
                ['N'] = 0,
                ['S'] = 0
            };
            var currDir = 'E';
            var mirrorDict = new Dictionary<char, char>()
            {
                ['N'] = 'S',
                ['E'] = 'W',
                ['S'] = 'N',
                ['W'] = 'E'
            };

            foreach (var line in File.ReadLines("Day12_input"))
            {
                char direction = line[0];
                int value = int.Parse(line.Substring(1));
                switch (direction)
                {
                    case 'F':
                        counts[currDir] += value;
                        break;
                    case 'R':
                    case 'L':
                        int grad = (direction == 'R') ? value : 360 - value;
                        while (grad > 0)
                        {
                            currDir = currDir switch
                            {
                                'N' => 'E',
                                'E' => 'S',
                                'S' => 'W',
                                'W' => 'N',
                                _ => currDir
                            };
                            grad -= 90;
                        }

                        break;
                    case 'N':
                    case 'E':
                    case 'W':
                    case 'S':
                        counts[direction] += value;
                        break;
                }
            }

            Console.WriteLine(
                $"Day 12 part 1: {Math.Abs(counts['N'] - counts['S']) + Math.Abs(counts['E'] - counts['W'])}");

            counts = new Dictionary<char, int>()
            {
                ['E'] = 0,
                ['W'] = 0,
                ['N'] = 0,
                ['S'] = 0
            };
            var wpDirX = 'E';
            var wpValX = 10;
            var wpDirY = 'N';
            var wpValY = 1;
            foreach (var line in File.ReadLines("Day12_input"))
            {
                char direction = line[0];
                int value = int.Parse(line.Substring(1));
                switch (direction)
                {
                    case 'F':
                        while (value > 0)
                        {
                            counts[wpDirX] += wpValX;
                            counts[wpDirY] += wpValY;
                            value--;
                        }
                        break;
                    case 'R':
                    case 'L':
                        int grad = (direction == 'R') ? value : 360 - value;
                        while (grad > 0)
                        {
                            wpDirX = wpDirX switch
                            {
                                'N' => 'E',
                                'E' => 'S',
                                'S' => 'W',
                                'W' => 'N',
                                _ => wpDirX
                            };
                            wpDirY = wpDirY switch
                            {
                                'N' => 'E',
                                'E' => 'S',
                                'S' => 'W',
                                'W' => 'N',
                                _ => wpDirY
                            };
                            grad -= 90;
                        }

                        break;
                    case 'N':
                    case 'E':
                    case 'W':
                    case 'S':
                        if (direction == wpDirX)
                        {
                            wpValX += value;
                        }
                        else if (direction == wpDirY)
                        {
                            wpValY += value;
                        }
                        else if (direction == mirrorDict[wpDirX])
                        {
                            if (wpValX < value)
                                wpDirX = mirrorDict[wpDirX];
                            wpValX = Math.Abs(wpValX - value);
                        }
                        else if (direction == mirrorDict[wpDirY])
                        {
                            if (wpValY < value)
                                wpDirY = mirrorDict[wpDirY];
                            wpValY = Math.Abs(wpValY - value);
                        }
                        break;
                }
            }

            Console.WriteLine(
                $"Day 12 part 2: {Math.Abs(counts['N'] - counts['S']) + Math.Abs(counts['E'] - counts['W'])}");
        }
    }
}