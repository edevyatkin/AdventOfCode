using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        
        var rulesList = new List<(int, int)>();
        var updatesList = new List<List<int>>();

        var isRules = true;
        foreach (var line in input)
        {
            if (line == string.Empty) {
                isRules = false;
            }
            else if (isRules)
            {
                var ruleArr = line.Split('|').Select(int.Parse).ToArray();
                rulesList.Add((ruleArr[0], ruleArr[1]));
            }
            else
            {
                updatesList.Add(line.Split(',').Select(int.Parse).ToList());
            }
        }

        foreach (var update in updatesList)
        {
            var inCorrectOrder = true;
            foreach (var rule in rulesList)
            {
                var leftIx = update.IndexOf(rule.Item1);
                var rightIx = update.IndexOf(rule.Item2);
                if (leftIx == -1 || rightIx == -1 || leftIx < rightIx)
                    continue;
                inCorrectOrder = false;
                break;
            }
            if (inCorrectOrder)
            {
                result1 += update[update.Count / 2];
            }
            else
            {
                var newUpdate = new List<int>();
                foreach (var leftPage in update)
                {
                    var leftIx = int.MaxValue;
                    foreach (var (_, rightPage) in rulesList.Where(r => r.Item1 == leftPage))
                    {
                        var ix = newUpdate.IndexOf(rightPage);
                        if (ix != -1) 
                            leftIx = Math.Min(leftIx, ix);
                    }
                    if (leftIx != int.MaxValue)
                        newUpdate.Insert(leftIx, leftPage);
                    else
                        newUpdate.Add(leftPage);
                }
                result2 += newUpdate[newUpdate.Count / 2];
            }
        }

        return new AocDayResult(result1, result2);
    }
}
