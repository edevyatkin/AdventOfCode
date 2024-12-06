using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,6)]
public class Day6 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        
        var startI = 0;
        var startJ = 0;

        for (var i = 0; i < input.Length; i++)
            for (var j = 0; j < input[0].Length; j++)
                if (input[i][j] == '^')
                    (startI, startJ) = (i,j);

        var dirs = new List<(int Di, int Dj)> { (-1,0), (0,1), (1,0), (0,-1) };
        
        var (curI, curJ) = (startI, startJ);
        var visited = new HashSet<(int,int)> { (curI, curJ) };
        var curDir = 0;
        while (true)
        {
            var ni = curI + dirs[curDir].Di;
            var nj = curJ + dirs[curDir].Dj;
            if (ni < 0 || ni >= input.Length || nj < 0 || nj >= input[0].Length)
                break;
            if (input[ni][nj] == '#')
            {
                curDir = (curDir + 1) % 4;
            }
            else
            {
                (curI, curJ) = (ni, nj);
                visited.Add((curI, curJ));
            }
        }
        
        result1 = visited.Count;

        foreach (var (oi,oj) in visited) {
            if (input[oi][oj] == '^')
                continue;
            curI = startI;
            curJ = startJ;
            curDir = 0;
            var visited2 = new HashSet<(int,int,int)> { (curI, curJ, curDir) };
            while (true)
            {
                var ni = curI + dirs[curDir].Di;
                var nj = curJ + dirs[curDir].Dj;
                if (ni < 0 || ni >= input.Length || nj < 0 || nj >= input[0].Length)
                    break;
                if (input[ni][nj] == '#' || ni == oi && nj == oj)
                {
                    curDir = (curDir + 1) % 4;
                }
                else
                {
                    (curI, curJ) = (ni, nj);
                    if (!visited2.Add((curI, curJ, curDir)))
                    {
                        result2++;
                        break;
                    }
                }
            }
        }

        return new AocDayResult(result1, result2);
    }
}
