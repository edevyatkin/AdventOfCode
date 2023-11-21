using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,9)]
public class Day9 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var stream = input[0];
        var result1 = 0;
        var result2 = 0;
        var group = 0;
        bool inGarbage = false;
        for (int i = 0; i < stream.Length; i++)
        {
            var c = stream[i];
            if (c == '<' && !inGarbage)
            {
                inGarbage = true;
            }
            else if (c == '!') 
            {
                i++;
            }
            else if (c == '>')
            {
                inGarbage = false;
            }
            else if (inGarbage)
            {
                result2++;
            }
            else if (c == '{') 
            {
                group++;
            } 
            else if (c  == '}' && group > 0) 
            {
                result1 += group;
                group--;
            }
        }
        return new AocDayResult(result1, result2);
    }
}
