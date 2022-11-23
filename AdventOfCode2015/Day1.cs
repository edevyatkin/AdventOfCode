using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,1)]
public class Day1 : IAocDay
{
    public async Task<AocDayResult> Solve(int year, int day)
    {
        var input = await AocHelper.FetchInputAsync(year, day);

        var instr = input[0];
        
        var result1 = instr.Sum(c => c == '(' ? 1 : -1);

        var result2 = 0;
        var floor = 0;
        for (var i = 0; i < instr.Length; i++)
        {
            var c = instr[i];
            floor += c == '(' ? 1 : -1;
            if (floor == -1)
            {
                result2 = i + 1;
                break;
            }
        }

        return new AocDayResult(result1, result2);
    }
}