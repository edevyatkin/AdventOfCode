using Xunit;

namespace AdventOfCode2015.Tests;

public class Day9Tests
{
    [Theory]
    [InlineData(new [] {"London to Dublin = 464", "London to Belfast = 518", "Dublin to Belfast = 141"}, 605)]
    public void Part1Test(string[] dists, int result)
    {
        Assert.Equal(result, new Day9().Solve(dists).Part1);
    }
    
    [Theory]
    [InlineData(new [] {"London to Dublin = 464", "London to Belfast = 518", "Dublin to Belfast = 141"}, 982)]
    public void Part2Test(string[] dists, int result)
    {
        Assert.Equal(result, new Day9().Solve(dists).Part2);
    }
}
