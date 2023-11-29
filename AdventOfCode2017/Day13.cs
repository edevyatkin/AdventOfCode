using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,13)]
public class Day13 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var dict = new Dictionary<int, Info>();
        foreach (var line in input)
        {
            var sp = line.Split(": ").Select(int.Parse).ToArray();
            var (depth, range) = (sp[0], sp[1]);
            dict[depth] = new (range, 0, 1);
        }

        var maxIndex = dict.Keys.Max();
        var arr = new Info[maxIndex+1];
        Array.Fill(arr, new Info(0,-1,0));
        foreach (var (k,v) in dict)
            arr[k] = v;
        
        var result1 = 0;
        var result2 = 0;
        var delay = 0;
        var cache = new List<Info[]> { arr };
        while (result2 == 0)
        {
            var d = -1;
            var res = 0;
            var isCaught = false;
            while (++d <= maxIndex)
            {
                var info = cache[d][d];
                if (info.Pos == 0)
                {
                    res += d * info.Range;
                    isCaught = true;
                }
                
                if (cache.Count-1 < d+1)
                {
                    var copy = (Info[])cache[d].Clone();
                    for (int kd = 0; kd <= maxIndex; kd++)
                    {
                        var kdInfo = copy[kd];
                        if (kdInfo.Range < 2)
                            continue;
                        if (kdInfo.Pos == 0 && kdInfo.Dir == -1)
                            kdInfo.Dir = 1;
                        if (kdInfo.Pos == kdInfo.Range-1 && kdInfo.Dir == 1)
                            kdInfo.Dir = -1;
                        kdInfo.Pos += kdInfo.Dir;
                        copy[kd] = kdInfo;
                    }
                    cache.Add(copy);
                    if (cache.Count > maxIndex+1)
                        cache.RemoveAt(0);
                }
            }

            if (delay == 0)
                result1 = res;
            if (!isCaught)
                result2 = delay;
            
            delay++;
        }
        
        return new AocDayResult(result1, result2);
    }

    private record struct Info(int Range, int Pos, int Dir);
}
