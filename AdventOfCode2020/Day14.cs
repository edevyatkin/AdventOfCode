using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day14
    {
        public static void Main(string[] args)
        {
            var maskP = @"mask = (?'mask'.+)";
            var writeP = @"mem\[(?'addr'\d+)\] = (?'value'\d+)";
            var input = File.ReadAllLines("Day14_input");
            long wOnes = 0, wZeroes = 0, mainMask = 0;
            var memory = new Dictionary<long, long>();
            foreach (var s in input)
            {
                if (Regex.IsMatch(s, maskP))
                {
                    wZeroes = 0;
                    wOnes = 0;
                    var maskMatch = Regex.Match(s, maskP);
                    var maskStr = maskMatch.Groups["mask"].Value;
                    foreach (var ch in maskStr)
                    {
                        if ((ch) == 'X')
                        {
                            wOnes |= 1;
                        }
                        else
                        {
                            wOnes |= (ch - '0') & 1;
                            wZeroes |= (ch - '0') & 1;
                        }
                        wOnes <<= 1;
                        wZeroes <<= 1;
                    }

                    wOnes >>= 1;
                    wZeroes >>= 1;
                    mainMask = wOnes ^ wZeroes;
                }
                else
                {
                    var writeMatch = Regex.Match(s, writeP);
                    var writeAddr = long.Parse(writeMatch.Groups["addr"].Value);                   
                    var writeValue = long.Parse(writeMatch.Groups["value"].Value);
                    var newValue = (writeValue & wOnes) | wZeroes;
                    memory[writeAddr] = newValue; 
                }
            }

            Console.WriteLine($"Day 14 part 1: {memory.Values.Sum()}");
            
            memory.Clear(); 
            foreach (var s in input)
            {
                if (Regex.IsMatch(s, maskP))
                {
                    wZeroes = 0;
                    wOnes = 0;
                    var maskMatch = Regex.Match(s, maskP);
                    var maskStr = maskMatch.Groups["mask"].Value;
                    foreach (var ch in maskStr)
                    {
                        if ((ch) == 'X')
                        {
                            wOnes |= 1;
                        }
                        else
                        {
                            wOnes |= (ch - '0') & 1;
                            wZeroes |= (ch - '0') & 1;
                        }
                        wOnes <<= 1;
                        wZeroes <<= 1;
                    }

                    wOnes >>= 1;
                    wZeroes >>= 1;
                    mainMask = wOnes ^ wZeroes;
                }
                else
                {
                    var writeMatch = Regex.Match(s, writeP);
                    var writeAddr = long.Parse(writeMatch.Groups["addr"].Value);                   
                    var writeValue = long.Parse(writeMatch.Groups["value"].Value);
                    Dfs(memory, writeAddr, writeValue, mainMask, 0, wZeroes);
                }
            }
            
            Console.WriteLine($"Day 14 part 2: {memory.Values.Sum()}");
        }

        private static void Dfs(Dictionary<long, long> memory, long writeAddr, in long writeValue, in long mainMask, int i, long wz)
        {
            if (i >= 36)
            {
                memory[writeAddr] = writeValue;
                return;
            }
            
            if ((((long) 1 << i) & mainMask) > 0)
            {
                Dfs(memory, writeAddr | ((long)1 << i), writeValue, mainMask, i+1, wz);
                Dfs(memory, writeAddr & ~((long)1 << i), writeValue, mainMask, i+1, wz);
            }
            else
            {
                if ((((long) 1 << i) & wz) > 0)
                    writeAddr |= (long) 1 << i; 
                Dfs(memory, writeAddr, writeValue, mainMask, i+1, wz);
            }
        }
    }
}