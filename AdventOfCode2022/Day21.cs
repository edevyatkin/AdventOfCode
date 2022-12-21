using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 21)]
public class Day21 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0D;
        var result2 = 0D;

        var valDict = new Dictionary<string, double>();
        var stDict = new Dictionary<string, Statement>();
        foreach (var s in input)
        {
            var sp = s.Split();
            var eq = sp[0][..^1];
            if (sp.Length == 2)
            {
                valDict[eq] = double.Parse(sp[1]);
            }
            else
            {
                var (l, op, r) = (sp[1], sp[2][0], sp[3]);
                var st = new Statement(l, op, r, eq);
                stDict[eq] = st;
            }
        }

        var sorted = SortEq(stDict, valDict);
        var resDict1 = GetResult(stDict, valDict, sorted);
        result1 = resDict1["root"];

        long i = 0;
        long j = long.MaxValue;
        var diff = resDict1[stDict["root"].R] - resDict1[stDict["root"].L];
        while (true)
        {
            var pos = i + (j - i) / 2;
            valDict["humn"] = pos;
            var sorted2 = SortEq(stDict, valDict);
            var resDict2 = GetResult(stDict, valDict, sorted2);
            var curDiff = resDict2[stDict["root"].R] - resDict2[stDict["root"].L];
            if (curDiff == 0)
            {
                result2 = pos;
                break;
            }
            if (curDiff < 0)
            {
                if (diff < 0) i = pos; else j = pos;
            }
            else
            {
                if (diff > 0) i = pos; else j = pos;
            }
        }

        return new AocDayResult(result1, result2);
    }

    class Statement
    {
        public string L { get; }
        public char Op { get; }
        public string R { get; }
        public string Eq { get; }

        public Statement(string l, char op, string r, string eq)
        {
            L = l;
            Op = op;
            R = r;
            Eq = eq;
        }
    }

    List<string> SortEq(Dictionary<string, Statement> stDict, Dictionary<string, double> valDict)
    {
        var sorted = new List<string>();
        var q = new Queue<string>(valDict.Keys);
        var needsTo = new Dictionary<string, HashSet<string>>();
        var dependsFrom = new Dictionary<string, HashSet<string>>();
        foreach (var s in stDict.Values)
        {
            if (!needsTo.ContainsKey(s.Eq))
                needsTo[s.Eq] = new HashSet<string>();
            if (!dependsFrom.ContainsKey(s.Eq))
                dependsFrom[s.Eq] = new HashSet<string>();
            if (!needsTo.ContainsKey(s.L))
                needsTo[s.L] = new HashSet<string>();
            if (!needsTo.ContainsKey(s.R))
                needsTo[s.R] = new HashSet<string>();
            dependsFrom[s.Eq].Add(s.L);
            dependsFrom[s.Eq].Add(s.R);
            needsTo[s.L].Add(s.Eq);
            needsTo[s.R].Add(s.Eq);
        }

        while (q.Count > 0)
        {
            var n = q.Dequeue();
            sorted.Add(n);
            foreach (var m in needsTo[n])
            {
                dependsFrom[m].Remove(n);
                if (dependsFrom[m].Count == 0)
                {
                    q.Enqueue(m);
                }
            }
        }

        return sorted;
    }

    Dictionary<string, double> GetResult(Dictionary<string, Statement> stDict, Dictionary<string, double> valDict,
        List<string> sortedKeys)
    {
        var resDict = new Dictionary<string, double>();

        foreach (var key in sortedKeys)
        {
            if (!stDict.ContainsKey(key))
            {
                resDict[key] = valDict[key];
            }
            else
            {
                var st = stDict[key];
                resDict[key] = st.Op switch
                {
                    '-' => resDict[st.L] - resDict[st.R],
                    '+' => resDict[st.L] + resDict[st.R],
                    '*' => resDict[st.L] * resDict[st.R],
                    '/' => resDict[st.L] / resDict[st.R]
                };
            }
        }

        return resDict;
    }
}
