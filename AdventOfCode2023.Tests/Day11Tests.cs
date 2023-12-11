namespace AdventOfCode2023.Tests;

public class Day11Tests
{
    [Theory]
    [InlineData(new[] { 
        "...#......",
        ".......#..",
        "#.........",
        "..........",
        "......#...",
        ".#........",
        ".........#",
        "..........",
        ".......#..",
        "#...#....."
    }, 374)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day11().Solve(input).Part1);
    }
}
