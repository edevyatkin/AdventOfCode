using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var nums = input[0].Split(',').Select(int.Parse).ToArray();
        var result1 = Intcode.Execute([..nums], 1)[^1];
        var result2 = Intcode.Execute([..nums], 5)[^1];
        return new AocDayResult(result1, result2);
    }
}
