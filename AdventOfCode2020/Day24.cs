using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
    public class Day24 {
        public static void Main(string[] args) {
            var input = File.ReadAllLines("Day24_example.txt");
            var ll = new LobbyLayout(input);
            Console.WriteLine($"Day 24 part 1: {ll.CalculateBlackTiles()}");
            
            int count = 1;
            while (count <= 100) {
                ll.Evolve();
                Console.WriteLine($"{count}: {ll.CalculateBlackTiles()}");
                count++;
            }
            Console.WriteLine($"Day 24 part 2: {ll.CalculateBlackTiles()}");
        }
    }
    public class LobbyLayout {
        private static Grid<Tile> _grid;
        internal class Tile : ICell {
            
            public int X { get; set; }
            public int Y { get; set; }
            public int Value { get; set; }

            private List<Tile> _neighnours = new ();
            public IEnumerable<Tile> Neighbours {
                get {
                    if (_neighnours.Count == 0)
                        _neighnours.AddRange(GetNeighbours());
                    return _neighnours;
                }
            }

            public bool IsBlack() {
                return (Value & 1) > 0;
            }
            public bool IsWhite() {
                return !IsBlack();
            }

            public void MarkBlack() {
                Value |= 2;
            }

            public void Evolve() {
                Value >>= 1;
            }

            IEnumerable ICell.GetNeighbours() {
                return GetNeighbours();
            }

            private IEnumerable<Tile> GetNeighbours() {
                if (X + 2 < _grid.DimensionSize)
                    yield return _grid.Get(X + 2, Y);
                if (X - 2 >= -_grid.DimensionSize)
                    yield return _grid.Get(X - 2, Y);
                if (X + 1 < _grid.DimensionSize && Y + 1 < _grid.DimensionSize)
                    yield return _grid.Get(X + 1, Y + 1);
                if (X - 1 >= -_grid.DimensionSize && Y + 1 < _grid.DimensionSize)
                    yield return _grid.Get(X - 1, Y + 1);
                if (X - 1 >= -_grid.DimensionSize && Y - 1 >= -_grid.DimensionSize)
                    yield return _grid.Get(X - 1, Y - 1);
                if (X + 1 < _grid.DimensionSize && Y - 1 >= -_grid.DimensionSize)
                    yield return _grid.Get(X + 1, Y - 1);
            }
        }

        public LobbyLayout(string[] input) {
            var dict = new Dictionary<(int, int), int>();
            foreach (var s in input) {
                var coord = ToCoord(s);
                if (!dict.ContainsKey(coord))
                    dict[coord] = 0;
                dict[coord]++;
            }

            _grid = new Grid<Tile>(500);

            foreach (var kvp in dict.Where(x => x.Value % 2 == 1)) {
                _grid.Set(kvp.Key.Item1, kvp.Key.Item2, 1);
            }
        }
        
        public int CalculateBlackTiles() {
            return _grid.Select(i => i).Count(j => j?.IsBlack() ?? false);
        }

        public static (int X, int Y) ToCoord(string input) {
            var x = 0;
            var y = 0;
            var inputReplaced = input.Replace("nw", " ⭦ ")
                .Replace("ne", " ⭧ ")
                .Replace("se", " ⭨ ")
                .Replace("sw", " ⭩ ")
                .Replace("w", " ⭠ ")
                .Replace("e", " ⭢ ");
            var moves = inputReplaced.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var move in moves) {
                switch (move) {
                    case "⭢":
                        x = x + 2;
                        break;
                    case "⭠":
                        x = x - 2;
                        break;
                    case "⭧":
                        x = x + 1;
                        y = y + 1;
                        break;
                    case "⭦":
                        x = x - 1;
                        y = y + 1;
                        break;
                    case "⭩":
                        x = x - 1;
                        y = y - 1;
                        break;
                    case "⭨":
                        x = x + 1;
                        y = y - 1;
                        break;
                }
            }
            return (x, y);
        }

        public void Evolve() {
            foreach (var tile in _grid) {
                int blackNeighboursCount = 0;
                foreach (var neighbour in tile.Neighbours) {
                    if (neighbour.IsBlack()) {
                        blackNeighboursCount++;
                    }
                }
                
                if (tile.IsBlack() && (blackNeighboursCount == 1 || blackNeighboursCount == 2)) {
                    tile.MarkBlack();
                } else if (tile.IsWhite() && blackNeighboursCount == 2) {
                    tile.MarkBlack();
                }
            }

            foreach (var tile in _grid) {
                tile.Evolve();
            }
        }
    }

    internal class Grid<T> : IEnumerable<T> where T : ICell, new() {
        private readonly T[][] _data;
        private readonly int _dimensionSize;
        public int DimensionSize => _dimensionSize;

        public Grid(int dimensionSize) {
            _dimensionSize = dimensionSize;
            _data = new T[dimensionSize * 2][];
            for (var i = 0; i < _data.Length; i++) {
                _data[i] = new T[dimensionSize * 2];
                for (int j = 0; j < _data[i].Length; j++) {
                    _data[i][j] = new T() {
                        Value = 0, 
                        X = i - dimensionSize, 
                        Y = j - dimensionSize
                    };
                }
            }
        }

        public T Get(int x, int y) {
            if (x < -_dimensionSize || x >= _dimensionSize)
                throw new ArgumentException(
                    $"Value must be greater or equal than -{_dimensionSize} and less than {_dimensionSize}", nameof(x));
            if (y < -_dimensionSize || y >= _dimensionSize)
                throw new ArgumentException(
                    $"Value must be greater or equal than -{_dimensionSize} and less than {_dimensionSize}", nameof(y));
            return _data[x + _dimensionSize][y + _dimensionSize];
        }

        public void Set(int x, int y, int value) {
            if (x < -_dimensionSize || x >= _dimensionSize)
                throw new ArgumentException(
                    $"Value must be greater or equal than -{_dimensionSize} and less than {_dimensionSize}", nameof(x));
            if (y < -_dimensionSize || y >= _dimensionSize)
                throw new ArgumentException(
                    $"Value must be greater or equal than -{_dimensionSize} and less than {_dimensionSize}", nameof(y));
            _data[x + _dimensionSize][y + _dimensionSize].Value = value;
        }

        public IEnumerator<T> GetEnumerator() {
            return new GridEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private class GridEnumerator : IEnumerator<T> {
            private T _current;
            private readonly Grid<T> _grid;
            private int _curX;
            private int _curY;

            public GridEnumerator(Grid<T> grid) {
                _grid = grid;
                _curX = -_grid._dimensionSize;
                _curY = -_grid._dimensionSize;
                _current = _grid.Get(_curX, _curY);
            }

            public bool MoveNext() {
                _curY++;
                if (_curY == _grid._dimensionSize) {
                    _curY = -_grid._dimensionSize;
                    _curX++;
                    if (_curX == _grid._dimensionSize)
                        return false;
                }

                _current = _grid.Get(_curX, _curY);
                return true;
            }

            public void Reset() {
                _curX = -_grid._dimensionSize;
                _curY = -_grid._dimensionSize;
                _current = _grid.Get(_curX, _curY);
            }

            public T Current => _current;

            object IEnumerator.Current => Current;
            public void Dispose() { }
        }
    }

    internal interface ICell {
        void Evolve();
        IEnumerable GetNeighbours();
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
    }
}