using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2020
{
    public class Day9
    {
        public static void Main(string[] args)
        {
            int index = 0;
            var dict = new Dictionary<long, int>();
            var list = new LinkedList<long>();
            long target = 0;
            List<long> numbers = File.ReadAllLines("Day9_input").Select(long.Parse).ToList();
            foreach (long n in numbers)
            {
                //long n = long.Parse(line);
                if (index < 25)
                {
                    dict[n] = 1;
                    list.AddLast(n);
                    index++;
                }
                else
                {
                    bool found = false;
                    foreach (long key in dict.Keys)
                    {
                        if (key > n)
                            continue;
                        if (dict.ContainsKey(n - key) && (n - key != key))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"Day 9 part 1: {n}");
                        target = n;
                        break;
                    }

                    if (dict.ContainsKey(n))
                        dict[n]++;
                    else
                        dict[n] = 1;
                    list.AddLast(n);
                    
                    if (dict[list.First.Value] == 1)
                        dict.Remove(list.First.Value);
                    else
                        dict[list.First.Value]--;
                    list.RemoveFirst();
                }
            }

            long sum = 0;
            list.Clear();
            foreach (long n in numbers)
            {
                while (list.Count > 0 && sum > target)
                {
                    sum -= list.First.Value;
                    list.RemoveFirst();
                }
                if (sum == target)
                {
                    var min = list.Min();
                    var max = list.Max();
                    Console.WriteLine($"Day 9 part 2: {min}+{max}={min + max}");
                    break;
                }
                sum += n;
                list.AddLast(n);
            }
        }
    }
}