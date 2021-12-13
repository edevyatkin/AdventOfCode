using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021 {
    public class Day13 {
        public static async Task Main(string[] args) {
            var input = await AocHelper.FetchInputAsync(2021, 13);
            
            // PART 1
            int result1 = 0;
            bool folds = false;
            var dots = new HashSet<(int, int)>();
            int foldsCount = 0;
            foreach (var s in input) {
                if (s == string.Empty) {
                    folds = true;
                    continue;
                }
                if (!folds) {
                    var sp = s.Split(',').Select(int.Parse).ToArray();
                    int i = sp[0], j = sp[1];
                    dots.Add((i, j));
                }
                else {
                    var sp = s.Split(' ')[2].Split('=').ToArray();
                    string direction = sp[0];
                    int line = int.Parse(sp[1]);
                    dots = Fold(dots, direction, line);
                    foldsCount++;
                    if (foldsCount == 1) {
                        result1 = dots.Count;
                    }
                }
            }
            Console.WriteLine(result1);

            // PART 2
            int maxI = dots.Max(d => d.Item2);
            int maxJ = dots.Max(d => d.Item1);
            for (int i = 0; i <= maxI; i++) {
                var ds = dots.Where(x => x.Item2 == i).Select(d => d.Item1).ToList();
                var line = new StringBuilder();
                for (int j = 0; j <= maxJ; j++) {
                    line.Append(ds.Contains(j) ? '#' : ' ');
                }
                Console.WriteLine(line);
            }
        }

        private static HashSet<(int,int)> Fold(HashSet<(int,int)> dots, string direction, int line) {
            var newDots = new HashSet<(int, int)>();
            foreach (var dot in dots) {
                if (direction == "x") {
                    if (dot.Item1 <= line) {
                        newDots.Add(dot);
                    } else {
                        newDots.Add((dot.Item1 - (dot.Item1-line) * 2, dot.Item2));
                    }
                }
                else {
                    if (dot.Item2 <= line) {
                        newDots.Add(dot);
                    } else {
                        newDots.Add((dot.Item1, dot.Item2 - (dot.Item2-line) * 2));
                    }
                }
            }
            return newDots;
        }
    }
}