namespace AdventOfCode2023;

[AocDay(2023,6)]
public class Day6 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var time = input[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..]
            .Select(int.Parse)
            .ToList();
        var distance = input[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..]
            .Select(int.Parse)
            .ToList();

        var result1 = 1L;
        
        for (var raceId = 0; raceId < time.Count; raceId++)
            result1 *= CountWays(time[raceId], distance[raceId]);

        var time2 = long.Parse(string.Concat(time));
        var distance2 = long.Parse(string.Concat(distance));

        var result2 = CountWays(time2, distance2);
        
        return new AocDayResult(result1, result2);
    }

    private static long CountWays(long time, long distance)
    {
        var result = 0L;
        for (var tt = 1; tt < time; tt++)
            if ((time - tt) * tt > distance)
                result++;
        return result;
    }
}
