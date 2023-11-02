using Xunit;

namespace AdventOfCode2016.Tests;

public class Day17Tests
{
    [Theory]
    [InlineData("hijkl", 0b1110)]
    [InlineData("hijklD", 0b1011)]
    [InlineData("hijklDR", 0b0000)]
    public void GetDoorSetTest(string passcodeAndPath, int doorSet)
    {
        Assert.Equal(doorSet, Day17.GetDoorSet(passcodeAndPath));
    }
    
    [Theory]
    [InlineData(new[]{"ihgpwlah"}, "DDRRRD")]
    [InlineData(new[]{"kglvqrro"}, "DDUDRLRRUDRD")]
    [InlineData(new[]{"ulqzkmiv"}, "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
    public void Part1Test(string[] passcode, string path)
    {
        Assert.Equal(path, new Day17().Solve(passcode).Part1);
    }
    
    [Theory]
    [InlineData(new[]{"ihgpwlah"}, 370)]
    [InlineData(new[]{"kglvqrro"}, 492)]
    [InlineData(new[]{"ulqzkmiv"}, 830)]
    public void Part2Test(string[] passcode, int maxLen)
    {
        Assert.Equal(maxLen, new Day17().Solve(passcode).Part2);
    }
}
