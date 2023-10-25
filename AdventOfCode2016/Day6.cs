using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,6)]
public class Day6 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = string.Empty;
        var result2 = string.Empty;
        var messageLength = input[0].Length;
        var counts = new Dictionary<char,int>[messageLength];
        foreach (var message in input)
        {
            for (var i = 0; i < message.Length; i++)
            {
                counts[i] ??= new();
                counts[i][message[i]] = counts[i].GetValueOrDefault(message[i]) + 1;
            }
        }
        for (var i = 0; i < counts.Length; i++)
        {
            var c1 = counts[i].MaxBy(kv => kv.Value).Key;
            result1 += c1;
            var c2 = counts[i].MinBy(kv => kv.Value).Key;
            result2 += c2;
        }
        return new AocDayResult(result1, result2);
    }
}
