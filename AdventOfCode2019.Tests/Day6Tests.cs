namespace AdventOfCode2019.Tests;

public class Day6Tests
{
    [Theory]
    [InlineData(new [] { 
        "COM)B",
        "B)C",
        "C)D",
        "D)E",
        "E)F",
        "B)G",
        "G)H",
        "D)I",
        "E)J",
        "J)K",
        "K)L" }, 42)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, Day6.SolvePart1(input));
    }
    
    [Theory]
    [InlineData(new [] { 
        "COM)B",
        "B)C",
        "C)D",
        "D)E",
        "E)F",
        "B)G",
        "G)H",
        "D)I",
        "E)J",
        "J)K",
        "K)L",
        "K)YOU",
        "I)SAN" }, 4)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, Day6.SolvePart2(input));
    }
}
