using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;

        var ranges = new List<(long S, long E)>();
        var ix = 0;
        while (input[ix] != string.Empty)
        {
            var sp = input[ix].Split('-').Select(long.Parse).ToArray();
            var (s, e) = (sp[0], sp[1]);
            ranges.Add((s, e));
            ix++;
        }

        ranges = Merge(ranges);
        
        result1 = input[(ix+1)..]
            .Select(long.Parse)
            .Sum(num => ranges.Count(r => num >= r.S && num <= r.E));
        result2 = ranges.Sum(r => r.E - r.S + 1);
        
        return new AocDayResult(result1, result2);
    }

    private static List<(long S, long E)> Merge(List<(long S, long E)> ranges)
    {
        ranges.Sort((a, b) => {
            if (a.S == b.S)
                return (a.E - b.E) switch {
                    > 0 => 1,
                    < 0 => -1,
                    _ => 0
                };

            if (a.S > b.S)
                return 1;
            return -1;
        });
        
        var (i, j) = (0, 1);
        while (j < ranges.Count)
            if (ranges[i].E < ranges[j].S)
                ranges[++i] = ranges[j++];
            else
                ranges[i] = ranges[i] with { E = Math.Max(ranges[i].E, ranges[j++].E) };
        
        return ranges[..(i+1)];
    }
}
