using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024, 16)]
public class Day16 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var si = input.Length - 2;
        var sj = 1;
        var ei = 1;
        var ej = input.Length - 2;
        
        var scoresFromStart = CalculateScores(input, si, sj, 1);
        var scoresFromEndSouth = CalculateScores(input, ei, ej, 2);
        var scoresFromEndWest = CalculateScores(input, ei, ej, 3);

        result1 = Math.Min(
            Math.Min(scoresFromStart[ei, ej, 0], scoresFromStart[ei, ej, 1]),
            Math.Min(scoresFromStart[ei, ej, 2], scoresFromStart[ei, ej, 3])
        );

        var tilesOnPathsWithLowestScore = new HashSet<(int,int)>();
        for (var i = 1; i <= input.Length - 2; i++)
        for (var j = 1; j <= input[0].Length - 2; j++)
        for (var k = 0; k < 4; k++)
            if (scoresFromStart[i, j, k] + scoresFromEndSouth[i, j, (k + 2) % 4] == result1 || 
                scoresFromStart[i, j, k] + scoresFromEndWest[i, j, (k + 2) % 4] == result1)
                tilesOnPathsWithLowestScore.Add((i, j));

        result2 = tilesOnPathsWithLowestScore.Count;

        return new AocDayResult(result1, result2);
    }
    
    private static readonly (int Di, int Dj)[] Diffs = [(-1, 0), (0, 1), (1, 0), (0, -1)];
    
    private static int[,,] CalculateScores(string[] input, int startI, int startJ, int startDir)
    {
        var scores = new int[input.Length, input[0].Length, 4];
        
        for (var i = 0; i < input.Length; i++)
        for (var j = 0; j < input[0].Length; j++)
        for (var k = 0; k < 4; k++)
            scores[i, j, k] = int.MaxValue;
        
        scores[startI, startJ, startDir] = 0;
        
        var q = new PriorityQueue<(int I, int J, int Dir, int Score), int>();
        q.Enqueue((startI, startJ, startDir, 0), 0);
        
        while (q.Count > 0)
        {
            var (i, j, dir, score) = q.Dequeue();
            if (score > scores[i, j, dir])
                continue;
            for (var newDir = 0; newDir < Diffs.Length; newDir++)
            {
                if ((dir + 2) % 4 == newDir) 
                    continue;
                var (di, dj) = Diffs[newDir];
                var ni = i + di;
                var nj = j + dj;
                if (input[ni][nj] == '#')
                    continue;
                int addition;
                if (dir == newDir)
                {
                    addition = 1;
                }
                else
                {
                    addition = 1000;
                    ni = i;
                    nj = j;
                }
                var newScore = score + addition;
                if (scores[ni, nj, newDir] > newScore)
                {
                    scores[ni, nj, newDir] = newScore;
                    q.Enqueue((ni, nj, newDir, newScore), newScore);
                }
            }
        }

        return scores;
    }
}
