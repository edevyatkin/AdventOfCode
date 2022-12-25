using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 22)]
public class Day22 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = CalculatePassword(input, false);
        var result2 = CalculatePassword(input, true);

        return new AocDayResult(result1, result2);
    }

    internal int CalculatePassword(string[] input, bool isPartTwo)
    {
        int i = 0, j = 0;
        while (input[i][j] != '.')
            j++;

        var facing = new (int Di, int Dj)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var f = 0;
        var transitions = new Dictionary<(int I, int J, int F), (int I, int J, int F)>();
        var inst = input[^1];
        var map = input[..^2];
        PadMap(map);
        var pi = 0;
        while (pi < inst.Length)
        {
            var n = 0;
            while (pi < inst.Length && char.IsDigit(inst[pi]))
            {
                n = 10 * n + (inst[pi] - '0');
                pi++;
            }

            var lastInMap = (-1, -1);
            while (n > 0)
            {
                var (ni, nj) = (i + facing[f].Di, j + facing[f].Dj);
                ni = (map.Length + ni) % map.Length;
                nj = (map[ni].Length + nj) % map[ni].Length;
                var nf = f;

                if (!isPartTwo)
                {
                    if (map[ni][nj] == ' ')
                    {
                        if (lastInMap == (-1, -1))
                        {
                            lastInMap = (i, j);
                        }

                        (i, j) = (ni, nj);
                        continue;
                    }
                }
                else
                {
                    if (transitions.Count == 0)
                    {
                        transitions = GenerateTransitions(map);
                    }

                    if (transitions.ContainsKey((i, j, f)))
                    {
                        (ni, nj, nf) = transitions[(i, j, f)];
                    }
                }

                if (map[ni][nj] == '#')
                {
                    if (!isPartTwo && map[i][j] == ' ')
                    {
                        (i, j) = lastInMap;
                    }

                    break;
                }

                if (map[ni][nj] == '.')
                {
                    (i, j, f) = (ni, nj, nf);
                    n--;
                }
            }

            if (pi < inst.Length && char.IsLetter(inst[pi]))
            {
                var sh = inst[pi] switch
                {
                    'R' => 1,
                    'L' => -1
                };
                f = (4 + (f + sh)) % 4;
                pi++;
            }
        }

        return 1000 * (i + 1) + 4 * (j + 1) + f;
    }

    // generate transitions from an edge cell to another edge cell
    private Dictionary<(int I, int J, int F), (int I, int J, int F)> GenerateTransitions(string[] map)
    {
        PadMap(map);
        var dict = new Dictionary<(int, int, int), (int, int, int)>();
        var corners = SearchForInnerCorners(map);
        var facing = new (int Di, int Dj)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        foreach (var (i, j, di, dj) in corners)
        {
            var (dir1, dir2) = ((di, 0), (0, dj));
            (int I, int J) c1 = (i + dir1.Item1, j + dir1.Item2);
            (int I, int J) c2 = (i + dir2.Item1, j + dir2.Item2);
            while (true)
            {
                var c1R = IsNeedRotate(map, c1, dir1);
                var c2R = IsNeedRotate(map, c2, dir2);
                var bothRotate = c1R && c2R;

                // for a cube with 1-size it does not work :(
                var c1S = FindSides(map, c1, dir1);
                var c2S = FindSides(map, c2, dir2);

                dict[(c1.I, c1.J, c1S.E)] = (c2.I, c2.J, c2S.I);
                dict[(c2.I, c2.J, c2S.E)] = (c1.I, c1.J, c1S.I);

                if (bothRotate)
                {
                    break;
                }

                if (!c1R)
                {
                    c1 = (c1.I + dir1.Item1, c1.J + dir1.Item2);
                }
                else
                {
                    dir1 = facing[c1S.I];
                }

                if (!c2R)
                {
                    c2 = (c2.I + dir2.Item1, c2.J + dir2.Item2);
                }
                else
                {
                    dir2 = facing[c2S.I];
                }
            }
        }

        return dict;
    }

    internal bool IsNeedRotate(string[] map, (int I, int J) c, (int Di, int Dj) d)
    {
        (int I, int J) cF = (c.I + d.Di, c.J + d.Dj);
        return (cF.I < 0 || cF.I == map.Length ||
                cF.J < 0 || cF.J == map[cF.I].Length ||
                map[cF.I][cF.J] == ' ');
    }

    // find external side and internal side. both are relative to map, not direction of edge traversing
    internal (int E, int I) FindSides(string[] map, (int I, int J) c, (int Di, int Dj) d)
    {
        var facing = new (int Di, int Dj)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var facingRev = new Dictionary<(int, int), int>
        {
            [(0, 1)] = 0, [(1, 0)] = 1, [(0, -1)] = 2, [(-1, 0)] = 3
        };
        var f = facingRev[d];
        var fL = (4 + (f - 1)) % 4;
        var fR = (4 + (f + 1)) % 4;
        var dL = facing[fL];
        var dR = facing[fR];
        (int I, int J) cL = (c.I + dL.Di, c.J + dL.Dj);
        (int I, int J) cR = (c.I + dR.Di, c.J + dR.Dj);

        int i = 0, e = 0;
        bool li = false, le = false, ri = false, re = false;
        if (cL.I < 0 || cL.I == map.Length || cL.J < 0 || cL.J == map[cL.I].Length || map[cL.I][cL.J] == ' ')
        {
            le = true;
        }
        else
        {
            li = true;
        }

        if (cR.I < 0 || cR.I == map.Length || cR.J < 0 || cR.J == map[cR.I].Length || map[cR.I][cR.J] == ' ')
        {
            re = true;
        }
        else
        {
            ri = true;
        }

        if (li == ri || le == re)
        {
            throw new ArgumentException("wrong side detection");
        }

        if (le && ri)
        {
            (e, i) = (fL, fR);
        }

        if (li && re)
        {
            (e, i) = (fR, fL);
        }

        return (e, i);
    }


    private void PadMap(string[] map)
    {
        var maxWidth = map.Max(s => s.Length);
        for (int k = 0; k < map.Length; k++)
        {
            map[k] = map[k].PadRight(maxWidth);
        }
    }

    // search for inner corners
    internal List<(int I, int J, int Di, int Dj)> SearchForInnerCorners(string[] map)
    {
        PadMap(map);
        var lst = new List<(int, int, int, int)>();
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                var emptyNeighbours = FindEmptyNeighbours(map, i, j);
                if (emptyNeighbours.Count == 1)
                {
                    lst.Add((i, j, emptyNeighbours[0].Di, emptyNeighbours[0].Dj));
                }
            }
        }

        return lst;
    }

    private List<(int Di, int Dj)> FindEmptyNeighbours(string[] map, int i, int j)
    {
        var emptyNeighbours = new List<(int Di, int Dj)>();
        for (int di = -1; di <= 1; di++)
        {
            for (int dj = -1; dj <= 1; dj++)
            {
                if (di == 0 && dj == 0) continue;
                var (ni, nj) = (i + di, j + dj);
                if (ni < 0 || ni == map.Length || nj < 0 || nj == map[i].Length) continue;
                if (map[ni][nj] == ' ')
                {
                    emptyNeighbours.Add((di, dj));
                }
            }
        }

        return emptyNeighbours;
    }

    void Dump(string[] input, Dictionary<(int, int), int> path, int toGo, int factGo)
    {
        Console.WriteLine($"toGo={toGo}, facGo={factGo}");
        for (var ii = 0; ii < input.Length - 2; ii++)
        {
            for (var jj = 0; jj < input[ii].Length; jj++)
            {
                var c = input[ii][jj];
                if (path.ContainsKey((ii, jj)))
                {
                    c = path[(ii, jj)] switch
                    {
                        0 => '>',
                        1 => 'v',
                        2 => '<',
                        3 => '^'
                    };
                    if (input[ii][jj] == '#' || input[ii][jj] != '.')
                    {
                        Console.WriteLine($"ERROR!!11! {(ii, jj, c)}");
                        return;
                    }
                }

                Console.Write(c);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}
