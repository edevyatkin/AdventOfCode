using System;
using System.Numerics;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day21Tests
{
    [Theory]
    [InlineData(@"root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32", 152)]
    public void Part1Test(string data, double result)
    { 
        Assert.Equal(result, new Day21().Solve(data.Split(Environment.NewLine)).Part1);
    }
    
    [Theory]
    [InlineData(@"root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32", 301)]
    public void Part2Test(string data, double result)
    { 
        Assert.Equal(result, new Day21().Solve(data.Split(Environment.NewLine)).Part2);
    }
}
