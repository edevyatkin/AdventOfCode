namespace AdventOfCode2023;

[AocDay(2023,9)]
public class Day9 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, false);
        var result2 = SolvePart(input, true);

        return new AocDayResult(result1, result2);
    }

    internal static long SolvePart(string[] input, bool isPartTwo)
    {
        var result = 0L;
        foreach (var line in input)
        {
            var seqList = new List<List<long>>();
            var seq = line.Split().Select(long.Parse).ToList();
            seqList.Add(seq);
            while (seq[^1] != 0)
            {
                var temp = new List<long>();
                var i = 1;
                while (i < seq.Count)
                {
                    temp.Add(seq[i] - seq[i-1]);
                    i++;
                }
                seqList.Add(temp);
                seq = temp;
            }
            
            var ix = seqList.Count - 2;

            if (!isPartTwo)
            {
                seqList[^1].Add(0);
                while (ix >= 0)
                {
                    seqList[ix].Add(seqList[ix][^1] + seqList[ix+1][^1]);
                    ix--;
                }
            
                result += seqList[0][^1];
            }
            else
            {
                seqList[^1].Insert(0, 0);
                while (ix >= 0)
                {
                    seqList[ix].Insert(0, seqList[ix][0] - seqList[ix+1][0]);
                    ix--;
                }

                result += seqList[0][0];
            }
        }

        return result;
    }
}
