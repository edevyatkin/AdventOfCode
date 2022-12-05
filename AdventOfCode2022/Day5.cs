using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, false);
        var result2 = SolvePart(input,  true);
        
        return new AocDayResult(result1, result2);
    }

    string SolvePart(string[] input, bool isPartTwo)
    {
        int i = 0;
        while (input[i] != string.Empty)
            i++;
        var countS = int.Parse(input[i - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[^1]);
        var arr = new StringBuilder[countS];
        for (var stackIndex = 0; stackIndex < arr.Length; stackIndex++)
        {
            arr[stackIndex] = new StringBuilder();
        }
        
        for (var j = i-2; j >= 0; j--)
        {
            var chI = 1;
            foreach (var sb in arr)
            {
                var sc = input[j][chI];
                if (sc != ' ')
                {
                    sb.Append(sc);
                }
                chI += 4;
            }
        }
        
        for (var opI = i+1; opI < input.Length; opI++)
        {
            var spO = input[opI].Split(' ');
            var carCount = int.Parse(spO[1]);
            var from = arr[int.Parse(spO[^3])-1];
            var to = arr[int.Parse(spO[^1])-1];
            if (isPartTwo)
            {
                to.Append(from.ToString()[^carCount..]);
                from.Length -= carCount;
            }
            else
            {
                while (--carCount >= 0)
                {
                    to.Append(from[^1]);
                    from.Length--;
                }   
            }
        }

        var resultSb = new StringBuilder();
        foreach (var sb in arr)
        {
            if (sb.Length > 0)
            {
                resultSb.Append(sb[^1]);
            }
        }

        return resultSb.ToString();
    }
}
