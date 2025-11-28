using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0L;
        var program = input[0].Split(',').Select(int.Parse).ToArray();
        foreach (var phaseSetting in GetPhaseSettings(0, 4))
        {
            var currentInput = 0;
            for (var i = 0; i < 5; i++)
                currentInput = (int)new Intcode(program).Execute(phaseSetting[i], currentInput)[^1];
            result1 = Math.Max(result1, currentInput);
        }
        
        foreach (var phaseSetting in GetPhaseSettings(5, 9))
        {
            var amplifiersPrograms = new Intcode[5];
            for (var i = 0; i < 5; i++)
            {
                amplifiersPrograms[i] = new Intcode(program);
                amplifiersPrograms[i].Execute(phaseSetting[i]);
            }
            var currentInput = 0L;
            var ix = 0;
            while (amplifiersPrograms[^1].State != IntcodeState.Completed)
            {
                currentInput = amplifiersPrograms[ix].Execute(currentInput)[^1];
                ix = (ix + 1) % amplifiersPrograms.Length;
            }
            result2 = Math.Max(result2, currentInput);
        }
        
        return new AocDayResult(result1, result2);
    }

    private IEnumerable<List<int>> GetPhaseSettings(int from, int to)
    {
        foreach (var phaseSetting in GetSetting([]))
            yield return phaseSetting;

        IEnumerable<List<int>> GetSetting(List<int> setting)
        {
            if (setting.Count == to - from + 1)
                yield return setting;
            for (var i = from; i <= to; i++)
            {
                if (setting.Contains(i))
                    continue;
                setting.Add(i);
                foreach (var s in GetSetting(setting))
                    yield return s;
                setting.Remove(i);
            }
        }
    }
}
