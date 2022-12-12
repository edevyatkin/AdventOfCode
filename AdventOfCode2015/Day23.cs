using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015, 23)]
public class Day23 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = DoOperations(input, false);
        var result2 = DoOperations(input, true);

        return new AocDayResult(result1, result2);
    }

    private static int DoOperations(string[] input, bool isPartTwo)
    {
        var ra = !isPartTwo ? 0 : 1;
        var rb = 0;
        var i = 0;
        while (i >= 0 && i < input.Length)
        {
            var sp = input[i].Split();
            switch (sp[0])
            {
                case "hlf":
                    if (sp[1] == "a")
                    {
                        ra /= 2;
                    }
                    else
                    {
                        rb /= 2;
                    }

                    break;
                case "tpl":
                    if (sp[1] == "a")
                    {
                        ra *= 3;
                    }
                    else
                    {
                        rb *= 3;
                    }

                    break;
                case "inc":
                    if (sp[1] == "a")
                    {
                        ra++;
                    }
                    else
                    {
                        rb++;
                    }

                    break;
                case "jmp":
                    var off1 = int.Parse(sp[1]);
                    i += off1;
                    continue;
                case "jie":
                    var er = sp[1];
                    var off2 = int.Parse(sp[2]);
                    if (er == "a,")
                    {
                        if (ra % 2 == 0)
                        {
                            i += off2;
                            continue;
                        }
                    }
                    else
                    {
                        if (rb % 2 == 0)
                        {
                            i += off2;
                            continue;
                        }
                    }

                    break;
                case "jio":
                    var or = sp[1];
                    var off3 = int.Parse(sp[2]);
                    if (or == "a,")
                    {
                        if (ra == 1)
                        {
                            i += off3;
                            continue;
                        }
                    }
                    else
                    {
                        if (rb == 1)
                        {
                            i += off3;
                            continue;
                        }
                    }

                    break;
            }

            i++;
        }

        return rb;
    }
}
