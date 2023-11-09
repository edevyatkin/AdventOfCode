namespace AdventOfCode2017.Tests;

public class Day3Tests
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(12, 3)]
    [InlineData(23, 2)]
    [InlineData(1024, 31)]
    public void Part1Test(int id, int result) 
    {   
        Assert.Equal(result, new Day3().Solve(new[]{id.ToString()}).Part1);
    } 
}
