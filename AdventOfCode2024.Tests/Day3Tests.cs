namespace AdventOfCode2024.Tests;

public class Day3Tests
{
    [Theory]
    [InlineData(new [] {
        "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
    }, 161)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day3().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
    }, 48)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day3().Solve(input).Part2);
    }
}
