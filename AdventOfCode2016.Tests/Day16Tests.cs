using Xunit;

namespace AdventOfCode2016.Tests;

public class Day16Tests
{
    [Theory]
    [InlineData("10000", 20, "10000011110010000111")]
    public void FillDiskTest(string input, int len, string result)
    {
        Assert.Equal(result, Day16.FillDisk(input, len));
    }
    
    [Theory]
    [InlineData("10000011110010000111","01100")]
    public void CalcCheckSumTest(string input, string result)
    {
        Assert.Equal(result, Day16.CalcCheckSum(input));
    }
}
