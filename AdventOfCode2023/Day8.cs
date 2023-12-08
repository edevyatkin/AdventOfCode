namespace AdventOfCode2023;

[AocDay(2023, 8)]
public class Day8 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input);
        var result2 = SolvePart2(input);
        
        return new AocDayResult(result1, result2);
    }

    internal static int SolvePart1(string[] input)
    {
        var (inst, nodes) = ParseInput(input);
        var steps = 0;
        var cur = "AAA";
        var ix = 0;
        while (cur != "ZZZ")
        {
            var (left, right) = nodes[cur];
            cur = inst[ix] == 'L' ? left : right;
            steps++;
            ix = (ix + 1) % inst.Length;
        }

        return steps;
    }
    
    internal static long SolvePart2(string[] input)
    {
        var (inst, nodes) = ParseInput(input);
        var nodesNames = nodes
            .Where(n => n.Key[^1] == 'A')
            .Select(n => n.Key)
            .ToList();

        return FindLcm(nodesNames.Select(FindNextZIndex).ToList());

        int FindNextZIndex(string name)
        {
            var cur = name;
            var ix = 0;
            var steps = 0;
            while (cur[^1] != 'Z')
            {
                var (left, right) = nodes[cur];
                cur = inst[ix] == 'L' ? left : right;
                steps++;
                ix = (ix + 1) % inst.Length;
            } 

            return steps;
        }
    }

    private static long FindLcm(List<int> nums)
    {
        var factors = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            var primeFactors = FindPrimeFactors(num);
            var numFactors = new Dictionary<int, int>();
            foreach (var f in primeFactors)
                numFactors[f] = numFactors.GetValueOrDefault(f, 0) + 1;
            foreach (var (f, count) in numFactors)
                factors[f] = Math.Max(count, factors.GetValueOrDefault(f, 0));
        }

        return factors.Select(kv => (long)Math.Pow(kv.Key, kv.Value))
            .Aggregate(1L, (a,f) => a * f);
 
    }

    private static List<int> FindPrimeFactors(int num)
    {
        var factors = new List<int>();
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

    private static (string inst, Dictionary<string, (string, string)> nodes) ParseInput(string[] input)
    {
        var inst = input[0];
        var nodes = new Dictionary<string, (string, string)>();
        for (var i = 2; i < input.Length; i++)
        {
            var sp = input[i].Split(" = ");
            var sp2 = sp[1].Split(", ");
            nodes[sp[0]] = (sp2[0][1..], sp2[1][..^1]);
        }

        return (inst, nodes);
    }
}
