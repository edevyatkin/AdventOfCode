using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2020.Tests {
    public class Tests {
        [SetUp]
        public void Setup() { }

        [Test]
        [TestCase("389125467", 10, "92658374")]
        [TestCase("389125467", 100, "67384529")]
        public void Day23Part1Test(string input, int moves, string result) {
            var game = new CrubCups(input);
            game.PlayGame(moves);
            var gameResult = game.CalculateResultForPart1();
            Assert.AreEqual(result,gameResult);
        }
        
        [Test]
        [TestCase("389125467", 10_000_000, 149245887792UL)]
        public void Day23Part2Test(string input, int moves, ulong result) {
            var game = new CrubCups(input, true);
            game.PlayGame(moves);
            var gameResult = game.CalculateResultForPart2();
            Assert.AreEqual(result,gameResult);
        }
    }
}