using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,3)]
public partial class Day3 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        
        var mulRegex = MulRegex();
        var doRegex = DoRegex();
        var dontRegex = DontRegex();
        
        var line = string.Join('0', input);
        
        foreach (Match m in mulRegex.Matches(line))
        {
            result1 += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
        }
        
        var matches = new List<Match>();
        matches.AddRange(mulRegex.Matches(line));
        matches.AddRange(doRegex.Matches(line));
        matches.AddRange(dontRegex.Matches(line));
        matches.Sort((m1,m2) => m1.Index.CompareTo(m2.Index));
        var enabled = true;
        foreach (var match in matches)
        {
            switch (match.Value[..3])
            {
                case "do(":
                    enabled = true;
                    break;
                case "don":
                    enabled = false;
                    break;
                default:
                    if (enabled)
                        result2 += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                    break;
            }
        }

        return new AocDayResult(result1, result2);
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)", RegexOptions.Compiled)]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"do\(\)", RegexOptions.Compiled)]
    private static partial Regex DoRegex();

    [GeneratedRegex(@"don't\(\)", RegexOptions.Compiled)]
    private static partial Regex DontRegex();
}
