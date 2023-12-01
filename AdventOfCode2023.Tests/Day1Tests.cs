namespace AdventOfCode2023.Tests;

public class Day1Tests
{
    [Theory]
    [InlineData(new [] {
        "1abc2",
        "pqr3stu8vwx",
        "a1b2c3d4e5f",
        "treb7uchet"
    }, 142)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day1().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "two1nine",
        "eightwothree",
        "abcone2threexyz",
        "xtwone3four",
        "4nineeightseven2",
        "zoneight234",
        "7pqrstsixteen"
    }, 281)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day1().Solve(input).Part2);
    }
}
