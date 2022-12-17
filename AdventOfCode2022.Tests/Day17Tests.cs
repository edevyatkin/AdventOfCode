using Xunit;

namespace AdventOfCode2022.Tests;

public class Day17Tests
{
    [Theory]
    [InlineData(@">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", 2022, 3068)]
    public void Part1Test(string data, long count, long result)
    {
        Assert.Equal(result, new Day17().TowerHeight(data.Split('\n'), count));
    }   
    
    [Theory]
    [InlineData(@">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>", 1000000000000, 1514285714288)]
    public void Part2Test(string data, long count, long result)
    {
        Assert.Equal(result, new Day17().TowerHeight(data.Split('\n'), count));
    }  
}
