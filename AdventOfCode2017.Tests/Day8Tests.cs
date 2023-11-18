namespace AdventOfCode2017.Tests;

public class Day8Tests
{
    [Theory]
    [InlineData(new []
    {
        "b inc 5 if a > 1",
        "a inc 1 if b < 5",
        "c dec -10 if a >= 1",
        "c inc -20 if c == 10"
    }, 1)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day8().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[]
    {
        "b inc 5 if a > 1",
        "a inc 1 if b < 5",
        "c dec -10 if a >= 1",
        "c inc -20 if c == 10"
    }, 10)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day8().Solve(input).Part2);
    }
}
