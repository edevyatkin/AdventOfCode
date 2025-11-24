using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var program = input[0].Split(',').Select(int.Parse).ToArray();
        foreach (var phaseSetting in GetPhaseSettings(5))
        {
            var currentInput = 0;
            for (var i = 0; i < 5; i++)
                currentInput = Intcode.Execute([..program], phaseSetting[i], currentInput)[^1];
            result1 = Math.Max(result1, currentInput);
        }
        return new AocDayResult(result1, result2);
    }

    private IEnumerable<List<int>> GetPhaseSettings(int amplifiersCount)
    {
        foreach (var phaseSetting in GetSetting([]))
            yield return phaseSetting;

        IEnumerable<List<int>> GetSetting(List<int> setting)
        {
            if (setting.Count == amplifiersCount)
                yield return setting;
            for (var i = 0; i <= 4; i++)
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
