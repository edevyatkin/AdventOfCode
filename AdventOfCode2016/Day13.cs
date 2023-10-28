using System.Numerics;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,13)]
public class Day13 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var favoriteNumber = int.Parse(input[0]);
        var result1 = CalcMinSteps(favoriteNumber, 31, 39);
        var result2 = FindNumberOfLocations(favoriteNumber, 50);
        return new AocDayResult(result1, result2);
    }

    public static int CalcMinSteps(int favoriteNumber, int ex, int ey)
    {
        var q = new Queue<(int I, int J)>();
        q.Enqueue((1,1));
        var visited = new HashSet<(int, int)>() { (1,1) };
        var steps = 0;
        var dist = new (int DI, int DJ)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        while (q.Count > 0)
        {
            var count = q.Count;
            while (count-- > 0)
            {
                var (i, j) = q.Dequeue();
                if (i == ex && j == ey)
                    return steps;
                foreach (var (di,dj) in dist)
                {
                    var ni = i + di;
                    var nj = j + dj;
                    if (ni < 0 || nj < 0 || visited.Contains((ni,nj)) || !IsOpenSpace(ni,nj,favoriteNumber))
                        continue;
                    visited.Add((ni, nj));
                    q.Enqueue((ni,nj));
                }
            }
            steps++;
        }
        return 0;
    }

    public static int FindNumberOfLocations(int favoriteNumber, int maxSteps)
    {
        var q = new Queue<(int I, int J)>();
        q.Enqueue((1,1));
        var visited = new HashSet<(int, int)>() { (1,1) };
        var steps = 0;
        var dist = new (int DI, int DJ)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        while (q.Count > 0 && steps < maxSteps)
        {
            var count = q.Count;
            while (count-- > 0)
            {
                var (i, j) = q.Dequeue();
                foreach (var (di,dj) in dist)
                {
                    var ni = i + di;
                    var nj = j + dj;
                    if (ni < 0 || nj < 0 || visited.Contains((ni,nj)) || !IsOpenSpace(ni,nj,favoriteNumber))
                        continue;
                    visited.Add((ni, nj));
                    q.Enqueue((ni,nj));
                }
            }
            steps++;
        }
        return visited.Count;
    }

    private static bool IsOpenSpace(int x, int y, int favoriteNumber)
    {
        return (BitOperations.PopCount((uint)(x * x + 3 * x + 2 * x * y + y + y * y + favoriteNumber)) & 1) == 0;
    }
}
