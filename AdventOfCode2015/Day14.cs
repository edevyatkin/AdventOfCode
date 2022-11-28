using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,14)]
public class Day14 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = GetWinningDistance(input, 2503);
        var result2 = GetWinningPoints(input, 2503);

        return new AocDayResult(result1, result2);
    }

    public int GetWinningDistance(string[] input, int time)
    {
        var reindeers = ParseReindeers(input);
        var result = 0;
        foreach (var reindeer in reindeers)
        {
            var dist = CalculateReindeerDistance(reindeer, time);
            result = Math.Max(result, dist);
        }

        return result;
    }

    public int GetWinningPoints(string[] input, int time)
    {
        var reindeers = ParseReindeers(input);
        var points = new int[reindeers.Count];
        var distances = new int[reindeers.Count];
        for (var t = 1; t <= time; t++)
        {
            for (var i = 0; i < reindeers.Count; i++)
            {
                distances[i] = CalculateReindeerDistance(reindeers[i], t);
            }

            var winningReindeers = 
                distances.Select((e, i) => new { Elem = e, Index = i })
                    .GroupBy(e => e.Elem, e => e.Index)
                    .MaxBy(g => g.Key);

            foreach (var index in winningReindeers!)
            {
                points[index]++;
            }
        }
        
        return points.Max();
    }

    private List<(int, int, int)> ParseReindeers(string[] input)
    {
        var reindeers = new List<(int, int, int)>();
        foreach (var reindeer in input)
        {
            var sp = reindeer.Split(' ');
            var speed = int.Parse(sp[3]);
            var flyTime = int.Parse(sp[6]);
            var restTime = int.Parse(sp[^2]);
            reindeers.Add((speed,flyTime,restTime));
        }

        return reindeers;
    }
    
    private int CalculateReindeerDistance((int, int, int) reindeer, int time)
    {
        var (speed, flyTime, restTime) = reindeer;
        var fullCount = time / (flyTime + restTime);
        var remTime = Math.Min(flyTime, time % (flyTime + restTime));
        
        return (fullCount * flyTime + remTime) * speed;
    }
}
