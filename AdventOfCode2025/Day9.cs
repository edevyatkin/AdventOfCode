using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 9)]
public class Day9 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        
        var tiles = new List<Point>();
        
        foreach (var line in input) 
        {
            var sp = line.Split(',').Select(int.Parse).ToArray();
            tiles.Add(new (sp[0], sp[1]));
        }
        
        for (var i = 0; i < tiles.Count-1; i++)
        {
            for (var j = i + 1; j < tiles.Count; j++)
            {
                result1 = Math.Max(result1,
                    Math.Abs((long)(tiles[i].X - tiles[j].X) + 1) * (Math.Abs(tiles[i].Y - tiles[j].Y) + 1));
            }
        }
        
        var xs = tiles
            .Select(t => t.X)
            .Distinct()
            .OrderBy(x => x)
            .Select((x,i) => new { X  = x, Ix = i })
            .ToDictionary(xi => xi.X, xi => xi.Ix);
        var ys = tiles
            .Select(t => t.Y)
            .Distinct()
            .OrderBy(y => y)
            .Select((y,i) => new { Y  = y, Ix = i })
            .ToDictionary(yi => yi.Y, yi => yi.Ix);

        var grid = new bool[ys.Count][];
        for (var y = 0; y < ys.Count; y++)
            grid[y] = new bool[xs.Count];

        var redTilesCount = tiles.Count;
        var edges = new List<(Point Start, Point End)>();
        for (var ti = 0; ti < redTilesCount; ti++)
        {
            var a = tiles[ti];
            var b = tiles[(ti + 1) % redTilesCount];
            edges.Add((a, b));
            var axi = xs[a.X];
            var ayi = ys[a.Y];
            var bxi = xs[b.X];
            var byi = ys[b.Y];

            if (axi == bxi)
            {
                for (var yi = Math.Min(ayi, byi) + 1; yi < Math.Max(ayi, byi); yi++)
                {
                    grid[yi][axi] = true;
                }
            }
            else if (ayi == byi)
            {
                for (var xi = Math.Min(axi, bxi) + 1; xi < Math.Max(axi, bxi); xi++)
                {
                    grid[ayi][xi] = true;
                }
            }
            grid[ayi][axi] = true;
            grid[byi][bxi] = true;
        }

        foreach (var (origX, x) in xs)
        {
            foreach (var (origY, y) in ys)
            {
                if (grid[y][x])
                    continue;
                var intersections = edges
                    .Select((e, i) => new { Edge = e, Index = i })
                    .Where(e => e.Edge.Start.X == e.Edge.End.X)
                    .Sum(e => 
                        origX < e.Edge.Start.X && 
                        (
                            origY > Math.Min(e.Edge.Start.Y, e.Edge.End.Y) && origY < Math.Max(e.Edge.Start.Y, e.Edge.End.Y)
                            || 
                            origY == e.Edge.End.Y && origY > Math.Min(e.Edge.Start.Y, edges[(e.Index + 2) % edges.Count].End.Y) 
                                                  && origY < Math.Max(e.Edge.Start.Y, edges[(e.Index + 2) % edges.Count].End.Y)
                        ) ? 1 : 0);
                if (intersections % 2 == 1)
                    grid[y][x] = true;
            }
        }

        var prefixSum = new long[ys.Count + 1][];
        for (var yi = 0; yi < ys.Count + 1; yi++)
            prefixSum[yi] = new long[xs.Count + 1];

        for (var yi = 0; yi < ys.Count; yi++)
            for (var xi = 0; xi < xs.Count; xi++)
                prefixSum[yi + 1][xi + 1] = prefixSum[yi][xi + 1] + prefixSum[yi + 1][xi] - prefixSum[yi][xi] + (grid[yi][xi] ? 1 : 0);

        for (var ti = 0; ti < redTilesCount; ti++)
        {
            for (var tj = ti + 1; tj < redTilesCount; tj++)
            {
                var axi = xs[tiles[ti].X];
                var ayi = ys[tiles[ti].Y];
                var bxi = xs[tiles[tj].X];
                var byi = ys[tiles[tj].Y];
                
                (axi, bxi) = (Math.Min(axi, bxi), Math.Max(axi, bxi));
                (ayi, byi) = (Math.Min(ayi, byi), Math.Max(ayi, byi));
                
                var currentAreaOnGrid = prefixSum[byi + 1][bxi + 1] - prefixSum[ayi][bxi + 1] - prefixSum[byi + 1][axi] + prefixSum[ayi][axi];
                var needAreaOnGrid = (long)(bxi - axi + 1) * (byi - ayi + 1);
                if (currentAreaOnGrid == needAreaOnGrid)
                {
                    var origArea = (long)(Math.Abs(tiles[ti].X - tiles[tj].X) + 1) * (Math.Abs(tiles[ti].Y - tiles[tj].Y) + 1);
                    result2 = Math.Max(result2, origArea);
                }
            }
        }
        
        return new AocDayResult(result1, result2);
    }

    private record struct Point(int X, int Y);
}
