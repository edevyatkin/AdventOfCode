using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var lines = new List<Line>();
        foreach (string s in input) {
            string[] ss = s.Split(" -> ");
            var p1 = ss[0].Split(',').Select(int.Parse).ToArray();
            var p2 = ss[1].Split(',').Select(int.Parse).ToArray();
            var line = new Line { X1 = p1[0], Y1 = p1[1], X2 = p2[0], Y2 = p2[1] };
            lines.Add(line);
        }
        
        // PART 1
        var lines1 = lines.Where(l => l.X1 == l.X2 || l.Y1 == l.Y2).ToList();
        var dict1 = new Dictionary<(int,int),int>();
        foreach (var line in lines1) {
            for (int x = Math.Min(line.X1, line.X2); x <= Math.Max(line.X1, line.X2); x++) {
                for (int y = Math.Min(line.Y1, line.Y2); y <= Math.Max(line.Y1, line.Y2); y++) {
                    if (!dict1.ContainsKey((x, y)))
                        dict1[(x, y)] = 0;
                    dict1[(x, y)]++;
                }
            }
        }

        var result1 = dict1.Count(e => e.Value > 1);

        // PART 2
        var dict2 = new Dictionary<(int,int),int>();
        foreach (var line in lines) {
            int dx = line.X2 - line.X1;
            int dy = line.Y2 - line.Y1;
            foreach (var i in Enumerable.Range(0, Math.Max(Math.Abs(dx), Math.Abs(dy)))) {
                int x = line.X1 + (dx > 0 ? 1 : dx < 0 ? -1 : 0) * i;
                int y = line.Y1 + (dy > 0 ? 1 : dy < 0 ? -1 : 0) * i;
                if (!dict2.ContainsKey((x, y)))
                    dict2[(x, y)] = 0;
                dict2[(x, y)]++;
            }
        }
        var result2 = dict2.Count(e => e.Value > 1);
        
        return new AocDayResult(result1, result2);
    }
    
    private struct Line {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public override string ToString() {
            return $"({X1},{Y1},{X2},{Y2})";
        }
    }
}