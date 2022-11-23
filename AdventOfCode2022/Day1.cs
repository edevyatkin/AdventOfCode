using AdventOfCodeClient;

[AocDay(2022,1)]
public class Day1 : IAocDay
{
    public async Task<AocDayResult> Solve(int year, int day)
    {
        var data = await AocHelper.FetchInputAsync(year, day);
        return new AocDayResult(42,42);
    }
}
