using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,10)]
public class Day10 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input[0], 256, isPartTwo: false);
        var result2 = SolvePart(input[0], 256, isPartTwo: true);
        return new AocDayResult(result1, result2);
    }

    internal object SolvePart(string input, int c, bool isPartTwo) {
        var knots = Enumerable.Range(0, c).ToArray();
        if (!isPartTwo) {
            var lengths1 = input.Split(',').Select(int.Parse).ToList();
            var pos1 = 0;
            var skip1 = 0;
            HashRound(knots, lengths1, ref pos1, ref skip1);
            return knots[0] * knots[1];
        }
        return Hash(input, c);
    }

    internal static string Hash(string input, int c)
    {
        var knots = Enumerable.Range(0, c).ToArray();
        var lengths = input.Select(l => (int)l).ToList();
        lengths.AddRange(new int[] { 17, 31, 73, 47, 23 });
        var count = 64;
        var pos = 0;
        var skip = 0;
        while (count-- > 0) {
            HashRound(knots, lengths, ref pos, ref skip);
        }
        return string.Concat(knots.Chunk(16)
            .Select(chnk => Convert.ToString(chnk.Aggregate(0, (acc,e) => acc ^ e), 16)
                .PadLeft(2, '0')));
    }

    private static void HashRound(int[] knots, List<int> lengths, ref int pos, ref int skip)
    {
        var c = knots.Length;
        foreach (var len in lengths) 
        {
            var i = pos;
            var j = pos + len - 1;
            while (i < j) 
            {
                (knots[(i + c) % c], knots[(j + c) % c]) = (knots[(j + c) % c], knots[(i + c) % c]);
                i++; j--;
            }
            pos += len + skip;
            pos %= c;
            skip++;
        }
    }
}
