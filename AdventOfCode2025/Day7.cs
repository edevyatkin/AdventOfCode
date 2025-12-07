using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        
        var row = new long[input.Length];
        row[input[0].IndexOf('S')] = 1;
        
        for (var i = 2; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (row[j] == 0 || input[i][j] != '^') 
                    continue;
                result1++;
                row[j - 1] += row[j];
                row[j + 1] += row[j];
                row[j] = 0;
            }
        }

        result2 = row.Sum();
        
        return new AocDayResult(result1, result2);
    }
}
