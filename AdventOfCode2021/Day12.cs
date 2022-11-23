using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,12)]
public class Day12 : IAocDay {
    public AocDayResult Solve(string[] input)
    {
        var data = new Dictionary<string, List<string>>();

        foreach (var s in input) {
            var sp = s.Split('-');
            if (!data.ContainsKey(sp[0]))
                data[sp[0]] = new List<string>();
            data[sp[0]].Add(sp[1]);
            if (!data.ContainsKey(sp[1]))
                data[sp[1]] = new List<string>();
            data[sp[1]].Add(sp[0]);
        }
        
        // PART 1
        int result1 = 0;
        foreach (var s in data["start"]) {
            result1 += CountPaths(data, s, new HashSet<string>());
        }

        // PART 2
        int result2 = 0;
        foreach (var s in data["start"]) {
            result2 += CountPaths2(data, s, new Dictionary<string, int>());
        }

        return new AocDayResult(result1, result2);
    }

    private static int CountPaths(Dictionary<string, List<string>> dict, string key, HashSet<string> visitedSmall) {
        if (key == "end")
            return 1;
        if (visitedSmall.Contains(key) || key == "start")
            return 0;
        if (key.ToLower() == key)
            visitedSmall.Add(key);
        int count = 0;
        foreach (var s in dict[key]) {
            count += CountPaths(dict, s, visitedSmall);
        }
        visitedSmall.Remove(key);
        return count;
    }
    
    private static int CountPaths2(Dictionary<string, List<string>> dict, string key, Dictionary<string,int> visitedSmall) {
        if (key == "end")
            return 1;
        if (key == "start")
            return 0;
        if (key == key.ToLower()) {
            if (!visitedSmall.ContainsKey(key)) {
                visitedSmall[key] = 1;
            }
            else {
                if (visitedSmall[key] == 2)
                    return 0;
                if (visitedSmall[key] == 1 && visitedSmall.Any(v => v.Value == 2))
                    return 0;
                visitedSmall[key]++;
            }
        }
        int count = 0;
        foreach (var s in dict[key]) {
            count += CountPaths2(dict, s, visitedSmall);
        }
        if (visitedSmall.ContainsKey(key) && visitedSmall[key] > 0)
            visitedSmall[key]--;
        return count;
    }
}