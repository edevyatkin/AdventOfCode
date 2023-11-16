namespace AdventOfCode2017.Tests;

public class Day7Tests
{
    [Theory]
    [InlineData(new [] {"pbga (66)", 
        "xhth (57)",
        "ebii (61)", 
        "havc (66)", 
        "ktlj (57)", 
        "fwft (72) -> ktlj, cntj, xhth",
        "qoyq (66)",
        "padx (45) -> pbga, havc, qoyq",
        "tknk (41) -> ugml, padx, fwft",
        "jptl (61)",
        "ugml (68) -> gyxo, ebii, jptl",
        "gyxo (61)",
        "cntj (57)"}, "tknk")]
    public void Part1Test(string[] input, string result)
    {
        Assert.Equal(result, new Day7().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {"pbga (66)", 
        "xhth (57)",
        "ebii (61)", 
        "havc (66)", 
        "ktlj (57)", 
        "fwft (72) -> ktlj, cntj, xhth",
        "qoyq (66)",
        "padx (45) -> pbga, havc, qoyq",
        "tknk (41) -> ugml, padx, fwft",
        "jptl (61)",
        "ugml (68) -> gyxo, ebii, jptl",
        "gyxo (61)",
        "cntj (57)"}, 60)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day7().Solve(input).Part2);
    }
}
