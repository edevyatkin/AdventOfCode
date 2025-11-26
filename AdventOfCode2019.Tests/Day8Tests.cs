namespace AdventOfCode2019.Tests;

public class Day8Tests
{
    [Theory]
    [InlineData(new [] { "123456789012" }, 1)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, Day8.SolvePart1(input, 3, 2));
    }
    
    [Theory]
    [InlineData(new [] { "0222112222120000" }, " ++ ")]
    public void Part2Test(string[] input, string result)
    {
        Assert.Equal(result, Day8.SolvePart2(input, 2, 2).ToString().Replace(Environment.NewLine, string.Empty));
    }
}
