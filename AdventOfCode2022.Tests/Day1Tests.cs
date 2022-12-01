using Xunit;

namespace AdventOfCode2022.Tests;

public class Day1Tests
{
    [Theory]
    [InlineData(new[] { "1000", "2000", "3000", "", "4000", "", "5000", 
        "6000", "", "7000", "8000", "9000", "", "10000" }, 24000)]
    public void Part1Test(string[] inventory, int result)
    {
        Assert.Equal(result, new Day1().Solve(inventory).Part1);
    }
    
    [Theory]
    [InlineData(new[] { "1000", "2000", "3000", "", "4000", "", "5000", 
        "6000", "", "7000", "8000", "9000", "", "10000" }, 45000)]
    public void Part2Test(string[] inventory, int result)
    {
        Assert.Equal(result, new Day1().Solve(inventory).Part2);
    }
}
