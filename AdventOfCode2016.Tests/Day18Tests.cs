using Xunit;

namespace AdventOfCode2016.Tests;

public class Day18Tests
{
    [Theory]
    [InlineData(".^^.^.^^^^", 10, 38)]
    public void CountSafeTilesTest(string map, int rows, int safeTiles)
    {
        Assert.Equal(safeTiles, Day18.CountSafeTiles(map, rows));
    }
}
