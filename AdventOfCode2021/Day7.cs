using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,7)]
public class Day7 : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);

        var positions = input[0].Split(',').Select(int.Parse).ToArray();

        // PART 1
        int result1 = int.MaxValue;
        for (int h = positions.Min(); h <= positions.Max(); h++) {
            result1 = Math.Min(result1, positions.Select(x => Math.Abs(h-x)).Sum());
        }
        
        // PART 2
        int result2 = int.MaxValue;
        for (int h = positions.Min(); h <= positions.Max(); h++) {
            result2 = Math.Min(result2, positions.Select(x => Math.Abs(h-x)*(Math.Abs(h-x)+1)/2).Sum());
        }

        return new AocDayResult(result1, result2);
    }
}