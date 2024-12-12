using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,12)]
public class Day12 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        
        var visited = new HashSet<(int, int)>();

        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (visited.Contains((i, j)))
                    continue;
                var perimeter = 0;
                var region = new HashSet<(int, int)>();
                Visit(i, j, input[i][j], region, ref perimeter);
                var area = region.Count;
                result1 += perimeter * area;
                result2 += CountSides(region) * area;
            }
        }
        
        return new AocDayResult(result1, result2);

        void Visit(int i, int j, char regionChar, HashSet<(int, int)> region, ref int perimeter)
        {
            if (visited.Contains((i,j)) || input[i][j] != regionChar)
                return;
            visited.Add((i, j));
            region.Add((i, j));
            var dirs = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
            foreach (var (di, dj) in dirs)
            {
                var (ni, nj) = (i + di, j + dj);
                if (ni < 0 || ni >= input.Length || nj < 0 || nj >= input[0].Length || input[ni][nj] != regionChar)
                {
                    perimeter++;
                    continue;
                }
                Visit(ni, nj, input[ni][nj], region, ref perimeter);
            }
        }

        int CountSides(HashSet<(int I, int J)> region)
        {
            var minI = region.Min(p => p.I);
            var maxI = region.Max(p => p.I);
            var minJ = region.Min(p => p.J);
            var maxJ = region.Max(p => p.J);
            var sides = 0;

            for (var i = minI; i <= maxI; i++)
            {
                var inTopFence = false;
                var inBottomFence = false;
                for (var j = minJ; j <= maxJ; j++)
                {
                    if (!region.Contains((i, j)))
                    {
                        inTopFence = false;
                        inBottomFence = false;
                        continue;
                    }
                    if (i == 0 || input[i - 1][j] != input[i][j])
                    {
                        if (!inTopFence)
                            sides++;
                        inTopFence = true;
                    }
                    else
                    {
                        inTopFence = false;
                    }

                    if (i == input.Length - 1 || input[i + 1][j] != input[i][j])
                    {
                        if (!inBottomFence)
                            sides++;
                        inBottomFence = true;
                    }
                    else
                    {
                        inBottomFence = false;
                    }
                }
            }

            for (var j = minJ; j <= maxJ; j++)
            {
                var inLeftFence = false;
                var inRightFence = false;
                for (var i = minI; i <= maxI; i++)
                {
                    if (!region.Contains((i, j)))
                    {
                        inLeftFence = false;
                        inRightFence = false;
                        continue;
                    }
                        
                    if (j == 0 || input[i][j - 1] != input[i][j])
                    {
                        if (!inLeftFence)
                            sides++;
                        inLeftFence = true;
                    }
                    else
                    {
                        inLeftFence = false;
                    }

                    if (j == input[0].Length - 1 || input[i][j + 1] != input[i][j])
                    {
                        if (!inRightFence)
                            sides++;
                        inRightFence = true;
                    }
                    else
                    {
                        inRightFence = false;
                    }
                }
            }
            return sides;
        }
    }
}
