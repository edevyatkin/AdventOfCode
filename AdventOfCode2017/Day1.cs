using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,1)]
public class Day1 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var s = input[0];
        for (var i = 0; i < s.Length; i++)
        {
            result1 += s[i] == s[(i + 1) % s.Length] ? s[i] - '0' : 0;
            result2 += s[i] == s[(i + s.Length/2) % s.Length] ? s[i] - '0' : 0;
        }
        return new AocDayResult(result1, result2);
    }
}
