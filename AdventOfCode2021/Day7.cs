using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021 {
    public class Day7 {
        public static async Task Main(string[] args) {
            var input = await AocHelper.FetchInputAsync(2021, 7);

            var positions = input[0].Split(',').Select(int.Parse).ToArray();

            // PART 1
            int result1 = int.MaxValue;
            for (int h = positions.Min(); h <= positions.Max(); h++) {
                result1 = Math.Min(result1, positions.Select(x => Math.Abs(h-x)).Sum());
            }
            Console.WriteLine(result1);
            
            // PART 2
            int result2 = int.MaxValue;
            for (int h = positions.Min(); h <= positions.Max(); h++) {
                result2 = Math.Min(result2, positions.Select(x => Math.Abs(h-x)*(Math.Abs(h-x)+1)/2).Sum());
            }
            Console.WriteLine(result2);

        }
    }
}