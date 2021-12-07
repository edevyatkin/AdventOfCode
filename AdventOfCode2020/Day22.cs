using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
    public class Day22 {
        public static void Main(string[] args) {
            var data = File.ReadAllText("Day22_input.txt").Split("\n\n");

            var deck1 = data[0].Split('\n', StringSplitOptions.RemoveEmptyEntries)[1..].Select(int.Parse).ToArray();
            var deck2 = data[1].Split('\n', StringSplitOptions.RemoveEmptyEntries)[1..].Select(int.Parse).ToArray();

            var deck1Part1 = new Queue<int>(deck1);
            var deck2Part1 = new Queue<int>(deck2);
            long part1 = RegularCombat(deck1Part1, deck2Part1);
            Console.WriteLine($"Day 22 part 1: {part1}");

            var deck1Part2 = new Queue<int>(deck1);
            var deck2Part2 = new Queue<int>(deck2);
            long part2 = RecursiveCombat(deck1Part2, deck2Part2, false).Result;
            Console.WriteLine($"Day 22 part 2: {part2}");
        }

        private static long RegularCombat(Queue<int> deck1, Queue<int> deck2) {
            int count = 0;
            while (deck1.Count > 0 && deck2.Count > 0) {
                int card1 = deck1.Dequeue();
                int card2 = deck2.Dequeue();
                if (card1 > card2) {
                    deck1.Enqueue(card1);
                    deck1.Enqueue(card2);
                }
                else {
                    deck2.Enqueue(card2);
                    deck2.Enqueue(card1);
                }

                count++;
            }

            Console.WriteLine(count);
            var q = (deck1.Count > 0) ? deck1 : deck2;
            long result = 0;
            for (var i = q.Count; i >= 1; i--) {
                result += q.Dequeue() * i;
            }

            return result;
        }

        private static (bool P1Wins, long Result) RecursiveCombat(Queue<int> deck1, Queue<int> deck2,
            bool isRecursiveGame) {
            var rounds = new HashSet<string>();
            //var round = new Round(deck1, deck2);
            bool p1WinsInThisGame = false;
            bool p1WinsInRecursiveGame = false;
            while (deck1.Count > 0 && deck2.Count > 0) {
                if (!rounds.Add(string.Join(',', deck1) + ' ' + string.Join(',', deck2))) {
                    if (isRecursiveGame)
                        return (true, 0);
                    p1WinsInThisGame = true;
                    break;
                }

                int card1 = deck1.Peek();
                int card2 = deck2.Peek();
                bool wasRecursion = false;
                if (card1 <= deck1.Count - 1 && card2 <= deck2.Count - 1) {
                    var newDeck1 = new Queue<int>(deck1.ToArray()[1..(card1 + 1)]);
                    var newDeck2 = new Queue<int>(deck2.ToArray()[1..(card2 + 1)]);
                    var res = RecursiveCombat(newDeck1, newDeck2, true);
                    p1WinsInRecursiveGame = res.P1Wins;
                    wasRecursion = true;
                }

                if (wasRecursion ? p1WinsInRecursiveGame : card1 > card2) {
                    deck1.Enqueue(card1);
                    deck1.Enqueue(card2);
                }
                else {
                    deck2.Enqueue(card2);
                    deck2.Enqueue(card1);
                }

                deck1.Dequeue();
                deck2.Dequeue();
            }

            p1WinsInThisGame = p1WinsInThisGame || deck1.Count > 0;

            long result = 0;
            if (!isRecursiveGame) {
                var q = deck1.Count > 0 ? deck1 : deck2;
                for (var i = q.Count; i >= 1; i--) {
                    result += q.Dequeue() * i;
                }
            }

            return (p1WinsInThisGame, result);
        }
    }
}