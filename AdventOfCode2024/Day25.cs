using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,25)]
public class Day25 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var keys = new List<int[]>();
        var locks = new List<int[]>();

        for (var i = 0; i < input.Length; i += 8)
        {
            var heights = new int[5];
            for (var j = 0; j < 5; j++)
                for (var ii = i + 1; ii <= i + 5; ii++)
                    if (input[ii][j] == '#')
                        heights[j]++;
            if (input[i][0] == '#')
                keys.Add(heights);
            else 
                locks.Add(heights);
        }

        foreach (var key in keys)
            foreach (var lck in locks)
                if (key.Zip(lck, (x, y) => x + y).All(r => r <= 5))
                    result1++;

        return new AocDayResult(result1, result2);
    }
}
