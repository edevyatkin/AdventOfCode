using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,1)]
public class Day1 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var total = 0;
        var totalList = new List<int>();
        foreach (var s in input)
        {
            if (s != string.Empty)
            {
                total += int.Parse(s);
            }
            else
            {
                totalList.Add(total);
                total = 0;
            }
        }
        totalList.Add(total);
        
        var result1 = totalList.Max();
        var result2 = totalList.OrderByDescending(x => x).Take(3).Sum();
        
        return new AocDayResult(result1, result2);
    }
}
