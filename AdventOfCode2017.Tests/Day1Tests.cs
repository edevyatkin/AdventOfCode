namespace AdventOfCode2017.Tests;

public class Day1Tests
{
    [Theory]
    [InlineData("1122",3)]
    [InlineData("1111",4)]
    [InlineData("1234",0)]
    [InlineData("91212129",9)]
    public void Part1Tests(string input, int result)
    {
        Assert.Equal(result, new Day1().Solve(new [] { input }).Part1);
    }
    
    [Theory]
    [InlineData("1212",6)]
    [InlineData("1221",0)]
    [InlineData("123425",4)]
    [InlineData("123123",12)]
    [InlineData("12131415",4)]
    public void Part2Tests(string input, int result)
    {
        Assert.Equal(result, new Day1().Solve(new [] { input }).Part2);
    }
}
