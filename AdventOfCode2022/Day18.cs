using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,18)]
public class Day18 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        int minI = int.MaxValue, maxI = 0, 
            minJ = int.MaxValue, maxJ = 0, 
            minK = int.MaxValue, maxK = 0;
        var droplets = new HashSet<(int, int, int)>();
        foreach (var s in input)
        {
            var droplet = s.Split(',').Select(int.Parse).ToArray();
            
            minI = Math.Min(minI, droplet[0] - 1);
            maxI = Math.Max(maxI, droplet[0] + 1);
            
            minJ = Math.Min(minJ, droplet[1] - 1);
            maxJ = Math.Max(maxJ, droplet[1] + 1);
            
            minK = Math.Min(minK, droplet[2] - 1);
            maxK = Math.Max(maxK, droplet[2] + 1);
            
            droplets.Add((droplet[0], droplet[1], droplet[2]));
        }

        int DFS(int i, int j, int k, HashSet<(int, int, int)> visited)
        {
            if (i < minI || i > maxI || j < minJ || j > maxJ || k < minK || k > maxK || visited.Contains((i,j,k)))
            {
                return 0;
            }
            if (droplets.Contains((i, j, k)))
            {
                return 1;
            }
            
            visited.Add((i, j, k));
            return DFS(i + 1, j, k, visited)
                   + DFS(i - 1, j, k, visited) 
                   + DFS(i, j + 1, k, visited)
                   + DFS(i, j - 1, k, visited)
                   + DFS(i, j, k + 1, visited)
                   + DFS(i, j, k - 1, visited);

        }

        var visited = new HashSet<(int, int, int)>();
        for (int i = minI; i <= maxI; i++)
        {
            for (int j = minJ; j <= maxJ; j++)
            {
                for (int k = minK; k <= maxK; k++)
                {
                    if (droplets.Contains((i, j, k)))
                    {
                        continue;
                    }
                    result1 += DFS(i, j, k, visited); 
                    if (result2 == 0)
                    {
                        result2 = result1;
                    }
                }
            }
        }

        return new AocDayResult(result1, result2);
    }
}
