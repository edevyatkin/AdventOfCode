using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,8)]
public class Day8 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result2 = 0;
        var reg = new Dictionary<string, int>();
        foreach (var line in input)
        {
            var sp = line.Split();
            var regName = sp[0];
            var op = sp[1];
            var diff = int.Parse(sp[2]);
            var regToCheck = sp[4];
            var compareOp = sp[5];
            var compareVal = int.Parse(sp[6]);
            reg.TryAdd(regName, 0);
            reg.TryAdd(regToCheck, 0);
            var isTrueResult = compareOp switch
            {
                "!=" => reg[regToCheck] != compareVal,
                "==" => reg[regToCheck] == compareVal,
                ">" => reg[regToCheck] > compareVal,
                "<" => reg[regToCheck] < compareVal,
                ">=" => reg[regToCheck] >= compareVal,
                "<=" => reg[regToCheck] <= compareVal,
            };
            if (!isTrueResult)
                continue;
            reg[regName] += op == "inc" ? diff : -diff;
            
            result2 = Math.Max(result2, reg[regName]);
        }
        
        var result1 = reg.Values.Max();
        
        return new AocDayResult(result1, result2);
    }
}
