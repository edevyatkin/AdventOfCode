using Xunit;

namespace AdventOfCode2016.Tests;

public class Day21Tests
{
    [Theory]
    [InlineData("abcde","swap position 4 with position 0","ebcda")]
    [InlineData("ebcda","swap letter d with letter b","edcba")]
    [InlineData("edcba","reverse positions 0 through 4","abcde")]
    [InlineData("abcde","rotate left 1 step","bcdea")]
    [InlineData("bcdea","move position 1 to position 4","bdeac")]
    [InlineData("bdeac","move position 3 to position 0","abdec")]
    [InlineData("abdec","rotate based on position of letter b","ecabd")]
    [InlineData("ecabd","rotate based on position of letter d","decab")]
    public void Part1Test(string str, string operation, string result)
    {
        Assert.Equal(result, Day21.PerformOperation(str, operation, false));
    }
    
    [Theory]
    [InlineData("ebcda","swap position 4 with position 0","abcde")]
    [InlineData("edcba","swap letter d with letter b","ebcda")]
    [InlineData("abcde","reverse positions 0 through 4","edcba")]
    [InlineData("bcdea","rotate left 1 step","abcde")]
    [InlineData("bdeac","move position 1 to position 4","bcdea")]
    [InlineData("abdec","move position 3 to position 0","bdeac")]
    [InlineData("ecabd","rotate based on position of letter b","abdec")]
    [InlineData("decab","rotate based on position of letter d","ecabd")]
    public void Part2Test(string str, string operation, string result)
    {
        Assert.Equal(result, Day21.PerformOperation(str, operation, true));
    }
}
