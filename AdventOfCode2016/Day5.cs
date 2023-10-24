using System.Security.Cryptography;
using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = string.Empty;
        var result2 = new char[8];
        var doorId = input[0];
        var count1 = 8;
        var count2 = 8;
        var index = 0;
        while (count1 > 0 || count2 > 0)
        {
            var password = doorId + index;
            var hash = Md5(password);
            if (hash[..5] == "00000")
            {
                if (count1 > 0)
                {
                    result1 += hash[5];
                    count1--;
                }

                if (count2 > 0)
                {
                    var pos = hash[5] - '0';
                    if (pos <= 7 && result2[pos] == '\0')
                    {
                        result2[pos] = hash[6];
                        count2--;
                    }
                }

            }
            index++;
        }
        
        return new AocDayResult(result1, new string(result2));
    }

    public static string Md5(string input) => 
        Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(input))).ToLowerInvariant();
}
