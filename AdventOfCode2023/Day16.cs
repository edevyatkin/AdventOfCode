namespace AdventOfCode2023;

[AocDay(2023, 16)]
public class Day16 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var contraption = new Contraption(input);
        contraption.AddBeam(0, 0, BDirection.Right);
        contraption.Light();
        var result1 = contraption.EnergizedTails;

        var result2 = 0;
        for (var i = 0; i < input.Length; i++)
        {
            result2 = Math.Max(result2, CountEnergizedTiles(input, i, 0, BDirection.Right));
            result2 = Math.Max(result2, CountEnergizedTiles(input, i, input[0].Length - 1, BDirection.Left));
        }

        for (var j = 0; j < input[0].Length; j++)
        {
            result2 = Math.Max(result2, CountEnergizedTiles(input, 0, j, BDirection.Bottom));
            result2 = Math.Max(result2, CountEnergizedTiles(input, input.Length - 1, j, BDirection.Top));
        }

        return new AocDayResult(result1, result2);
    }

    private static int CountEnergizedTiles(string[] input, int si, int sj, BDirection direction)
    {
        var contraption = new Contraption(input);
        contraption.AddBeam(si, sj, direction);
        contraption.Light();
        return contraption.EnergizedTails;
    }
}

public enum BDirection
{
    Top,
    Bottom,
    Left,
    Right
}

public class Contraption
{
    private static string[] _grid = Array.Empty<string>();
    private List<Beam> _beams;
    private readonly Dictionary<Tile, List<BDirection>> _tiles;
    public int EnergizedTails => _tiles.Keys.Count;

    public Contraption(string[] input)
    {
        _grid = input;
        _beams = new();
        _tiles = new();
    }

    public void Light()
    {
        while (_beams.Count > 0)
        {
            var newBeams = new List<Beam>();
            foreach (var beam in _beams)
            {
                var tile = new Tile(beam.I, beam.J);

                _tiles.TryAdd(tile, new());
                if (_tiles[tile].Contains(beam.Direction))
                    continue;

                _tiles[tile].Add(beam.Direction);

                newBeams.AddRange(beam
                    .Light()
                    .Where(b => b.I >= 0
                                && b.I != _grid.Length
                                && b.J >= 0
                                && b.J != _grid[0].Length));
            }

            _beams = newBeams;
        }
    }

    private readonly record struct Beam(int I, int J, BDirection Direction)
    {
        public IEnumerable<Beam> Light()
        {
            switch (_grid[I][J])
            {
                case '-':
                    if (Direction is BDirection.Left or BDirection.Right)
                        yield return Redirect(Direction);

                    yield return Redirect(BDirection.Left);
                    yield return Redirect(BDirection.Right);
                    break;

                case '|':
                    if (Direction is BDirection.Top or BDirection.Bottom)
                        yield return Redirect(Direction);

                    yield return Redirect(BDirection.Top);
                    yield return Redirect(BDirection.Bottom);
                    break;

                case '/':
                    yield return Direction switch {
                        BDirection.Top => Redirect(BDirection.Right),
                        BDirection.Bottom => Redirect(BDirection.Left),
                        BDirection.Left => Redirect(BDirection.Bottom),
                        BDirection.Right => Redirect(BDirection.Top),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;

                case '\\':
                    yield return Direction switch {
                        BDirection.Top => Redirect(BDirection.Left),
                        BDirection.Bottom => Redirect(BDirection.Right),
                        BDirection.Left => Redirect(BDirection.Top),
                        BDirection.Right => Redirect(BDirection.Bottom),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;

                default:
                    yield return Redirect(Direction);
                    break;
            }
        }

        private Beam Redirect(BDirection direction)
        {
            (int Di, int Dj) diffs = direction switch {
                BDirection.Right => (0, 1),
                BDirection.Top => (-1, 0),
                BDirection.Bottom => (1, 0),
                BDirection.Left => (0, -1),
                _ => throw new ArgumentOutOfRangeException()
            };
            return new(I + diffs.Di, J + diffs.Dj, direction);
        }
    }

    public void AddBeam(int i, int j, BDirection direction)
    {
        _beams.Add(new Beam(i, j, direction));
    }

    private record struct Tile(int I, int J);
}
