using System.Collections;

namespace AdventOfCode2023;

[AocDay(2023, 19)]
public class Day19 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;

        var workflowSystem = new WorkflowSystem();

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            if (char.IsLetter(line[0]))
            {
                workflowSystem.AddWorkflow(line);
            }

            if (line[0] == '{')
            {
                var part = new Part(line);
                var result = workflowSystem.ProcessPart(part);
                if (result.IsAccepted)
                {
                    result1 += part.X + part.M + part.A + part.S;
                }
            }
        }

        var result2 = workflowSystem
            .EnumerateWorkflowsAcceptedRangedParts()
            .Sum(pr =>
                ((long)pr.RangeX.Max - pr.RangeX.Min + 1)
                * ((long)pr.RangeM.Max - pr.RangeM.Min + 1)
                * ((long)pr.RangeA.Max - pr.RangeA.Min + 1)
                * ((long)pr.RangeS.Max - pr.RangeS.Min + 1));

        return new AocDayResult(result1, result2);
    }
}

public class WorkflowSystem
{
    private Dictionary<string, Workflow> _workflows = new();

    public void AddWorkflow(string workflowStr)
    {
        var w = new Workflow(workflowStr);
        _workflows[w.Name] = w;
    }

    public WorkflowSystemPartResult ProcessPart(Part part)
    {
        var currentWorkflowName = "in";
        while (currentWorkflowName is not ("A" or "R"))
            currentWorkflowName = _workflows[currentWorkflowName].Rules.ToNextWorkflowName(part);

        return new WorkflowSystemPartResult() {
            Part = part,
            IsAccepted = currentWorkflowName == "A"
        };
    }

    public IEnumerable<RangedPart> EnumerateWorkflowsAcceptedRangedParts()
    {
        var currentWorkflowName = "in";
        var rangedPart = new RangedPart();
        var stack = new Stack<(string WorkflowName, RangedPart RangedPart)>();
        stack.Push((currentWorkflowName, rangedPart));
        while (stack.Count > 0)
        {
            var (wn, rp) = stack.Pop();
            if (wn is "R")
                continue;

            if (wn is "A")
            {
                yield return rp;
                continue;
            }

            var tempRp = rp;
            foreach (var rule in _workflows[wn].Rules)
            {
                var cuttedRp = tempRp.CutBy(rule);
                if (cuttedRp.IsValid)
                    stack.Push((rule.NextWorkflow, cuttedRp));
                var invertedRp = tempRp.InvertedCutBy(rule);
                tempRp = invertedRp;
            }

            stack.Push((_workflows[wn].Rules.DefaultWorkflow, tempRp));
        }
    }
}

public struct WorkflowSystemPartResult
{
    public Part Part { get; set; }
    public bool IsAccepted { get; set; }
}

public struct Part
{
    public Part(string line)
    {
        var l = line[1..^1];
        var sp = l.Split(',');
        X = int.Parse(sp[0][2..]);
        M = int.Parse(sp[1][2..]);
        A = int.Parse(sp[2][2..]);
        S = int.Parse(sp[3][2..]);
    }

    public int X { get; }
    public int M { get; }
    public int A { get; }
    public int S { get; }
}

public record struct RangedPart
{
    public RangedPart()
    {
    }

    public (int Min, int Max) RangeX { get; set; } = (1, 4000);
    public (int Min, int Max) RangeM { get; set; } = (1, 4000);
    public (int Min, int Max) RangeA { get; set; } = (1, 4000);
    public (int Min, int Max) RangeS { get; set; } = (1, 4000);

    public bool IsValid => RangeX != default
                           && RangeM != default
                           && RangeA != default
                           && RangeS != default;

    internal RangedPart CutBy(Workflow.Rule rule)
    {
        var rangePart = this;
        var sign = rule.Sign;
        switch (rule.Category)
        {
            case 'x':
                rangePart.RangeX = Cut(RangeX, sign, rule.Value);
                break;
            case 'm':
                rangePart.RangeM = Cut(RangeM, sign, rule.Value);
                break;
            case 'a':
                rangePart.RangeA = Cut(RangeA, sign, rule.Value);
                break;
            case 's':
                rangePart.RangeS = Cut(RangeS, sign, rule.Value);
                break;
            default:
                throw new ArgumentException(nameof(rule.Category));
        }

        return rangePart;
    }

    private static (int Min, int Max) Cut((int Min, int Max) range, string sign, int value)
    {
        switch (sign)
        {
            case ">" or ">=":
                if (value < range.Min)
                    return range;
                if (range.Min <= value && value <= range.Max)
                    return range with { Min = value + (sign == ">" ? 1 : 0) };
                if (range.Max < value)
                    return default;
                break;
            case "<" or "<=":
                if (value < range.Min)
                    return default;
                if (range.Min <= value && value <= range.Max)
                    return range with { Max = value - (sign == "<" ? 1 : 0) };
                if (range.Max < value)
                    return range;
                break;
        }

        return default;
    }

    public RangedPart InvertedCutBy(Workflow.Rule rule)
    {
        var ruleInverted = rule with {
            Sign = rule.Sign switch {
                ">" => "<=",
                "<" => ">=",
                _ => throw new ArgumentOutOfRangeException()
            }
        };

        return CutBy(ruleInverted);
    }
}

public class Workflow
{
    public Workflow(string line)
    {
        var ix = line.IndexOf('{');
        Name = line[..ix];
        var rulesStr = line[(ix + 1)..^1];
        var workflowRules = new WorkflowRules();
        foreach (var r in rulesStr.Split(','))
        {
            if (r.Contains(':'))
            {
                var rule = new Rule(r);
                workflowRules.AddRule(rule);
            }
            else
            {
                workflowRules.DefaultWorkflow = r;
            }
        }

        Rules = workflowRules;
    }

    internal string Name { get; set; }
    internal WorkflowRules Rules { get; set; }

    public struct Rule
    {
        public char Category { get; init; }
        public int Value { get; init; }
        public string Sign { get; init; }
        public string NextWorkflow { get; init; }

        public Rule(string str)
        {
            var sp = str.Split(':');
            Category = sp[0][0];
            Sign = sp[0][1].ToString();
            Value = int.Parse(sp[0][2..]);
            NextWorkflow = sp[1];
        }
    }

    internal class WorkflowRules : IEnumerable<Rule>
    {
        private readonly List<Rule> _rules = new();

        public void AddRule(Rule rule)
        {
            _rules.Add(rule);
        }

        public string ToNextWorkflowName(Part part)
        {
            foreach (var rule in _rules)
            {
                var partVal = 0;
                switch (rule.Category)
                {
                    case 'x':
                        partVal = part.X;
                        break;
                    case 'm':
                        partVal = part.M;
                        break;
                    case 'a':
                        partVal = part.A;
                        break;
                    case 's':
                        partVal = part.S;
                        break;
                    default:
                        throw new ArgumentException(nameof(rule.Category));
                }

                if (rule.Sign == ">" ? partVal > rule.Value : partVal < rule.Value)
                {
                    return rule.NextWorkflow;
                }
            }

            return DefaultWorkflow;
        }

        public string DefaultWorkflow { get; set; }

        public IEnumerator<Rule> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
