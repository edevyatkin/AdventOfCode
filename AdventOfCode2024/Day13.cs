using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,13)]
public partial class Day13 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;

        for (var i = 0; i < input.Length; i += 4)
        {
            var data = input[i..(i + 3)]
                .Select(line => LineRegex().Match(line).Groups)
                .Select(g => (long.Parse(g[1].Value), long.Parse(g[2].Value)))
                .ToList();
            var ((ax, ay),(bx, by), (px, py)) = (data[0], data[1], data[2]);
            if (TrySolve(ax, ay, bx, by, px, py, out var res1, isPartTwo: false))
                result1 += res1;
            if (TrySolve(ax, ay, bx, by, px, py, out var res2, isPartTwo: true))
                result2 += res2;
        }
        
        return new AocDayResult(result1, result2);
    }

    private static bool TrySolve(long ax, long ay, long bx, long by, long px, long py, out long result, bool isPartTwo)
    {
        if (isPartTwo)
        {
            px += 10000000000000L;
            py += 10000000000000L;
        }
        result = 0;
        var remB = (ax * py - ay * px) % (ax * by - ay * bx);
        if (remB != 0)
            return false;
        var b = (ax * py - ay * px) / (ax * by - ay * bx);
        var remA = (px - bx * b) % ax;
        if (remA != 0)
            return false;
        var a = (px - bx * b) / ax;
        if (!isPartTwo && (a > 100 || b > 100))
            return false;
        result = 3 * a + b;
        return true;
    }

    [GeneratedRegex(@"X[+=](\d+), Y[+=](\d+)$")]
    private static partial Regex LineRegex();
}
