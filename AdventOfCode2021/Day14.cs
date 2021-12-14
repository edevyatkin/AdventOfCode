using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace AdventOfCode2021 {
    public class Day14 {
        public static async Task Main(string[] args) {
            var input = await AocHelper.FetchInputAsync(2021, 14);
            
            // PART 1 && PART 2
            
            var str = input[0];
            var counts = new long[26];
            var pairs = new Dictionary<(char, char), long>();
            for (var i = 0; i < str.Length-1; i++) {
                char c1 = str[i], c2 = str[i + 1];
                counts[c1 - 'A']++;
                if (!pairs.ContainsKey((c1, c2)))
                    pairs[(c1, c2)] = 0;
                pairs[(c1, c2)]++;
            }

            counts[str[^1] - 'A']++;
            
            var rules = new Dictionary<(char, char), char>();
            for (int i = 2; i < input.Length; i++) {
                var inst = input[i].Split(" -> ").ToArray();
                var pair = inst[0];
                var insCh = inst[1][0];
                rules[(pair[0], pair[1])] = insCh;
            }

            int step = 1;
            long result1 = 0;
            while (step <= 40) {
                var newPairs = new Dictionary<(char, char), long>();
                foreach (var kv in pairs) {
                    newPairs[kv.Key] = newPairs.GetValueOrDefault(kv.Key, 0) + kv.Value;
                    if (rules.ContainsKey(kv.Key) && kv.Value > 0) {
                        var fp = (kv.Key.Item1, rules[kv.Key]);
                        var sp = (rules[kv.Key], kv.Key.Item2);
                        var count = kv.Value;
                        if (!newPairs.ContainsKey(fp))
                            newPairs[fp] = 0;
                        newPairs[fp] += count;
                        if (!newPairs.ContainsKey(sp))
                            newPairs[sp] = 0;
                        newPairs[sp] += count;
                        counts[rules[kv.Key] - 'A'] += count;
                        newPairs[kv.Key] -= count;
                    }
                }

                if (step == 10) {
                    result1 = GetResult();
                }
                pairs = newPairs;
                step++;
            }
            
            Console.WriteLine(result1);

            long result2 = GetResult();
            
            Console.WriteLine(result2);

            long GetResult() {
                long min = counts.Where(x => x > 0).Min();
                long max = counts.Max();
                return max - min;
            }
        }
    }
}