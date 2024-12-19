using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,19)]
public class Day19 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0L;
        
        var patterns = input[0].Split(", ").ToList();
        var hs = new HashSet<string>(patterns);

        for (var k = 2; k < input.Length; k++)
        {
            var len = input[k].Length;
            var dp = new long[len+1];
            dp[0] = 1;
            var j = 1;
            while (j <= len) {
                var i = j - 1;
                do {
                    if (dp[i] > 0 && hs.Contains(input[k][i..j])) {
                        dp[j] += dp[i];
                    }
                    i--;
                } while (i >= 0);
                j++;
            }
            result1 += dp[len] > 0 ? 1 : 0;
            result2 += dp[len];
        }
        
        return new AocDayResult(result1, result2); 
    }
}
