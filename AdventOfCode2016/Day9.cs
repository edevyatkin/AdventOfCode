using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,9)]
public class Day9 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input[0], false);
        var result2 = SolvePart(input[0], true);
        return new AocDayResult(result1, result2);
    }

    private static long SolvePart(string file, bool isPartTwo)
    {
        var result = 0L;
        var markerSb = new StringBuilder();
        var inMarker = false;
        for (var i = 0; i < file.Length; i++)
        {
            var c = file[i];
            if (c == '(')
            {
                inMarker = true;
                markerSb.Clear();
            }
            else if (c == ')')
            {
                var marker = markerSb.ToString();
                var pair = marker.Split('x')
                    .Select(int.Parse).ToArray();
                if (!isPartTwo)
                {
                    result += pair[0] * pair[1];
                }
                else
                {
                    result += SolvePart(file[(i + 1)..(i + 1 + pair[0])], true) * pair[1];
                }
                i += pair[0];
                inMarker = false;
            }
            else
            {
                if (inMarker)
                {
                    markerSb.Append(c);
                }
                else
                {
                    result++;
                }
            }
        }

        return result;
    }
}
