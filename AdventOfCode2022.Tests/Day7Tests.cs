using System;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day7Tests
{

    [Theory]
    [InlineData(@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k", 95437)]
    public void Part1Test(string data, int result)
    {
        Assert.Equal(result, new Day7().Solve(data.Split(Environment.NewLine)).Part1);
    }

    [Theory]
    [InlineData(@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k", 24933642)]
    public void Part2Test(string data, int result)
    {
        Assert.Equal(result, new Day7().Solve(data.Split(Environment.NewLine)).Part2);
    }
}
