using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021 {
    public class Day10 {
        public static async Task Main(string[] args) {
            var input = await AocHelper.FetchInputAsync(2021, 10);
            
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
            Console.WriteLine(result1);
            
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

            Console.WriteLine(result2);
        }
    }
}