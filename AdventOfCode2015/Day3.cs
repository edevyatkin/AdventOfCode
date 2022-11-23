using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,3)]
public class Day3 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var hs = new HashSet<(int, int)> { (0, 0) };
        int i = 0, j = 0;
        foreach (var move in input[0])
        {
            if (move == '>')
                j++;
            else if (move == '<')
                j--;
            else if (move == 'v') 
                i++;
            else if (move == '^')
                i--;

            hs.Add((i, j));
        }

        result1 = hs.Count;
        
        var result2 = 0;
        int i1 = 0, j1 = 0, i2 = 0, j2 = 0;
        var hs2 = new HashSet<(int, int)> { (0, 0) };
        for (var index = 0; index < input[0].Length; index++)
        {
            var move = input[0][index];
            if (move == '>')
                if (index % 2 == 0) j1++; else j2++;
            else if (move == '<')
                if (index % 2 == 0) j1--; else j2--;
            else if (move == 'v')
                if (index % 2 == 0) i1++; else i2++;
            else if (move == '^')
                if (index % 2 == 0) i1--; else i2--;

            hs2.Add((i1, j1));
            hs2.Add((i2, j2));
        }

        result2 = hs2.Count;
        
        return new AocDayResult(result1, result2);
    }
}