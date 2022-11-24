using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,6)]
public class Day6 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var instructions = new List<(string Op, int X1, int Y1, int X2, int Y2)>();
        foreach (var s in input)
        {
            var coords = s.Split(' ');
            var c1 = coords[^3].Split(',').Select(int.Parse).ToArray();
            var (x1, y1) = (c1[0], c1[1]);
            var c2 = coords[^1].Split(',').Select(int.Parse).ToArray();
            var (x2, y2) = (c2[0], c2[1]);
            instructions.Add((coords[^4], x1, y1, x2, y2));
        }

        var result1 = CalculateLights(instructions);
        var result2 = CalculateLights(instructions, true);

        return new AocDayResult(result1, result2);
    }

    private static int CalculateLights(List<(string Op, int X1, int Y1, int X2, int Y2)> instructions, bool isPart2 = false)
    {
        var arr = new int[1000][];
        for (int i = 0; i < 1000; i++)
            arr[i] = new int[1000];
        
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                foreach (var instr in instructions)
                {
                    if (instr.X1 <= i && i <= instr.X2 && instr.Y1 <= j && j <= instr.Y2)
                    {
                        switch (instr.Op)
                        {
                            case "on":
                                if (!isPart2) arr[i][j] = 1; else arr[i][j] += 1;
                                break;
                            case "off":
                                if (!isPart2) arr[i][j] = 0; else arr[i][j] = Math.Max(0, arr[i][j]-1);
                                break;
                            case "toggle":
                                if (!isPart2) arr[i][j]^= 1; else arr[i][j] += 2;
                                break;
                        }
                    }
                }
            }
        }

        return arr.Sum(r => r.Sum());
    }
}