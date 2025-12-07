using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 6)]
public class Day6 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        
        var ops = input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        var problems1 = new long[ops.Length];
        for (var i = 0; i < input.Length - 1; i++)
        {
            var line = input[i]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            for (var li = 0; li < line.Length; li++)
            {
                if (problems1[li] == 0)
                    problems1[li] = ops[li] == "+" ? 0 : 1;
                problems1[li] = ops[li] == "+" ? problems1[li] + line[li] : problems1[li] * line[li];
            }
        }

        var problems2 = new long[ops.Length];
        var j = 0;
        var pi = 0;
        while (j < input[0].Length)
        {
            var num = 0L;
            for (var i = 0; i < input.Length - 1; i++)
                num = (input[i][j] != ' ' ? num * 10 + (input[i][j] - '0') : num);
            if (num == 0)
            {
                pi++;
                j++;
                continue;
            }
            if (problems2[pi] == 0)
                problems2[pi] = ops[pi] == "+" ? 0 : 1;
            problems2[pi] = ops[pi] == "+" ? problems2[pi] + num  : problems2[pi] * num;
            j++;
        }
        
        result1 = problems1.Sum();
        result2 = problems2.Sum();
        
        return new AocDayResult(result1, result2);
    }
}
