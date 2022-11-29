using Xunit;

namespace AdventOfCode2015.Tests;

public class Day15Tests
{
    [Theory]
    [InlineData(new[]
    {
        "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8",
        "Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3"
    }, 62842880)]
    public void Part1Test(string[] ingredients, long result)
    {
        Assert.Equal(result, new Day15().Solve(ingredients).Part1);
    }
    
    [Theory]
    [InlineData(new[]
    {
        "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8",
        "Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3"
    }, 57600000)]
    public void Part2Test(string[] ingredients, long result)
    {
        Assert.Equal(result, new Day15().Solve(ingredients).Part2);
    }
}
