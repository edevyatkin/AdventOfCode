using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,19)]
public class Day19 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var diagram = new Diagram(input);
        var path = diagram.BuildPath();
        var pathSequence = path.FollowPath().ToArray();
        
        var result1 = string.Concat(
            pathSequence
                .Select(info => info.C)
                .Where(char.IsLetter)
        );
        
        var result2 = pathSequence.Length;

        return new AocDayResult(result1, result2);
    }
}

internal class Diagram
{
    internal char[][] Input { get; }

    internal Diagram(string[] input)
    {
        Input = input.Select(line => line.ToCharArray()).ToArray();
    }
    
    public DiagramPath BuildPath()
    {
        var segments = BuildSegments();
        var pointsSequence = ArrangePathPoints(segments);
        return new DiagramPath(this, pointsSequence);
    }

    private List<Segment> BuildSegments()
    {
        var startIndex = Array.IndexOf(Input[0], '|');
        Input[0][startIndex] = '+';

        var segments = new List<Segment>();
        
        for (var x = 0; x < Input.Length; x++)
            Build(new(x, 0), new(x, Input[0].Length-1));
        
        for (var y = 0; y < Input[0].Length; y++)
            Build(new(0, y), new(Input.Length-1, y));

        void Build(SimplePoint from, SimplePoint to)
        {
            var path = new DiagramPath(this, new List<SimplePoint> { from, to });
            var crossesAndLetters = path
                .FollowPath()
                .Where(info => info.C == '+' || char.IsLetter(info.C))
                .ToArray();
            
            for (var ix = 1; ix < crossesAndLetters.Length; ix++)
            {
                var left = crossesAndLetters[ix - 1];
                var right = crossesAndLetters[ix];
                if (left.Point.X == right.Point.X)
                {
                    if (left.Point.Y+1 == right.Point.Y ||
                        Input[left.Point.X][left.Point.Y + 1] is '-' or '|'
                        && Input[right.Point.X][right.Point.Y - 1] is '-' or '|')
                    {
                        segments.Add(new Segment(left.Point, right.Point));
                    }
                }
                else
                {
                    if (left.Point.X+1 == right.Point.X ||
                        Input[left.Point.X + 1][left.Point.Y] is '-' or '|'
                        && Input[right.Point.X - 1][right.Point.Y] is '-' or '|')
                    {
                        segments.Add(new Segment(left.Point, right.Point));
                    }
                }
            }
        }

        return segments;
    }

    private List<SimplePoint> ArrangePathPoints(List<Segment> segments)
    {
        var pointsMap = new Dictionary<SimplePoint, List<SimplePoint>>();
        foreach (var segment in segments)
        {
            if (!pointsMap.ContainsKey(segment.From))
                pointsMap[segment.From] = new();
            pointsMap[segment.From].Add(segment.To);
            if (!pointsMap.ContainsKey(segment.To))
                pointsMap[segment.To] = new();
            pointsMap[segment.To].Add(segment.From);
        }
        
        var startPoint = pointsMap.First(s => s.Key.X == 0).Key;
        var current = startPoint;
        var previous = SimplePoint.Default;
        var pointSequence = new List<SimplePoint> { current };
        while (true)
        {
            var next = pointsMap[current]
                .FirstOrDefault(p => p != previous, SimplePoint.Default);
            if (next == SimplePoint.Default)
                break;
            pointSequence.Add(next);
            previous = current;
            current = next;
        }

        return pointSequence;
    }
}

internal record struct SimplePoint(int X, int Y)
{
    public static SimplePoint Default => new(-1,-1);
}

internal record Segment(SimplePoint From, SimplePoint To);

internal class DiagramPath
{
    private readonly Diagram _diagram;
    private readonly List<SimplePoint> _pointsSequence;

    internal DiagramPath(Diagram diagram, List<SimplePoint> pointsSequence)
    {
        _diagram = diagram;
        _pointsSequence = pointsSequence;
    }

    public IEnumerable<(char C, SimplePoint Point)> FollowPath()
    {
        var pos = 0;
        while (pos != _pointsSequence.Count - 1)
        {
            var (fr, to) = (_pointsSequence[pos], _pointsSequence[pos + 1]);
            foreach (var c in Follow(fr, to))
                yield return c;
            pos++;
        }
        var endPoint = _pointsSequence[^1];
        yield return (_diagram.Input[endPoint.X][endPoint.Y], endPoint);
    }

    private IEnumerable<(char C, SimplePoint Point)> Follow(SimplePoint fr, SimplePoint to)
    {
        IEnumerable<char> letters = null;
        IEnumerable<SimplePoint> points = null;

        if (fr.X == to.X)
        {
            letters = fr.Y < to.Y
                ? _diagram.Input[fr.X][fr.Y..to.Y]
                : _diagram.Input[fr.X][(to.Y+1)..(fr.Y+1)]
                    .Select((c,ix) => (c,ix))
                    .OrderByDescending(l => l)
                    .Select(p => p.c);
            points = fr.Y < to.Y
                ? Enumerable.Range(fr.Y, to.Y - fr.Y)
                    .Select(v => fr with { Y = v })
                : Enumerable.Range(to.Y + 1, fr.Y - to.Y)
                    .OrderByDescending(ix => ix)
                    .Select(v => fr with { Y = v });
        }
        else
        {
            letters = fr.X < to.X 
                ? _diagram.Input[fr.X..to.X].Select(l => l[fr.Y]) 
                : _diagram.Input[(to.X+1)..(fr.X+1)]
                    .Select((l,ix) => (l[fr.Y],ix))
                    .OrderByDescending(p => p.ix)
                    .Select(p => p.Item1);
            points = fr.X < to.X
                ? Enumerable.Range(fr.X, to.X - fr.X)
                    .Select(v => fr with { X = v })
                : Enumerable.Range(to.X + 1, fr.X - to.X)
                    .OrderByDescending(ix => ix)
                    .Select(v => fr with { X = v });
        }
        
        foreach (var c in letters.Zip(points))
            yield return c;
    }
}
