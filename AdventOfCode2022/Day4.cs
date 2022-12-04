using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,4)]
public class Day4 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        foreach (var s in input)
        {
            var sp = s.Split(',');
            var sec1 = sp[0].Split('-').Select(int.Parse).ToArray();
            var sec2 = sp[1].Split('-').Select(int.Parse).ToArray();
            if (sec1[0] <= sec2[0] && sec2[1] <= sec1[1] || sec2[0] <= sec1[0] && sec1[1] <= sec2[1])
            {
                result1++;
            }
            if (sec1[0] <= sec2[0] && sec2[0] <= sec1[1] || sec2[0] <= sec1[0] && sec1[0] <= sec2[1])
            {
                result2++;
            }
        }

        return new AocDayResult(result1, result2);
    }
}
