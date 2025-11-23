using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 3)]
public class Day3 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var crosses = new Dictionary<(int X, int Y), (int Wire1, int Wire2)>();
        var dirs = new Dictionary<char, (int,int)> {
            ['U'] = (-1, 0), ['D'] = (1, 0), ['L'] = (0, -1), ['R'] = (0, 1)
        };
        Walk(input[0], 1);
        Walk(input[1], 2);
        result1 = crosses.Where(kv => kv.Value is { Wire1: > 0, Wire2: > 0 }).Min(kv => Math.Abs(kv.Key.X) + Math.Abs(kv.Key.Y));
        result2 = crosses.Where(kv => kv.Value is { Wire1: > 0, Wire2: > 0 }).Min(kv => kv.Value.Wire1 +  kv.Value.Wire2);
        
        return new AocDayResult(result1, result2);
        
        void Walk(string wirePath, int wire)
        {
            var parts = wirePath.Split(',');
            var (x, y) = (0, 0);
            var steps = 0;
            foreach (var part in parts)
            {
                var (dir, dist) = (dirs[part[0]], int.Parse(part[1..]));
                while (dist-- > 0)
                {
                    steps++;
                    x += dir.Item1;
                    y += dir.Item2;
                    var cur = crosses.GetValueOrDefault((x, y));
                    if (wire == 1 && cur.Wire1 == 0)
                        crosses[(x,y)] = cur with { Wire1 = steps };
                    else if (wire == 2 && cur.Wire2 == 0)
                        crosses[(x,y)] = cur with { Wire2 = steps };
                }
            }
        }
    }
}
