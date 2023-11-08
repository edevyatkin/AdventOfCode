using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,2)]
public class Day2 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var s in input)
        {
            var nums = s.Split().Select(int.Parse).ToList();
            result1 += Math.Abs(nums.Max() - nums.Min());
            foreach (var num1 in nums)
            {
                foreach (var num2 in nums)
                {
                    if (num1 == num2)
                        continue;
                    if (num1 % num2 == 0)
                        result2 += num1 / num2;
                }
            }
        }
        
        return new AocDayResult(result1, result2);
    }
}
