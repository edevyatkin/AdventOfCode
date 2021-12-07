using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;

namespace AdventOfCode2020 {
    public class Day11 {
        public static void Main(string[] args) {
            List<char[]> grid = File.ReadAllLines("Day11_input")
                .Select(l => l.Replace('L', '#').ToCharArray()).ToList();

            (int rd, int cd)[] diffs = {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)};
            int changes;
            do {
                changes = 0;
                CleanGrid(grid);
                DumpGrid(grid);

                for (int row = 0; row < grid.Count; row++) {
                    for (int col = 0; col < grid[0].Length; col++) {
                        int busyAround = 0;
                        foreach (var diff in diffs) {
                            // Part 1
                            //int r = row + diff.rd;
                            //int c = col + diff.cd;
                            //if (r < 0 || r == grid.Count || c < 0 || c == grid[0].Length)
                            //continue;
                            //if (grid[r][c] == '#' || grid[r][c] == '-')
                            //busyAround++;

                            //Part 2
                            if (IsOccupiedDirection(grid, row, col, diff.rd, diff.cd))
                                busyAround++;
                        }

                        if (grid[row][col] == 'L' && busyAround == 0) {
                            grid[row][col] = '+';
                            changes++;
                        }

                        if (grid[row][col] == '#' && busyAround >= 5) {
                            grid[row][col] = '-';
                            changes++;
                        }
                    }
                }
            } while (changes > 0);

            Console.WriteLine($"Day 11 part 1: {Seats(grid)}");
        }

        private static bool IsOccupiedDirection(List<char[]> grid, int row, int col, int diffRd, int diffCd) {
            int r = row + diffRd;
            int c = col + diffCd;
            if (r < 0 || r == grid.Count || c < 0 || c == grid[0].Length)
                return false;
            if (grid[r][c] == 'L' || grid[r][c] == '+')
                return false;
            if (grid[r][c] == '#' || grid[r][c] == '-')
                return true;
            return IsOccupiedDirection(grid, r, c, diffRd, diffCd);
        }

        private static int Seats(List<char[]> grid) {
            int seats = 0;
            foreach (var row in grid) {
                foreach (char ch in row) {
                    if (ch == '#') seats++;
                }
            }

            return seats;
        }

        private static void CleanGrid(List<char[]> grid) {
            for (int row = 0; row < grid.Count; row++) {
                for (int col = 0; col < grid[0].Length; col++) {
                    if (grid[row][col] == '.')
                        continue;
                    if (grid[row][col] == '-')
                        grid[row][col] = 'L';
                    if (grid[row][col] == '+')
                        grid[row][col] = '#';
                }
            }
        }

        private static void DumpGrid(List<char[]> grid) {
            for (int i = 0; i < grid.Count; i++) {
                Console.WriteLine(string.Concat(grid[i]));
            }

            Console.WriteLine();
        }
    }
}