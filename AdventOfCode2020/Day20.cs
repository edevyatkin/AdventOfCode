using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020 {
    public class Day20 {
        public static void Main(string[] args) {
            var tiles = ParseInput("Day20_example.txt");
            Console.WriteLine($"Day 20 part 1: {SolvePart1(tiles)}");
            // Console.WriteLine($"Day 20 part 2: {SolvePart2(tiles)}");
        }

        private static long SolvePart1(List<Tile> tiles) {
            var sideFits = new Dictionary<int, HashSet<(int TileId, Side Pos)>>();
            foreach (var tile in tiles) {
                foreach (var tr in tile.GenerateTransformations()) {
                    for (int i = 0; i < tr.Sides.Length; i++) {
                        if (!sideFits.ContainsKey(tr.Sides[i])) {
                            sideFits[tr.Sides[i]] = new HashSet<(int TileId, Side Pos)>();
                        }
                        sideFits[tr.Sides[i]].Add((tr.Id, (Side) i));
                    }
                }
            }

            foreach (var side in sideFits) {
                Console.WriteLine($"{SideToString(side.Key)}" +
                                  " => " +
                                  $"[ {string.Join(' ', side.Value)} ]");
            }

            return sideFits.Values
                .Where(sf => sf.GroupBy(x => x.TileId).Count() == 1)
                .Select(x => x.First().TileId)
                .GroupBy(x => x)
                .Where(x => x.Count() == 4)
                .Select(x => (long)x.Key)
                .Aggregate(1L, (x, y) => x * y);
        }
        
        private static long SolvePart2(List<Tile> tiles) {
            var sideFits = new Dictionary<int, HashSet<(int TileId, Side Pos)>>();
            foreach (var tile in tiles) {
                foreach (var tr in tile.GenerateTransformations()) {
                    for (int i = 0; i < tr.Sides.Length; i++) {
                        if (!sideFits.ContainsKey(tr.Sides[i])) {
                            sideFits[tr.Sides[i]] = new HashSet<(int TileId, Side Pos)>();
                        }
                        sideFits[tr.Sides[i]].Add((tr.Id, (Side) i));
                    }
                }
            }

            foreach (var side in sideFits) {
                Console.WriteLine($"{SideToString(side.Key)}" +
                                  " => " +
                                  $"[ {string.Join(' ', side.Value)} ]");
            }

            //var tilesDict = tiles.ToDictionary(x => x.Id, x => x);
            var cornerIds = sideFits.Values
                .Where(sf => sf.GroupBy(x => x.TileId).Count() == 1)
                .Select(x => x.First().TileId)
                .GroupBy(x => x)
                .Where(x => x.Count() == 4)
                .Select(x => (long) x.Key)
                .ToList();
            Console.WriteLine(string.Join(' ', cornerIds));
            var corners = tiles.Where(t => cornerIds.Contains(t.Id)).ToList();
            var edgesIds = sideFits.Values
                .Where(sf => sf.GroupBy(x => x.TileId).Count() == 1)
                .Select(x => x.First().TileId)
                .GroupBy(x => x)
                .Where(x => x.Count() == 2)
                .Select(x => (long) x.Key)
                .ToList();
            var edges = tiles.Where(t => edgesIds.Contains(t.Id)).ToList();
            var cornersTrans = corners.SelectMany(c => c.GenerateTransformations()).ToList();
            var edgesTrans = edges.SelectMany(e => e.GenerateTransformations()).ToList();
            Console.WriteLine(string.Join(' ', edgesIds));
            Tile[][] puzzle = new Tile[12][];
            for (var i = 0; i < puzzle.Length; i++) {
                puzzle[i] = new Tile[12];
            }

            int ix = 0, jx = 0;

            HashSet<Tile> used = new HashSet<Tile>();
            foreach (var corner in corners) {
                foreach (var cornerTr in corner.GenerateTransformations()) {
                    
                }
            }

            return 0;
        }


        private static void DumpTileSides(int[] sides) {
            var space = new string(' ', 10);
            var sb = new StringBuilder();
            sb.AppendLine(' ' + SideToString(sides[0]) + ' ');
            for (int i = 0; i < 10; i++) {
                sb.AppendLine(BitToChar(sides[3], 9 - i) + space + BitToChar(sides[1], 9 - i));
            }

            sb.AppendLine(' ' + SideToString(sides[2]) + ' ');

            Console.WriteLine(sb.ToString());
        }

        private static char BitToChar(int val, int bitNum) {
            return (((1 << bitNum) & val) > 0) ? '1' : '0';
        }

        private static string SideToString(int side) {
            return Convert.ToString(side, 2).PadLeft(10, '0');
        }

        enum Side {
            Top = 0,
            Right = 1,
            Bottom = 2,
            Left = 3
        }

        private static List<Tile> ParseInput(string input) {
            var tiles = new List<Tile>();
            var tilesData = File.ReadAllText(input).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            foreach (var tileStr in tilesData) {
                var tile = ParseTile(tileStr);
                tiles.Add(tile);
            }

            return tiles;
        }

        private static Tile ParseTile(string tileStr) {
            var tileData = tileStr.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            // parse tile id
            int tileId = ParseTileId(tileData);
            //Console.WriteLine(tileId);
            // parse tile sides
            var tileSides = ParseTileData(tileData);
            // Console.WriteLine(Convert.ToString(tileSides[0], 2).PadLeft(10,'0'));
            // Console.WriteLine(Convert.ToString(tileSides[1], 2).PadLeft(10,'0'));
            // Console.WriteLine(Convert.ToString(tileSides[2], 2).PadLeft(10,'0'));
            // Console.WriteLine(Convert.ToString(tileSides[3], 2).PadLeft(10,'0'));
            // Console.WriteLine();
            return new Tile(tileId, tileSides);
        }

        private static int ParseTileId(string[] tileData) => int.Parse(tileData[0].Split(' ')[1][..^1]);

        private static int[] ParseTileData(string[] tileData) {
            var tileSidesData = tileData[1..];
            int top = 0, right = 0, bottom = 0, left = 0;

            int TilePixelToBit(char pixel) => pixel == '#' ? 1 : 0;

            for (int y = 0; y < tileSidesData.Length; y++) {
                if (y == 0) {
                    for (int x = 0; x < tileSidesData[y].Length; x++) {
                        top = (top << 1) | TilePixelToBit(tileSidesData[y][x]);
                    }
                }

                left = (left << 1) | TilePixelToBit(tileSidesData[y][0]);
                right = (right << 1) | TilePixelToBit(tileSidesData[y][^1]);
                if (y == tileSidesData.Length - 1) {
                    for (int x = 0; x < tileSidesData[y].Length; x++) {
                        bottom = (bottom << 1) | TilePixelToBit(tileSidesData[y][x]);
                    }
                }
            }

            return new[] {top, right, bottom, left};
        }

        public class Tile {
            public int Id { get; }
            public int[] Sides { get; }
            public int Top => Sides[0];
            public int Right => Sides[1];
            public int Bottom => Sides[2];
            public int Left => Sides[3];
            

            public Tile(int id, int[] sides) {
                Id = id;
                Sides = sides;
            }

            public IEnumerable<Tile> GenerateTransformations() {
                foreach (var rotation in GetRotations(this)) {
                    yield return rotation;
                }

                var flipVertical = FlipVertical(this);
                yield return flipVertical;
                foreach (var rotation in GetRotations(flipVertical)) {
                    yield return rotation;
                }
                // DumpTileSides(flipVertical);
                // Console.WriteLine();

                var flipHorizontal = FlipHorizontal(this);
                yield return flipHorizontal;
                foreach (var rotation in GetRotations(flipHorizontal)) {
                    yield return rotation;
                }

                // DumpTileSides(flipHorizontal);
                // Console.WriteLine();
            }

            private static Tile FlipHorizontal(Tile tile) {
                return new Tile(tile.Id,
                    new[] {
                        ReverseBitsOfSide(tile.Sides[0]),
                        tile.Sides[3],
                        ReverseBitsOfSide(tile.Sides[2]),
                        tile.Sides[1]
                    });
            }

            private static Tile FlipVertical(Tile tile) {
                return new Tile(tile.Id,
                    new[] {
                        tile.Sides[2],
                        ReverseBitsOfSide(tile.Sides[1]),
                        tile.Sides[0],
                        ReverseBitsOfSide(tile.Sides[3])
                    });
            }

            private static IEnumerable<Tile> GetRotations(Tile tile) {
                int[] temp = new int[4];
                tile.Sides.CopyTo(temp, 0);
                for (int i = 1; i < 4; i++) {
                    var rotatedState = RotateCw(temp);
                    yield return new Tile(tile.Id, rotatedState);
                    temp = rotatedState;
                }
            }

            private static int[] RotateCw(in int[] init) {
                int[] rotated = new int[4];
                for (var i = 0; i < init.Length; i++) {
                    rotated[(i + 1) % 4] = ((i % 4) % 2 == 1) ? ReverseBitsOfSide(init[i]) : init[i];
                }

                return rotated;
            }

            private static int ReverseBitsOfSide(int side) {
                int reversed = 0;
                int k = 10;
                for (int i = 0; i < k; i++) {
                    reversed |= (side & (1 << i)) != 0 ? 1 << (k - 1 - i) : 0;
                }

                return reversed;
            }

            public override string ToString() {
                var sb = new StringBuilder();
                sb.AppendLine(SideToString(Sides[0]) + ' '
                              + SideToString(Sides[1]) + ' '
                              + SideToString(Sides[2]) + ' '
                              + SideToString(Sides[3]));
                return sb.ToString();
            }
        }
    }

}