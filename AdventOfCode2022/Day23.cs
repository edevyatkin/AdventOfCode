using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,23)]
public class Day23 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var elves = new HashSet<(int I, int J)>();
        for (var i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == '#')
                {
                    elves.Add((i, j));
                }
            }
        }

        var round = 1;
        var dirs = new[]
        {
            new[] { (-1, -1), (-1, 0), (-1, 1) },
            new[] { (1, -1), (1, 0), (1, 1) },
            new[] { (-1, -1), (0, -1), (1, -1) },
            new[] { (-1, 1), (0, 1), (1, 1) }
        };
        var firstDir = 0;
        while (true)
        {
            var elves2 = new HashSet<(int I, int J)>();
            
            // first half
            var propList = new Dictionary<(int, int), (int, int)>();
            foreach (var (i, j) in elves)
            {
                if (CountNeighboursAround(elves, i, j) == 0)
                {
                    elves2.Add((i, j));
                    continue;
                }
                var elfDir = firstDir;
                do
                {
                    var isProp = true;
                    foreach ((int di, int dj) in dirs[elfDir])
                    {
                        var (si, sj) = (i + di, j + dj);
                        if (elves.Contains((si, sj)))
                        {
                            isProp = false;
                            break;
                        }
                    }
                    if (isProp)
                    {
                        (int di, int dj) = dirs[elfDir][1];
                        propList[(i, j)] = (i + di, j + dj);
                        break;
                    }
                    elfDir = (elfDir + 1) % 4;
                }
                while (elfDir != firstDir);
            }
            
            // second half
            var moves = 0;
            var elvesNotToMove = propList.GroupBy(kv => kv.Value,
                    kv => kv.Key, (k, e) => e.ToList())
                .Where(e => e.Count > 1)
                .SelectMany(e => e).ToHashSet();
            foreach (var (i, j) in elves)
            {
                if (elvesNotToMove.Contains((i, j)))
                {
                    elves2.Add((i, j));
                }
                else
                {
                    if (propList.ContainsKey((i, j)))
                    {
                        elves2.Add(propList[(i, j)]);
                        moves++;
                    }
                    else
                    {
                        elves2.Add((i, j));
                    }
                }
            }
            elves = elves2;
            
            if (moves == 0)
            {
                break;
            }
            firstDir = (firstDir + 1) % 4;
            
            if (round == 10)
            {
                result1 = CalculateGround(elves);
            }
            round++;
        }

        result2 = round;
        
        return new AocDayResult(result1, result2);
    }
    
    private int CountNeighboursAround(HashSet<(int,int)> hs, int i, int j)
    {
        var count = 0;
        for (int di = -1; di <= 1; di++)
        {
            for (int dj = -1; dj <= 1; dj++)
            {
                if (di == 0 && dj == 0) continue;
                var (ni, nj) = (i + di, j + dj);
                if (hs.Contains((ni, nj)))
                {
                    count++;
                }
            }
        }
        return count;
    }
    
    int CalculateGround(HashSet<(int I, int J)> elves)
    {
        var result = 0;
        var minI = elves.Min(e => e.I);
        var maxI = elves.Max(e => e.I);
        var minJ = elves.Min(e => e.J);
        var maxJ = elves.Max(e => e.J);
        for (int i = minI; i <= maxI; i++)
        {
            for (int j = minJ; j <= maxJ; j++)
            {
                if (!elves.Contains((i, j)))
                {
                    result++;
                }
            }
        }

        return result;
    }
}
