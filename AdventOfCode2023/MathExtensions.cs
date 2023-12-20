namespace AdventOfCode2023;

public static class MathExtensions
{
    public static long FindLcm(IEnumerable<int> nums) => 
        FindLcm(nums.Select(n => (long)n));

    public static long FindLcm(IEnumerable<long> nums)
    {
        var factors = new Dictionary<long, long>();
        foreach (var num in nums)
        {
            var primeFactors = FindPrimeFactors(num);
            var numFactors = new Dictionary<long, long>();
            foreach (var f in primeFactors)
                numFactors[f] = numFactors.GetValueOrDefault(f, 0) + 1;
            foreach (var (f, count) in numFactors)
                factors[f] = Math.Max(count, factors.GetValueOrDefault(f, 0));
        }

        return factors.Select(kv => (long)Math.Pow(kv.Key, kv.Value))
            .Aggregate(1L, (a,f) => a * f);
 
    }

    public static List<int> FindPrimeFactors(int num) => 
        FindPrimeFactors((long)num).Select(n => (int)n).ToList();

    public static List<long> FindPrimeFactors(long num)
    {
        var factors = new List<long>();
        var div = 2;
        while (num > 1)
        {
            while (num % div == 0)
            {
                factors.Add(div);
                num /= div;
            }
            div++;
        }

        return factors;
    }
}
