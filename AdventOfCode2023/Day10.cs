namespace AdventOfCode2023;

[AocDay(2023,10)]
public class Day10 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var (si, sj) = FindStart(input);

        var directionsToDiff = new Dictionary<Direction, (int Di, int Dj)> {
            [Direction.North] = (-1, 0),
            [Direction.South] = (1, 0),
            [Direction.West] = (0, -1),
            [Direction.East] = (0, 1)
        };

        bool IsConnected(char pos1, char pos2, Direction direction)
        {
            var connections = new Dictionary<char, List<Direction>> {
                ['|'] = new() { Direction.North, Direction.South },
                ['-'] = new() { Direction.East, Direction.West },
                ['L'] = new() { Direction.North, Direction.East },
                ['J'] = new() { Direction.North, Direction.West },
                ['7'] = new() { Direction.South, Direction.West },
                ['F'] = new() { Direction.South, Direction.East },
                ['S'] = new() { Direction.North, Direction.South, Direction.West, Direction.East }
            };
            
            var con1 = connections[pos1];
            var con2 = connections[pos2];
            return direction switch {
                Direction.North => con1.Contains(Direction.North) && con2.Contains(Direction.South),
                Direction.South => con1.Contains(Direction.South) && con2.Contains(Direction.North),
                Direction.West => con1.Contains(Direction.West) && con2.Contains(Direction.East),
                Direction.East => con1.Contains(Direction.East) && con2.Contains(Direction.West),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        bool IsIntersected(char pos1, char pos2, Direction direction)
        {
            return direction switch {
                Direction.North => pos1 == 'L' && pos2 == '7' || pos1 == 'J' && pos2 == 'F',
                Direction.South => pos1 == '7' && pos2 == 'L' || pos1 == 'F' && pos2 =='J',
                Direction.West => pos1 == 'J' && pos2 == 'F' || pos1 == '7' && pos2 =='L',
                Direction.East => pos1 == 'F' && pos2 == 'J' || pos1 == 'L' && pos2 =='7',
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        var q = new Queue<(int, int)>();
        q.Enqueue((si,sj));
        var mazePath = new HashSet<(int, int)>();
        var visited = new HashSet<(int, int)>();
        var result1 = -1;

        while (q.Count > 0)
        {
            var c = q.Count;
            result1++;
            while (c-- > 0)
            {
                var (i,j) = q.Dequeue();
                mazePath.Add((i,j));
                visited.Add((i,j));
                var cur = input[i][j];
                foreach (var (dir, (di,dj)) in directionsToDiff)
                {
                    var (ni, nj) = (i + di, j + dj);
                    if (ni < 0 || ni == input.Length || nj < 0 || nj == input[0].Length 
                        || input[ni][nj] == '.' || visited.Contains((ni,nj)))
                        continue;
                    var next = input[ni][nj];
                    if (IsConnected(cur, next, dir))
                        q.Enqueue((ni,nj));
                }
            }
        }

        var result2 = 0;
        
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (mazePath.Contains((i,j)))
                    continue;
                var isValid = true;
                foreach (var (dir, (di, dj)) in directionsToDiff)
                {
                    (char E, char S) edgeAndSkip = dir switch {
                        Direction.North or Direction.South => ('-', '|'),
                        _ => ('|', '-')
                    };
                    
                    var edgesCount = 0;

                    var cur = '*';
                    var prev = '*';
                    
                    for (int ni = i + di, nj = j + dj; 
                         ni >= 0 && ni < input.Length && nj >= 0 && nj < input[0].Length; 
                         ni += di, nj += dj)
                    {
                        if (!mazePath.Contains((ni, nj)) || input[ni][nj] == edgeAndSkip.S)
                            continue;
                        prev = cur;
                        cur = input[ni][nj];
                        if (cur == edgeAndSkip.E || IsIntersected(prev, cur, dir))
                            edgesCount++;
                    }

                    if (edgesCount % 2 != 1)
                    {
                        isValid = false;
                        break;
                    }

                }

                if (isValid)
                    result2++;
            }
        }

        return new AocDayResult(result1, result2);
    }

    private (int si, int sj) FindStart(string[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == 'S')
                {
                    return (i, j);
                }
            }
        }

        return default;
    }
    
    enum Direction
    {
        North,
        South,
        West,
        East
    }
}


