using System.Security.Cryptography;
using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,14)]
public class Day14 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var salt = input[0];
        var result1 = SolvePart(salt, Md5);
        var result2 = SolvePart(salt, Md5Extra2016);
        return new AocDayResult(result1, result2);
    }

    public static int SolvePart(string salt, Func<string, string> md5Func)
    {
        var index = 0;
        var keysList = new List<int>();
        var md5Cache = new Dictionary<string, string>();
        while (keysList.Count < 64)
        {
            var possibleKey = salt + index;
            if (!md5Cache.ContainsKey(possibleKey))
                md5Cache[possibleKey] = md5Func(possibleKey);
            var md5 = md5Cache[possibleKey]; 
            var tripletFound = false;
            for (int i = 0; i < md5.Length; i++)
            {
                var letter = md5[i];
                if (!tripletFound && i <= md5.Length - 3 && letter == md5[i+1] && letter == md5[i+2])
                {
                    tripletFound = true;
                    for (var fiveIndex = index + 1; fiveIndex <= index + 1000; fiveIndex++)
                    {
                        var fivePossibleKey = salt + fiveIndex;
                        if (!md5Cache.ContainsKey(fivePossibleKey))
                            md5Cache[fivePossibleKey] = md5Func(fivePossibleKey);
                        var md5Five = md5Cache[fivePossibleKey]; 
                        for (int j = 0; j < md5.Length; j++)
                        {
                            var letterFive = md5Five[j];
                            if (letterFive == letter
                                && j <= md5Five.Length - 5
                                && letterFive == md5Five[j+1] && letterFive == md5Five[j+2] 
                                && letterFive == md5Five[j+3] && letterFive == md5Five[j+4])
                            {
                                if (keysList.Count > 0 && keysList[^1] == index)
                                    continue;
                                keysList.Add(index); 
                            }
                        }
                    }
                }
            }
            index++;
        }
        return keysList[^1];
    }

    public static string Md5(string input) => 
        Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(input))).ToLowerInvariant();

    public static string Md5Extra2016(string input)
    {
        var result = Md5(input);
        var count = 2016;
        while (count-- > 0)
            result = Md5(result);
        return result;
    }
}
