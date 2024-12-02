using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,2)]
public class Day2 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var line in input)
        {
            var numbers = line.Split().Select(int.Parse).ToArray();
            if (IsSafe(numbers))
                result1++;
            if (numbers.Select((_, i) => (int[])[..numbers[..i], ..numbers[(i + 1)..]]).Any(IsSafe))
                result2++;
        }

        return new AocDayResult(result1, result2);

        bool IsSafe(int[] arr)
        {
            var diffs = new int[arr.Length-1];
            for (var i = 0; i < diffs.Length; i++)
                diffs[i] = arr[i] - arr[i+1];
            return diffs.All(diff => diff is > 0 and <= 3)
                   || diffs.All(diff => diff is < 0 and >= -3);
        }
    }
}
