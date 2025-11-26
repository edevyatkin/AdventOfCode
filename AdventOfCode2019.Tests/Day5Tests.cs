namespace AdventOfCode2019.Tests;

public class Day5Tests
{
    [Theory]
    [InlineData(new [] { "3,0,4,0,99" }, 6, 6)]
    public void Part1Test(string[] instr, int input, int result)
    {
        var nums = instr[0].Split(',').Select(int.Parse).ToArray();
        Assert.Equal(result, new Intcode(nums).Execute(input)[^1]);
    }
    
    [Theory]
    [InlineData(new [] { "3,9,8,9,10,9,4,9,99,-1,8" }, 8, 1)]
    [InlineData(new [] { "3,9,8,9,10,9,4,9,99,-1,8" }, 7, 0)]
    [InlineData(new [] { "3,9,7,9,10,9,4,9,99,-1,8" }, 7, 1)]
    [InlineData(new [] { "3,9,7,9,10,9,4,9,99,-1,8" }, 8, 0)]
    [InlineData(new [] { "3,9,7,9,10,9,4,9,99,-1,8" }, 9, 0)]
    [InlineData(new [] { "3,3,1108,-1,8,3,4,3,99" }, 7, 0)]
    [InlineData(new [] { "3,3,1108,-1,8,3,4,3,99" }, 8, 1)]
    [InlineData(new [] { "3,3,1108,-1,8,3,4,3,99" }, 9, 0)]
    [InlineData(new [] { "3,3,1107,-1,8,3,4,3,99" }, 7, 1)]
    [InlineData(new [] { "3,3,1107,-1,8,3,4,3,99" }, 8, 0)]
    [InlineData(new [] { "3,3,1107,-1,8,3,4,3,99" }, 9, 0)]
    [InlineData(new [] { "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9" }, 0, 0)]
    [InlineData(new [] { "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9" }, 8, 1)]
    [InlineData(new [] { "3,3,1105,-1,9,1101,0,0,12,4,12,99,1" }, 0, 0)]
    [InlineData(new [] { "3,3,1105,-1,9,1101,0,0,12,4,12,99,1" }, 8, 1)]
    [InlineData(new [] { "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99" }, 7, 999)]
    [InlineData(new [] { "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99" }, 8, 1000)]
    [InlineData(new [] { "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99" }, 9, 1001)]
    public void Part2Test(string[] instr, int input, int result)
    {
        var nums = instr[0].Split(',').Select(int.Parse).ToArray();
        Assert.Equal(result, new Intcode(nums).Execute(input)[^1]);
    }
}
