using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var ip in input)
        {
            result1 += IsSupportTls(ip) ? 1 : 0;
            result2 += IsSupportSsl(ip) ? 1 : 0;
        }
        return new AocDayResult(result1, result2);
    }

    public static bool IsSupportTls(string ip)
    {
        var isSupportTlsIn = true;
        var isSupportTlsOut = false;
        var inBrackets = false;
        for (int i = 0; i < ip.Length - 3; i++)
        {
            if (ip[i] == '[')
            {
                inBrackets = true;
            } else if (ip[i] == ']')
            {
                inBrackets = false;
            }
            else
            {
                if (ip[i] != ip[i + 1] &&
                    char.IsLetter(ip[i]) && 
                    char.IsLetter(ip[i + 1]) &&
                    ip[i] == ip[i + 3] && 
                    ip[i + 1] == ip[i + 2])
                {
                    if (inBrackets)
                    {
                        isSupportTlsIn = false;
                    }
                    else
                    {
                        isSupportTlsOut = true;
                    }
                }
            }
        }

        return isSupportTlsIn && isSupportTlsOut;
    }

    public static bool IsSupportSsl(string ip)
    {
        var inBrackets = false;
        var inHs = new HashSet<string>();
        var outHs = new HashSet<string>();
        for (int i = 0; i < ip.Length - 2; i++)
        {
            if (ip[i] == '[')
            {
                inBrackets = true;
            } else if (ip[i] == ']')
            {
                inBrackets = false;
            }
            else
            {
                if (ip[i] != ip[i + 1] &&
                    char.IsLetter(ip[i]) && 
                    char.IsLetter(ip[i + 1]) &&
                    ip[i] == ip[i + 2])
                {
                    if (inBrackets)
                    {
                        inHs.Add(ip[i.. (i + 3)]);
                    }
                    else
                    {
                        outHs.Add(ip[i.. (i + 3)]);
                    }
                }
            }
        }
        
        foreach (var ins in inHs)
        {
            foreach (var outs in outHs)
            {
                if (ins[0] == outs[1] && ins[1] == outs[0])
                    return true;
            }
        }

        return false;
    }
}
