using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,3)]
public class Day3 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        foreach (var s in input)
        {
            var hs1 = s[..(s.Length / 2)].ToHashSet();
            var hs2 = s[(s.Length / 2)..].ToHashSet();
            hs1.IntersectWith(hs2);
            result1 += GetPriority(hs1.First());
        }

        for (var i = 0; i < input.Length; i += 3)
        {
            var hs1 = input[i].ToHashSet();
            var hs2 = input[i+1].ToHashSet();
            var hs3 = input[i+2].ToHashSet();
            hs2.IntersectWith(hs3);
            hs1.IntersectWith(hs2);
            result2 += GetPriority(hs1.First());
        }

        return new AocDayResult(result1, result2);
    }

    private static int GetPriority(char c)
    {
        return c switch
        {
            >= 'a' and <= 'z' => (c - 'a') + 1,
            >= 'A' and <= 'Z' => (c - 'A') + 27
        };
    }
}
