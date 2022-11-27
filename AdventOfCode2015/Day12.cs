using System.Text;
using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,12)]
public class Day12 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var json = input[0];

        var result1 = GetSum(json);

        var ix = 0;
        var result2 = GetSumWithoutRed(json, ref ix);

        return new AocDayResult(result1, result2);
    }
    
    private int GetSum(string json)
    {
        return Regex.Replace(json,"([^-,0-9]|[a-z])", string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Sum();
    }
    
    private int GetSumWithoutRed(string json, ref int ix)
    {
        var clBrace = json[ix] == '{' ? '}' : ']';
        var sb = new StringBuilder();
        while (++ix < json.Length && json[ix] != clBrace)
        {
            if (json[ix] == '{' || json[ix] == '[')
            {
                sb.Append(GetSumWithoutRed(json, ref ix));
            }
            else
            {
                sb.Append(json[ix]);
            }
        }
        var str = sb.ToString();
        return !str.Contains("red") || clBrace == ']' ? GetSum(str): 0;
    }
}
