namespace AdventOfCode2017.Tests;

public class Day11Tests 
{
    [Theory]
    [InlineData("ne,ne,ne", 3)]
    [InlineData("ne,ne,sw,sw", 0)]
    [InlineData("ne,ne,s,s", 2)]
    [InlineData("se,sw,se,sw,sw", 3)]
    public void Part1Test(string input, int result)
    {
        Assert.Equal(result, new Day11().Solve(new [] { input }).Part1);
    }
}
