using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,1)]
public class Day1 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var list1 = new List<int>();
        var list2 = new List<int>();
        foreach (var line in input)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            list1.Add(numbers[0]);
            list2.Add(numbers[1]);
        }
        list1.Sort();
        list2.Sort();
        for (var i = 0; i < list1.Count; i++)
        {
            result1 += Math.Abs(list1[i] - list2[i]);
            result2 += list1[i] * list2.Count(x => x == list1[i]);
        }
        
        return new AocDayResult(result1, result2);
    }
}
