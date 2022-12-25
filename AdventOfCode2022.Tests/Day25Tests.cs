using Xunit;

namespace AdventOfCode2022.Tests;

public class Day25Tests
{
    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "1=")]
    [InlineData(4, "1-")]
    [InlineData(5, "10")]
    [InlineData(6, "11")]
    [InlineData(7, "12")]
    [InlineData(8, "2=")]
    [InlineData(9, "2-")]
    [InlineData(10, "20")]
    [InlineData(15, "1=0")]
    [InlineData(20, "1-0")]
    [InlineData(2022, "1=11-2")]
    [InlineData(12345, "1-0---0")]
    [InlineData(314159265, "1121-1110-1=0")]
    [InlineData(1747, "1=-0-2")]
    [InlineData(906, "12111")]
    [InlineData(11, "21")]
    [InlineData(201, "2=01")]
    [InlineData(31, "111")]
    [InlineData(1257, "20012")]
    [InlineData(32, "112")]
    [InlineData(353, "1=-1=")]
    [InlineData(107, "1-12")]
    [InlineData(37, "122")]
    public void Part1DecimalToSnafuTest(long n, string result)
    { 
        Assert.Equal(result, new Day25().DecimalToSnafu(n));
    }
}
