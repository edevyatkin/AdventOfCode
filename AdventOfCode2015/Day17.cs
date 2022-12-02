using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,17)]
public class Day17 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = CountCombinations(input, 150, false);
        var result2 = CountCombinations(input, 150, true);

        return new AocDayResult(result1, result2);
    }

    public int CountCombinations(string[] input, int litres, bool isPartTwo)
    {
        var containers = input.Select(int.Parse).ToArray();
        var pathLengths = new List<int>();

        int GenerateCombinations(int ix, int sum, int pathLen)
        {
            if (sum == litres)
            {
                pathLengths.Add(pathLen);
                return 1;
            }

            if (sum > litres)
            {
                return 0;
            }

            var result = 0;
            for (int i = ix; i < containers.Length; i++)
            {
                result += GenerateCombinations(i + 1, sum + containers[i], pathLen + 1);
            }

            return result;
        }

        var result = GenerateCombinations(0, 0, 0);
        if (isPartTwo) 
            result = pathLengths.GroupBy(x => x).MinBy(g => g.Key)!.Count();
        
        return result;
    }
}
