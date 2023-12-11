namespace AdventOfCode2023;

[AocDay(2023, 11)]
public class Day11 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;

        var space = input.Select(l => l.ToCharArray()).ToArray();

        var expandedRows = new List<int>();
        for (var i = 0; i < space.Length; i++)
            if (space[i].All(c => c == '.'))
                expandedRows.Add(i);

        var expandedColumns = new List<int>();
        for (var j = 0; j < space[0].Length; j++)
            if (space.Select(l => l[j]).All(c => c == '.'))
                expandedColumns.Add(j);

        var galaxies = new List<(int, int)>();
        for (var i = 0; i < space.Length; i++)
            for (var j = 0; j < space[0].Length; j++)
                if (space[i][j] == '#')
                    galaxies.Add((i, j));

        for (var i = 0; i < galaxies.Count; i++)
        {
            for (var j = i + 1; j < galaxies.Count; j++)
            {
                result1 += Dist(i, j, 2);
                result2 += Dist(i, j, 1_000_000);
            }
        }

        long Dist(int g1, int g2, long expandSize)
        {
            var (i1, j1) = galaxies[g1];
            var (i2, j2) = galaxies[g2];
            var rc = expandedRows.Count(i => i1 < i && i < i2 || i2 < i && i < i1);
            var cc = expandedColumns.Count(j => j1 < j && j < j2 || j2 < j && j < j1);
            return Math.Abs(i1 - i2) + Math.Abs(j1 - j2) + cc * (expandSize - 1) + rc * (expandSize - 1);
        }

        return new AocDayResult(result1, result2);
    }
}
