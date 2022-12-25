using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 16)]
public class Day16 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var valves = ParseInput(input);

        var result1 = MostPressure(valves, 30, false);
        var result2 = MostPressure(valves, 26, true);

        return new AocDayResult(result1, result2);
    }

    private int MostPressure(Dictionary<string, Valve> valves, int minutes, bool isPartTwo)
    {
        var valvesToOpen = valves.Count(v => v.Value.Rate > 0);
        var cache = new Dictionary<(Valve, long, int, bool), int>();

        int MaxP(Valve valve, long openedValves, int time, bool goElephant)
        {
            if (time == 0)
            {
                return goElephant ? MaxP(valves["AA"], openedValves, minutes, false) : 0;
            }

            if (cache.ContainsKey((valve, openedValves, time, goElephant)))
            {
                return cache[(valve, openedValves, time, goElephant)];
            }

            if (BitOperations.PopCount((ulong)openedValves) == valvesToOpen)
            {
                return 0;
            }

            var result = 0;
            if (valve.IsClosed(openedValves) && valve.CanBeOpened())
            {
                var newOpenedValves = openedValves | (1 << valve.Id);
                result = valve.Rate * (time - 1) + MaxP(valve, newOpenedValves, time - 1, goElephant);
            }

            foreach (var nv in valve.TunnelsTo)
            {
                result = Math.Max(result, MaxP(nv, openedValves, time - 1, goElephant));
            }

            return cache[(valve, openedValves, time, goElephant)] = result;
        }

        return MaxP(valves["AA"], 0, minutes, isPartTwo);
    }

    private Dictionary<string, Valve> ParseInput(string[] input)
    {
        var valves = new Dictionary<string, Valve>();
        var id = 0;
        foreach (var s in input)
        {
            var p = @"Valve ([A-Z]{2}).*rate=(\d+).*valves? (?:([A-Z]{2})(?:, )?)+";
            var data = Regex.Match(s, p).Groups.Values.SelectMany(v => v.Captures).ToArray();

            var name = data[1].Value;
            var rate = int.Parse(data[2].Value);

            var tunnelsNames = data[3..].Select(tunnel => tunnel.Value);
            var tunnelsTo = new List<Valve>();
            foreach (var tunnelToName in tunnelsNames)
            {
                if (valves.ContainsKey(tunnelToName))
                {
                    tunnelsTo.Add(valves[tunnelToName]);
                }
                else
                {
                    valves[tunnelToName] = new Valve(id++, tunnelToName);
                    tunnelsTo.Add(valves[tunnelToName]);
                }
            }

            if (valves.ContainsKey(name))
            {
                valves[name].Rate = rate;
                valves[name].TunnelsTo = tunnelsTo;
            }
            else
            {
                valves[name] = new Valve(id++, name, rate, tunnelsTo);
            }
        }

        return valves;
    }
}

static class ValveExtensions
{
    internal static bool IsClosed(this Valve valve, long openedValvesMask)
    {
        return (openedValvesMask & (1 << valve.Id)) == 0;
    }

    internal static bool CanBeOpened(this Valve valve)
    {
        return valve.Rate > 0;
    }
}

class Valve
{
    public int Id { get; }
    public string Name { get; }
    public int Rate { get; set; }

    public List<Valve> TunnelsTo { get; set; } = new();

    public Valve(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Valve(int id, string name, int rate, List<Valve> tunnelsTo) : this(id, name)
    {
        Rate = rate;
        TunnelsTo = tunnelsTo;
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}
