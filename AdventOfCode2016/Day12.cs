using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,12)]
public class Day12 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, isPartTwo: false);
        var result2 = SolvePart(input, isPartTwo: true);
        return new AocDayResult(result1, result2);
    }

    private static int SolvePart(string[] input, bool isPartTwo)
    {
        var registers = new Dictionary<char, int>()
        {
            ['a'] = 0,
            ['b'] = 0,
            ['c'] = 0,
            ['d'] = 0,
        };
        if (isPartTwo) 
            registers['c'] = 1;
        var index = 0;
        while (index < input.Length)
        {
            var inst = input[index];
            var sp = inst.Split();
            var op = sp[0];
            switch (op)
            {
                case "cpy":
                    registers[sp[2][0]] = int.TryParse(sp[1], out var val1) ? val1 : registers[sp[1][0]];
                    break;
                case "inc":
                    registers[sp[1][0]]++;
                    break;
                case "dec":
                    registers[sp[1][0]]--;
                    break;
                case "jnz":
                    var value = int.TryParse(sp[1], out var val2) ? val2 : registers[sp[1][0]];
                    if (value != 0)
                    {
                        index += int.Parse(sp[2]);
                        continue;
                    }
                    break;
            }
            index++;
        }
        return registers['a'];
    }
}
