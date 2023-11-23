namespace AdventOfCode2017.Tests;

public class Day12Tests
{
    [Theory]
    [InlineData(new [] {
        "0 <-> 2",
        "1 <-> 1",
        "2 <-> 0, 3, 4",
        "3 <-> 2, 4",
        "4 <-> 2, 3, 6",
        "5 <-> 6",
        "6 <-> 4, 5"
    }, 6)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day12().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new [] {
        "0 <-> 2",
        "1 <-> 1",
        "2 <-> 0, 3, 4",
        "3 <-> 2, 4",
        "4 <-> 2, 3, 6",
        "5 <-> 6",
        "6 <-> 4, 5"
    }, 2)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day12().Solve(input).Part2);
    }
}
