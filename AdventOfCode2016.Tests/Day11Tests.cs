using Xunit;

namespace AdventOfCode2016.Tests;

public class Day11Tests
{
    [Theory]
    [InlineData(new[] { 
        "The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.",
        "The second floor contains a hydrogen generator.",
        "The third floor contains a lithium generator.",
        "The fourth floor contains nothing relevant." 
    }, 9)]
    public void Part1Test(string[] file, int result)
    {
        Assert.Equal(result, new Day11().Solve(file).Part1);
    }
    
    [Theory]
    [InlineData(new[] { 
        "The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.",
        "The second floor contains a hydrogen generator.",
        "The third floor contains a lithium generator.",
        "The fourth floor contains nothing relevant." 
    }, 33)]
    public void Part2Test(string[] file, int result)
    {
        Assert.Equal(result, new Day11().Solve(file).Part2);
    }
}
