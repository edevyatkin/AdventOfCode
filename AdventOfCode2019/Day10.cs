using System.Numerics;
using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 10)]
public class Day10 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var (asteroidIndexForStation, result1) = SolvePart1(input);
        var result2 = SolvePart2(input, asteroidIndexForStation, 200);
        
        return new AocDayResult(result1, result2);
    }

    internal static (int AsteroidIndexForStation, int MaxVisibleAsteroids) SolvePart1(string[] input)
    {
        var asteroids = new List<(int,int)>();
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == '#')
                    asteroids.Add((i, j));
            }
        }
        var result = 0;
        var asteroidIndexForStation = 0;
        for (var ix = 0; ix < asteroids.Count; ix++)
        {
            var slopes = new HashSet<(int,int)>();
            for (var jx = 0; jx < asteroids.Count; jx++)
            {
                if (ix == jx) continue;
                var diffI = asteroids[jx].Item1 - asteroids[ix].Item1;
                var diffJ = asteroids[jx].Item2 - asteroids[ix].Item2;
                var gcd = BigInteger.GreatestCommonDivisor(diffI, diffJ);
                var slope = (diffI / (int)gcd, diffJ / (int)gcd);
                slopes.Add(slope);
            }
            
            if (result < slopes.Count)
            {
                asteroidIndexForStation = ix;
                result = slopes.Count;
            }
        }

        return (asteroidIndexForStation, result);
    }

    internal static int SolvePart2(string[] input, int asteroidIndexForStation, int needAsteroidNumber)
    {
        var asteroids = new List<(int,int)>();
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == '#')
                    asteroids.Add((i, j));
            }
        }
        if (needAsteroidNumber > asteroids.Count)
            throw new ArgumentException("needAsteroidNumber must be less than or equal to count of asteroids");
        var (si, sj) = asteroids[asteroidIndexForStation];
        var slopesDict = new Dictionary<(int,int),List<(int,int)>>();
        for (var ix = 0; ix < asteroids.Count; ix++)
        {
            if (asteroidIndexForStation == ix) continue;
            var diffI = asteroids[ix].Item1 - si;
            var diffJ = asteroids[ix].Item2 - sj;
            var gcd = BigInteger.GreatestCommonDivisor(diffI, diffJ);
            var slope = (diffI / (int)gcd, diffJ / (int)gcd);
            if (!slopesDict.ContainsKey(slope))
                slopesDict[slope] = [];
            slopesDict[slope].Add(asteroids[ix]);
        }

        // sort from farthest to nearest asteroid
        foreach (var slopesAsteroids in slopesDict.Values)
        {
            slopesAsteroids.Sort(Comparer<(int,int)>.Create((a, b) => 
                Dist((si,sj), b) - 
                Dist((si,sj), a))
            );
        }

        var comparer = Comparer<(int,int)>.Create((a, b) => 
            Lcm(a.Item2, b.Item2) / a.Item2 * a.Item1 - Lcm(a.Item2, b.Item2) / b.Item2 * b.Item1);
        var topRight = slopesDict.Keys.Where(k => k is { Item1: < 0, Item2: > 0 }).Order(comparer);
        var bottomRight = slopesDict.Keys.Where(k => k is { Item1: > 0, Item2: > 0 }).Order(comparer);
        var bottomLeft = slopesDict.Keys.Where(k => k is { Item1: > 0, Item2: < 0 }).Order(comparer);
        var topLeft = slopesDict.Keys.Where(k => k is { Item1: < 0, Item2: < 0 }).Order(comparer);

        (int, int)[] resultCycleSlopes = [(-1, 0), ..topRight, (0, 1), ..bottomRight, (1, 0), ..bottomLeft, (0, -1), ..topLeft];

        var asteroid = (0, 0);
        var slopeId = 0;
        while (needAsteroidNumber > 0)
        {
            if (slopesDict.TryGetValue(resultCycleSlopes[slopeId], out var slopeList) && slopeList.Count > 0)
            {
                asteroid = slopeList[^1];
                slopeList.RemoveAt(slopeList.Count - 1);
                needAsteroidNumber--;
            }
            slopeId = (slopeId + 1) % resultCycleSlopes.Length;
        }
        
        return asteroid.Item2 * 100 +  asteroid.Item1;
    }

    private static int Lcm(int a, int b)
    {
        var gcd = BigInteger.GreatestCommonDivisor(a, b);
        return Math.Abs(a * b) / (int)gcd;
    }

    private static int Dist((int,int) stationCoords, (int,int) asteroidCoords)
    {
        var i2 = (stationCoords.Item1 - asteroidCoords.Item1) * (stationCoords.Item1 - asteroidCoords.Item1);
        var j2 = (stationCoords.Item2 - asteroidCoords.Item2) * (stationCoords.Item2 - asteroidCoords.Item2);
        return i2 + j2;
    }
}
