using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019,2)]
public class Day2 : IAocDay 
{
    public AocDayResult Solve(string[] input)
    {
        var nums = input[0].Split(',').Select(int.Parse).ToArray();
        var result1 = Run(nums, 12, 2);
        var result2 = 0;
        for (var noun = 0; noun <= 99; noun++)
        {
            for (var verb = 0; verb <= 99; verb++)
            {
                if (Run(nums, noun, verb) == 19690720)
                {
                    result2 = 100 * noun + verb;
                    goto End;
                }
            }
        }
        End:
        return new AocDayResult(result1, result2);
    }

    internal long Run(int[] nums, int noun, int verb)
    {
        nums = [..nums];
        nums[1] = noun;
        nums[2] = verb;
        var intCode = new Intcode(nums);
        intCode.Execute(nums);
        return intCode.ProgramMemory[0];
    }
}
