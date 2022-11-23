using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,15)]
public class Day15 : IAocDay {
    public AocDayResult Solve(string[] input)
    {
        var data = new List<int[]>();
        for (var i = 0; i < input.Length; i++) {
            var line = input[i].ToCharArray().Select(c => c - '0').ToArray();
            data.Add(line);
        }

        // PART 1
        int result1 = Solve(data, repeat: 1);

        // PART 2
        int result2 = Solve(data, repeat:5);

        return new AocDayResult(result1, result2);
    }

    private static int Solve(List<int[]> data, int repeat) {
        int height = data.Count;
        int width = data[0].Length;
        var heap = new PriorityQueue<(int,int,int), int>();
        heap.Enqueue((0,0,0),0);
        var visited = new HashSet<(int,int)>();
        var risks = new Dictionary<(int,int), int>();
        risks[(0,0)] = 0;

        int GetValue(int i, int j) {
            (int ix, int jx) = (i % height, j % width);
            int extra = i / height + j / width;
            var num = data[ix][jx] + extra;
            return num % 10 + num / 10;
        }
        
        var diffs = new List<(int DI, int DJ)> { (-1, 0), (0, 1), (1, 0), (0, -1) };
        while (heap.Count > 0) {
            var (prisk, i, j) = heap.Dequeue(); // get pos
            if (visited.Contains((i,j)))
                continue;
            if (risks.ContainsKey((i,j)) && risks[(i,j)] != prisk)
                continue;
            foreach (var diff in diffs) {
                int di = i + diff.DI;
                int dj = j + diff.DJ;
                if (di < 0 || di == height*repeat || dj < 0 || dj == width*repeat)
                    continue;
                if (visited.Contains((di,dj)))
                    continue;
                int riskFromPos = GetValue(di, dj);
                int riskToPos = risks[(i, j)];
                int targetRisk = risks.GetValueOrDefault((di, dj), int.MaxValue);
                int newTargetRisk = Math.Min(targetRisk, riskToPos + riskFromPos);
                risks[(di, dj)] = newTargetRisk;
                heap.Enqueue((newTargetRisk, di, dj), newTargetRisk);
            }
            visited.Add((i, j));
        }

        return risks[(height * repeat - 1, width * repeat - 1)];            
    }
}