using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,9)]
public class Day9 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var dists = new Dictionary<string, List<(string City, int Dist)>>();
        foreach (var s in input)
        {
            var arr = s.Split(' ').ToArray();
            var city1 = arr[0];
            var city2 = arr[2];
            var dist = arr[4];
            if (!dists.ContainsKey(city1))
                dists[city1] = new();
            dists[city1].Add((city2, int.Parse(dist)));
            if (!dists.ContainsKey(city2))
                dists[city2] = new();
            dists[city2].Add((city1, int.Parse(dist)));
        }

        var result1 = int.MaxValue;
        var result2 = 0;
        
        int cityCount = dists.Count;
        var visited = new HashSet<string>();

        void Travel(string curCity, int elapsedCities, int dist)
        {
            if (visited.Contains(curCity))
                return;
            visited.Add(curCity);
            if (elapsedCities == 0)
            {
                result1 = Math.Min(result1, dist);
                result2 = Math.Max(result2, dist);
            }
            else
            {
                foreach (var pair in dists[curCity])
                {
                    Travel(pair.City, elapsedCities-1, dist + pair.Dist);
                }
            }
            visited.Remove(curCity);
        }
        
        foreach (var city in dists.Keys)
        {
            Travel(city, cityCount - 1, 0);
        }

        return new AocDayResult(result1, result2);
    }
}
