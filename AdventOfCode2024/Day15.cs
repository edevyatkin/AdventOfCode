using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,15)]
public class Day15 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(isPartTwo: false);
        var result2 = SolvePart(isPartTwo: true);
        
        return new AocDayResult(result1, result2);

        int SolvePart(bool isPartTwo)
        {
            var emptyLineIx = -1;
            while (input[++emptyLineIx] != string.Empty) ;
            
            var grid = CreateGrid(emptyLineIx, input[0].Length, isPartTwo);

            var robotI = 0;
            var robotJ = 0;
            
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == '@')
                    {
                        robotI = i;
                        robotJ = j;
                    }
                }
            }
            
            var moves = string.Join(string.Empty, input[(emptyLineIx+1)..]);

            foreach (var move in moves)
            {
                (int Di, int Dj) diff = move switch {
                    '^' => (-1, 0),
                    '>' => (0, 1),
                    'v' => (1, 0),
                    '<' => (0, -1),
                    _ => (0, 0)
                };
                var s = new SortedSet<(int, int, char)>(
                    Comparer<(int I, int J, char C)>.Create((a, b) => 
                        diff.Di == 0 ? 
                            (-a.J*diff.Dj,a.I).CompareTo((-b.J*diff.Dj,a.I)) :
                            (-a.I*diff.Di,a.J).CompareTo((-b.I*diff.Di,b.J)))
                );
                var canMove = MoveBoxes(grid, robotI, robotJ, diff, s);
                if (canMove) {
                    foreach (var (i, j, c)  in s)
                    {
                        grid[i+diff.Di][j+diff.Dj] = c;
                        grid[i][j] = '.';
                        if (c == '@')
                            (robotI,robotJ) = (i+diff.Di, j+diff.Dj);
                    }
                }
            }

            var result = 0;
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] is 'O' or '[')
                        result += i * 100 + j;
                }
            }

            return result;
        }

        char[][] CreateGrid(int rows, int columns, bool isPartTwo)
        {
            var g = new char[rows][];
            for (var i = 0; i < rows; i++)
            {
                if (!isPartTwo)
                {
                    g[i] = input[i].ToCharArray();
                }
                else
                {
                    var line = new StringBuilder();
                    for (var j = 0; j < columns; j++)
                    {
                        var instead = input[i][j] switch {
                            '#' => "##",
                            'O' => "[]",
                            '.' => "..",
                            '@' => "@.",
                            _ => string.Empty
                        };
                        line.Append(instead);
                    }
                    g[i] = line.ToString().ToCharArray();
                }
            }

            return g;
        }

        bool MoveBoxes(char[][] grid, int i, int j, (int Di, int Dj) dir, SortedSet<(int I, int J, char C)> s)
        {
            s.Add((i,j,grid[i][j]));

            var (ni,nj) = (i+dir.Di, j+dir.Dj);
            
            if (grid[ni][nj] == '#')
                return false;
            if (grid[ni][nj] == '.')
                return true;
            
            if (grid[ni][nj] == '[' && (dir == (-1, 0) || dir == (1, 0)))
                return MoveBoxes(grid, ni, nj, dir, s) && MoveBoxes(grid, ni, nj+1, dir, s);
        
            if (grid[ni][nj]== ']' && (dir == (-1, 0) || dir == (1, 0)))
                return MoveBoxes(grid, ni, nj, dir, s) && MoveBoxes(grid, ni, nj-1, dir, s);
            
            return MoveBoxes(grid, ni, nj, dir, s);
        }
    }
}
