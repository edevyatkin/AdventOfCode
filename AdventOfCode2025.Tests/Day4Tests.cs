namespace AdventOfCode2025.Tests;

public class Day4Tests
{
    [Theory]
    [InlineData(new[] {
        "..@@.@@@@.",
        "@@@.@.@.@@",
        "@@@@@.@.@@",
        "@.@@@@..@.",
        "@@.@@@@.@@",
        ".@@@@@@@.@",
        ".@.@.@.@@@",
        "@.@@@.@@@@",
        ".@@@@@@@@.",
        "@.@.@@@.@."
    }, 13)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day4().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "..@@.@@@@.",
        "@@@.@.@.@@",
        "@@@@@.@.@@",
        "@.@@@@..@.",
        "@@.@@@@.@@",
        ".@@@@@@@.@",
        ".@.@.@.@@@",
        "@.@@@.@@@@",
        ".@@@@@@@@.",
        "@.@.@@@.@."
    }, 43)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day4().Solve(input).Part2);
    }
}
