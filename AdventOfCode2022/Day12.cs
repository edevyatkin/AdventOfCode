using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,12)]
public class Day12 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = int.MaxValue;
        var result2 = int.MaxValue;
        
        for (var i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == 'S')
                {
                    result1 = FindMinDistance((i, j));
                }
                if (input[i][j] == 'a')
                {
                    result2 = Math.Min(result2, FindMinDistance((i, j)));
                }
            }
        }

        int FindMinDistance((int I, int J) startPos)
        {
            var pq = new PriorityQueue<(int I ,int J, int Pr),int>();
            var dist = new Dictionary<(int I, int J), int> { [startPos] = 0 };
            var visited = new HashSet<(int, int)>();
            pq.Enqueue((startPos.I,startPos.J,0), 0);
            var diffs = new(int Di, int Dj)[] { (0, -1), (0, 1), (1, 0), (-1, 0) };
            var (result1I, result1J) = (0, 0);
            while (pq.Count > 0)
            {
                var (i, j, d) = pq.Dequeue();
                if (visited.Contains((i, j)))
                {
                    continue;
                }
                foreach (var (dI, dJ) in diffs)
                {
                    var (nI, nJ) = (i + dI, j + dJ);
                    if (nI < 0 || nI == input.Length || nJ < 0 || nJ == input[0].Length 
                        || (input[nI][nJ] != 'E' ? input[nI][nJ] : 'z') - (input[i][j] != 'S' ? input[i][j] : 'a') > 1)
                    {
                        continue;
                    }
                    if (input[nI][nJ] == 'E')
                    {
                        result1I = nI;
                        result1J = nJ;
                    }
                    var nd = dist.GetValueOrDefault((nI, nJ), (int)(1e9 + 7));
                    if (nd > d + 1)
                    {
                        dist[(nI, nJ)] = d + 1;
                        pq.Enqueue((nI, nJ, d + 1), d + 1);
                    }
                }
                visited.Add((i, j));
            }

            return dist.GetValueOrDefault((result1I, result1J), (int)(1e9 + 7));
        }

        return new AocDayResult(result1, result2);
    }
}
