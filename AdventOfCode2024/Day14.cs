using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,14)]
public class Day14 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, 103, 101, false);
        var result2 = SolvePart(input, 103, 101, true);
        
        return new AocDayResult(result1, result2);
    }

    public long SolvePart(string[] input, int height, int width, bool isPartTwo)
    {
        var grid = new Dictionary<(int I, int J), List<(int Vx, int Vy)>>();
        foreach (var line in input)
        {
            var m = Regex.Match(line, @"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)");
            var px = int.Parse(m.Groups[1].Value);
            var py = int.Parse(m.Groups[2].Value);
            var vx = int.Parse(m.Groups[3].Value);
            var vy = int.Parse(m.Groups[4].Value);
            if (!grid.ContainsKey((py, px)))
                grid[(py, px)] = [];
            grid[(py, px)].Add((vx, vy));
        }

        var counter = 1;
        var patternLen = 20; // top border size of tree picture
        while (true)
        {
            var grid2 = new Dictionary<(int I, int J), List<(int Vx, int Vy)>>();
            foreach (var (k,v) in grid)
            {
                foreach (var (vx, vy) in v)
                {
                    var ni = (k.I + vy + height) % height;
                    var nj = (k.J + vx + width) % width;
                    if (!grid2.ContainsKey((ni,nj)))
                        grid2[(ni,nj)] = [];
                    grid2[(ni,nj)].Add((vx, vy));
                }
            }
            
            grid = grid2;
            
            if (counter == 100 && !isPartTwo)
            {
                var quadrants = new int[4];
                foreach (var (k, v) in grid) {
                    var currentQuadrant = GetQuadrant(k.I, k.J);
                    if (currentQuadrant > -1)
                        quadrants[currentQuadrant] += v.Count;
                }
        
                return quadrants.Aggregate(1L, (current, quadrant) => current * quadrant);
            }
            
            foreach (var (k, v) in grid)
            {
                if (k.J > width - patternLen)
                    continue;
                var count = 1;
                while (count < patternLen)
                {
                    if (!grid.ContainsKey((k.I, k.J+count)))
                        break;
                    count++;
                }
                if (count == patternLen)
                    return counter;
            }

            counter++;
        }
        
        int GetQuadrant(int i, int j)
        {
            if (i < height / 2 && j < width / 2)
                return 0;
            if (i < height / 2 && j > width / 2)
                return 1;
            if (i > height / 2 && j < width / 2)
                return 2;
            if (i > height / 2 && j > width / 2)
                return 3;
            return -1;
        }
    }

}
