using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,2)]
public class Day2 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var winD = new Dictionary<char, List<(char Shape, int Score)>>
        {
            ['A'] = new() { ('X', 3), ('Y', 6), ('Z', 0) },
            ['B'] = new() { ('X', 0), ('Y', 3), ('Z', 6) },
            ['C'] = new() { ('X', 6), ('Y', 0), ('Z', 3) }
        };
        
        foreach (var s in input)
        {
            var round = s.Split(" ").Select(char.Parse).ToArray();
            var data = winD[round[0]].First(v => v.Shape == round[1]);
            result1 += data.Score + (data.Shape - 'W');
            result2 += round[1] switch
            {
                'X' => (winD[round[0]].MinBy(v => v.Score).Shape - 'W'),
                'Y' => (winD[round[0]].First(v => v.Score == 3).Shape - 'W') + 3,
                _ => (winD[round[0]].MaxBy(v => v.Score).Shape - 'W') + 6
            };
        }

        return new AocDayResult(result1, result2);
    }
}
