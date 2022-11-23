using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,2)]
public class Day2 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
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

        var result = position * depth;
            
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

        var result2 = position * depth;
        return new AocDayResult(result, result2);
    }
}