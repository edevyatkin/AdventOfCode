using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,19)]
public class Day19 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var replacements = new Dictionary<string, List<List<string>>>();
        foreach (var s in input)
        {
            if (s == string.Empty)
            {
                break;
            }
            var sp = s.Split(" => ");
            var from = sp[0];
            var to = ParseMolecule(sp[1]).ToList();
            if (!replacements.ContainsKey(from))
                replacements[from] = new();
            replacements[from].Add(to);
        }
        
        var moleculeList = new LinkedList<string>();
        moleculeList.AddLast(string.Empty);
        var moleculeDict = new Dictionary<string, List<LinkedListNode<string>>>();
        foreach (var mol in ParseMolecule(input[^1]))
        {
            if (!moleculeDict.ContainsKey(mol))
                moleculeDict[mol] = new();
            var node = moleculeList.AddLast(mol);
            moleculeDict[mol].Add(node);
        }
        moleculeList.AddLast(string.Empty);

        // PART 1

        var newMolecules = new HashSet<string>();
        foreach (var (from, to) in replacements)
        {
            if (!moleculeDict.ContainsKey(from))
            {
                continue;
            }

            for (var mIndex = 0; mIndex < moleculeDict[from].Count; mIndex++)
            {
                var atomNode = moleculeDict[from][mIndex];
                var newNodeStack = new Stack<LinkedListNode<string>>();
                var prevNode = atomNode.Previous;
                var atomName = atomNode.Value;
                moleculeList.Remove(atomNode);
                foreach (var newAtoms in to)
                {
                    var tmp = prevNode;
                    foreach (var newAtom in newAtoms)
                    {
                        var newNode = moleculeList.AddAfter(tmp, newAtom);
                        newNodeStack.Push(newNode);
                        tmp = newNode;
                    }

                    var resultMolecule = string.Concat(moleculeList);
                    newMolecules.Add(resultMolecule);
                    while (newNodeStack.Count > 0)
                    {
                        moleculeList.Remove(newNodeStack.Pop());
                    }
                }

                var restoredNode = moleculeList.AddAfter(prevNode, atomName);
                moleculeDict[from][mIndex] = restoredNode;
            }
        }

        result1 = newMolecules.Count;

        // PART 2

        var revReplacements = new Dictionary<string, string>();
        foreach (var s in input)
        {
            if (s == string.Empty)
            {
                break;
            }
            var sp = s.Split(" => ");
            revReplacements[sp[1]] = sp[0];
        }

        var q = new Queue<(string Str,int Steps)>();
        q.Enqueue((input[^1], 0));
        var froms = revReplacements.Keys
            .GroupBy(r => r.Length)
            .OrderByDescending(r => r.Key)
            .Select(g => g.ToList())
            .ToList();
        while (q.Count > 0)
        {
            var (s, steps) = q.Dequeue();
            if (s == "e")
            {
                result2 = steps;
                break;
            }
            foreach (var from in froms)
            {
                var replaced = 0;
                foreach (var f in from)
                {
                    var t = revReplacements[f];
                    for (var i = 0; i < s.Length - f.Length + 1; i++)
                    {
                        var j = 0;
                        while (j < f.Length && s[i + j] == f[j])
                        {
                            j++;
                        }
                        if (j == f.Length)
                        {
                            s = s[..i] + t + s[(i + f.Length)..];
                            replaced++;
                        }
                    }
                }
                if (replaced > 0)
                {
                    q.Enqueue((s, steps+replaced));
                    break;
                }
            }
        }

        return new AocDayResult(result1, result2);
    }

    private IEnumerable<string> ParseMolecule(string molecule)
    {
        var i = 0;
        while (i < molecule.Length)
        {
            if (i < molecule.Length - 1)
            {
                var sub = molecule[i..(i + 2)];
                if (char.IsLower(sub[1]))
                {
                    yield return sub;
                    i += 2;
                }
                else
                {
                    yield return sub[0].ToString();
                    i++;
                }
            }
            else
            {
                yield return molecule[i].ToString();
                i++;
            }
        }
    }
}
