namespace AdventOfCode2017.Tests;

public class Day4Tests
{
    [Theory]
    [InlineData(new []
    {
        "aa bb cc dd ee",
        "aa bb cc dd aa",
        "aa bb cc dd aaa"
    }, 2)]
    public void Part1Test(string[] input, int result) 
    {   
        Assert.Equal(result, new Day4().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new []
    {
        "abcde fghij",
        "abcde xyz ecdab",
        "a ab abc abd abf abj",
        "iiii oiii ooii oooi oooo",
        "oiii ioii iioi iiio"
    }, 3)]
    public void Part2Test(string[] input, int result) 
    {   
        Assert.Equal(result, new Day4().Solve(input).Part2);
    }   
}
