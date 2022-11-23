using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,8)]
public class Day8 : IAocDay {
    public AocDayResult Solve(string[] input)
    {
        // PART 1
        int result1 = 0;
        foreach (var s in input) {
            var data = s.Split(" | ").ToArray();
            result1 += data[1].Split(' ').Count(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7);
        }

        // PART 2
        int result2 = 0;
        foreach (var s in input) {
            var data = s.Split(" | ").ToArray();
            var entries = data[0].Split(' ').OrderBy(str => str.Length).ToArray();
            var values = new Dictionary<int, HashSet<char>> {
                [1] = new(entries[0]),
                [7] = new(entries[1]),
                [4] = new(entries[2])
            };
            for (var index = entries.Length-1; index > 2; index--) {
                var entry = entries[index];
                var hashSet = new HashSet<char>(entry);
                switch (entry.Length) {
                    case 5:
                        if (entry.Intersect(values[6]).Count() == 5) {
                            values[5] = hashSet;
                        } else if (entry.Intersect(values[4]).Intersect(values[1]).Count() == 2) {
                            values[3] = hashSet;
                        } else {
                            values[2] = hashSet;
                        }
                        break;
                    case 6: {
                        if (entry.Intersect(values[1]).Count() != 2) {
                            values[6] = hashSet;
                        }

                        if (entry.Intersect(values[7]).Count() == 3) {
                            if (entry.Intersect(values[4]).Count() == 4)
                                values[9] = hashSet;
                            else {
                                values[0] = hashSet;
                            }
                        }

                        break;
                    }
                    case 7:
                        values[8] = hashSet;
                        break;
                }
            }

            var digits = data[1].Split(' ').ToArray();
            int num = digits.Aggregate(0, (current, number) => current * 10 + values.First(kv => kv.Value.SetEquals(number)).Key);
            result2 += num;
        }

        return new AocDayResult(result1, result2);
    }
}