using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,6)]
public class Day6 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var s = input[0];
        var result1 = FindStart(s, 4);
        var result2 = FindStart(s, 14);

        return new AocDayResult(result1, result2);
    }

    private static int FindStart(string s, int count)
    {
        var result = 0;
        for (var i = 0; i < s.Length - count + 1; i++)
        {
            if (s[i..(i + count)].ToHashSet().Count == count)
            {
                result = i + count;
                break;
            }
        }

        return result;
    }
}
