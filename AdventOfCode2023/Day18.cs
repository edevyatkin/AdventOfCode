namespace AdventOfCode2023;

[AocDay(2023,18)]
public class Day18 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = FindLavaLagoonSize(input, false);
        var result2 = FindLavaLagoonSize(input, true);

        return new AocDayResult(result1, result2);
    }

    internal static long FindLavaLagoonSize(string[] input, bool isPartTwo)
    {
        var result = 0L;
        var digPlan = new List<(char Dir, long Len)>();
        foreach (var line in input)
        {
            var sp = line.Split();
            var dir = ' ';
            var len = 0L;
            if (!isPartTwo)
            {
                dir = sp[0][0];
                len = long.Parse(sp[1]);
            }
            else
            {
                dir = sp[2][^2] switch {
                    '0' => 'R',
                    '1' => 'D',
                    '2' => 'L',
                    '3' => 'U',
                    _ => throw new ArgumentOutOfRangeException()
                };
                var lenHex = sp[2][2..^2];
                len = Convert.ToInt64(lenHex, 16);
            }
            digPlan.Add((dir, len));
        }

        var turns = new Dictionary<(char, char), Turn>() {
            [('R', 'D')] = Turn.Right,
            [('D', 'L')] = Turn.Right,
            [('L', 'U')] = Turn.Right,
            [('U', 'R')] = Turn.Right,
            [('R', 'U')] = Turn.Left,
            [('U', 'L')] = Turn.Left,
            [('L', 'D')] = Turn.Left,
            [('D', 'R')] = Turn.Left
        };
        
        var curPos = (0L, 0L);
        var plan = new List<(Turn T, long I, long J)>();
        for (var index = 0; index < digPlan.Count; index++)
        {
            var prev = digPlan[((index-1)+digPlan.Count) % digPlan.Count];
            var cur = digPlan[index];
            var diffs = cur.Dir switch {
                'U' => (-1, 0),
                'R' => (0, 1),
                'D' => (1, 0),
                'L' => (0, -1),
                _ => throw new ArgumentOutOfRangeException()
            };
            curPos.Item1 += diffs.Item1 * cur.Len;
            curPos.Item2 += diffs.Item2 * cur.Len;
            plan.Add((turns[(prev.Dir, cur.Dir)], curPos.Item1, curPos.Item2));
        }

        var countRight = plan.Count(p => p.T == Turn.Right);
        var countLeft = plan.Count - countRight;

        var mainTurn = countRight > countLeft ? Turn.Right : Turn.Left;
        var otherTurn = mainTurn == Turn.Left ? Turn.Right : Turn.Left;

        var planIx = plan.FindIndex(p => p.T == mainTurn);
        var adds = new Dictionary<(Turn, Turn), int>() {
            [(mainTurn, mainTurn)] = 1,
            [(mainTurn, otherTurn)] = 0,
            [(otherTurn, mainTurn)] = 0,
            [(otherTurn, otherTurn)] = -1
        };
        
        var pos = (0L, 0L);
        var positions = new List<(long, long)>() { (0,0) };
        for (int i = planIx; i != (planIx-1+plan.Count) % plan.Count; i = (i + 1) % plan.Count)
        {
            var cur = plan[i];
            var next = plan[(i+1) % plan.Count];
            var nextNext = plan[(i+2) % plan.Count];
            var dist = (next.I - cur.I + next.J - cur.J);
            dist += adds[(next.T, nextNext.T)] * (dist < 0 ? -1 : 1);
            if (cur.I != next.I)
                pos.Item1 += dist;
            else
                pos.Item2 += dist;
            positions.Add(pos);
        }
        
        for (int i = 0; i < positions.Count; i++)
        {
            var cur = positions[i];
            var next = positions[(i + 1) % positions.Count];
            result += cur.Item1 * next.Item2 - cur.Item2 * next.Item1;
        }

        return Math.Abs(result / 2);
    }
}

public enum Turn
{
    Left,
    Right
}


