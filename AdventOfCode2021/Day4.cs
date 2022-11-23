using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,4)]
public class Day4 : IAocDay
{
    public async Task<AocDayResult> Solve(int year, int day)
    {
        var input = await AocHelper.FetchInputAsync(year, day);

        // PART 1
        var numberSet = input[0].Split(',').Select(int.Parse).ToArray();
        var boards = new List<int[][]>();
        int line = 1;
        while (line < input.Length) {
            line++;
            var board = new int[5][];
            for (int i = 0; i < 5; i++) {
                board[i] = input[line + i]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }
            boards.Add(board);
            line += 5;
        }

        int finalScore = 0;
        bool gameEnded = false;
        foreach (var number in numberSet) {
            foreach (var board in boards) {
                var marked = MarkNumberOnBoard(board, number);
                if (marked && BoardWins(board)) {
                    var sumUnmarkedNumbers = board.SelectMany(bl => bl).Where(e => e >= 0).Sum();
                    finalScore = number * sumUnmarkedNumbers;
                    gameEnded = true;
                    break;
                }
            }

            if (gameEnded) break;
        }
        
        // PART 2
        int finalScore2 = 0;
        var winningBoards = new HashSet<int[][]>();
        foreach (var number in numberSet) {
            foreach (var board in boards) {
                if (winningBoards.Contains(board))
                    continue;
                var marked = MarkNumberOnBoard(board, number);
                if (marked && BoardWins(board)) {
                    winningBoards.Add(board);
                    var sumUnmarkedNumbers = board.SelectMany(bl => bl).Where(e => e >= 0).Sum();
                    finalScore2 = number * sumUnmarkedNumbers;
                }
            }
        }

        return new AocDayResult(finalScore, finalScore2);
    }

    private static bool BoardWins(int[][] board) {
        if (board.Any(line => Array.TrueForAll(line, x => x < 0))) {
            return true;
        }
        for (int j = 0; j < 5; j++) {
            var isCompletedRow = board.All(x => x[j] < 0);
            if (isCompletedRow) 
                return true;
        }
        return false;
    }

    private static bool MarkNumberOnBoard(int[][] board, int number) {
        var marked = false;
        foreach (var boardLine in board) {
            for (var index = 0; index < boardLine.Length; index++) {
                if (boardLine[index] == number) {
                    boardLine[index] = -boardLine[index];
                    marked = true;
                }
            }
        }
        return marked;
    }
}