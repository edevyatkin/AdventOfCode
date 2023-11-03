using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,21)]
public class Day21 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = "abcdefgh";
        foreach (var operation in input)
            result1 = PerformOperation(result1, operation, false);
        var result2 = "fbgdceah";
        for (var i = input.Length - 1; i >= 0; i--)
            result2 = PerformOperation(result2, input[i], true);
        return new AocDayResult(result1, result2);
    }

    public static string PerformOperation(string initStr, string operation, bool isPartTwo)
    {
        var sb = new StringBuilder(initStr);
        var sp = operation.Split();
        if (sp[0] == "swap" && sp[1] == "position")
        {
            var ix1 = int.Parse(sp[2]);
            var ix2 = int.Parse(sp[^1]);
            (sb[ix1], sb[ix2]) = (sb[ix2], sb[ix1]);
        }
        else if (sp[0] == "swap" && sp[1] == "letter")
        {
            var str = sb.ToString();
            var ix1 = str.IndexOf(sp[2][0]);
            var ix2 = str.IndexOf(sp[^1][0]);
            (sb[ix1], sb[ix2]) = (sb[ix2], sb[ix1]);
        }
        else if (sp[0] == "rotate")
        {
            if (isPartTwo)
            {
                if (sp[1] == "left")
                    sp[1] = "right";
                else if (sp[1] == "right")
                    sp[1] = "left";
            }
            switch (sp[1])
            {
                case "left":
                    var stepsl = int.Parse(sp[^2]);
                    while (stepsl-- > 0)
                    {
                        var ch = sb[0];
                        sb.Remove(0, 1);
                        sb.Append(ch);
                    }
                    break;
                case "right":
                    var stepsr = int.Parse(sp[^2]);
                    while (stepsr-- > 0)
                    {
                        var ch = sb[^1];
                        sb.Remove(sb.Length - 1, 1);
                        sb.Insert(0, ch);
                    }
                    break;
                case "based":
                    if (isPartTwo)
                    {
                        var testBased = string.Empty;
                        var reBased = initStr;
                        while (testBased != initStr)
                        {
                            reBased = reBased.Remove(0, 1) + reBased[0];
                            testBased = PerformOperation(reBased, operation, false);
                        } 
                        return reBased;
                    }

                    var str = sb.ToString();
                    var pos = str.IndexOf(sp[^1][0]);
                    var stepsb = 1 + pos + (pos >= 4 ? 1 : 0);
                    while (stepsb-- > 0)
                    {
                        var ch = sb[^1];
                        sb.Remove(sb.Length - 1, 1);
                        sb.Insert(0, ch);
                    }
                    break;
            }
        }
        else if (sp[0] == "reverse")
        {
            var pos1 = int.Parse(sp[2]);
            var pos2 = int.Parse(sp[^1]);
            while (pos1 < pos2)
            {
                (sb[pos1], sb[pos2]) = (sb[pos2], sb[pos1]);
                pos1++;
                pos2--;
            }
        }
        else if (sp[0] == "move")
        {
            var pos1 = int.Parse(sp[2]);
            var pos2 = int.Parse(sp[^1]);
            if (isPartTwo)
                (pos1, pos2) = (pos2, pos1);
            var letter = sb[pos1];
            sb.Remove(pos1, 1);
            sb.Insert(pos2, letter);
        }
        return sb.ToString();
    }
}
