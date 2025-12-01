using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 14)]
public class Day14 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input);
        var result2 = SolvePart2(input);
        
        return new AocDayResult(result1, result2);
    }

    internal long SolvePart1(string[] input)
    {
        var warehouse = new Dictionary<string, long>();
        return Produce1Fuel(ParseReactions(input), warehouse);        
    }
    
    internal long SolvePart2(string[] input)
    {
        var result = 0L;
        var reactions = ParseReactions(input);
        var warehouse = new Dictionary<string, long>();
        var oreCount = 1000000000000;
        while (true)
        {
            var ores = Produce1Fuel(reactions, warehouse);
            if (ores > oreCount)
                break;
            oreCount -= ores;
            result++;
        }

        return result;
    }

    private static Dictionary<string, (int Cnt, List<(int Cnt, string Chem)> Chems)> ParseReactions(string[] input)
    {
        var reactions = new Dictionary<string, (int Cnt, List<(int Cnt, string Chem)> Chems)>();
        foreach (var line in input)
        {
            var parts = Regex.Matches(line, @"(\d+ [A-Z]+)")
                .Select(m => {
                    var spaceIx = m.Value.IndexOf(' ');
                    return (int.Parse(m.Value[..spaceIx]), m.Value[(spaceIx + 1)..]);
                }).ToList();
            reactions[parts[^1].Item2] = (parts[^1].Item1, parts[..^1]);
        }

        return reactions;
    }

    static long Produce1Fuel(Dictionary<string, (int Cnt, List<(int Cnt, string Chem)> Chems)> reactions, Dictionary<string, long> warehouse)
    {
        var ores = 0L;
        Dfs(1, "FUEL");
        return ores;
        
        void Dfs(long needCnt, string needChem)
        {
            if (needChem == "ORE")
            {
                ores += needCnt;
                return;
            }

            var (outputCnt, inputChems) = reactions[needChem];
            if (warehouse.GetValueOrDefault(needChem) < needCnt)
            {
                var reactionsCount = 
                    (int)Math.Ceiling((double)(needCnt - warehouse.GetValueOrDefault(needChem)) / outputCnt);
                foreach (var (inputCnt, inputChem) in inputChems)
                    Dfs(inputCnt * reactionsCount, inputChem);
                warehouse[needChem] = warehouse.GetValueOrDefault(needChem) + outputCnt * reactionsCount;
            }
            warehouse[needChem] -= needCnt;
        }
    }
}
