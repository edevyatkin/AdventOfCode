using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,14)]
public class Day14 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var rocks = new HashSet<(int, int)>();
        var bottom = 0;
        foreach (var s in input)
        {
            var sp = s.Split(" -> ");
            for (int j = 0; j < sp.Length-1; j++)
            {
                var p1 = sp[j].Split(',').Select(int.Parse).ToArray();
                var p2 = sp[j+1].Split(',').Select(int.Parse).ToArray();

                if (p1[0] > p2[0])
                {
                    (p1[0], p2[0]) = (p2[0], p1[0]);
                }
                if (p1[1] > p2[1])
                {
                    (p1[1], p2[1]) = (p2[1], p1[1]);
                }
                
                for (var k = p1[0]; k <= p2[0]; k++)
                {
                    for (var l = p1[1]; l <= p2[1]; l++)
                    {
                        rocks.Add((k, l));
                        bottom = Math.Max(bottom, l);
                    }
                }
            }
        }
        
        var toAbyss = false;
        var floor = bottom + 2;
        while (!rocks.Contains((500,0)))
        {
            int si = 500, sj = 0;

            while (true)
            {
                if (!rocks.Contains((si, sj + 1)) && sj+1 < floor)
                {
                    sj++;
                } 
                else if (!rocks.Contains((si - 1, sj + 1)) && sj+1 < floor)
                {
                    si--;
                    sj++;
                } 
                else if (!rocks.Contains((si + 1, sj + 1)) && sj+1 < floor)
                {
                    si++;
                    sj++;
                }
                else
                {
                    if (!toAbyss)
                    {
                        result1++;
                    }
                    result2++;
                    rocks.Add((si, sj));
                    break;
                }
                if (sj > bottom)
                {
                    toAbyss = true;
                }
            }
        }

        return new AocDayResult(result1, result2);
    }
}
