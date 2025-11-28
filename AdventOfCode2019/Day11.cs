using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 11)]
public class Day11 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input);
        var result2 = SolvePart2(input);
        return new AocDayResult(result1, result2);
    }

    private static int SolvePart1(string[] input)
    {
        var tiles = PaintHull(input, 0);
        return tiles.Count;
    }
    
    private static StringBuilder SolvePart2(string[] input)
    {
        var tiles = PaintHull(input, 1);
        var (minI, maxI, minJ, maxJ) = (
            tiles.Min(kv => kv.Key.Item1), 
            tiles.Max(kv => kv.Key.Item1),
            tiles.Min(kv => kv.Key.Item2), 
            tiles.Max(kv => kv.Key.Item2)
        );
        var (height, width) = (maxI - minI + 1, maxJ - minJ + 1);
        var hull = new char[height][];
        for (var i = 0; i < hull.Length; i++)
        {
            hull[i] = new char[width];
            Array.Fill(hull[i], ' ');
        }
        foreach (var (pos, color) in tiles)
        {
            var (ri, rj) = (pos.Item1 - minI,  pos.Item2 - minJ);
            hull[ri][rj] = color == 1 ? '+' : ' ';
        }

        return new StringBuilder().AppendJoin(Environment.NewLine, hull.Select(l => new string(l)));
    }

    private static Dictionary<(int,int), int> PaintHull(string[] input, int initialTileColor)
    {
        var tiles = new Dictionary<(int,int), int> { [(0,0)] = initialTileColor };
        var pos = (0, 0);
        var dir = 0;
        (int Di, int Dj)[] dirs = [(-1, 0), (0, 1), (1, 0), (0, -1)];
        var intCode = new Intcode(input);
        while (intCode.State != IntcodeState.Completed)
        {
            var output = intCode.Execute(tiles.GetValueOrDefault(pos))[^2..];
            (var color, dir) = ((int)output[0], (dir + 4 + (output[1] == 0 ? -1 : 1)) % 4);
            tiles[pos] = color;
            pos = (pos.Item1 + dirs[dir].Di, pos.Item2 +  dirs[dir].Dj);
        }
        
        return tiles;
    }
}
