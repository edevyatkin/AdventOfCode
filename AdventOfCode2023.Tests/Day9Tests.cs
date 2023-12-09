namespace AdventOfCode2023.Tests;

public class Day9Tests
{
    [Theory]
    [InlineData(new [] {
        "0 3 6 9 12 15",
        "1 3 6 10 15 21",
        "10 13 16 21 30 45" }, 114)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, Day9.SolvePart(input, false));
    }

    [Theory]
    [InlineData(new [] {
        "0 3 6 9 12 15",
        "1 3 6 10 15 21",
        "10 13 16 21 30 45" }, 2)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, Day9.SolvePart(input, true));
    }
}
