using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,18)]
public class Day18 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = CountLights(input, 100, false);
        var result2 = CountLights(input, 100, true);
        
        return new AocDayResult(result1, result2);
    }

    public int CountLights(string[] input, int steps, bool isPartTwo)
    {
        var grid = input.Select(l => l.Select(e => e == '#').ToArray()).ToArray();

        if (isPartTwo)
        {
            grid[0][0] = grid[0][^1] = grid[^1][0] = grid[^1][^1] = true;
        }
        
        while (--steps >= 0)
        {
            var grid2 = new bool[input.Length][];
            for (var i = 0; i < grid.Length; i++)
            {
                grid2[i] = new bool[input[0].Length];
            }
            
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[0].Length; j++)
                {
                    var neighboursLights = 0;
                    for (int d1 = -1; d1 <= 1; d1++)
                    {
                        for (int d2 = -1; d2 <= 1; d2++)
                        {
                            if (d1 == 0 && d2 == 0)
                            {
                                continue;
                            }

                            int ix = i + d1;
                            int jx = j + d2;

                            if (ix < 0 || ix == grid.Length || jx < 0 || jx == grid[0].Length)
                            {
                                continue;
                            }

                            neighboursLights += grid[ix][jx] ? 1 : 0;
                        }                        
                    }

                    if (grid[i][j])
                    {
                        grid2[i][j] = neighboursLights is 2 or 3;
                    }
                    else
                    {
                        grid2[i][j] = neighboursLights is 3;
                    }
                }
            }
            
            if (isPartTwo)
            {
                grid2[0][0] = grid2[0][^1] = grid2[^1][0] = grid2[^1][^1] = true;
            }
            
            grid = grid2;
        }

        return grid.SelectMany(g => g).Count(e => e);
    }
}
