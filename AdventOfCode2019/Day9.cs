using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 9)]
public class Day9 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var nums = input[0].Split(',').Select(int.Parse).ToArray();
        var result1 = new Intcode(nums).Execute(1)[^1];
        var result2 = new Intcode(nums).Execute(2)[^1];
        return new AocDayResult(result1, result2);
    }
}
