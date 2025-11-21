using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019,1)]
public class Day1 : IAocDay 
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = input.Sum(l => int.Parse(l) / 3 - 2);
        var result2 = 0;
        foreach (var l in input)
        {
            var fuel = int.Parse(l);
            while (fuel / 3 - 2 > 0)
            {
                fuel = fuel / 3 - 2;
                result2 += fuel;
            }
        }
        return new AocDayResult(result1, result2);
    }
}
