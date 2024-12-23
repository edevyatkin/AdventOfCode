using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,23)]
public class Day23 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = string.Empty;

        var computers = new HashSet<string>();
        
        foreach (var line in input)
        {
            var splitLine = line.Split("-");
            var c1 = splitLine[0];
            var c2 = splitLine[1];
            computers.Add(c1);
            computers.Add(c2);
        }

        var computersList = computers.ToList();
        var computersDict = computersList
            .Select((c, i) => new {c, i})
            .ToDictionary(c => c.c, c => c.i);
        
        var edges = new bool[computersList.Count][];
        for (var i = 0; i < edges.Length; i++)
            edges[i] = new bool[computersList.Count];

        var stronglyConnected = new Queue<int[]>();
        
        foreach (var line in input)
        {
            var splitLine = line.Split("-");
            var c1 = splitLine[0];
            var c2 = splitLine[1];
            edges[computersDict[c1]][computersDict[c2]] = true;
            edges[computersDict[c2]][computersDict[c1]] = true;
            stronglyConnected.Enqueue([computersDict[c1], computersDict[c2]]);
        }
        
        var checkedComponents = new HashSet<string>();
        var maxComponent = 0;
        var result2Comp = Array.Empty<int>();

        while (stronglyConnected.Count > 0)
        {
            var comp = stronglyConnected.Dequeue();
            for (var i = 0; i < computersList.Count; i++)
            {
                if (comp.Contains(i))
                    continue;
                if (comp.All(ix => edges[i][ix]))
                {
                    int[] component = [..comp, i];
                    var hash = string.Join(",", component
                        .OrderBy(e => e));
                    if (!checkedComponents.Add(hash))
                        continue;
                    if (component.Length == 3 && component
                            .Select(e => computersList[e])
                            .Any(comps => comps.StartsWith('t')))
                        result1++;
                    if (component.Length > maxComponent)
                    {
                        maxComponent = component.Length;
                        result2Comp = component;
                    }
                    stronglyConnected.Enqueue(component);
                }
            }
        }

        result2 = string.Join(',', result2Comp.Select(e => computersList[e]).OrderBy(s => s));
        
        return new AocDayResult(result1, result2);
    }
}
