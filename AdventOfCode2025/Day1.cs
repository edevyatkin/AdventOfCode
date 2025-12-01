using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025,1)]
public class Day1 : IAocDay 
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var point = 50;
        foreach (var line in input)
        {
            var c = int.Parse(line[1..]);
            if (line[0] == 'L')
            {
                if (point - c <= 0)
                    result2 += -(point - c) / 100 + (point > 0 ? 1 : 0);
            }
            else
            {
                if (point + c >= 100)
                    result2 += (point + c) / 100;
            }
            point = (point + 1000 + (line[0] == 'L' ? -1 : 1) * c) % 100;
            if (point == 0)
                result1++;
        }
        return new AocDayResult(result1, result2);
    }
}
