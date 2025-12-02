using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025,2)]
public class Day2 : IAocDay 
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        foreach (var range in input[0].Split(','))
        {
            var ids = range.Split('-');
            var from = long.Parse(ids[0]);
            var to = long.Parse(ids[1]);
            result1 += CheckDouble(from, to, isPartTwo: false);
            result2 += CheckDouble(from, to, isPartTwo: true);
        }
        return new AocDayResult(result1, result2);
        
        long CheckDouble(long from, long to, bool isPartTwo = false)
        {
            var sum = 0L;
            for (var id = from; id <= to; id++)
            {
                var idStr = id.ToString();
                if (!isPartTwo)
                {
                    var mid = idStr.Length / 2;
                    if (idStr[..mid] == idStr[mid..])
                        sum += id;
                }
                else
                {
                    for (var d = 1; d <= idStr.Length / 2; d++)
                    {
                        if (idStr.Chunk(d).Select(chs => int.Parse(chs)).Distinct().Count() == 1)
                        {
                            sum += id;
                            break;
                        }
                    }
                }
            }

            return sum;
        }   
    }
}
