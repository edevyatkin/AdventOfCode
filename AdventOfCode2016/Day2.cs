using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,2)]
public class Day2 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var button = 5;
        foreach (var line in input)
        {
            foreach (var c in line)
            {
                if (c == 'U' && button >= 4)
                    button -= 3;
                if (c == 'D' && button <= 6)
                    button += 3;
                if (c == 'L' && button is not (1 or 4 or 7))
                    button--;
                if (c == 'R' && button is not (3 or 6 or 9))
                    button++;
            }
            result1 = 10 * result1 + button;
        }

        var result2 = string.Empty;
        var keypad = new[]
        {
            "  1  ",
            " 234 ",
            "56789",
            " ABC ",
            "  D  "
        };
        int bi = 2, bj = 0;
        foreach (var line in input)
        {
            foreach (var c in line)
            {
                if (c == 'U' && bi-1 >= 0 && keypad[bi-1][bj] != ' ')
                    bi--;
                if (c == 'D' && bi+1 < keypad.Length && keypad[bi+1][bj] != ' ')
                    bi++;
                if (c == 'L' && bj-1 >= 0 && keypad[bi][bj-1] != ' ')
                    bj--;
                if (c == 'R' && bj+1 < keypad[0].Length && keypad[bi][bj+1] != ' ')
                    bj++;
            }
            result2 += keypad[bi][bj];
        }
        return new AocDayResult(result1, result2);
    }
}
