using System.Runtime.InteropServices.ComTypes;
using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var programs = new Dictionary<string, ProgramInfo>();
        foreach (var line in input)
        {
            var sp = line.Split();
            var name = sp[0];
            var weight = int.Parse(sp[1][1..^1]);
            var programInfo = new ProgramInfo(weight);
            if (sp.Length > 2 && sp[2] == "->")
            {
                for (int i = 3; i < sp.Length; i++)
                {
                    programInfo.AddProgram(sp[i].TrimEnd(','));
                }
            }
            programs[name] = programInfo;
        }

        var programNames = programs.Keys.ToList();
        foreach (var info in programs.Values)
        {
            foreach (var aboveProgram in info.ProgramsAbove)
            {
                programNames.Remove(aboveProgram);
            }
        }
        
        var result1 = programNames[0];
        var result2 = 0;
        
        int Dfs(string node)
        {
            var above = programs[node].ProgramsAbove;
            if (above.Count == 0)
                return programs[node].Weight;
            var sumList = new List<int>();
            for (var i = 0; i < above.Count; i++)
            {
                sumList.Add(Dfs(above[i]));
            }

            if (sumList.ToHashSet().Count != 1 && result2 == 0)
            {
                var unbalancedWeight = sumList
                    .GroupBy(w => w, (w, elems) => (w, elems.Count()))
                    .First(pair => pair.Item2 == 1).Item1;
                var unbalancedWeightIndex = sumList.IndexOf(unbalancedWeight);
                var aboveNode = programs[node].ProgramsAbove[unbalancedWeightIndex];
                var min = sumList.Min();
                var max = sumList.Max();
                var diff = max - min;
                result2 = programs[aboveNode].Weight + (unbalancedWeight == max ? -diff : diff);
            }

            return programs[node].Weight + sumList.Sum();
        }

        Dfs(result1);
        
        return new AocDayResult(result1, result2);
    }

    class ProgramInfo
    {
        public int Weight { get; }
        public List<string> ProgramsAbove { get; } = new();

        public ProgramInfo(int weight)
        {
            Weight = weight;
        }

        public void AddProgram(string program)
        {
            ProgramsAbove.Add(program);
        }
    }
}
