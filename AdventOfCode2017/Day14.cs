using System.Numerics;
using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,14)]
public class Day14 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var grid = new char[128][];
        
        for (var i = 0; i < 128; i++)
        {
            var str = $"{input[0]}-{i}";
            var hash = Day10.Hash(str, 256);
            
            result1 += hash.Sum(c => BitOperations.PopCount(HexCharToUInt(c)));
            
            grid[i] = hash.SelectMany(c => Convert.ToString(HexCharToUInt(c), 2)
                .PadLeft(4, '0')).ToArray();
        }

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == '0')
                    continue;
                Dfs(grid, i, j);
                result2++;
            }
        }
        
        return new AocDayResult(result1, result2);
    }

    private static uint HexCharToUInt(char c) => (uint)(char.IsDigit(c) ? c - '0' : 10 + c - 'a');

    private static void Dfs(char[][] grid, int i, int j)
    {
        var dirs = new[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        grid[i][j] = '0';
        foreach (var (di,dj) in dirs)
        {
            var (ni, nj) = (i + di, j + dj);
            if (ni < 0 || ni == grid.Length || nj < 0 || nj == grid[0].Length || grid[ni][nj] == '0')
                continue;
            Dfs(grid,ni,nj);
        }
    }
}
