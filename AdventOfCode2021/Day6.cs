using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021 {
    public class Day6 {
        public static async Task Main(string[] args) {
            var input = await AocHelper.FetchInputAsync(2021, 6);
            
            var fishes = input[0].Split(',').Select(int.Parse).ToList();
            
            // PART 1
            Console.WriteLine(CalculateFishes(fishes, 80));
            
            // PART 2
            Console.WriteLine(CalculateFishes(fishes, 256));
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
}