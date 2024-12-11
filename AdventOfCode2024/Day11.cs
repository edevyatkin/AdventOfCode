using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,11)]
public class Day11 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        
        var nums = input[0].Split(' ').Select(long.Parse).ToArray();
        var cache = new Dictionary<(long,int), long>();
        result1 = nums.Sum(n => ChangeStone(n, cache, 25));
        result2 = nums.Sum(n => ChangeStone(n, cache, 75));
        
        return new AocDayResult(result1, result2);
    }

    private static long ChangeStone(long stone, Dictionary<(long,int), long> dp, int blinks)
    {
        if (blinks == 0)
            return 1;
        if (dp.ContainsKey((stone, blinks)))
            return dp[(stone,blinks)];
        if (stone == 0)
            return dp[(stone, blinks)] = ChangeStone(1, dp, blinks - 1);
        var digits = (int)(Math.Log10(stone) + 1);
        if (digits % 2 == 0)
        {
            var tens = (int)Math.Pow(10, digits / 2.0);
            var (left, right) = Math.DivRem(stone, tens);
            return dp[(stone, blinks)] = ChangeStone(left, dp, blinks - 1) + ChangeStone(right, dp, blinks - 1);
        }
        return dp[(stone, blinks)] = ChangeStone(stone * 2024, dp, blinks - 1);
    }
}
