using Xunit;

namespace AdventOfCode2016.Tests;

public class Day3Tests
{
    [Theory]
    [InlineData(new[]
    {
        "5 10 25",
        "3 5 7",
        "1 2 3"
    }, 1)]
    public void Part1Test(string[] specifications, int result)
    {
        Assert.Equal(result, new Day3().Solve(specifications).Part1);
    }
    
}
