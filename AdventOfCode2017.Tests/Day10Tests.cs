namespace AdventOfCode2017.Tests;

public class Day10Tests
{
    [Theory]
    [InlineData("3,4,1,5", 5, 12)]
    public void Part1Test(string input, int c, int result) 
    {
        Assert.Equal(result, new Day10().SolvePart(input, c, isPartTwo: false));
    }

    [Theory]
    [InlineData("", 256, "a2582a3a0e66e6e86e3812dcb672a272")]
    [InlineData("AoC 2017", 256, "33efeb34ea91902bb2f59c9920caa6cd")]
    [InlineData("1,2,3", 256, "3efbe78a8d82f29979031a4aa0b16a9d")]
    [InlineData("1,2,4", 256, "63960835bcdc130f0b66d7ff4f6a5a8e")]
    public void Part2Test(string input, int c, string result) 
    {
        Assert.Equal(result, new Day10().SolvePart(input, c, isPartTwo: true));
    }
}
