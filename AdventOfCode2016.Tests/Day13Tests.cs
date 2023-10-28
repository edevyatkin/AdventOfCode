using Xunit;

namespace AdventOfCode2016.Tests;

public class Day13Tests
{
    [Theory]
    [InlineData(10, 7,4,11)]
    public void Part1Test(int favoriteNumber, int x, int y, int result)
    {
        Assert.Equal(result, Day13.CalcMinSteps(favoriteNumber, x, y));
    }
}
