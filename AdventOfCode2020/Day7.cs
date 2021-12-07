using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day7
    {
        public static void Main(string[] args)
        {
            var dict = new Dictionary<string, List<(string Color, int Count)>>();
            foreach (var line in File.ReadLines("Day7_input"))
            {
                var parsed = ParseLine(line);
                dict[parsed.Color] = parsed.InnerBags;
            }
            Console.WriteLine(dict.Count);
            Dictionary<string, List<string>> reversedDict = new Dictionary<string, List<string>>();
            foreach (var kv in dict)
            {
                foreach (var innerBag in kv.Value)
                {
                    if (!reversedDict.ContainsKey(innerBag.Color))
                        reversedDict[innerBag.Color] = new List<string>();
                    reversedDict[innerBag.Color].Add(kv.Key);
                }
            }
            Console.WriteLine(reversedDict.Count);
            const string bag = "shiny gold";
            HashSet<string> visitedBags = new HashSet<string>();
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(bag);
            while (queue.Count > 0)
            {
                string key = queue.Dequeue();
                visitedBags.Add(key);
                if (!reversedDict.ContainsKey(key)) continue;
                foreach (var container in reversedDict[key])
                {
                    if (!visitedBags.Contains(container))
                        queue.Enqueue(container);
                    visitedBags.Add(container);
                }
            }
            Console.WriteLine($"Day 7 part 1: {visitedBags.Count-1}");

            int CountBags(string bagColor, Dictionary<string, List<(string Color, int Count)>> dict)
            {
                int sum = 0;
                foreach (var innerBag in dict[bagColor])
                    sum += innerBag.Count + innerBag.Count * CountBags(innerBag.Color, dict);
                return sum;
            }
            
            Console.WriteLine($"Day 7 part 2: {CountBags(bag, dict)}");
        }

        private static (string Color, List<(string Color, int Count)> InnerBags) ParseLine(string line)
        {
            string bagPattern = @"(?'color'\w+ \w+) bags contain";
            string innerBagsPattern = @"((?'count'\d+) (?'color'\w+ \w+))+?";
            string noInnerPattern = "no other bags";
            List<(string Color, int Count)> list = new List<(string Color, int Count)>();
            string color = Regex.Match(line, bagPattern).Groups["color"].Value;
            if (!Regex.IsMatch(line, noInnerPattern))
            {
                foreach (Match match in Regex.Matches(line, innerBagsPattern))
                {
                    list.Add((match.Groups["color"].Value, int.Parse(match.Groups["count"].Value)));
                }               
            }
            return (color, list);
        }
    }
}