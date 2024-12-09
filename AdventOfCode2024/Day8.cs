using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,8)]
public class Day8 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        
        var antennaPositions = new Dictionary<char, List<(int, int)>>();

        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == '.') 
                    continue;
                if (!antennaPositions.ContainsKey(input[i][j]))
                    antennaPositions[input[i][j]] = [];
                antennaPositions[input[i][j]].Add((i, j));
            }
        }
        
        var antinodesPositions = new HashSet<(int,int)>();

        var mul = 1;
        while (true)
        {
            var onGrid = false;
            foreach (var positions in antennaPositions.Values)
            {
                for (var i = 0; i < positions.Count; i++)
                {
                    for (var j = i + 1; j < positions.Count; j++)
                    {
                        var (a1i, a1j) = positions[i];
                        var (a2i, a2j) = positions[j];
                        var di = a1i - a2i;
                        var dj = a1j - a2j;
                        a1i += di * mul;
                        a1j += dj * mul;
                        if (a1i >= 0 && a1i < input.Length && a1j >= 0 && a1j < input[0].Length)
                        {
                            onGrid = true;
                            antinodesPositions.Add((a1i, a1j));
                        }
                        a2i -= di * mul;
                        a2j -= dj * mul;
                        if (a2i >= 0 && a2i < input.Length && a2j >= 0 && a2j < input[0].Length)
                        {
                            onGrid = true;
                            antinodesPositions.Add((a2i, a2j));
                        }
                    }
                }
            }
            if (mul == 1)
                result1 = antinodesPositions.Count;
            if (!onGrid)
                break;
            mul++;
        }

        result2 = antinodesPositions.Count + antennaPositions.Values
            .SelectMany(positions => positions)
            .Count(position => !antinodesPositions.Contains(position));


        return new AocDayResult(result1, result2);
    }
}
