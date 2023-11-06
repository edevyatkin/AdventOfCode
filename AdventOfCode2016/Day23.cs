using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,23)]
public class Day23 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, false);
        var result2 = SolvePart(input, true);;
        return new AocDayResult(result1, result2);
    }

    public long SolvePart(string[] input, bool isPartTwo)
    {
        var registers = new Dictionary<char, long>()
        {
            ['a'] = 0,
            ['b'] = 0,
            ['c'] = 0,
            ['d'] = 0,
        };
        registers['a'] = !isPartTwo ? 7 : 12;

        List<(string Name, string Args)> instructions = input.Select(i => (i[..3],i[4..])).ToList();

        if (isPartTwo)
        {
            instructions[4] = ("mul", "b d");
            instructions[5] = ("add", "d a");
            instructions[6] = ("cpy", "0 c");
            instructions[7] = ("cpy", "0 d");
            instructions[8] = ("nop", string.Empty);
            instructions[9] = ("nop", string.Empty);
        }

        void ToggleInstruction(long pos)
        {
            if (pos < 0 || pos >= instructions.Count)
                return;
            var newInst = instructions[(int)pos].Name switch
            {
                "cpy" => "jnz",
                "inc" => "dec",
                "dec" => "inc",
                "jnz" => "cpy",
                "tgl" => "inc"
            };
            instructions[(int)pos] = instructions[(int)pos] with { Name = newInst };
        }
        
        var index = 0L;
        while (index < instructions.Count)
        {
            // Console.WriteLine(string.Join(' ', index, string.Join(' ', registers.Values)));
            var (name, args) = instructions[(int)index];
            switch (name)
            {
                case "cpy":
                    var m1 = Regex.Match(args, @"^(\w|-?\d+) (\w)$");
                    if (m1.Success)
                    {
                        var a = m1.Groups[1].Value;
                        var b = m1.Groups[2].Value;
                        registers[b[0]] = int.TryParse(a, out var val1) ? val1 : registers[a[0]];
                    }
                    break;
                case "inc":
                    var m2 = Regex.Match(args, @"^(\w)$");
                    if (m2.Success)
                    {
                        registers[m2.Groups[1].Value[0]]++;
                    }
                    break;
                case "dec":
                    var m3 = Regex.Match(args, @"^(\w)$");
                    if (m3.Success)
                    {
                        registers[m3.Groups[1].Value[0]]--;
                    }
                    break;
                case "jnz":
                    var m4 = Regex.Match(args, @"^(\w|-?\d+) (\w|-?\d+)$");
                    if (m4.Success)
                    {
                        var a = m4.Groups[1].Value;
                        var b = m4.Groups[2].Value;
                        var value1 = int.TryParse(a, out var v1) ? v1 : registers[a[0]];
                        if (value1 != 0)
                        {
                            var value2 = int.TryParse(b, out var v2) ? v2 : registers[b[0]];
                            index = index + value2;
                            continue;
                        }
                    }
                    break;
                case "tgl":
                    var m5 = Regex.Match(args, @"^(\w|-?\d+)$");
                    if (m5.Success)
                    {
                        var jump = m5.Groups[1].Value;
                        var value = long.TryParse(jump, out long v) ? v : registers[jump[0]];
                        ToggleInstruction(index + value);
                    }
                    break;
                case "mul":
                    var m6 = Regex.Match(args, @"^(\w|-?\d+) (\w)$");
                    if (m6.Success)
                    {
                        var a = m6.Groups[1].Value;
                        var b = m6.Groups[2].Value;
                        registers[b[0]] *= int.TryParse(a, out var val1) ? val1 : registers[a[0]];
                    }
                    break;
                case "add":
                    var m7 = Regex.Match(args, @"^(\w|-?\d+) (\w)$");
                    if (m7.Success)
                    {
                        var a = m7.Groups[1].Value;
                        var b = m7.Groups[2].Value;
                        registers[b[0]] += int.TryParse(a, out var val1) ? val1 : registers[a[0]];
                    }
                    break;
                case "nop":
                    break;
            }
            index++;
        }
        return registers['a'];
    }
}
