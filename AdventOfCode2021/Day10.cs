using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,10)]
public class Day10 : IAocDay {
    public AocDayResult Solve(string[] input)
    {
        // PART 1
        int result1 = 0;
        var part2Lines = new List<char[]>();
        var dict = new Dictionary<char, (char Parenthesis, int Points)> {
            [')'] = ('(', 3),
            [']'] = ('[', 57),
            ['}'] = ('{', 1197),
            ['>'] = ('<', 25137)
        };
        foreach (var line in input) {
            var stack = new Stack<char>();
            int points = 0;
            foreach (var c in line) {
                if (!dict.ContainsKey(c)) {
                    stack.Push(c);
                    continue;                        
                }
                if (stack.Count == 0 || stack.Peek() != dict[c].Parenthesis) {
                    points += dict[c].Points;
                    break;
                }
                stack.Pop();
            }

            result1 += points;
            
            if (points == 0) {
                part2Lines.Add(stack.ToArray());
            }
        }
        
        // PART 2
        long result2 = 0;
        var scores = new List<long>();
        foreach (var line in part2Lines) {
            long score = 0;
            for (var i = 0; i < line.Length; i++) {
                score = score * 5 + line[i] switch {
                    '(' => 1,
                    '[' => 2,
                    '{' => 3,
                    '<' => 4,
                    _ => 0
                };
            }
            scores.Add(score);
        }

        int middle = scores.Count / 2;
        var sorted = scores.OrderBy(s => s).ToArray();
        result2 = sorted[middle];

        return new AocDayResult(result1, result2);
    }
}