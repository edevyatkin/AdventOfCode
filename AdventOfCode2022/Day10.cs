using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,10)]
public class Day10 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var x = 1;
        var c = 0;
        var crt = new char[240];
        var cycles = new[] { 20, 60, 100, 140, 180, 220 };
        foreach (var s in input)
        {
            var sp = s.Split(' ');
            var cc = (sp[0] == "noop") ? 1 : 2;
            var xd = (sp[0] == "noop") ? 0 : int.Parse(sp[1]);;
            while (--cc >= 0)
            {
                crt[c] = Math.Abs(c % 40 - x) <= 1 ? '#': '.';
                c += 1;
                if (cycles.Contains(c))
                {
                    result1 += x * c;
                }    
            }
            x += xd;
        }

        Display(crt);

        return new AocDayResult(result1, result2);
    }

    private void Display(char[] crt)
    {
        for (var index = 0; index < crt.Length; index++)
        {
            Console.Write(crt[index]);
            if ((index + 1) % 40 == 0)
            {
                Console.WriteLine();
            }
        }
    }
}
