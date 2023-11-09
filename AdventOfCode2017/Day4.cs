using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,4)]
public class Day4 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var s in input)
        {
            var sp = s.Split().ToArray();
            if (sp.ToHashSet().Count == sp.Length)
                result1++;
            var sorted  = sp.Select(str => new string(str.ToCharArray().OrderBy(c => c).ToArray())).ToHashSet();
            if (sorted.Count == sp.Length)
                result2++;
        }
        return new AocDayResult(result1, result2);
    }
}
