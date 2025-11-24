using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 6)]
public class Day6 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input);
        var result2 = SolvePart2(input);
        return new AocDayResult(result1, result2);
    }

    internal static int SolvePart1(string[] input)
    {
        var result = 0;
        var graph = new Dictionary<string, List<string>>();
        foreach (var line in input)
        {
            var sp = line.Split(')');
            if (!graph.ContainsKey(sp[0]))
                graph[sp[0]] = [];
            graph[sp[0]].Add(sp[1]);
        }
        var stack = new Stack<(string, int)>();
        stack.Push(("COM", 0));
        while (stack.Count > 0)
        {
            var (obj, count) = stack.Pop();
            result += count;
            if (graph.TryGetValue(obj, out var orbitsObjs))
                foreach (var onOrbit in orbitsObjs)
                    stack.Push((onOrbit, count+1));
        }
        return result;
    }
    
    internal static int SolvePart2(string[] input)
    {
        var graph = new Dictionary<string, List<string>>();
        foreach (var line in input)
        {
            var sp = line.Split(')');
            if (!graph.ContainsKey(sp[0]))
                graph[sp[0]] = [];
            if (!graph.ContainsKey(sp[1]))
                graph[sp[1]] = [];
            graph[sp[0]].Add(sp[1]);
            graph[sp[1]].Add(sp[0]);
        }
        var queue = new Queue<(string, int)>();
        var visited = new HashSet<string>();
        queue.Enqueue(("YOU", 0));
        while (queue.Count > 0)
        {
            var (obj, count) = queue.Dequeue();
            if (!visited.Add(obj))
                continue;
            if (obj == "SAN")
                return count - 2;
            foreach (var neigh in graph[obj])
                queue.Enqueue((neigh, count+1));
        }
        return 0;
    }
}
