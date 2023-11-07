using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,25)]
public class Day25 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var registers = new Dictionary<char, long>()
        {
            ['a'] = 0,
            ['b'] = 0,
            ['c'] = 0,
            ['d'] = 0,
        };

        List<(string Name, string Args)> instructions = input.Select(i => (i[..3],i[4..])).ToList();

        var num = int.Parse(instructions[2].Args.Split()[0]);
        instructions[2] = ("mul", $"{num} c");
        instructions[3] = ("add", "c d");
        instructions[4] = ("cpy", "0 b");
        instructions[5] = ("cpy", "0 c");
        instructions[6] = ("nop", string.Empty);
        instructions[7] = ("nop", string.Empty);

        var val = 0;
        while (true)
        {
            registers['a'] = ++val;
            var counter = 0;
            var index = 0L;
            var signal = 1L;
            var found = false;
            var skipNextA = false;
            while (index < instructions.Count)
            {
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
                    case "out":
                        var m8 = Regex.Match(args, @"^(\w)$");
                        if (m8.Success)
                        {
                            var value = m8.Groups[1].Value;
                            var curSignal = int.TryParse(value, out var val1) ? val1 : registers[value[0]];
                            if ((curSignal ^ signal) == 1L)
                            {
                                counter++;
                                if (counter > 10)
                                {
                                    result1 = val;
                                    found = true;
                                    break;
                                }
                                signal = curSignal;
                            } 
                            else
                            {
                                counter = 0;
                                signal = 1L;
                                skipNextA = true;
                            }
                        }                        
                        break;
                }
                if (found) break;
                if (skipNextA) break;
                index++;
            }
            if (found) break;
        }

        var result2 = 0;
        return new AocDayResult(result1, result2);
    }
}
