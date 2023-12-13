namespace AdventOfCode2023;

[AocDay(2023, 13)]
public class Day13 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        for (var index = 0; index < input.Length; index++)
        {
            var arr = new List<char[]>();

            while (index < input.Length && !string.IsNullOrEmpty(input[index]))
            {
                arr.Add(input[index].ToCharArray());
                index++;
            }

            result1 += SolvePart1(arr.ToArray());
            result2 += SolvePart2(arr.ToArray());
        }

        return new AocDayResult(result1, result2);
    }

    private static int SolvePart2(char[][] arr)
    {
        var reflectionLine = FindReflectionLine(arr);
        for (var i = 0; i < arr.Length; i++)
        {
            for (var j = 0; j < arr[0].Length; j++)
            {
                arr[i][j] = arr[i][j] == '#' ? '.' : '#';
                var newReflectionLine = FindReflectionLine(arr, previousLine: reflectionLine);
                if (newReflectionLine != reflectionLine && newReflectionLine != (-1, -1))
                    return CountResult(newReflectionLine);
                arr[i][j] = arr[i][j] == '#' ? '.' : '#';
            }
        }

        return 0;
    }

    private static int SolvePart1(char[][] arr)
    {
        var line = FindReflectionLine(arr);
        return CountResult(line);
    }

    private static int CountResult((int RowIx, int COlIx) line)
    {
        return line.RowIx >= 0 ? (line.RowIx + 1) * 100 : (line.COlIx + 1);
    }

    private static (int RowIx, int COlIx) FindReflectionLine(char[][] arr, (int, int) previousLine = default)
    {
        (int RowIx, int ColIx) defaultLine = (-1, -1);
        for (var i = 0; i < arr.Length - 1; i++)
        {
            var i1 = i;
            var i2 = i + 1;
            var found = true;
            while (i1 >= 0 && i2 < arr.Length)
            {
                for (int j1 = 0, j2 = 0; j1 < arr[0].Length && j2 < arr[0].Length; j1++, j2++)
                {
                    if (arr[i1][j1] != arr[i2][j2])
                    {
                        found = false;
                        break;
                    }
                }

                if (!found)
                    break;

                i1--;
                i2++;
            }

            if (found && (i1 < 0 || i2 >= arr.Length))
            {
                var line = defaultLine with { RowIx = i };
                if (line != previousLine)
                    return line;
            }
        }

        for (var j = 0; j < arr[0].Length - 1; j++)
        {
            var j1 = j;
            var j2 = j + 1;
            var found = true;
            while (j1 >= 0 && j2 < arr[0].Length)
            {
                for (int i1 = 0, i2 = 0; i1 < arr.Length && i2 < arr.Length; i1++, i2++)
                {
                    if (arr[i1][j1] != arr[i2][j2])
                    {
                        found = false;
                        break;
                    }
                }

                if (!found)
                    break;

                j1--;
                j2++;
            }

            if (found && (j1 < 0 || j2 >= arr[0].Length))
            {
                var line = defaultLine with { ColIx = j };
                if (line != previousLine)
                    return line;
            }
        }

        return defaultLine;
    }
}
