using AdventOfCodeClient;

namespace AdventOfCode2025;

[AocDay(2025, 11)]
public class Day11 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;

        var graph = new Dictionary<string, List<string>>();

        foreach (var line in input)
        {
            var sp = line.Split(' ');
            var from = sp[0][..^1];
            if (!graph.ContainsKey(from))
                graph[from] = [];
            graph[from].AddRange(sp[1..]);
        }

        result1 = CountPaths("you", "out");

        result2 += CountPaths("svr", "dac") * CountPaths("dac", "fft") * CountPaths("fft", "out") +
                   CountPaths("svr", "fft") * CountPaths("fft", "dac") * CountPaths("dac", "out");

        return new AocDayResult(result1, result2);
        
        long CountPaths(string from, string to)
        {
            var result = 0L;
            
            var countIn = new Dictionary<string, long>();
            var stack = new Stack<string>();
            stack.Push(from);
            var visited = new HashSet<string>();
            while (stack.Count > 0)
            {
                var sFrom = stack.Pop();
                visited.Add(sFrom);
                foreach (var sTo in graph.GetValueOrDefault(sFrom, []))
                {
                    countIn[sTo] = countIn.GetValueOrDefault(sTo) + 1;
                    if (!visited.Contains(sTo))
                        stack.Push(sTo);
                }
            }
            
            var countPaths = new Dictionary<string, long> {
                [from] = 1
            };
            var queue = new Queue<string>();
            queue.Enqueue(from);
            while (queue.Count > 0)
            {
                var qFrom = queue.Dequeue();
                foreach (var qTo in graph.GetValueOrDefault(qFrom, []))
                {
                    countIn[qTo]--;
                    countPaths[qTo] = countPaths.GetValueOrDefault(qTo) + countPaths[qFrom];
                    if (countIn[qTo] == 0)
                        queue.Enqueue(qTo);
                }
            }            

            return countPaths.GetValueOrDefault(to);
        }
    }
}
