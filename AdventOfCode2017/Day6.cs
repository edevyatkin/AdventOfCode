using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,6)]
public class Day6 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var banks = input[0].Split('\t').Select(int.Parse).ToArray();
        var seen = new Dictionary<string,int> { [string.Join(' ', banks)] = 0 };
        while (true)
        {
            result1++;
            var max = banks.Max();
            var ix = Array.IndexOf(banks, max);
            var blocks = banks[ix];
            banks[ix] = 0;
            while (blocks-- > 0)
            {
                ix = (ix + 1) % banks.Length;
                banks[ix]++;
            }
            var banksStr = string.Join(' ', banks);
            if (!seen.TryAdd(banksStr, result1))
            {
                result2 = result1 - seen[banksStr];
                break;
            }
        }
        return new AocDayResult(result1, result2);
    }
}
