using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,22)]
public class Day22 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var nodes = new Dictionary<Coord, Info>();
        for (var index = 2; index < input.Length; index++)
        {
            var sp = input[index].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var spDirMatch = Regex.Match(sp[0], @".*-x(\d+)-y(\d+)$");
            var coord = new Coord
            {
                X = int.Parse(spDirMatch.Groups[1].Value),
                Y = int.Parse(spDirMatch.Groups[2].Value)
            };
            var info = new Info
            {
                Size = int.Parse(sp[1][..^1]),
                Used = int.Parse(sp[2][..^1])
            };
            nodes[coord] = info;
        }

        foreach (var (aCoord, aInfo) in nodes)
        {
            foreach (var (bCoord, bInfo) in nodes)
            {
                if (aCoord == bCoord)
                    continue;
                if (aInfo.Used != 0 && aInfo.Used <= bInfo.Size-bInfo.Used)
                    result1++;
            }
        }

        var result2 = 0;

        var maxX = nodes.Keys.Max(c => c.X);
        var maxY = nodes.Keys.Max(c => c.Y);
        
        Coord emptyStart = nodes.First(kv => kv.Value.Used == 0).Key;
        Coord goalStart = new Coord(maxX, 0);

        int Mdist(Coord a, Coord b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        int Heuristic(Coord e, Coord g) => Mdist(e, g) + Mdist(g, new(0, 0));

        var pq = new PriorityQueue<(Coord Empty, Coord Goal), (int Cost, int Dist)>();
        pq.Enqueue((emptyStart,goalStart),(Heuristic(emptyStart,goalStart), 1));

        var minDist = new Dictionary<(Coord empty, Coord goal), int>() { [(emptyStart, goalStart)] = 0 };
        var visited = new HashSet<(Coord, Coord)>();
        var dist = 1;
        while (pq.Count > 0)
        {
            var (empty, goal) = pq.Dequeue();
            if (goal is { X: 0, Y: 0 })
            {
                result2 = minDist[(empty,goal)];
                break;
            }
            visited.Add((empty, goal));
            foreach (var neigh in GetNeighbours(empty,goal,maxX,maxY))
            {
                if (visited.Contains(neigh))
                    continue;
                var (ne, ng) = neigh;
                if (nodes[ne].Size > 100)
                    continue;
                var nDist = minDist[(empty,goal)] + 1;
                if (minDist.TryGetValue(neigh, out var cDist) && cDist <= nDist)
                    continue;
                minDist[neigh] = nDist;
                pq.Enqueue((ne,ng),(nDist + Heuristic(ne,ng), dist+1));
            }
            dist++;
        }

        // Print(nodes, maxX, maxY);
        
        return new AocDayResult(result1, result2);
    }

    private IEnumerable<(Coord,Coord)> GetNeighbours(Coord e, Coord g, int maxX, int maxY)
    {
        var diffs = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
        foreach (var (dx,dy) in diffs)
        {
            var nx = e.X + dx;
            var ny = e.Y + dy;
            if (nx < 0 || nx > maxX || ny < 0 || ny > maxY)
                continue;
            var ne = new Coord(nx, ny);
            if (ne == g)
                yield return (g, e);
            else
                yield return (ne, g);
        }
    }

    struct Coord
    {
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public bool Equals(Coord other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coord other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Coord left, Coord right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coord left, Coord right)
        {
            return !left.Equals(right);
        }
    }
    
    struct Info
    {
        public int Size { get; set; }
        public int Used { get; set; }

        public override string ToString()
        {
            return $"({Used}/{Size})";
        }
    }

    private void Print(Dictionary<Coord,Info> nodes, int maxX, int maxY)
    {
        for (int j = 0; j <= maxY; j++)
        {
            for (int i = 0; i <= maxX; i++)
            {
                var node = nodes[new Coord() { X = i, Y = j }];
                if (node.Size > 500)
                    Console.Write('#');
                else if (node.Used == 0)
                    Console.Write('_');
                else if (i == maxX && j == 0)
                    Console.Write('G');
                else
                    Console.Write('.');
            }
            Console.WriteLine();
        }
    }
    
}
