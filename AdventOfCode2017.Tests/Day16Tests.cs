namespace AdventOfCode2017.Tests;

public class Day16Tests
{
    [Theory]
    [InlineData("s1,x3/4,pe/b", "baedc")]
    public void Part1Test(string moves, string result)
    {
        Assert.Equal(result, Day16.Dance(moves, 5, 1));
    }
    
    [Theory]
    [InlineData("s1,x3/4,pe/b", "ceadb")]
    public void Part2Test(string moves, string result)
    {
        Assert.Equal(result, Day16.Dance(moves, 5, 2));
    }
}
