using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,8)]
public class Day8 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var lcd = new bool[6][];
        for (var i = 0; i < lcd.Length; i++)
        {
            lcd[i] = new bool[50];
        }
        foreach (var op in input)
        {
            if (op.Contains("rect"))
            {
                var rectSize = op.Split()[1].Split('x').Select(int.Parse).ToArray();
                for (int i = 0; i < rectSize[1]; i++)
                {
                    for (int j = 0; j < rectSize[0]; j++)
                    {
                        lcd[i][j] = true;
                    }
                }
            } else if (op.Contains("rotate row"))
            {
                var rowShift = op.Split('=')[1].Split(" by ").Select(int.Parse).ToArray();
                var (i, sh) = (rowShift[0], rowShift[1]);
                var shifted = new bool[50];
                for (int j = 0; j < 50; j++)
                {
                    if (lcd[i][j] == true)
                    {
                        shifted[(j + sh) % 50] = true;
                    }
                }
                lcd[i] = shifted;
            } else if (op.Contains("rotate column"))
            {
                var colShift = op.Split('=')[1].Split(" by ").Select(int.Parse).ToArray();
                var (j, sh) = (colShift[0], colShift[1]);
                var shifted = new bool[6];
                for (int i = 0; i < 6; i++)
                {
                    if (lcd[i][j] == true)
                    {
                        shifted[(i + sh) % 6] = true;
                    }
                }

                for (int i = 0; i < 6; i++)
                {
                    lcd[i][j] = shifted[i];
                }
            }
        }

        var result1 = lcd.SelectMany(r => r).Count(e => e == true);
        
        ShowLcd(lcd);
        
        var result2 = 0;
        return new AocDayResult(result1, result2);
    }

    void ShowLcd(bool[][] lcd)
    {
        foreach (var line in lcd)
        {
            Console.WriteLine(new string(line.Select(e => e ? '#' : ' ').ToArray()));
        }
    }
}
