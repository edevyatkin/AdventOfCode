using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,1)]
public class Day1 : IAocDay
{
    public AocDayResult Solve(string[] inp)
    {
        var input = inp.Select(int.Parse).ToArray();
        int result = 0;
        for (int i = 1; i < input.Length; i++)
            result += (input[i - 1] < input[i]) ? 1 : 0;

        int result2 = 0;
        int[] sums = new int[input.Length + 1];
        sums[1] = input[0];
        for (int i = 2; i < sums.Length; i++)
        {
            sums[i] = sums[i - 1] + input[i - 1];
        }

        int windowSize = 3;
        for (int i = windowSize + 1; i < sums.Length; i++)
        {
            result2 += (sums[i] - sums[i - windowSize]) > (sums[i - 1] - sums[i - (windowSize + 1)]) ? 1 : 0;
        }

        return new AocDayResult(result, result2);
    }
}