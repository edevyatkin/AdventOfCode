using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, false);
        var result2 = SolvePart(input, true);
        return new AocDayResult(result1, result2);
    }

    public static int SolvePart(string[] input, bool isPartTwo)
    {
        var result = 1;
        var nums = input.Select(int.Parse).ToArray();
        var ix = 0;
        while (true)
        {
            var diff = nums[ix];
            var nextIx = ix + diff;
            if (nextIx >= 0 && nextIx < nums.Length)
            {
                if (isPartTwo && nums[ix] >= 3)
                {
                    nums[ix]--;
                }
                else
                {
                    nums[ix]++;
                }
                ix = nextIx;
                result++;
            }
            else
            {
                return result;
            }
        }
    }
}
