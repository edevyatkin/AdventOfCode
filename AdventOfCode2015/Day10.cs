using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,10)]
public class Day10 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var line = input[0];
        
        var result1 = BuildString(line, 40);
        var result2 = BuildString(line, 50);

        return new AocDayResult(result1, result2);
    }

    public int BuildString(string line, int times)
    {
        while (--times >= 0)
        {
            var sb = new StringBuilder();
            var count = 1;
            for (var i = 1; i < line.Length; i++)
            {
                if (line[i] == line[i - 1])
                {
                    count++;
                }
                else
                {
                    sb.Append($"{count}{line[i - 1]}");
                    count = 1;
                }
            }

            sb.Append($"{count}{line[^1]}");
            line = sb.ToString();
        }

        return line.Length;
    }
}
