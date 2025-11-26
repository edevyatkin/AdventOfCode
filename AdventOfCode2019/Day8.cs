using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 8)]
public class Day8 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input, 25, 6);
        var result2 = SolvePart2(input, 25, 6);
        return new AocDayResult(result1, result2);
    }

    internal static int SolvePart1(string[] input, int wide, int tall)
    {
        var layers = new List<Dictionary<int, int>>();
        foreach (var layer in input[0].Select(c => c - '0').Chunk(wide * tall))
        {
            var counts = new Dictionary<int, int>();
            foreach (var color in layer)
                counts[color] = counts.GetValueOrDefault(color, 0) + 1;
            layers.Add(counts);
        }
        var layerWithMin0 = layers.MinBy(l => l.GetValueOrDefault(0));
        return layerWithMin0[1] *  layerWithMin0[2];
    }

    internal static StringBuilder SolvePart2(string[] input, int wide, int tall)
    {
        var result = new StringBuilder();
        var resultImage = new char[tall][];
        for (var i = 0; i < tall; i++)
            resultImage[i] = new char[wide];
        foreach (var layer in input[0].Chunk(wide * tall).Reverse())
        {
            for (var px = 0; px < wide * tall; px++)
            {
                if (layer[px] != '2')
                {
                    var (row, col) = (px / wide, px % wide);
                    resultImage[row][col] = layer[px];
                }
            }
        }
        result.AppendJoin(Environment.NewLine,
            resultImage.Select(chars => new string(chars.Select(c => c == '1' ? '+' : ' ').ToArray())));
        return result;
    }
}
