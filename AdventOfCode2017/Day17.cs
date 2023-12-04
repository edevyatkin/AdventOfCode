using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,17)]
public class Day17 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var steps = int.Parse(input[0]);
        var list = new List<int> { 0 };
        var curPos = 0;
        var val = 1;
        while (true)
        {
            var insPos = (curPos + steps) % list.Count;
            list.Insert(insPos+1, val);
            if (val == 2017)
            {
                result1 = list[(insPos + 2) % list.Count];
                break;
            }
            curPos = insPos + 1;
            val++;
        }
        
        var counter = 50_000_000;
        var c = 0;
        var len = 1;
        var pos = 0;
        while (c++ < counter)
        {
            pos = (pos + steps) % len;
            if (pos == 0)
                result2 = c;
            pos++;
            len++;
        }

        return new AocDayResult(result1, result2);
    }
}
