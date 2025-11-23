using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 4)]
public class Day4 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var range = input[0].Split('-').Select(int.Parse).ToArray();
        for (var code = range[0]; code <= range[1]; code++)
        {
            var codeStr = code.ToString();
            var isValidCode1 = false;
            var isValidCode2 = false;
            for (var i = 0; i < codeStr.Length - 1; i++)
            {
                if (codeStr[i] == codeStr[i + 1])
                {
                    isValidCode1 = true;
                    var (leftNotTheSame, rightNotTheSame) = (true, true);
                    if (i > 0 && codeStr[i-1] == codeStr[i])
                        leftNotTheSame = false;
                    if (i + 2 < codeStr.Length && codeStr[i+1] == codeStr[i+2])
                        rightNotTheSame = false;
                    if (leftNotTheSame && rightNotTheSame)
                        isValidCode2 = true;
                }
                if (codeStr[i] <= codeStr[i + 1]) 
                    continue;
                isValidCode1 = false;
                isValidCode2 = false;
                break;
            }
            if (isValidCode1)
                result1++;
            if (isValidCode2)
                result2++;
        }
        return new AocDayResult(result1, result2);
    }
}
