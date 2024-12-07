using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;

        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var value = long.Parse(parts[0]);
            var numbers = parts[1].Split(' ').Select(int.Parse).ToArray();
            if (IsTrue(numbers, value, isPartTwo: false))
                result1 += value;
            if (IsTrue(numbers, value, isPartTwo: true))
                result2 += value;
        }
        
        return new AocDayResult(result1, result2);
    }
    
    private static bool IsTrue(int[] numbers, long value, bool isPartTwo)
    {
        return Check(1, numbers[0]);
        
        bool Check(int i, long res)
        {
            if (res < 0 || res > value) 
                return false;
            if (i == numbers.Length)
                return res == value;
            return Check(i + 1, res + numbers[i]) || 
                   Check(i + 1, res * numbers[i]) || 
                   isPartTwo && Check(i + 1, res * (int)Math.Pow(10, (int)(Math.Log10(numbers[i])+1)) + numbers[i]);
        }
    }
}
