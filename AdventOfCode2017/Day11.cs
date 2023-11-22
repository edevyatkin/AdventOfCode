using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,11)]
public class Day11 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var sp = input[0].Split(',');
        var i = 0;
        var j = 0;
        var result2 = 0;
        var Dist = (int i, int j) => (Math.Abs(i) + Math.Abs(j)) / 2;
        foreach (var dir in sp)
        {
            (int Di, int Dj) diff = dir switch
            {
                "n"     => (-2, 0),
                "s"     => ( 2, 0),
                "nw"    => (-1,-1),
                "ne"    => (-1, 1),
                "sw"    => ( 1,-1),
                "se"    => ( 1, 1),
                _ => default
            };
            i += diff.Di;
            j += diff.Dj;
            result2 = Math.Max(result2, Dist(i, j));
        }
        var result1 = Dist(i, j);
        return new AocDayResult(result1, result2);
    }
}
