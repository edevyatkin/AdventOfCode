using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017, 12)]
public class Day12 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        var pipes = new Dictionary<int,int[]>();
        foreach (var line in input) 
        {
            var sp = line.Split(" <-> ").ToArray();
            var pipe = int.Parse(sp[0]);
            var pipeConn = sp[1].Split(", ").Select(int.Parse).ToArray();
            pipes[pipe] = pipeConn;
        }

        var visited = new HashSet<int>();
        var q = new Queue<int>();
        var partOneSolved = false;
        for (int id = 0; id < input.Length; id++)
        {
            if (visited.Contains(id))
                continue;
            q.Enqueue(id);
            while (q.Count > 0)
            {
                var program = q.Dequeue();
                if (!visited.Add(program))
                    continue;
                if (!partOneSolved)
                    result1++;
                foreach (var neigh in pipes[program])
                    q.Enqueue(neigh);
            }
            partOneSolved = true;
            result2++;
        }

        return new AocDayResult(result1, result2);
    }
}
