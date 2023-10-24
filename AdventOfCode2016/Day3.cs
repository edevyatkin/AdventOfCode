using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,3)]
public class Day3 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var spec3 = new int[3][];
        foreach (var spec in input)
        {
            var sides = spec
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            if (IsTriangle(sides))
                result1++;
        }

        var result2 = 0;
        for (var i = 0; i < input.Length; i += 3)
        {
            var specs = input[i..(i + 3)]
                .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();
            TransposeMatrix(specs);
            result2 += specs.Count(IsTriangle);
        }

        return new AocDayResult(result1, result2);
    }

    bool IsTriangle(int[] sides) =>
        (long)sides[0] + sides[1] > sides[2] &&
        (long)sides[0] + sides[2] > sides[1] &&
        (long)sides[1] + sides[2] > sides[0];

    void TransposeMatrix(int[][] matrix)
    {
        for (var i = 0; i < matrix.Length; i++)
        {
            for (var j = i; j < matrix[0].Length; j++)
            {
                (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);
            }
        }
    }
}
