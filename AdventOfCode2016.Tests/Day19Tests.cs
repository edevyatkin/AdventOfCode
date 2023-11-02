using Xunit;

namespace AdventOfCode2016.Tests;

public class Day19Tests
{
    [Theory]
    [InlineData(5,3)]
    public void Part1Test(int elvesCount, int elf)
    {
        Assert.Equal(elf, Day19.GetElfNumber(elvesCount, false));
    }
    
    [Theory]
    [InlineData(5,2)]
    public void Part2Test(int elvesCount, int elf)
    {
        Assert.Equal(elf, Day19.GetElfNumber(elvesCount, true));
    }
}
