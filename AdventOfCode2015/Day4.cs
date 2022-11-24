using System.Security.Cryptography;
using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,4)]
public class Day4 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = GetNumber(input, 5);
        var result2 = GetNumber(input, 6);

        return new AocDayResult(result1, result2);
    }

    private static int GetNumber(string[] input, int zeroCount)
    {
        var num = 1;
        var md5 = MD5.Create();
        var zeroStr = "".PadRight(zeroCount, '0');
        while (num < 10_000_000)
        {
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input[0] + num));
            var strHash = Convert.ToHexString(hash);
            if (strHash[..zeroCount] == zeroStr)
                return num;
            num++;
        }

        return -1;
    }
}