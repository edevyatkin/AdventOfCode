using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 17)]
public class Day17 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = TowerHeight(input, 2022);
        var result2 = TowerHeight(input, 1000000000000);

        return new AocDayResult(result1, result2);
    }

    public long TowerHeight(string[] input, long stoppedFalling)
    {
        var tower = new HashSet<(long I, long J)>();
        var towerHeight = 0L;
        var cache = new Dictionary<(string Hs, int Mi, int Fi), (long Fl, long H)>();

        using var moveIterator = GetNextMove(input[0]).GetEnumerator();
        using var figureIterator = GetNextFigure().GetEnumerator();
        var falling = 1L;

        while (falling <= stoppedFalling)
        {
            figureIterator.MoveNext();
            var fi = figureIterator.Current.Ix;
            var fg = figureIterator.Current.Fg;

            long posFI = 2L;
            long posFJ = fg.Split().Length + towerHeight + 2;
            var sp = fg.Split();
            var figurePositions = new HashSet<(long I, long J)>();
            for (var ci = 0; ci < sp.Length; ci++)
            {
                for (var cj = 0; cj < sp[ci].Length; cj++)
                {
                    if (sp[ci][cj] == '#')
                    {
                        figurePositions.Add((posFI + cj, posFJ));
                    }
                }

                posFJ--;
            }

            bool isNotDown = false;
            moveIterator.MoveNext();
            var mi = moveIterator.Current.Ix;
            while (!isNotDown)
            {
                var positionsAfterMove = new HashSet<(long I, long J)>();
                var move = moveIterator.Current.Ch;
                foreach (var figurePosition in figurePositions)
                {
                    switch (move)
                    {
                        case '>':
                            positionsAfterMove.Add((figurePosition.I + 1, figurePosition.J));
                            break;
                        case '<':
                            positionsAfterMove.Add((figurePosition.I - 1, figurePosition.J));
                            break;
                    }
                }

                if (positionsAfterMove.Any(fp => fp.I is < 0 or > 6) || positionsAfterMove.Overlaps(tower))
                {
                    positionsAfterMove = figurePositions;
                }

                var positionsAfterDown = new HashSet<(long I, long J)>();
                foreach (var figurePosition in positionsAfterMove)
                {
                    positionsAfterDown.Add((figurePosition.I, figurePosition.J - 1));
                }

                if (positionsAfterDown.Any(fp => fp.J < 0) || positionsAfterDown.Overlaps(tower))
                {
                    isNotDown = true;
                    positionsAfterDown = positionsAfterMove;
                }
                else
                {
                    moveIterator.MoveNext();
                }

                figurePositions = positionsAfterDown;
            }

            var hash = new StringBuilder();
            for (var j = towerHeight - 1; j >= towerHeight-12; j--)
            {
                for (int i = 0; i <= 6; i++)
                {
                    hash.Append(tower.Contains((i, j)) ? '#' : '.');
                }
            }
            
            tower.UnionWith(figurePositions);
            
            foreach (var (i, j) in figurePositions)
            {
                towerHeight = Math.Max(towerHeight, j + 1);
            }

            var hashStr = hash.ToString();
            
            if (cache.ContainsKey((hashStr, mi, fi)))
            {
                var (prevFalling, prevTowerHeight) = cache[(hashStr,mi, fi)];
                var fDiff = falling - prevFalling;
                var hDiff = towerHeight - prevTowerHeight;
                var count = (stoppedFalling - falling) / fDiff;
                if (count > 0)
                {
                    towerHeight += count * hDiff;
                    falling += count * fDiff;
                    tower = tower.Select(t => (t.I, t.J + count * hDiff)).ToHashSet();
                }
            }
            else
            {
                cache[(hashStr, mi, fi)] = (falling, towerHeight);
            }
            falling++;
        }

        return towerHeight;
    }

    IEnumerable<(char Ch, int Ix)> GetNextMove(string input)
    {
        var index = 0;
        while (true)
        {
            index %= input.Length;
            yield return (input[index], index);
            index++;
        }
    }

    IEnumerable<(string Fg, int Ix)> GetNextFigure()
    {
        var figures = new List<string>()
        {
            @"####",
            @".#.
###
.#.",
            @"..#
..#
###",
            @"#
#
#
#",
            @"##
##"
        };
        var index = 0;
        while (true)
        {
            index %= figures.Count;
            yield return (figures[index], index);
            index++;
        }
    }
}
