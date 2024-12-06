namespace AdventOfCode2024.Tests;

public class Day6Tests
{
    [Theory]
    [InlineData(new [] {
        "....#.....",
        ".........#",
        "..........",
        "..#.......",
        ".......#..",
        "..........",
        ".#..^.....",
        "........#.",
        "#.........",
        "......#..."
    }, 41)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day6().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "....#.....",
        ".........#",
        "..........",
        "..#.......",
        ".......#..",
        "..........",
        ".#..^.....",
        "........#.",
        "#.........",
        "......#..."
    }, 6)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day6().Solve(input).Part2);
    }
}
