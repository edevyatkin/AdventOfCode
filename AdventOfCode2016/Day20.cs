using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,20)]
public class Day20 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = -1L;
        var result2 = 0L;
        var rules = ParseRules(input);
        rules.Sort();
        var merged = new List<(long Min, long Max)>();
        var (mn,mx) = rules[0];
        for (int i = 1; i < rules.Count; i++)
        {
            var (mn2, mx2) = rules[i];
            if (mx+1 >= mn2 && mx < mx2)
            {
                mx = mx2;
            }
            else if (mx < mn2)
            {
                if (result1 == -1)
                    result1 = mx + 1;
                result2 += (mx - mn + 1);
                merged.Add((mn,mx));
                mn = mn2;
                mx = mx2;
            }
        }
        if (merged[^1].Max < mn)
            result2 += (mx - mn + 1);
        return new AocDayResult(result1, uint.MaxValue - result2 + 1);
    }

    private List<(long Min, long Max)> ParseRules(string[] input)
    {
        var list = new List<(long, long)>();
        foreach (var s in input)
        {
            var sp = s.Split('-');
            list.Add((long.Parse(sp[0]),long.Parse(sp[1])));
        }
        return list;
    }
}
