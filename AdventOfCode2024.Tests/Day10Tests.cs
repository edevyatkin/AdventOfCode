namespace AdventOfCode2024.Tests;

public class Day10Tests
{
    [Theory]
    [InlineData(new [] {
        "0123",
        "1234",
        "8765",
        "9876"
    }, 1)]
    [InlineData(new [] {
        "...0...",
        "...1...",
        "...2...",
        "6543456",
        "7.....7",
        "8.....8",
        "9.....9"
    }, 2)]
    [InlineData(new [] {
        "..90..9",
        "...1.98",
        "...2..7",
        "6543456",
        "765.987",
        "876....",
        "987...."
    }, 4)]
    [InlineData(new [] {
        "10..9..",
        "2...8..",
        "3...7..",
        "4567654",
        "...8..3",
        "...9..2",
        ".....01"
    }, 3)]
    [InlineData(new [] {
        "89010123",
        "78121874",
        "87430965",
        "96549874",
        "45678903",
        "32019012",
        "01329801",
        "10456732"
    }, 36)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day10().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        ".....0.",
        "..4321.",
        "..5..2.",
        "..6543.",
        "..7..4.",
        "..8765.",
        "..9...."
    }, 3)]
    [InlineData(new [] {
        "..90..9",
        "...1.98",
        "...2..7",
        "6543456",
        "765.987",
        "876....",
        "987...."
    }, 13)]
    [InlineData(new [] {
        "012345",
        "123456",
        "234567",
        "345678",
        "4.6789",
        "56789."
    }, 227)]
    [InlineData(new [] {
        "89010123",
        "78121874",
        "87430965",
        "96549874",
        "45678903",
        "32019012",
        "01329801",
        "10456732"
    }, 81)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day10().Solve(input).Part2);
    }
}
