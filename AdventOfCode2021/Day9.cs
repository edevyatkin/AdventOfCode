using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,9)]
public class Day9 : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);

        var flow = new int[input.Length][];
        for (var index = 0; index < input.Length; index++) {
            var s = input[index];
            flow[index] = s.Select(c => c - '0').ToArray();
        }

        // PART 1
        var lowPoints = new HashSet<(int X, int Y)>();
        int result1 = 0;
        for (var i = 0; i < flow.Length; i++) {
            for (int j = 0; j < flow[0].Length; j++) {
                int pos = flow[i][j];
                if (pos == 9)
                    continue;
                if (i > 0 && flow[i-1][j] < pos 
                    || i < flow.Length-1 && flow[i+1][j] < pos
                    || j > 0 && flow[i][j-1] < pos 
                    || j < flow[0].Length-1 && flow[i][j+1] < pos)
                    continue;
                result1 += pos + 1;
                lowPoints.Add((i, j));
            }
        }
        
        // PART 2
        int result2 = 0;
        var areas = lowPoints
            .Select(low => CalculateBasinArea(flow, low.X, low.Y, new HashSet<(int, int)>()))
            .ToList();

        int CalculateBasinArea(int[][] arr, int i, int j, HashSet<(int,int)> visited) { 
            if (i < 0 || i == arr.Length || j < 0 || j == arr[0].Length 
                 || arr[i][j] == 9 || visited.Contains((i,j)))
                return 0;
            visited.Add((i, j));
            return 1 + 
                CalculateBasinArea(arr, i - 1, j, visited) +
                CalculateBasinArea(arr, i + 1, j, visited) +
                CalculateBasinArea(arr, i, j - 1, visited) +
                CalculateBasinArea(arr, i, j + 1, visited);
        }

        result2 = areas.OrderByDescending(s => s).Take(3).Aggregate(1, (x, y) => x * y);
        
        return new AocDayResult(result1, result2);
    }
}