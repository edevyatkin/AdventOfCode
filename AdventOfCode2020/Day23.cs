using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020 {
    public class Day23 {
        public static void Main(string[] args) {
            var input = File.ReadAllText("Day23_input.txt");
            var game1 = new CrubCups(input);
            game1.PlayGame(100);
            var result1 = game1.CalculateResultForPart1(); 
            Console.WriteLine($"Day 23 part 1: {result1}");
            
            var game2 = new CrubCups(input, true);
            game2.PlayGame(10_000_000);
            var result2 = game2.CalculateResultForPart2();
            Console.WriteLine($"Day 23 part 2: {result2}");
        }
    }



    public class CrubCups {
        private readonly LinkedList<int> _circle;
        private int _currentCup;
        private readonly int[] _sortedCircle;
        private readonly Dictionary<int, LinkedListNode<int>> _circleDict;

        public CrubCups(string input, bool isPart2 = false) {
            var tempCircle = input.Select(c => c - '0').ToList();
            if (isPart2) {
                int max = tempCircle.Max();
                tempCircle.AddRange(Enumerable.Range(max + 1, 1_000_000 - tempCircle.Count));
            }

            _circle = new LinkedList<int>(tempCircle);
            _circleDict = new Dictionary<int, LinkedListNode<int>>();
            var current = _circle.First;
            while (current != null) {
                _circleDict[current.Value] = current;
                current = current.Next;
            }

            _sortedCircle = new List<int>(tempCircle).ToArray();
            Array.Sort(_sortedCircle);
            _currentCup = _circle.First!.Value;
        }

        public void PlayGame(int i) {
            int moves = i;
            while (i > 0) {
                // Console.WriteLine($"-- move {moves - i + 1} --");
                Move();
                //Console.ReadKey();
                i--;
            }

        }

        private void Move() {
            //var currentCircle = new List<int>(_circle);
            var pickedCups = PickUpThreeCups();
            var destinationCup = _currentCup - 1;
            while (true) {
                if (destinationCup < _sortedCircle[0]) {
                    destinationCup = _sortedCircle[^1];
                }
                if (pickedCups.One.Value == destinationCup || 
                    pickedCups.Two.Value == destinationCup || 
                    pickedCups.Three.Value == destinationCup) {
                    destinationCup--;
                    continue;
                }
                if (Array.BinarySearch(_sortedCircle, destinationCup) >= 0)
                    break;
                destinationCup--;
            }

            // var sb = new StringBuilder();
            // foreach (var num in currentCircle) {
            //     if (num == _currentCup) {
            //         sb.Append($"({num})");
            //     }
            //     else {
            //         sb.Append(num);
            //     }
            //     sb.Append(' ');
            // }
            // Console.WriteLine($"cups: {sb}");
            // Console.WriteLine($"pick up: {string.Join(' ', pickedCups.Select(x=> x.Value))}");
            // Console.WriteLine($"destination: {destinationCup}");

            PlaceCups(pickedCups, destinationCup);
            SelectCurrentCup();
        }

        private void PlaceCups((LinkedListNode<int> One, LinkedListNode<int> Two, LinkedListNode<int> Three) cups, in int destinationCup) {
            var destinationCupNode = _circleDict[destinationCup];
            var dest = destinationCupNode.Next ?? _circle.First;
            _circleDict[cups.One.Value] = cups.One;
            _circleDict[cups.Two.Value] = cups.Two;
            _circleDict[cups.Three.Value] = cups.Three;
            _circle.AddBefore(dest!, cups.One);
            _circle.AddBefore(dest!, cups.Two);
            _circle.AddBefore(dest!, cups.Three);
        }

        private (LinkedListNode<int> One, LinkedListNode<int> Two, LinkedListNode<int> Three) PickUpThreeCups() {
            // var cups = new LinkedListNode<int>[3];
            var currentCupNode = _circleDict[_currentCup];
            var cur = currentCupNode.Next ?? _circle.First;
            var i = 0;
            LinkedListNode<int> one = null, two = null, three = null;
            while (i < 3) {
                if (i == 0) one = cur;
                if (i == 1) two = cur;
                if (i == 2) three = cur;
                var toDelete = cur;
                cur = cur.Next ?? _circle.First;
                _circle.Remove(toDelete);
                i++;
            }
            return (one,two,three);
        }

        private void SelectCurrentCup() {
            var next = _circleDict[_currentCup].Next ?? _circle.First;
            _currentCup = next!.Value;
        }


        public string CalculateResultForPart1() {
            var sb = new StringBuilder();
            var one = _circleDict[1];
            var cur = one.Next ?? _circle.First;
            while (cur!.Value != 1) {
                sb.Append(cur.Value);
                cur = cur.Next ?? _circle.First;
            }
            return sb.ToString();
        }

        public ulong CalculateResultForPart2() {
            var one = _circleDict[1];
            var cup1 = one.Next ?? _circle.First;
            Console.WriteLine($"Cup 1: {cup1.Value}");
            var cup2 = cup1!.Next ?? _circle.First;
            Console.WriteLine($"Cup 2: {cup2.Value}");
            return Convert.ToUInt64(cup1.Value) * Convert.ToUInt64(cup2!.Value);
        }
    }
}