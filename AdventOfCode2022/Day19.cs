using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 19)]
public class Day19 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 1;

        for (var i = 0; i < input.Length; i++)
        {
            var blueprint = ParseBlueprint(input[i]);
            Console.WriteLine(blueprint);
            var maxGeodes1 = MaxGeodes(blueprint, 24);
            var qualityLevel = (i + 1) * maxGeodes1;
            result1 += qualityLevel;
            if (i < 3)
            {
                var maxGeodes2 = MaxGeodes(blueprint, 32);
                result2 *= maxGeodes2;
            }
        }

        return new AocDayResult(result1, result2);
    }

    private int MaxGeodes(Blueprint blp, int tmax)
    {
        var q = new PriorityQueue<(int Or, int Cl, int Obs, int Ge, int OrR, int ClR, int ObsR, int GeR, int T), long>();
        var cache = new HashSet<(int Or, int Cl, int Obs, int Ge, int OrR, int ClR, int ObsR, int GeR, int T)>();

        q.Enqueue((0, 0, 0, 0, 1, 0, 0, 0, tmax), long.MinValue);
        var result = 0;
        int counter = 0, hits = 0, maxGeHits = 0;
        var maxOre = new[] { blp.OreRobotOre, blp.ClayRobotOre, blp.ObsidianRobotOre, blp.GeodeRobotOre }.Max();
        while (q.Count > 0)
        {
            counter++;
            var d = q.Dequeue();
            var (or, cl, obs, ge, orr, clr, obsr, ger, t) = d;
            if (counter % 1000000 == 0)
            {
                Console.WriteLine($"{d}, counter={counter} cacheHits={hits} ({hits / (double)counter * 100:0.00000}%, " 
                                  + $"maxGeHits={maxGeHits} ({maxGeHits / (double)counter * 100:0.00000}%)");
            }

            result = Math.Max(result, ge);

            if (t == 0)
            {
                continue;
            }

            var maxGe = ge + t * (ger + (ger + t - 1)) / 2;
            if (maxGe <= result)
            {
                maxGeHits++;
                continue;
            }

            if (cache.Contains(d))
            {
                hits++;
                continue;
            }

            cache.Add(d);

            orr = Math.Min(orr, maxOre);
            clr = Math.Min(clr, blp.ObsidianRobotClay);
            obsr = Math.Min(obsr, blp.GeodeRobotObsidian);

            var next = (or + orr, cl + clr, obs + obsr, ge + ger, orr, clr, obsr, ger, t - 1);
            q.Enqueue(next, GetPriority(next, blp));
            if (or >= blp.OreRobotOre)
            {
                next = (or + orr - blp.OreRobotOre, cl + clr, obs + obsr, ge + ger, orr + 1, clr, obsr, ger, t - 1);
                q.Enqueue(next, GetPriority(next, blp));
            }

            if (or >= blp.ClayRobotOre)
            {
                next = (or + orr - blp.ClayRobotOre, cl + clr, obs + obsr, ge + ger, orr, clr + 1, obsr, ger, t - 1);
                q.Enqueue(next, GetPriority(next, blp));
            }

            if (or >= blp.ObsidianRobotOre && cl >= blp.ObsidianRobotClay)
            {
                next = (or + orr - blp.ObsidianRobotOre, cl + clr - blp.ObsidianRobotClay, obs + obsr, ge + ger, orr,
                    clr, obsr + 1, ger, t - 1);
                q.Enqueue(next, GetPriority(next, blp));
            }

            if (or >= blp.GeodeRobotOre && obs >= blp.GeodeRobotObsidian)
            {
                next = (or + orr - blp.GeodeRobotOre, cl + clr, obs + obsr - blp.GeodeRobotObsidian, ge + ger, orr, clr,
                    obsr, ger + 1, t - 1);
                q.Enqueue(next, GetPriority(next, blp));
            }
        }

        return result;
    }

    private long GetPriority((int or, int cl, int obs, int ge, int orr, int clr, int obsr, int ger, int t) next,
        Blueprint blp)
    {
        return -1 * (2 * blp.OreRobotOre * next.or * next.orr
                               + 5 * blp.ClayRobotOre * next.cl * next.clr * 10
                               + 11 * blp.ObsidianRobotOre * blp.ObsidianRobotClay * next.obs * next.obsr * 100
                               + 23 * blp.GeodeRobotOre * blp.GeodeRobotObsidian * next.ge * next.ger * 1000);
    }

    private Blueprint ParseBlueprint(string s)
    {
        var sp = s.Split();
        var oreRobotOre = int.Parse(sp[6]);
        var clayRobotOre = int.Parse(sp[12]);
        var obsidianRobotOre = int.Parse(sp[18]);
        var obsidianRobotClay = int.Parse(sp[21]);
        var geodeRobotOre = int.Parse(sp[27]);
        var geodeRobotObsidian = int.Parse(sp[30]);
        return new Blueprint(oreRobotOre, clayRobotOre, obsidianRobotOre, obsidianRobotClay, geodeRobotOre,
            geodeRobotObsidian);
    }

    record Blueprint(int OreRobotOre, int ClayRobotOre, int ObsidianRobotOre, int ObsidianRobotClay, int GeodeRobotOre,
        int GeodeRobotObsidian);
}
