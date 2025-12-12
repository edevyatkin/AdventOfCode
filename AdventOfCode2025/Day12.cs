using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 12)]
public class Day12 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var i = 0;
        var presents = new List<List<string>>();
        while (i < input.Length)
        {
            var line = input[i];
            if (line[1] == ':')
            {
                var present = new List<string>();
                while (input[i] != string.Empty)
                {
                    present.Add(input[i]);
                    i++;
                }
                presents.Add(present);
            }
            else
            {
                var sp = input[i].Split(": ");
                var (dimS, countsS) = (sp[0], sp[1]);
                var dimSp = dimS.Split('x');
                var regionArea = int.Parse(dimSp[0]) * int.Parse(dimSp[1]);
                var allPresentsArea = countsS
                    .Split()
                    .Select(int.Parse)
                    .Sum(c => c * 9);
                var allPresentsMinArea = countsS
                    .Split()
                    .Select((cs, csi) => 
                        int.Parse(cs) * presents[csi].SelectMany(l => l).Count(c => c == '#'))
                    .Sum(c => c);
                if (regionArea >= allPresentsArea && regionArea >= allPresentsMinArea)
                    result1++;
            }
            i++;
        }

        return new AocDayResult(result1, result2);
    }
}
