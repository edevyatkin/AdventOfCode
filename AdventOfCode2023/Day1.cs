namespace AdventOfCode2023;

[AocDay(2023,1)]
public class Day1 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var line in input)
        {
            result1 += GetNum(line, false);
            result2 += GetNum(line, true);
        }
        return new AocDayResult(result1, result2);
    }

    private static int GetNum(string line, bool isPartTwo)
    {
        var num = 0;
        
        for (var i = 0; i < line.Length; i++)
        {
            if (TryGetNum(line, i, isPartTwo, out var n))
            {
                num = n;
                break;
            }
        }

        for (var i = line.Length - 1; i >= 0; i--)
        {
            if (TryGetNum(line, i, isPartTwo, out var n))
            {
                num = num * 10 + n;
                break;
            }
        }

        return num;
    }

    private static bool TryGetNum(string line, int i, bool isPartTwo, out int num)
    {
        var numDict = new Dictionary<string, int> {
            ["zero"] = 0,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
        };
        
        if (char.IsDigit(line[i]))
        {
            num = line[i] - '0';
            return true;
        }
        if (isPartTwo)
        {
            foreach (var key in numDict.Keys)
            {
                if (i + key.Length <= line.Length && line[i..(i + key.Length)] == key)
                {
                    num = numDict[key];
                    return true;
                }
            }
        }

        num = -1;
        return false;
    }
}
