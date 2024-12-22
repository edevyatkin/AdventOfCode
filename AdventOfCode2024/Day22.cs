using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,22)]
public class Day22 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        
        const long mod = 16777216;
        
        var dict = new Dictionary<string, Dictionary<int, int>>();

        for (var buyerId = 0; buyerId < input.Length; buyerId++)
        {
            var line = input[buyerId];
            var n = long.Parse(line);
            var c = 0;
            var arr = new[] { 0, (int)n % 10, 0, 0, 0 };
            while (++c <= 2000)
            {
                n = ((n << 6) ^ n) % mod;
                n = ((n >> 5) ^ n) % mod;
                n = ((n << 11) ^ n) % mod;
                
                if (c <= 3)
                {
                    arr[c+1] = (int)n % 10;
                    continue;
                }

                Array.Copy(arr, 1, arr, 0, arr.Length-1);
                arr[4] = (int)n % 10;
                var diffs = new int[4];
                for (var i = 0; i < 4; i++)
                    diffs[i] = arr[i + 1] - arr[i];
                var diffsStr = string.Join(string.Empty, diffs);
                if (!dict.ContainsKey(diffsStr))
                    dict[diffsStr] = [];
                if (!dict[diffsStr].ContainsKey(buyerId))
                    dict[diffsStr][buyerId] = (int)n % 10;
            }
            result1 += n;
        }
        
        result2 = dict.Values.Max(kv => kv.Values.Sum());

        return new AocDayResult(result1, result2);
    }
}
