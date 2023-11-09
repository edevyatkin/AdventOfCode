using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,3)]
public class Day3 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var id = int.Parse(input[0]);
        var result1 = SolvePart(id, false);
        var result2 = SolvePart(id, true);
        return new AocDayResult(result1, result2);
    }

    private int SolvePart(int id, bool isPartTwo)
    {
        if (id == 1 && !isPartTwo)
            return 0;
        
        var curId = 1;
        int x = 0, y = 0;
        var dirs = new[] { (0, 1), (-1, 0), (0, -1), (1, 0) };
        var angleDir = 0;
        var angleSide = 1;
        var hs = new Dictionary<(int, int), int>() { [(0, 0)] = 1 };
        while (true)
        {
            for (int c = 0; c < 2; c++)
            {
                for (var aLen = angleSide; aLen > 0; aLen--)
                {
                    x += dirs[angleDir].Item1;
                    y += dirs[angleDir].Item2;
                    if (!isPartTwo)
                    {
                        if (++curId == id)
                            return Math.Abs(x) + Math.Abs(y);
                    }
                    else
                    {
                        var sum = 0;
                        for (int i = -1; i <= 1; i++)
                            for (int j = -1; j <= 1; j++)
                                sum += hs.GetValueOrDefault((x + i, y + j), 0);
                        hs[(x, y)] = sum;
                        if (sum > id)
                            return sum;
                    }

                }
                angleDir = (angleDir + 1) % 4;
            }
            angleSide++;
        }
    }
}
