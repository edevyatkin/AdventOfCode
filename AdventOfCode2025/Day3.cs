using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 3)]
public class Day3 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        foreach (var batteries in input)
        {
            var dp = new long[13][];
            for (var i = 0; i < 13; i++)
                dp[i] = new long[batteries.Length];
            for (var i = 1; i <= 12; i++)
                for (var j = i - 1; j < batteries.Length; j++)
                    dp[i][j] = Math.Max(j > 0 ? dp[i][j - 1] : 0, (i > 0 && j > 0 ? 10L * dp[i - 1][j - 1] : 0) + (batteries[j] - '0'));

            result1 += dp[2][^1];
            result2 += dp[12][^1];
        }
        return new AocDayResult(result1, result2);
    }
}
