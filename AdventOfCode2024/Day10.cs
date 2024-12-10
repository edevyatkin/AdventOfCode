using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024, 10)]
public class Day10 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var dirs = new[] { (0, 1), (0, -1), (-1, 0), (1, 0) };

        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] != '0')
                    continue;
                var stack = new Stack<(int,int)>();
                stack.Push((i,j));
                var scoreSet = new HashSet<(int,int)>();
                while (stack.Count > 0)
                {
                    var (ci, cj) = stack.Pop();
                    if (input[ci][cj] == '9')
                    {
                        scoreSet.Add((ci, cj));
                        result2++;
                    }
                    foreach (var (di, dj) in dirs)
                    {
                        var (ni, nj) = (ci + di, cj + dj);
                        if (ni < 0 || ni == input.Length || nj < 0 || nj == input[0].Length) 
                            continue;
                        if ((input[ci][cj] - '0') + 1 != input[ni][nj] - '0')
                            continue;
                        stack.Push((ni, nj));
                    }
                }
                result1 += scoreSet.Count;
            }
        }
        
        return new AocDayResult(result1, result2);
    }
}
