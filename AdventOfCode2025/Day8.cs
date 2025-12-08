using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 8)]
public class Day8 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input, 1000);
        var result2 = SolvePart2(input);
        
        return new AocDayResult(result1, result2);
    }

    internal static long SolvePart1(string[] input, int count)
    {
        var boxes = ParseInput(input);
        var pq = new PriorityQueue<(Box b1, Box b2), long>();
        for (var i = 0; i < boxes.Length-1; i++)
            for (var j = i + 1; j < boxes.Length; j++)
                pq.Enqueue((boxes[i], boxes[j]), Dist2(boxes[i], boxes[j]));
        
        var uf = new UnionFind(boxes.Length);
        
        while (count > 0)
        {
            var (b1,b2) = pq.Dequeue();
            uf.Union(b1.Ix, b2.Ix);
            count--;
        }

        var counts = new Dictionary<int, int>();
        for (var i = 0; i < boxes.Length; i++)
            counts[uf[i]] = counts.GetValueOrDefault(uf[i]) + 1;
        return counts.Values.OrderByDescending(n => n).Take(3).Aggregate(1L, (s, v) => s * v);
    }
    
    internal static long SolvePart2(string[] input)
    {
        var boxes = ParseInput(input);
        var pq = new PriorityQueue<(Box b1, Box b2), long>();
        for (var i = 0; i < boxes.Length-1; i++)
            for (var j = i + 1; j < boxes.Length; j++)
                pq.Enqueue((boxes[i], boxes[j]), Dist2(boxes[i], boxes[j]));
        
        var uf = new UnionFind(boxes.Length);
        var (b1, b2) = (boxes[0], boxes[1]);
        
        while (Enumerable.Range(0, boxes.Length).Select(n => uf[n]).Distinct().Count() > 1)
        {
            (b1, b2) = pq.Dequeue();
            uf.Union(b1.Ix, b2.Ix);
        }

        return b1.X * b2.X;
    }

    private static Box[] ParseInput(string[] input)
    {
        var boxes = new Box[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var sp = line.Split(',').Select(long.Parse).ToArray();
            boxes[i] = new Box(sp[0], sp[1], sp[2], i);
        }
        return boxes;
    }

    static long Dist2(Box b1, Box b2) =>
          Math.Abs(b1.X - b2.X) * Math.Abs(b1.X - b2.X) + 
          Math.Abs(b1.Y - b2.Y) * Math.Abs(b1.Y - b2.Y) + 
          Math.Abs(b1.Z - b2.Z) * Math.Abs(b1.Z - b2.Z);
}

public record Box(long X, long Y, long Z, int Ix);

public class UnionFind
{
    private readonly int[] _uf;
    private readonly int[] _ranks;
    public int this[int i] => Find(_uf[i]);
    public int Count => _uf.Length;

    public UnionFind(int n)
    {
        _uf = new int[n];
        foreach (var i in Enumerable.Range(0, n))
            _uf[i] = i;
        _ranks = new int[n];
    }
    
    public int Find(int n)
    {
        if (_uf[n] != n)
            return _uf[n] = Find(_uf[n]);
        return _uf[n];
    }

    public void Union(int n1, int n2)
    {
        n1 = Find(n1);
        n2 = Find(n2);
        if (n1 == n2)
            return;
        if (_ranks[n1] < _ranks[n2])
            (n1, n2) = (n2, n1);
        _uf[n2] = n1;
        if (_ranks[n1] == _ranks[n2])
            _ranks[n1]++;
    }
}
