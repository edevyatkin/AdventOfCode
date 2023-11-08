namespace AdventOfCode2017.Tests;

public class Day2Tests
{
    [Theory]
    [InlineData(new []
    {
        "5 1 9 5",
        "7 5 3",
        "2 4 6 8"
    }, 18)]
    public void Part1Test(string[] input, int result) 
    {   
        Assert.Equal(result, new Day2().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new []
    {
        "5 9 2 8",
        "9 4 7 3",
        "3 8 6 5"
    }, 9)]
    public void Part2Test(string[] input, int result) 
    {   
        Assert.Equal(result, new Day2().Solve(input).Part2);
    }
}
