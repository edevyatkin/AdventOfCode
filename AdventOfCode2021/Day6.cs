using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,6)]
public class Day6 : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);
            
        var fishes = input[0].Split(',').Select(int.Parse).ToList();
            
        // PART 1
        var result = CalculateFishes(fishes, 80);
            
        // PART 2
        var result2 = CalculateFishes(fishes, 256);
        
        return new AocDayResult(result, result2);
    }

    private static long CalculateFishes(List<int> fishes, int days) {
        var counts = new long[9];
        foreach (var fish in fishes)
            counts[fish]++;
        int day = 1;
        while (day <= days) {
            var newFishes = counts[0];
            for (int i = 1; i < 9; i++)
                counts[i-1] = counts[i];
            counts[6] += newFishes;
            counts[8] = newFishes;
            day++;
        }

        return counts.Sum();
    }
}