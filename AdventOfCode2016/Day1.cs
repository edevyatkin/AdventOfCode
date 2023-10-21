using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,1)]
public class Day1 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var diff = new[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        var dir = 0;
        int posX = 0, posY = 0;
        var moves = input[0].Split(", ").Select(m => (m[0], int.Parse(m[1..]))).ToList();
        var visited = new HashSet<(int, int)>() { (0,0) };
        bool isPartTwoSolved = false;
        foreach (var m in moves)
        {
            dir += m.Item1 == 'L' ? -1 : 1;
            dir = (dir + 4) % 4;
            for (int d = 1; d <= m.Item2; d++)
            {
                posX += diff[dir].Item1;
                posY += diff[dir].Item2;
                if (!visited.Add((posX, posY)) && !isPartTwoSolved)
                {
                    result2 = Math.Abs(posX) + Math.Abs(posY);
                    isPartTwoSolved = true;
                }
            }
        }
        result1 = Math.Abs(posX) + Math.Abs(posY);
        return new AocDayResult(result1, result2);
    }
}
