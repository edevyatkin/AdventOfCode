using Xunit;

namespace AdventOfCode2016.Tests;

public class Day9Tests
{
    [Theory]
    [InlineData(new[] { "ADVENT" }, 6)]
    [InlineData(new[] { "A(1x5)BC" }, 7)]
    [InlineData(new[] { "(3x3)XYZ" }, 9)]
    [InlineData(new[] { "A(2x2)BCD(2x2)EFG" }, 11)]
    [InlineData(new[] { "(6x1)(1x3)A" }, 6)]
    [InlineData(new[] { "X(8x2)(3x3)ABCY" }, 18)]
    public void Part1Test(string[] file, long result)
    {
        Assert.Equal(result, new Day9().Solve(file).Part1);
    }
    
    [Theory]
    [InlineData(new[] { "(3x3)XYZ" }, 9)]
    [InlineData(new[] { "X(8x2)(3x3)ABCY" }, 20)]
    [InlineData(new[] { "(27x12)(20x12)(13x14)(7x10)(1x12)A" }, 241920)]
    [InlineData(new[] { "(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN" }, 445)]
    public void Part2Test(string[] file, long result)
    {
        Assert.Equal(result, new Day9().Solve(file).Part2);
    }
}
