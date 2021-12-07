using System.IO;
using NUnit.Framework;

namespace AdventOfCode2020.Tests {
    public class Day24Tests {
        [Test]
        [TestCase("esenee", 6, 0)]
        [TestCase("esew", 1, -1)]
        [TestCase("nwwswee", 0, 0)]
        public void Day24CoordTest(string input, int x, int y) {
            var result = LobbyLayout.ToCoord(input);
            Assert.AreEqual(x, result.X);
            Assert.AreEqual(y, result.Y);
        }
        
        [Test]
        [TestCase("Day24_example.txt", 10)]
        public void Day24LargeExampleTest(string fileName, int blackCount) {
            var input = File.ReadAllLines(fileName);
            var ll = new LobbyLayout(input);
            Assert.AreEqual(blackCount, ll.CalculateBlackTiles());
        }
        
        [Test]
        [TestCase("Day24_example.txt", 1, 15)]
        [TestCase("Day24_example.txt", 2, 12)]
        [TestCase("Day24_example.txt", 3, 25)]
        public void Day24LargeExampleTest(string fileName, int day, int blackCount) {
            var input = File.ReadAllLines(fileName);
            var ll = new LobbyLayout(input);
            while (day > 0) {
                ll.Evolve();
                day--;
            }
            Assert.AreEqual(blackCount, ll.CalculateBlackTiles());
        }
    }
}