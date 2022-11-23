using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,25)]
public class Day25 : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);

        var height = input.Length;
        var width = input[0].Length;
        
        var seafloor = new Dictionary<(int I, int J), bool>(); // east - false, south - true
        
        for (var i = 0; i < height; i++) {
            for (var j = 0; j < width; j++) {
                if (input[i][j] == '>') {
                    seafloor[(i, j)] = false;
                } else if (input[i][j] == 'v') {
                    seafloor[(i, j)] = true;
                }
            }
        }

        // PART 1
        int result1 = 0;
        int step = 0;
        while (true) {
            int moves = 0;
            var seafloor2 = new Dictionary<(int I, int J), bool>();
            foreach (var kv in seafloor) {
                if (kv.Value == false) {
                    if (!seafloor.ContainsKey((kv.Key.I, (kv.Key.J + 1) % width))) {
                        seafloor2[(kv.Key.I, (kv.Key.J + 1) % width)] = false;
                        moves++;
                    }
                    else {
                        seafloor2[(kv.Key.I, kv.Key.J)] = false;
                    }
                }
                else {
                    if ((seafloor.TryGetValue(((kv.Key.I + 1) % height, kv.Key.J), out bool cucumber1) && cucumber1) ||
                        (seafloor.TryGetValue(((kv.Key.I + 1) % height, kv.Key.J == 0 ? width - 1 : (kv.Key.J - 1) % width), out bool cucumber2) 
                            && !cucumber2 && !seafloor.ContainsKey(((kv.Key.I + 1) % height, kv.Key.J))) ||
                        (seafloor.TryGetValue(((kv.Key.I + 1) % height, kv.Key.J), out bool cucumber3) 
                            && !cucumber3 && seafloor.ContainsKey(((kv.Key.I + 1) % height, (kv.Key.J + 1) % width)))) {
                        seafloor2[(kv.Key.I, kv.Key.J)] = true;
                    }
                    else {
                        seafloor2[((kv.Key.I + 1) % height, kv.Key.J)] = true;
                        moves++;
                    }
                }
            }

            step++;
            if (moves == 0)
                break;
            seafloor = seafloor2;
        }

        result1 = step;

        return new AocDayResult(result1, 0);
    }
}