using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 25)]
public class Day25 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var sum = 0L;
        foreach (var s in input)
        {
            sum += SnafuToDecimal(s);
        }

        var result1 = DecimalToSnafu(sum);

        return new AocDayResult(result1, "no result");
    }

    internal string DecimalToSnafu(long n)
    {
        var remMap = new Dictionary<int, char>()
        {
            [0] = '0',
            [1] = '1',
            [2] = '2',
            [3] = '=',
            [4] = '-'
        };
        var res = string.Empty;
        while (n > 0)
        {
            res = remMap[(int)(n % 5)] + res;
            if (n % 5 is 3 or 4)
                n += n % 5;
            n /= 5;
        }

        return res;
    }

    internal long SnafuToDecimal(string s)
    {
        var map = new Dictionary<char, int>()
        {
            ['2'] = 2,
            ['1'] = 1,
            ['0'] = 0,
            ['-'] = -1,
            ['='] = -2
        };
        var mul = 1L;
        var num = 0L;
        for (var i = s.Length - 1; i >= 0; i--)
        {
            num += map[s[i]] * mul;
            mul *= 5;
        }

        return num;
    }
}
