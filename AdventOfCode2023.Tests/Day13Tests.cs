namespace AdventOfCode2023.Tests;

public class Day13Tests
{
    [Theory]
    [InlineData("#.##..##.\n..#.##.#.\n##......#\n##......#\n..#.##.#.\n..##..##.\n#.#.##.#.", 5)]
    [InlineData("#...##..#\n#....#..#\n..##..###\n#####.##.\n#####.##.\n..##..###\n#....#..#", 400)]
    public void Part1Test(string pattern, int result)
    {
        Assert.Equal(result, new Day13().Solve(pattern.Split()).Part1);
    }

    [Theory]
    [InlineData("#.##..##.\n..#.##.#.\n##......#\n##......#\n..#.##.#.\n..##..##.\n#.#.##.#.", 300)]
    [InlineData("#...##..#\n#....#..#\n..##..###\n#####.##.\n#####.##.\n..##..###\n#....#..#", 100)]
    public void Part2Test(string pattern, int result)
    {
        Assert.Equal(result, new Day13().Solve(pattern.Split()).Part2);
    }
}
