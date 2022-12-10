using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 9)]
public class Day9 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = TailVisited(input, 2);
        var result2 = TailVisited(input, 10);

        return new AocDayResult(result1, result2);
    }

    public int TailVisited(string[] input, int knotsCount)
    {
        if (knotsCount == 1)
        {
            return 0;
        }
        var knots = new (int, int)[knotsCount];
        var tailVisited = new HashSet<(int, int)> { (0, 0) };

        foreach (var s in input)
        {
            var sp = s.Split(' ');
            var dir = sp[0][0];
            var len = int.Parse(sp[1]);
            while (--len >= 0)
            {
                var diff = dir switch
                {
                    'U' => (-1,0),
                    'R' => (0,1),
                    'D' => (1,0),
                    'L' => (0,-1)
                };
                MoveKnot(knots, 0, diff);
                tailVisited.Add(knots[^1]);
            }
        }

        return tailVisited.Count;
    }

    private void MoveKnot((int, int)[] knots, int i, (int, int) moveDiff)
    {
        var head = knots[i];
        var movedHead = (head.Item1 + moveDiff.Item1, head.Item2 + moveDiff.Item2);
        knots[i] = movedHead;

        if (i == knots.Length-1)
        {
            return;
        }

        var tail = knots[i+1];

        var afterDiffI = movedHead.Item1 - tail.Item1; 
        var afterDiffJ = movedHead.Item2 - tail.Item2;
        
        if ((Math.Abs(afterDiffI) == 2 && Math.Abs(afterDiffJ) == 0) || // vertically
            (Math.Abs(afterDiffI) == 0 && Math.Abs(afterDiffJ) == 2) || // horizontally
            ((Math.Abs(afterDiffI) + Math.Abs(afterDiffJ) > 2) && (afterDiffI != 0 || afterDiffJ != 0))) // diagonally
        {
            var diffI = (afterDiffI > 0 ? 1 : (afterDiffI < 0) ? -1 : 0);
            var diffJ = (afterDiffJ > 0 ? 1 : (afterDiffJ < 0) ? -1 : 0);
            MoveKnot(knots, i + 1, (diffI, diffJ));
        }
    }
}
