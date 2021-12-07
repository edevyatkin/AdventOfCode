using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020 {
    public class Day19 {
        public static void Main(string[] args) {
            var data = File.ReadAllText("Day19_input").Split("\n\n");

            var rules = data[0].Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(rule => rule.Split(": ", StringSplitOptions.RemoveEmptyEntries)).ToList()
                .ToDictionary(kvp => kvp[0], kvp => kvp[1]);
            
            var messages = data[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var messagesHs = messages.ToHashSet();
            var matchedMessages = new List<string>();
            var rulesCache = new Dictionary<string, HashSet<string>>();
            foreach (var str in CollapseRule(rules["0"], rules, rulesCache)) {
                if (messagesHs.Contains(str)) {
                    messagesHs.Remove(str);
                    matchedMessages.Add(str);
                }
            }

            Console.WriteLine($"Day 19 part 1: {matchedMessages.Count}");
        }

        private static IEnumerable<string> CollapseRule(string ruleStr, Dictionary<string, string> rules,
            Dictionary<string, HashSet<string>> rulesCache) {
            if (Regex.IsMatch(ruleStr, "\"(?'letter'.*)\"")) {
                var collapseRule = ruleStr.Substring(1, ruleStr.Length - 2);
                yield return collapseRule;
            }

            if (Regex.IsMatch(ruleStr, @"^(?'num'\d+)$")) {
                if (!rulesCache.ContainsKey(ruleStr)) {
                    foreach (var str in CollapseRule(rules[ruleStr], rules, rulesCache)) {
                        if (!rulesCache.ContainsKey(ruleStr)) {
                            rulesCache[ruleStr] = new HashSet<string>();
                        }
                        rulesCache[ruleStr].Add(str);
                    }                   
                }
                foreach (var cachedStr in rulesCache[ruleStr]) {
                    yield return cachedStr;
                }
            }

            if (ruleStr.Contains(" | ")) {
                var orParts = ruleStr.Split(" | ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var leftPart in CollapseRule(orParts[0], rules, rulesCache)) {
                    foreach (var rightPart in CollapseRule(orParts[1], rules, rulesCache)) {
                        yield return leftPart;
                        yield return rightPart;
                    }
                }
            } else if (ruleStr.Contains(' ')) {
                int spaceIndex = ruleStr.IndexOf(' ');
                var leftStr = ruleStr[..spaceIndex];
                var rightStr = ruleStr[(spaceIndex+1)..];
                foreach (var leftPart in CollapseRule(leftStr, rules, rulesCache)) {
                    foreach (var rightPart in CollapseRule(rightStr, rules, rulesCache)) {
                        yield return string.Concat(leftPart, rightPart);
                    }
                }
            }
        }
    }
}