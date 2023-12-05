namespace AdventOfCode2023;

[AocDay(2023,5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, false);
        var result2 = SolvePart(input, true);
        
        return new AocDayResult(result1, result2);
    }

    internal static long SolvePart(string[] input, bool isPartTwo)
    {
        List<(long, long)> ranges;
        if (!isPartTwo)
        {
            ranges = input[0].Split(" ")[1..]
                .Select(n => (long.Parse(n), long.Parse(n)))
                .ToList();
        }
        else
        {
            ranges = input[0].Split(" ")[1..]
                .Chunk(2)
                .Select(ch => (long.Parse(ch[0]), long.Parse(ch[0]) + long.Parse(ch[1]) - 1))
                .ToList();
        }


        var maps = new List<Dictionary<(long S, long E), long>>();
        for (var i = 3; i < input.Length; i += 2)
        {
            var mapRanges = new Dictionary<(long S, long E), long>();
            while (i < input.Length && input[i] != string.Empty)
            {
                var line = input[i];
                var sp = line.Split().Select(long.Parse).ToArray();
                mapRanges[(sp[1], sp[1] + sp[2] - 1)] = sp[0] - sp[1];
                i++; 
            }
            maps.Add(mapRanges);
        }
        
        foreach (var map in maps)
        {
            var newRanges = new List<(long, long)>();
            foreach (var (fromL, fromR) in ranges)
            {
                var found = false;
                var rangesBeforeDiff = new List<(long, long)>();
                foreach (var ((toL, toR), toDiff) in map)
                {
                    var cmp = (fromL, fromR).CompareTo((toL, toR));
                    var (leftL, leftR) = cmp <= 0 ? (fromL, fromR) : (toL, toR);
                    var (rightL, rightR) = cmp > 0 ? (fromL, fromR) : (toL, toR);

                    if (leftR < rightL)
                        continue;

                    var left = rightL;
                    var right = Math.Min(leftR, rightR);
                    rangesBeforeDiff.Add((left, right));
                    newRanges.Add((left + toDiff, right + toDiff));
                    found = true;
                }

                if (!found)
                {
                    newRanges.Add((fromL, fromR));
                }
                else
                {
                    rangesBeforeDiff = Merge(rangesBeforeDiff);
                    var left = fromL;
                    foreach (var (dl,dr) in rangesBeforeDiff)
                    {
                        if (left < dl)
                            newRanges.Add((left, dl-1));
                        left = dr + 1;
                    }
                    if (left <= fromR)
                        newRanges.Add((left, fromR));
                }
            }
            
            ranges = Merge(newRanges);
        }

        return ranges.Min(r => r.Item1);
    }

    private static List<(long, long)> Merge(List<(long, long)> ranges)
    {
        ranges.Sort();
        var result = new List<(long, long)> { ranges[0] };
        for (var i = 1; i < ranges.Count; i++)
        {
            if (result[^1].Item2 + 1 < ranges[i].Item1)
                result.Add(ranges[i]);
            else
                result[^1] = (result[^1].Item1, Math.Max(result[^1].Item2, ranges[i].Item2));
        }

        return result;
    }
}
