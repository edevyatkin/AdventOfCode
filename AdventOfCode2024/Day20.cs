using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,20)]
public class Day20 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        
        var (si, sj, ei, ej) = (0, 0, 0, 0);
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == 'S')
                {
                    si = i;
                    sj = j;
                }

                if (input[i][j] == 'E')
                {
                    ei = i;
                    ej = j;
                }
            }
        }

        var shortestPathsVisited = new int[input.Length][];
        for (var i = 0; i < input.Length; i++)
            shortestPathsVisited[i] = new int[input[0].Length];
        var searchCounter = 1;
        var distances = new DistanceDictionary();
        for (var i = 1; i < input.Length-1; i++)
        {
            for (int j = 1; j < input[0].Length-1; j++)
            {
                if (input[i][j] == '#')
                    continue;
                ShortestPaths(input, i, j, shortestPathsVisited, searchCounter, distances);
                searchCounter++;
            }
        }

        Dictionary<int,int> CalculateCheats(int maxDistance)
        {
            var cheats = new Dictionary<int,int>();
            var visited = new HashSet<(int I, int J)>();
            for (var i = 1; i < input.Length-1; i++)
            {
                for (var j = 1; j < input[0].Length-1; j++)
                {
                    if (input[i][j] == '#')
                        continue;
                    foreach (var neigh in EnumerateManhattan(input, (i, j), maxDistance)
                                 .Where(neigh => input[neigh.I][neigh.J] != '#' && !visited.Contains((neigh.I, neigh.J))))
                    {
                        visited.Add((i, j));
                        CheckCheat(cheats, (i, j), (neigh.I, neigh.J));
                    }
                }
            }
            return cheats;
        }
        
        
        void CheckCheat(Dictionary<int,int> cheats, (int I, int J) first, (int I, int J) second)
        {
            if (input[first.I][first.J] == '#' || input[second.I][second.J] == '#')
                return;
            
            var cheatStartEnd = distances.Get(si, sj, first.I, first.J) 
                                + distances.Get(ei, ej, second.I, second.J)
                                + Math.Abs(second.I - first.I) + Math.Abs(second.J - first.J);
            var realStartEnd = distances.Get(si, sj, first.I, first.J)
                               + distances.Get(ei, ej, second.I, second.J)
                               + distances.Get(first.I, first.J, second.I, second.J);
            
            var cheatEndStart = distances.Get(si, sj, second.I, second.J)
                                + distances.Get(ei, ej, first.I, first.J)
                                + Math.Abs(second.I - first.I) + Math.Abs(second.J - first.J);
            var realEndStart = distances.Get(si, sj, second.I, second.J)
                               + distances.Get(ei, ej, first.I, first.J)
                               + distances.Get(second.I, second.J, first.I, first.J);
             
            var minDist = Math.Min(realStartEnd, realEndStart);
            var minCheat = Math.Min(cheatStartEnd, cheatEndStart);
            var cheat = minDist - minCheat;
            if (cheat > 0)
            {
                cheats[cheat] = cheats.GetValueOrDefault(cheat, 0) + 1;
            }
        }
        
        var cheats1 = CalculateCheats(2);
        var cheats2 = CalculateCheats(20);
        
        result1 = cheats1.Where(kvp => kvp.Key >= 100).Sum(kvp => kvp.Value);
        result2 = cheats2.Where(kvp => kvp.Key >= 100).Sum(kvp => kvp.Value);
        
        return new AocDayResult(result1, result2);
    }

    void ShortestPaths(string[] grid, int si, int sj, int[][] shortestPathsVisited,
        int searchCounter, DistanceDictionary distances)
    {
        var dirs = new (int Di, int Dj)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        var pq = new Queue<(int I, int J)>();
        pq.Enqueue((si, sj));
        var len = 0;
        while (pq.Count > 0)
        {
            var count = pq.Count;
            while (count-- > 0)
            {
                var (i, j) = pq.Dequeue();
                foreach (var (di, dj) in dirs)
                {
                    var (ni, nj) = (i + di, j + dj);
                    if (ni < 0 || ni >= grid.Length || nj < 0 || nj >= grid[0].Length)
                        continue;
                    if (grid[ni][nj] == '#' || shortestPathsVisited[ni][nj] == searchCounter)
                        continue;
                    distances.Set(si, sj, ni, nj, len + 1);
                    pq.Enqueue((ni, nj));
                }
                shortestPathsVisited[i][j] = searchCounter;
            }
            len++;
        }
    }

    IEnumerable<(int I, int J, int Dist)> EnumerateManhattan(string[] input, (int I, int J) start, int maxDistance)
    {
        if (maxDistance <= 0)
            throw new ArgumentException("maxDistance must be greater than 0");
        for (var di = -maxDistance; di <= maxDistance; di++)
        {
            for (var dj = -(maxDistance - di); dj <= maxDistance - di; dj++)
            {
                if (di == 0 && dj == 0 || Math.Abs(di) + Math.Abs(dj) > maxDistance)
                    continue;
                var (ni, nj) = (start.I + di, start.J + dj);
                if (ni >= 0 && ni < input.Length && nj >= 0 && nj < input[0].Length)
                    yield return (ni, nj, Math.Abs(start.I - ni) + Math.Abs(start.J - nj));
            }
        }
    }

    class DistanceDictionary
    {
        private readonly Dictionary<int, int> _dict = [];

        internal int Get(int i1, int j1, int i2, int j2)
        {
            var key = ToKey(i1, j1, i2, j2);
            var reversedKey = (key << 16) | (key >> 16);
            if (_dict.TryGetValue(key, out var dist) || _dict.TryGetValue(reversedKey, out dist))
                return dist;
            return 1_000_000;
        }

        internal void Set(int i1, int j1, int i2, int j2, int dist)
        {
            var key = ToKey(i1, j1, i2, j2);
            _dict[key] = dist;
        }

        private static int ToKey(int i1, int j1, int i2, int j2)
        {
            return (i1 << 24) | (j1 << 16) | (i2 << 8) | j2;
        }
    }
}


