using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021 {
    public class Day11 {
        public static async Task Main(string[] args) {
            var input = await AocHelper.FetchInputAsync(2021, 11);

            // PART 1
            int result1 = 0;
            var data1 = TakeInputData(input);
            int s = 1;
            while (s <= 100) {
                List<(int I, int J)> octopuses = TakeOctopusesToFlash(data1);
                result1 += octopuses.Sum(oc => FlashOctopus(data1, oc.I, oc.J));
                s++;
            }
            
            Console.WriteLine(result1);

            // PART 2
            int result2 = 0;
            var data2 = TakeInputData(input);
            s = 1;
            while (true) {
                List<(int I, int J)> octopuses = TakeOctopusesToFlash(data2);
                octopuses.ForEach(oc => FlashOctopus(data2, oc.I, oc.J));
                if (data2.Aggregate(true, (cur, line) => cur && line.All(e => e == 0)))
                    break;
                s++;
            }

            result2 = s;
            
            Console.WriteLine(result2);
        }

        private static int[][] TakeInputData(string[] input) {
            var data = new int[input.Length][];
            for (int i = 0; i < input.Length; i++) {
                data[i] = input[i].Select(c => c - '0').ToArray();
            }
            
            return data;
        }

        private static List<(int I, int J)> TakeOctopusesToFlash(int[][] data) {
            List<(int,int)> oct = new();
            for (var i = 0; i < data.Length; i++) {
                for (var j = 0; j < data[0].Length; j++) {
                    data[i][j]++;
                    if (data[i][j] > 9)
                       oct.Add((i,j));
                }
            }
            
            return oct;
        }

        private static int FlashOctopus(int[][] data, int i, int j) {
            if (i < 0 || i == data.Length || j < 0 || j == data[0].Length || data[i][j] == 0) 
                return 0;
            data[i][j] += 1;
            if (data[i][j] is not (10 or 11)) 
                return 0;
            data[i][j] = 0;
            var diffs = new List<(int DI, int DJ)>()
                { (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1) };
            return 1 + diffs.Sum(diff => FlashOctopus(data, i + diff.DI, j + diff.DJ));
        }
    }
}