namespace AdventOfCode2019.Tests;

public class Day9Tests
{
    [Theory]
    [InlineData(new [] { "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99" }, 
        "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99")]
    public void Part1Test1(string[] instr, string result)
    {
        var nums = instr[0].Split(',').Select(int.Parse).ToArray();
        Assert.Equal(result, string.Join(',', new Intcode(nums).Execute( 1)));
    }
}
