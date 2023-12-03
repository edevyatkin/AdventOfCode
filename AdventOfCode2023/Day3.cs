namespace AdventOfCode2023;

[AocDay(2023,3)]
public class Day3 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var numbers = new Dictionary<(int, int), int>();
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var inNumber = false;
            int si = 0, sj = 0, num = 0;
            for (var j = 0; j < line.Length; j++)
            {
                if (char.IsDigit(line[j]))
                {
                    if (!inNumber)
                    {
                        si = i;
                        sj = j;
                    }
                    inNumber = true;
                    num = num * 10 + (line[j]- '0');
                }
                else if (inNumber)
                {
                    numbers[(si, sj)] = num;
                    num = 0;
                    inNumber = false;
                }
            }

            if (inNumber) 
                numbers[(si, sj)] = num;
        }

        var arr = input.Select(r => r.ToCharArray()).ToArray();
        for (var i = 0; i < arr.Length; i++)
        {
            for (var j = 0; j < arr[0].Length; j++)
            {
                if (!char.IsDigit(arr[i][j]) && arr[i][j] is not '.' or ' ')
                    Dfs(arr, i, j,default, false);
            }
        }

        var result1 = numbers.Where(kv => arr[kv.Key.Item1][kv.Key.Item2] == ' ')
            .Sum(kv => kv.Value);
        
        var arr2 = input.Select(r => r.ToCharArray()).ToArray();
        var result2 = 0;

        for (var i = 0; i < arr.Length; i++)
        {
            for (var j = 0; j < arr[0].Length; j++)
            {
                if (arr2[i][j] != '*') 
                    continue;
                var partsCoords = new HashSet<(int, int)>();
                Dfs(arr2, i, j, partsCoords, true);
                var parts = numbers.Keys.Where(c => partsCoords.Contains(c)).ToArray();
                if (parts.Length == 2) 
                    result2 += numbers[parts[0]] * numbers[parts[1]];
            }
        }
        
        return new AocDayResult(result1, result2);
    }
    
    private static void Dfs(char[][] arr, int i, int j, HashSet<(int,int)> partsCoords, bool isPartTwo)
    {
        if (isPartTwo)
            partsCoords.Add((i,j));
        arr[i][j] = ' ';
        for (var di = -1; di <= 1; di++)
        {
            for (var dj = -1; dj <= 1; dj++)
            {
                if (di == 0 && dj == 0)
                    continue;
                var (ni, nj) = (i + di, j + dj);
                if (ni < 0 || ni == arr.Length || nj < 0 || nj == arr[0].Length || arr[ni][nj] is '.' or ' ')
                    continue;
                Dfs(arr, ni, nj, partsCoords, isPartTwo);
            }
        }
    }
}
