using Xunit;

namespace AdventOfCode2016.Tests;

public class Day24Tests
{
    [Theory]
    [InlineData(new []
    {
        "###########",
        "#0.1.....2#",
        "#.#######.#",
        "#4.......3#",
        "###########"
    }, 14)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day24().SolvePart(input, false));
    }
}
