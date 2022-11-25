using System.Drawing;
using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var wires = new Dictionary<string, string>();
        foreach (var s in input)
        {
            var arr = s.Split(" -> ").ToArray();
            wires[arr[1]] = arr[0];
        }

        var cache = new Dictionary<string, int>();

        int GetSignal(string wire)
        {
            if (cache.ContainsKey(wire))
                return cache[wire];
            if (int.TryParse(wire, out int sig))
                return cache[wire] = sig;
            var arr = wires[wire].Split(' ').ToArray();
            if (arr.Length == 1)
                return GetSignal(arr[0]);
            var op = arr[^2];
            var res = 0;
            switch (op)
            {
                case "AND": 
                    res = GetSignal(arr[0]) & GetSignal(arr[2]);
                    break;
                case "OR": 
                    res = GetSignal(arr[0]) | GetSignal(arr[2]);
                    break;
                case "NOT": 
                    res = ~GetSignal(arr[1]) + 65536;
                    break;
                case "LSHIFT": 
                    res = GetSignal(arr[0]) << int.Parse(arr[2]);
                    break;
                case "RSHIFT": 
                    res = GetSignal(arr[0]) >> int.Parse(arr[2]);
                    break;
            }

            return cache[wire] = res;
        }
        
        var result1 = GetSignal("a");
        cache.Clear();
        cache["b"] = result1;
        var result2 = GetSignal("a");

        return new AocDayResult(result1, result2);
    }
}
