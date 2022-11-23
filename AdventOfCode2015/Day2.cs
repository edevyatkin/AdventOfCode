using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,2)]
public class Day2 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var box in input)
        {
            var sides = box.Split('x').Select(int.Parse).OrderBy(s => s).ToArray();
            result1 += 3*sides[0]*sides[1] + 2*sides[0]*sides[2] + 2*sides[1]*sides[2];
            result2 += 2*(sides[0]+sides[1]) + sides[0]*sides[1]*sides[2];
        }

        return new AocDayResult(result1, result2);
    }
}