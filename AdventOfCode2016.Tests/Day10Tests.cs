using Xunit;

namespace AdventOfCode2016.Tests;

public class Day10Tests
{
    [Theory]
    [InlineData(new[] { 
        "value 5 goes to bot 2",
        "bot 2 gives low to bot 1 and high to bot 0",
        "value 3 goes to bot 1",
        "bot 1 gives low to output 1 and high to bot 0",
        "bot 0 gives low to output 2 and high to output 0",
        "value 2 goes to bot 2" 
    }, 5, 2, 2)]
    public void Part1Test(string[] instructions, int v1, int v2, int result)
    {
        var botId = new Day10().SolvePart1(instructions, v1, v2);
        Assert.Equal(result, botId);
    }
    
    [Theory]
    [InlineData(new[] { 
        "value 5 goes to bot 2",
        "bot 2 gives low to bot 1 and high to bot 0",
        "value 3 goes to bot 1",
        "bot 1 gives low to output 1 and high to bot 0",
        "bot 0 gives low to output 2 and high to output 0",
        "value 2 goes to bot 2" 
    }, 30)]
    public void Part2Test(string[] instructions, int result)
    {
        var mul = new Day10().SolvePart2(instructions);
        Assert.Equal(result, mul);
    }
}
