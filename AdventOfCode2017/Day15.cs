using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,15)]
public class Day15 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var gen1 = int.Parse(input[0].Split()[^1]);
        var gen2 = int.Parse(input[1].Split()[^1]);
        var result1 = CountMatches(gen1, gen2, 40_000_000, false);
        var result2 = CountMatches(gen1, gen2, 5_000_000, true);;
        return new AocDayResult(result1, result2);
    }

    internal static int CountMatches(long gen1, long gen2, int pairs, bool isPartTwo)
    {
        long GetNext(long gen, int n) => gen * (n == 1 ? 16807 : 48271) % 2147483647;
        var count = 0;
        while (pairs-- > 0)
        {
            if (!isPartTwo)
            {
                gen1 = GetNext(gen1, 1);
                gen2 = GetNext(gen2, 2);
            }
            else
            {
                do
                {
                    gen1 = GetNext(gen1, 1);
                } while (gen1 % 4 != 0);
                do
                {
                    gen2 = GetNext(gen2, 2);
                } while (gen2 % 8 != 0);
            }

            if ((((1 << 16) - 1) & (gen1 ^ gen2)) == 0)
                count++;
        }

        return count;
    }
}
