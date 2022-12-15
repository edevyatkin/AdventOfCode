using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,15)]
public class Day15 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var data = ParseInput(input);

        var result1 = GetPart1Result(data, 2_000_000);
        var result2 = GetPart2Result(data, 4_000_000);

        return new AocDayResult(result1, result2);
    }

    internal List<(int Sx, int Sy, int Bx, int By)> ParseInput(string[] input)
    {
        var data = new List<(int, int, int, int)>();
        foreach (var s in input)
        {
            var match = Regex.Match(s, @".*x=(?<sx>-?\d+), y=(?<sy>-?\d+):.*x=(?<bx>-?\d+), y=(?<by>-?\d+)");
            var sx = int.Parse(match.Groups[1].Value);
            var sy = int.Parse(match.Groups[2].Value);
            var bx = int.Parse(match.Groups[3].Value);
            var by = int.Parse(match.Groups[4].Value);
            data.Add((sx, sy, bx, by));
        }

        return data;
    }

    public long GetPart2Result(List<(int, int, int, int)> data, int max)
    {
        var result2 = 0L;
        for (int r = 0; r <= max; r++)
        {
            var intervals = GetIntervalsForRow(data, r);
            var first = Array.FindIndex(intervals, interval => interval.R >= 0);
            var last = Math.Max(intervals.Length, Array.FindIndex(intervals, interval => interval.L > max));
            var intForRow = intervals[first..last];
            if (intForRow.Length == 2)
            {
                result2 = (intForRow[0].R + 1) * 4000000L + r;
                break;
            }
        }

        return result2;
    }

    public int GetPart1Result(List<(int Sx, int Sy, int Bx, int By)> data, int row)
    {
        var intervals = GetIntervalsForRow(data, row);
        return intervals.Sum(v => v.R - v.L + 1) - 
               data.Where(d => d.By == row).DistinctBy(b=> (b.Bx,b.By)).Count();
    }
    
    private (int L, int R)[] GetIntervalsForRow(List<(int, int, int, int)> data, int row)
    {
        var intervals = new List<(int, int)>();
        foreach (var (sx, sy, bx, by) in data)
        {
            var md = Math.Abs(sx - bx) + Math.Abs(sy - by);
            var dist = Math.Abs(sy - row);
            if (dist > md)
            {
                continue;
            }
            var diff = md - dist;
            intervals.Add((sx-diff, sx+diff));
        }

        return MergeIntervals(intervals);
    }

    private (int L, int R)[] MergeIntervals(List<(int L, int R)> intervals)
    {
        var result = new List<(int L, int R)>();
        intervals.Sort();
        for (var i = 0; i < intervals.Count; i++)
        {
            var interval = intervals[i];
            for (var j = i+1; j < intervals.Count; j++)
            {
                var next = intervals[j];
                if (interval.R < next.L)
                {
                    break;
                }
                if (interval.R >= next.R)
                {
                    i++;
                    continue;
                } 
                if (interval.R >= next.L)
                {
                    i++;
                    interval = (interval.L, next.R);
                }
            }
            result.Add(interval);
        }
        return result.ToArray();
    }
}
