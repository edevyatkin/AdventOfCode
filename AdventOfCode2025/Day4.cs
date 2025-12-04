using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 4)]
public class Day4 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        var dirs = new (int Di, int Dj)[] { (0, 1), (1, 0), (-1, 0), (0, -1), (-1, -1), (-1, 1), (1, -1), (1, 1) };
        var removed = new HashSet<(int,int)>();
        var prevCount = 0;
        var isFirst = true;
        do
        {
            prevCount = removed.Count;
            var newRemoved = new HashSet<(int,int)>();
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '.' || (removed.Contains((i, j)) && !isFirst))
                        continue;
                    var count = 0;
                    foreach (var dir in dirs)
                    {
                        var (ni,nj) = (i + dir.Di, j + dir.Dj);
                        if (ni < 0 || ni == input.Length || nj < 0 || nj == input[0].Length || removed.Contains((ni, nj)))
                            continue;
                        if (input[ni][nj] == '@')
                            count++;
                    }

                    if (count < 4)
                    {
                        newRemoved.Add((i, j));
                        if (isFirst)
                            result1++;
                    }
                }
            }

            isFirst = false;
            removed.UnionWith(newRemoved);
        } while (removed.Count != prevCount);

        result2 = removed.Count;
        
        return new AocDayResult(result1, result2);
    }
}
