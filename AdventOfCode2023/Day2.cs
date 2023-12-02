namespace AdventOfCode2023;

[AocDay(2023,2)]
public class Day2 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var game in input)
        {
            var fewestNumberOfCubes = new Dictionary<string, int> {
                ["red"] = 0,
                ["green"] = 0,
                ["blue"] = 0,
            };
            var isValid = true;
            var sp = game.Split(": ");
            var id = int.Parse(sp[0].Split()[1]);
            foreach (var gameSet in sp[1].Split("; "))
            {
                var cubesCounts = new Dictionary<string, int> {
                    ["red"] = 12,
                    ["green"] = 13,
                    ["blue"] = 14,
                };
                foreach (var cubes in gameSet.Split(", "))
                {
                    var cubesInfo = cubes.Split(" ");
                    var count = int.Parse(cubesInfo[0]);
                    var color = cubesInfo[1];
                    cubesCounts[color] -= count;
                    fewestNumberOfCubes[color] = Math.Max(fewestNumberOfCubes[color], count);
                }
                if (!cubesCounts.Values.All(c => c >= 0))
                    isValid = false;
            }
            result2 += fewestNumberOfCubes.Values.Aggregate(1, (a, c) => a * c);
            if (!isValid)
                continue;
            result1 += id;
        }
        return new AocDayResult(result1, result2);
    }
}
