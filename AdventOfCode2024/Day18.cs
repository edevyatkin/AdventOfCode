using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,18)]
public class Day18 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = (int) SolvePart(input, 71, 71, 1024, false);
        var result2 = (string) SolvePart(input, 71, 71, 1024, true);
        
        return new AocDayResult(result1, result2);
    }

    private int[][] CreateGrid(int width, int height)
    {
        var grid = new int[height][];
        for (var y = 0; y < height; y++)
        {
            grid[y] = new int[width];
            Array.Fill(grid[y], int.MaxValue);
        }
        grid[0][0] = 0;
        return grid;
    }

    public object SolvePart(string[] input, int gridWidth, int gridHeight, int bytesCount, bool isPartTwo)
    {
        (int X, int Y)[] bytes = input
            .Select(line => line.Split(',').Select(int.Parse).ToArray())
            .Select(a => (a[0], a[1]))
            .ToArray();
        var dirs = new (int Di, int Dj)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        int i = bytesCount-1, j = bytes.Length-1;
        while (i <= j)
        {
            var mid = i + (j - i) / 2;
            if (!isPartTwo)
                mid = i;
            var b = bytes[mid];
            var fallenBytes = new HashSet<(int X, int Y)>(bytes[..(mid+1)]);
            var dists = CreateGrid(gridWidth, gridHeight);
            var pq = new PriorityQueue<(int X, int Y, int Len), int>();
            pq.Enqueue((0, 0, 0), 0);
            while (pq.Count > 0)
            {
                var (x, y, len) = pq.Dequeue();
                if (len > dists[x][y])
                    continue;
                foreach (var (di, dj) in dirs)
                {
                    var (ni, nj) = (y + di, x + dj);
                    if (ni < 0 || ni >= gridHeight || nj < 0 || nj >= gridWidth)
                        continue;
                    if (fallenBytes.Contains((nj,ni)))
                        continue;
                    if (dists[nj][ni] > len + 1)
                    {
                        dists[nj][ni] = len + 1;
                        pq.Enqueue((nj, ni, len + 1), len + 1);
                    }
                }
                dists[x][y] = -dists[x][y]; // mark visited
            }
            
            if (!isPartTwo)
                return -dists[gridWidth-1][gridHeight-1]; // inverse mark

            if (dists[gridWidth-1][gridHeight-1] == int.MaxValue)
            {
                if (i == j)
                    return $"{b.X},{b.Y}";
                j = mid;
            }
            else
            {
                i = mid + 1;
            }
        }

        return default;
    }
}
