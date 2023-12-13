namespace AdventOfCode2023.Tests;

public class Day12Tests
{
    [Theory]
    [InlineData("???.### 1,1,3", 1)]
    [InlineData(".??..??...?##. 1,1,3",4)]
    [InlineData("?#?#?#?#?#?#?#? 1,3,1,6",1)]
    [InlineData("????.#...#... 4,1,1",1)]
    [InlineData("????.######..#####. 1,6,5",4)]
    [InlineData("?###???????? 3,2,1", 10)]
    public void Part1Test(string record, long result)
    {
        var sp = record.Split(' ');
        var springs = sp[0];
        var groups = sp[1].Split(',').Select(int.Parse).ToArray();
            
        Assert.Equal(result, Day12.Part1Count(springs, groups));
    }
    
    [Theory]
    [InlineData("???.### 1,1,3", 1)]
    [InlineData(".??..??...?##. 1,1,3",16384)]
    [InlineData("?#?#?#?#?#?#?#? 1,3,1,6",1)]
    [InlineData("????.#...#... 4,1,1",16)]
    [InlineData("????.######..#####. 1,6,5",2500)]
    [InlineData("?###???????? 3,2,1", 506250)]
    public void Part2Test(string record, long result)
    {
        var sp = record.Split(' ');
        var springs = sp[0];
        var groups = sp[1].Split(',').Select(int.Parse).ToArray();
            
        Assert.Equal(result, Day12.Part2Count(springs, groups));
    }
}
