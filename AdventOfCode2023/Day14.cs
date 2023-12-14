namespace AdventOfCode2023;

[AocDay(2023, 14)]
public class Day14 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var arr = input.Select(r => r.ToCharArray()).ToArray();

        var dish = new Dish(arr);

        var tiltedDish = dish.Tilt(Direction.North);
        var result1 = tiltedDish.CalculateBeamLoadNorth();

        var cache = new Dictionary<(Dish, Direction), (Dish Dish, int C)>();
        var count = 1_000_000_000;
        var dirs = new[] { Direction.North, Direction.West, Direction.South, Direction.East };
        while (count > 0)
        {
            if (cache.ContainsKey((dish, Direction.North)))
            {
                var prevCount = cache[(dish, Direction.North)].C;
                var diff = prevCount - count;
                count %= diff;
            }

            foreach (var dir in dirs)
            {
                if (cache.ContainsKey((dish, dir)))
                {
                    dish = cache[(dish, dir)].Dish;
                }
                else
                {
                    var tmp = dish.Tilt(dir);
                    cache[(dish, dir)] = (tmp, count);
                    dish = tmp;
                }
            }

            count--;
        }

        var result2 = dish.CalculateBeamLoadNorth();

        return new AocDayResult(result1, result2);
    }
}

public enum Direction
{
    North,
    West,
    South,
    East
}

public class Dish : IEquatable<Dish>
{
    public char[][] Platform { get; }

    public Dish(char[][] platform)
    {
        Platform = platform;
    }

    public int CalculateBeamLoadNorth()
    {
        var load = 0;
        for (var i = 0; i < Platform.Length; i++)
        for (var j = 0; j < Platform[0].Length; j++)
            load += (Platform[i][j] == 'O' ? 1 : 0) * (Platform.Length - i);
        return load;
    }

    public Dish Tilt(Direction direction)
    {
        var newPlatform = new char[Platform.Length][];
        for (int i = 0; i < newPlatform.Length; i++)
        {
            newPlatform[i] = new char[Platform[0].Length];
            Platform[i].CopyTo(newPlatform[i], 0);
        }

        for (int i = 0; i < newPlatform.Length; i++)
        {
            for (int j = 0; j < newPlatform.Length; j++)
            {
                switch (direction)
                {
                    case Direction.North:
                        TiltRoundedRocksToNorth(newPlatform, i, j);
                        break;
                    case Direction.West:
                        TiltRoundedRocksToWest(newPlatform, i, j);
                        break;
                    case Direction.South:
                        TiltRoundedRocksToSouth(newPlatform, i, j);
                        break;
                    case Direction.East:
                        TiltRoundedRocksToEast(newPlatform, i, j);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }
        }

        return new Dish(newPlatform);
    }

    private static void TiltRoundedRocksToEast(char[][] plt, int i, int j)
    {
        if (j == plt[0].Length - 1)
            j++;
        else if (plt[i][j] is not '#')
            return;
        var toJx = j - 1;
        while (toJx >= 0)
        {
            if (plt[i][toJx] is not '.')
            {
                toJx--;
                continue;
            }

            var fromJx = toJx - 1;
            while (fromJx >= 0 && plt[i][fromJx] != '#')
            {
                if (plt[i][fromJx] is 'O')
                {
                    (plt[i][fromJx], plt[i][toJx]) = (plt[i][toJx], plt[i][fromJx]);
                }

                fromJx--;
            }

            toJx--;
        }
    }

    private static void TiltRoundedRocksToSouth(char[][] plt, int i, int j)
    {
        if (i == plt.Length - 1)
            i++;
        else if (plt[i][j] is not '#')
            return;
        var toIx = i - 1;
        while (toIx >= 0)
        {
            if (plt[toIx][j] is not '.')
            {
                toIx--;
                continue;
            }

            var fromIx = toIx - 1;
            while (fromIx >= 0 && plt[fromIx][j] != '#')
            {
                if (plt[fromIx][j] is 'O')
                {
                    (plt[fromIx][j], plt[toIx][j]) = (plt[toIx][j], plt[fromIx][j]);
                }

                fromIx--;
            }

            toIx--;
        }
    }

    private static void TiltRoundedRocksToWest(char[][] plt, int i, int j)
    {
        if (j == 0)
            j--;
        else if (plt[i][j] is not '#')
            return;
        var toJx = j + 1;
        while (toJx < plt[0].Length)
        {
            if (plt[i][toJx] is not '.')
            {
                toJx++;
                continue;
            }

            var fromJx = toJx + 1;
            while (fromJx < plt[0].Length && plt[i][fromJx] != '#')
            {
                if (plt[i][fromJx] is 'O')
                {
                    (plt[i][fromJx], plt[i][toJx]) = (plt[i][toJx], plt[i][fromJx]);
                }

                fromJx++;
            }

            toJx++;
        }
    }

    private static void TiltRoundedRocksToNorth(char[][] plt, int i, int j)
    {
        if (i == 0)
            i--;
        else if (plt[i][j] is not '#')
            return;
        var toIx = i + 1;
        while (toIx < plt.Length)
        {
            if (plt[toIx][j] is not '.')
            {
                toIx++;
                continue;
            }

            var fromIx = toIx + 1;
            while (fromIx < plt.Length && plt[fromIx][j] != '#')
            {
                if (plt[fromIx][j] is 'O')
                {
                    (plt[fromIx][j], plt[toIx][j]) = (plt[toIx][j], plt[fromIx][j]);
                }

                fromIx++;
            }

            toIx++;
        }
    }

    public bool Equals(Dish? other)
    {
        if (other is null || Platform.Length != other.Platform.Length
                          || Platform[0].Length != other.Platform[0].Length)
            return false;

        for (var i = 0; i < other.Platform.Length; i++)
        {
            for (var j = 0; j < other.Platform[0].Length; j++)
            {
                if (Platform[i][j] != other.Platform[i][j])
                    return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Dish)obj);
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override string ToString()
    {
        return string.Concat(Platform.SelectMany(l => l));
    }

    public void Dump()
    {
        for (var i = 0; i < Platform.Length; i++)
        {
            for (var j = 0; j < Platform[0].Length; j++)
            {
                Console.Write(Platform[i][j]);
            }

            Console.WriteLine();
        }
    }
}
