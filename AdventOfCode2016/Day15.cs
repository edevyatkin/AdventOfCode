using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,15)]
public class Day15 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var disks = new List<(int PosCount, int StartPos)>();
        foreach (var diskStr in input)
        {
            var sp = diskStr[..^1].Split();
            var diskPosCount = int.Parse(sp[3]);
            var diskStartPos = int.Parse(sp[^1]);
            disks.Add((diskPosCount, diskStartPos));
        }
        var result1 = FindTime(new List<(int, int)>(disks));
        disks.Add((11,0));
        var result2 = FindTime(disks);
        return new AocDayResult(result1, result2);
    }

    private static int FindTime(List<(int PosCount, int StartPos)> disks)
    {
        var time = 0;
        while (true)
        {
            var movedDisks =
                disks.Select((d, ix) => (d.StartPos + ix + 1 + time) % d.PosCount);
            if (movedDisks.All(p => p == 0))
                break;
            time++;
        }
        return time;
    }
}
