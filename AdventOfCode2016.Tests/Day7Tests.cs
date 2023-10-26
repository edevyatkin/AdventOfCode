using Xunit;

namespace AdventOfCode2016.Tests;

public class Day7Tests
{
    [Theory]
    [InlineData(new[] { 
        "abba[mnop]qrst",
        "abcd[bddb]xyyx",
        "aaaa[qwer]tyui",
        "ioxxoj[asdfgh]zxcvbn"}, 2)]
    public void Part1Test(string[] ips, int result)
    {
        Assert.Equal(result, new Day7().Solve(ips).Part1);
    }

    [Theory]
    [InlineData("abba[mnop]qrst", true)]
    [InlineData("abcd[bddb]xyyx", false)]
    [InlineData("aaaa[qwer]tyui", false)]
    [InlineData("ioxxoj[asdfgh]zxcvbn", true)]
    public void Part1IsSupportTls(string ip, bool result)
    {
        Assert.Equal(result, Day7.IsSupportTls(ip));
    }
    
    [Theory]
    [InlineData(new[] { 
        "aba[bab]xyz",
        "xyx[xyx]xyx",
        "aaa[kek]eke",
        "zazbz[bzb]cdb"}, 3)]
    public void Part2Test(string[] ips, int result)
    {
        Assert.Equal(result, new Day7().Solve(ips).Part2);
    }
    
    [Theory]
    [InlineData("aba[bab]xyz", true)]
    [InlineData("xyx[xyx]xyx", false)]
    [InlineData("aaa[kek]eke", true)]
    [InlineData("zazbz[bzb]cdb", true)]
    public void Part2IsSupportSsl(string ip, bool result)
    {
        Assert.Equal(result, Day7.IsSupportSsl(ip));
    }
}
