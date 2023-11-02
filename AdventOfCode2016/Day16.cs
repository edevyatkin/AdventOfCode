using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,16)]
public class Day16 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var disk1 = FillDisk(input[0], 272);
        var result1 = CalcCheckSum(disk1);
        var disk2 = FillDisk(input[0], 35651584);
        var result2 = CalcCheckSum(disk2);
        return new AocDayResult(result1, result2);
    }

    public static string FillDisk(string input, int len)
    {
        var result = input;
        while (result.Length < len)
        {
            var sb = new StringBuilder(result);
            sb.Append('0');
            for (var i = result.Length - 1; i >= 0; i--)
            {
                sb.Append(result[i] == '0' ? '1' : '0');
            }
            result = sb.ToString();
        }
        return result[..len];
    }

    public static string CalcCheckSum(string data)
    {
        var chks = data;
        while (chks.Length % 2 == 0)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < chks.Length; i += 2)
            {
                sb.Append(chks[i] == chks[i + 1] ? '1' : '0');
            }
            chks = sb.ToString();
        }
        return chks;
    }
}
