using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    public class Day5
    {
        public static void Main(string[] args)
        {
            int minSeatId = int.MaxValue;
            int maxSeatId = int.MinValue;
            HashSet<int> ids = new HashSet<int>();
            foreach (string pass in File.ReadLines("Day5_input"))
            {
                Seat seat = ParsePass(pass);
                minSeatId = Math.Min(minSeatId, seat.SeatId);
                maxSeatId = Math.Max(maxSeatId, seat.SeatId);
                ids.Add(seat.SeatId);
            }

            Console.WriteLine($"Max seat id: {maxSeatId}");
            
            for (int id = minSeatId; id <= maxSeatId; id++)
            {
                if (!ids.Contains(id))
                {
                    Console.WriteLine($"My seat: {id}");
                    break;
                }
            }
        }

        private static Seat ParsePass(string pass)
        {
            string rowPos = pass.Substring(0, 7);
            string colPos = pass.Substring(7, 3);
            int row = BinaryParse(rowPos, 'F', 'B');
            int col = BinaryParse(colPos, 'L', 'R');
            return new Seat(row, col);
        }

        private static int BinaryParse(string s, char lowerChar, char upperChar)
        {
            int max = (int)Math.Pow(2, s.Length);
            int i = 0, j = max;
            int si = 0;
            while (si < s.Length)
            {
                int mid = i + (j - i) / 2;
                if (s[si] == lowerChar) {
                    j = mid;
                }
                else if (s[si] == upperChar) {
                    i = mid;
                }

                si++;
            }

            return i;
        }

        private readonly struct Seat
        {
            public int Row { get; }
            public int Col { get; }
            public int SeatId => Row * 8 + Col;

            public Seat(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }    
    }
}

