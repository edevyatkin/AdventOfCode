using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2021 {
    public class Day2 {
        public static async Task Main(string[] args) {
            var input = await AocHelper.FetchInputAsync(2021, 2);
            int position = 0;
            int depth = 0;
            foreach (var line in input) {
                var items = line.Split(' ');
                (string command, int units) = (items[0], int.Parse(items[1]));
                switch (command) {
                    case "forward":
                        position += units;
                        break;
                    case "up":
                        depth -= units;
                        break;
                    case "down":
                        depth += units;
                        break;
                }
            }
            Console.WriteLine(position * depth);
            
            position = 0;
            depth = 0;
            int aim = 0;
            foreach (var line in input) {
                var items = line.Split(' ');
                (string command, int units) = (items[0], int.Parse(items[1]));
                switch (command) {
                    case "forward":
                        position += units;
                        depth += aim * units;
                        break;
                    case "up":
                        aim -= units;
                        break;
                    case "down":
                        aim += units;
                        break;
                }
            }
            Console.WriteLine(position * depth);
        }
    }
}