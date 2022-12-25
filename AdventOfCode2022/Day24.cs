using AdventOfCodeClient;

namespace AdventOfCode2022;

using Blizzards = Dictionary<(int, int), List<(int, int)>>;

[AocDay(2022, 24)]
public class Day24 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = CalculateResult(input, false);
        var result2 = CalculateResult(input, true);

        return new AocDayResult(result1, result2);
    }

    private int CalculateResult(string[] input, bool isPartTwo)
    {
        var blizzards = ParseBlizzards(input);
        int result;
        if (!isPartTwo)
        {
            var (se, _) = FindMinTime(input, blizzards, 0, 1, input.Length - 1, input[0].Length - 2);
            result = se;
        }
        else
        {
            var (se, bl) = FindMinTime(input, blizzards, 0, 1, input.Length - 1, input[0].Length - 2);
            var (es, bl2) = FindMinTime(input, bl, input.Length - 1, input[0].Length - 2, 0, 1);
            var (se2, _) = FindMinTime(input, bl2, 0, 1, input.Length - 1, input[0].Length - 2);
            result = se + es + se2;
        }

        return result;
    }

    private (int, Blizzards) FindMinTime(string[] input, Blizzards blizzards, int fi, int fj, int ti, int tj)
    {
        var q = new Queue<(int, int, int)>();
        q.Enqueue((fi, fj, 0));
        var cache = new HashSet<(int, int, int)>();
        var dirs = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        var prevT = -1;
        while (q.Count > 0)
        {
            var (i, j, t) = q.Dequeue();

            if (t > prevT)
            {
                blizzards = MoveBlizzards(blizzards);
                prevT = t;
            }

            var d = Dist(i, j);
            if (d == 1)
            {
                return (t + 1, blizzards);
            }

            // wait if possible
            if (!blizzards.ContainsKey((i, j)))
            {
                MoveTo(i, j, t + 1);
            }

            foreach (var (mdi, mdj) in dirs)
            {
                var (mpi, mpj) = (i + mdi, j + mdj);
                if (mpi < 1 || mpi >= input.Length - 1 || mpj < 1 || mpj >= input[i].Length - 1)
                {
                    continue;
                }

                if (!blizzards.ContainsKey((mpi, mpj))) // move to empty cells
                {
                    MoveTo(mpi, mpj, t + 1);
                }
            }
        }

        Dictionary<(int, int), List<(int, int)>> MoveBlizzards(Blizzards bl)
        {
            var bl2 = new Blizzards();
            foreach (((int I, int J) Bp, List<(int, int)> Bdl) in bl)
            {
                foreach (var Bd in Bdl)
                {
                    var (ni, nj) = ApplyDiff(Bp.I, Bp.J, Bd);
                    if (!bl2.ContainsKey((ni, nj)))
                    {
                        bl2[(ni, nj)] = new List<(int, int)>();
                    }

                    bl2[(ni, nj)].Add((Bd.Item1, Bd.Item2));
                }
            }

            return bl2;
        }

        int Dist(int i, int j) => Math.Abs(i - ti) + Math.Abs(j - tj);

        (int, int) ApplyDiff(int i, int j, (int, int) d)
        {
            var (ni, nj) = (i + d.Item1, j + d.Item2);
            ni = (ni == 0) ? input.Length - 2 : ni;
            ni = (ni == input.Length - 1) ? 1 : ni;
            nj = (nj == 0) ? input[0].Length - 2 : nj;
            nj = (nj == input[0].Length - 1) ? 1 : nj;
            return (ni, nj);
        }

        void MoveTo(int i, int j, int nt)
        {
            var tpl = (i, j, nt);
            if (!cache.Contains(tpl))
            {
                q.Enqueue((i, j, nt));
                cache.Add(tpl);
            }
        }

        return default;
    }

    private Dictionary<(int, int), List<(int, int)>> ParseBlizzards(string[] input)
    {
        var blizzards = new Blizzards();
        for (int i = 1; i < input.Length - 1; i++)
        {
            for (int j = 1; j < input[i].Length - 1; j++)
            {
                if (input[i][j] != '.')
                {
                    var blzDir = input[i][j] switch
                    {
                        '^' => (-1, 0),
                        'v' => (1, 0),
                        '>' => (0, 1),
                        '<' => (0, -1)
                    };
                    blizzards[(i, j)] = new List<(int, int)> { blzDir };
                }
            }
        }

        return blizzards;
    }
}
