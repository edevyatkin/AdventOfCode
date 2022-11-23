using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

//[AocDay(2021,11)]
public class Day11_Iterative : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);

        // PART 1 && PART 2
        int result1 = 0, result2 = 0;
        var data = TakeInputData(input);
        int s = 1;
        int flashes = 0;
        while (true) {
            var q = new Queue<(int I, int J)>();
            for (var i = 0; i < data.Length; i++) {
                for (var j = 0; j < data[0].Length; j++) {
                    data[i][j]++;
                    if (data[i][j] > 9)
                        q.Enqueue((i, j));
                }
            }

            while (q.Count > 0) {
                var pos = q.Dequeue();
                if (pos.I < 0 || pos.I == data.Length 
                              || pos.J < 0 || pos.J == data[0].Length 
                              || data[pos.I][pos.J] == 0 || ++data[pos.I][pos.J] <= 9)
                    continue;
                data[pos.I][pos.J] = 0;
                if (s <= 100)
                    flashes++;
                for (int ii = -1; ii <= 1; ii++) {
                    for (int jj = -1; jj <= 1; jj++) {
                        if (ii == 0 && jj == 0)
                            continue;
                        q.Enqueue((pos.I + ii, pos.J + jj));
                    }
                }
            }
            
            if (data.Aggregate(true, (cur, line) => cur && line.All(e => e == 0)))
                break;
            
            s++;
        }

        result1 = flashes;
        
        result2 = s;

        return new AocDayResult(result1, result2);
    }
    
    private static int[][] TakeInputData(string[] input) {
        var data = new int[input.Length][];
        for (int i = 0; i < input.Length; i++) {
            data[i] = input[i].Select(c => c - '0').ToArray();
        }
        
        return data;
    }
}