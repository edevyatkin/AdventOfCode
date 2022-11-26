using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,8)]
public class Day8 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var s in input)
        {
            int symbolsCount1 = 0;
            int symbolsCount2 = 0;
            int i = 0;
            while (i < s.Length)
            {
                if (s[i] == '\\')
                {
                    if (s[i + 1] == 'x')
                    {
                        i += 4;
                        symbolsCount2 += 5;
                    }
                    else
                    {
                        i += 2;
                        symbolsCount2 += 4;
                    }
                }
                else if (s[i] == '"')
                {
                    i++;
                    symbolsCount2 += 2;
                }
                else
                {
                    i++;
                    symbolsCount2++;
                }
                symbolsCount1++;
            }
            result1 += s.Length - symbolsCount1 + 2;
            result2 += symbolsCount2 - s.Length + 2;
        }
        return new AocDayResult(result1, result2);
    }
}
