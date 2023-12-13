namespace AdventOfCode2023;

[AocDay(2023, 12)]
public class Day12 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;

        foreach (var record in input)
        {
            var sp = record.Split(' ');
            var springs = sp[0];
            var groups = sp[1].Split(',').Select(int.Parse).ToArray();

            result1 += Part1Count(springs, groups);
            result2 += Part2Count(springs, groups);
        }

        return new AocDayResult(result1, result2);
    }

    internal static long Part1Count(string springs, int[] groups) =>
        GenerateArrangement(springs).Count(arrangement => arrangement.FitTo(groups));

    private static IEnumerable<Arrangement> GenerateArrangement(string springs)
    {
        var initNum = 0;
        for (var index = 0; index < springs.Length; index++)
        {
            var c = springs[index];
            if (c is '#')
                initNum |= (1 << (springs.Length - 1 - index));
        }

        var unknownPositions = springs
            .Select((c, i) => (c, i))
            .Where(p => p.c == '?')
            .Select(p => p.i);

        var nums = new List<int> { initNum };

        yield return new Arrangement(initNum);

        foreach (var unknownPos in unknownPositions)
        {
            var count = nums.Count;
            for (var index = 0; index < count; index++)
            {
                var num = nums[index];
                var next = num | (1 << (springs.Length - 1 - unknownPos));
                yield return new Arrangement(next);
                nums.Add(next);
            }
        }
    }

    internal static long Part2Count(string springs, int[] groups)
    {
        springs = string.Join('?', Enumerable.Repeat(springs, 5));
        groups = Enumerable.Repeat(groups, 5).SelectMany(n => n).ToArray();

        return Dp(0, 0, springs, groups, new());
    }

    private static long Dp(int si, int gi, string s, int[] g, Dictionary<(int, int), long> cache)
    {
        if (cache.ContainsKey((si, gi)))
            return cache[(si, gi)];

        if (si >= s.Length)
            return gi >= g.Length ? 1 : 0;
        if (gi >= g.Length)
            return s[si..].Any(c => c == '#') ? 0 : 1;

        var groupLen = g[gi];
        var damagedCount = 0;
        var res = 0L;
        for (var curSi = si; curSi < s.Length; curSi++)
        {
            var spring = s[curSi];

            if (spring is '.')
            {
                if (s[si..curSi].Any(c => c is '#'))
                    break;
                damagedCount = 0;
                continue;
            }

            damagedCount++;

            if (damagedCount > groupLen)
                damagedCount--;

            if (damagedCount != groupLen)
                continue;

            if (curSi - groupLen >= 0 && s[curSi - groupLen] is '#')
                break;
            if (curSi + 1 < s.Length && s[curSi + 1] is '#')
                continue;

            res += Dp(curSi + 2, gi + 1, s, g, cache);
        }

        return cache[(si, gi)] = res;
    }
}

internal readonly struct Arrangement
{
    private readonly int _num;

    public Arrangement(int num)
    {
        _num = num;
    }

    public bool FitTo(int[] groups)
    {
        var num = _num;
        var groupSize = 0;
        var numIx = groups.Length - 1;
        while (num > 0 && numIx >= 0)
        {
            if ((num & 1) > 0)
            {
                groupSize++;
            }
            else if (groupSize > 0)
            {
                if (groups[numIx] != groupSize)
                    return false;
                numIx--;
                groupSize = 0;
            }

            num >>= 1;
        }

        if (groupSize > 0)
            return numIx == 0 && groups[numIx] == groupSize;

        return false;
    }
}
