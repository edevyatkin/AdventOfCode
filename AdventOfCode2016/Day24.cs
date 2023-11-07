using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,24)]
public class Day24 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, false);
        var result2 = SolvePart(input, true);;
        return new AocDayResult(result1, result2);
    }

    public int SolvePart(string[] input, bool isPartTwo)
    {
        var locations = new Dictionary<int, (int X, int Y)>();
        var (m, n) = (input.Length, input[0].Length);
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                var c = input[i][j];
                if (char.IsNumber(c))
                    locations[c -'0'] = (i, j);
            }
        }
        
        var locationsDist = new int[locations.Count,locations.Count];
        int MDist((int X, int Y) a, (int X, int Y) b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        var diffs = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

        for (var i = 0; i < locations.Count; i++)
        {
            for (var j = i+1; j < locations.Count; j++)
            {
                locationsDist[i,j] = locationsDist[j,i] = int.MaxValue;
                
                var from = locations[i];
                var to = locations[j];
                
                var visited = new HashSet<(int X, int Y)>();
                var dist = new Dictionary<(int X, int Y), int>();
                var pq = new PriorityQueue<((int X, int Y), int Dist), int>();
                pq.Enqueue((from, 0), MDist(from, to));
                while (pq.Count > 0)
                {
                    var (c, d) = pq.Dequeue();
                    if (visited.Contains(c))
                        continue;
                    if (c == to)
                        break;
                    foreach (var (dx,dy) in diffs)
                    {
                        var nx = c.X + dx;
                        var ny = c.Y + dy;
                        if (input[nx][ny] == '#')
                            continue;
                        if (dist.TryGetValue((nx, ny), out var storedDist) && d + 1 >= storedDist)
                            continue;
                        dist[(nx, ny)] = d + 1;
                        pq.Enqueue(((nx,ny), d + 1),(d + 1) + MDist((nx,ny),to));
                    }
                    visited.Add(c);
                }
                locationsDist[i,j] = locationsDist[j,i] = dist[(to.X,to.Y)];
            }
        }

        int FindMinPath(int currentLocation, HashSet<int> visited)
        {
            if (visited.Count == locations.Count)
                return !isPartTwo ? 0 : locationsDist[currentLocation, 0];
            var result = int.MaxValue;
            for (int nextLocation = 1; nextLocation < locations.Count; nextLocation++)
            {
                if (visited.Contains(nextLocation))
                    continue;
                visited.Add(nextLocation);
                result = Math.Min(result, locationsDist[currentLocation, nextLocation] + FindMinPath(nextLocation, visited));
                visited.Remove(nextLocation);
            }
            return result;
        }

        return FindMinPath(0, new () { 0 });
    }

}
