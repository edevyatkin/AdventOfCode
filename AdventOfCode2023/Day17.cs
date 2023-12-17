namespace AdventOfCode2023;

[AocDay(2023, 17)]
public class Day17 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var crucible = new Crucible(input);
        var result1 = crucible.MinimizeHeatLoss(1, 3);
        var result2 = crucible.MinimizeHeatLoss(4, 10);

        return new AocDayResult(result1, result2);
    }
}

public class Crucible
{
    private readonly string[] _input;

    public Crucible(string[] input)
    {
        _input = input;
    }

    public int MinimizeHeatLoss(int minStraightLine, int maxStraightLine)
    {
        var dirs = new[] { (-1, 0), (1, 0), (0, 1), (0, -1) };
        var height = _input.Length;
        var width = _input[0].Length;
        var minHeatLoss = new Dictionary<(int I, int J, int DirIx, int DirLen), int>();
        var q = new PriorityQueue<(int I, int J, int HeatLoss, HashSet<(int, int)> Visited, int DirIx, int DirLen), int>();
        q.Enqueue((0, 0, 0, new(), 0, 0), 0);
        while (q.Count > 0)
        {
            var (i, j, currentHeatLoss, vis, dirIx, dirLen) = q.Dequeue();
            vis.Add((i, j));
            if ((i, j) == (height - 1, width - 1))
                return currentHeatLoss;
            for (var dIx = 0; dIx < dirs.Length; dIx++)
            {
                var (di, dj) = dirs[dIx];
                var (ni, nj) = (i, j);
                var dLen = dIx == dirIx ? dirLen : 0;
                var heatDiff = 0;
                for (var len = dLen + 1; len <= maxStraightLine; len++)
                {
                    ni += di;
                    nj += dj;
                    if (ni < 0 || ni >= height || nj < 0 || nj >= width || vis.Contains((ni, nj)))
                        break;
                    heatDiff += _input[ni][nj] - '0';
                    if (len < minStraightLine)
                        continue;
                    var newHeatLoss = currentHeatLoss + heatDiff;
                    if (minHeatLoss.TryGetValue((ni, nj, dIx, len), out var heatLoss) && heatLoss <= newHeatLoss)
                        break;
                    minHeatLoss[(ni, nj, dIx, len)] = newHeatLoss;
                    q.Enqueue((ni, nj, newHeatLoss, new(vis), dIx, len), newHeatLoss);
                }
            }
        }

        return -1;
    }
}
