using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var topDir = BuildTree(input);

        var result1 = SumDirectoriesSizes(topDir, 100_000);
        var result2 = FindDirectorySizeToDelete(topDir, 70_000_000, 30_000_000);

        return new AocDayResult(result1, result2);
    }

    private Dir BuildTree(string[] input)
    {
        var topDir = new Dir("/");
        var st = new Stack<Dir>();
        st.Push(topDir);
        var index = 0;
        while (index < input.Length)
        {
            var s = input[index];
            if (s.StartsWith("$ cd"))
            {
                var cd = s.Split(' ')[^1].TrimEnd();
                if (cd == "/")
                {
                    while (st.Peek().Name != "/")
                    {
                        st.Pop();
                    }
                }
                else if (cd == "..")
                {
                    st.Pop();
                }
                else
                {
                    var curD = st.Peek();
                    var subDir = curD.AddSubDirIfNotExist(cd);
                    st.Push(subDir);
                }

                index++;
            }
            else if (s.StartsWith("$ ls"))
            {
                var curDir = st.Peek();
                index++;
                while (index < input.Length && !input[index].StartsWith("$"))
                {
                    var ln = input[index];
                    var sp = ln.Split(' ');
                    if (sp[0].StartsWith("dir"))
                    {
                        curDir.AddSubDirIfNotExist(sp[1].TrimEnd());
                    }
                    else
                    {
                        curDir.AddFileIfNotExist(sp[1].TrimEnd(), int.Parse(sp[0]));
                    }

                    index++;
                }
            }
        }

        return topDir;
    }

    private int SumDirectoriesSizes(Dir dir, int maxDirSize)
    {
        var dirSizes = GetDirectoriesSizes(dir);
        return dirSizes.Where(s => s <= maxDirSize).Sum();
    }

    private int FindDirectorySizeToDelete(Dir dir, int allSpace, int needSpace)
    {
        var dirSizes = GetDirectoriesSizes(dir);
        var usedSpace = dirSizes.Max();

        var minDirSize = int.MaxValue;
        foreach (var size in dirSizes)
        {
            var usedSpaceAfterDelete = allSpace - (usedSpace - size);
            if (usedSpaceAfterDelete >= needSpace)
            {
                minDirSize = Math.Min(minDirSize, size);
            }
        }

        return minDirSize;
    }

    private List<int> GetDirectoriesSizes(Dir dir)
    {
        var dirSizes = new List<int>();

        var q = new Queue<Dir>();
        q.Enqueue(dir);
        while (q.Count > 0)
        {
            var pd = q.Dequeue();
            dirSizes.Add(pd.Size);
            foreach (var subDir in pd.SubDirs.Values)
            {
                q.Enqueue(subDir);
            }
        }

        return dirSizes;
    }

    public void PrintTree(Dir dir)
    {
        var sb = new StringBuilder();
        Dfs(dir, 1);
        Console.WriteLine(sb.ToString());

        void Dfs(Dir dir, int level)
        {
            sb.AppendLine($"{new string(' ', level)} - {dir.Name} (dir)");
            foreach (var fil in dir.Files)
                sb.AppendLine($"{new string(' ', level+1)} - {fil.Name} (file, size={fil.Size})");
            foreach (var subDir in dir.SubDirs.Values)
                Dfs(subDir, level + 1);
        }
    }
}

public class Fil
{
    public string Name { get; }
    public int Size { get; }

    public Fil(string name, int size)
    {
        Name = name;
        Size = size;
    }
}

public class Dir
{
    public string Name { get; }

    public int Size
    {
        get
        {
            if (_needSizeUpdate)
            {
                _sumFiles = Files.Sum(f => f.Size);
                _sumSubDirs = SubDirs.Sum(sd => sd.Value.Size);
                _needSizeUpdate = false;
            }

            return _sumFiles + _sumSubDirs;
        }
    }

    public List<Fil> Files { get; } = new();
    public Dictionary<string, Dir> SubDirs { get; } = new();

    private bool _needSizeUpdate;
    private int _sumFiles;
    private int _sumSubDirs;
    private readonly Dir? _parent;

    public Dir(string name, Dir? parent = null)
    {
        Name = name;
        _parent = parent;
    }

    public Fil AddFileIfNotExist(string name, int size)
    {
        var fi = Files.FindIndex(f => f.Name == name);
        if (fi != -1)
            throw new ArgumentException("File already exists", nameof(name));
        var fil = new Fil(name, size);
        Files.Add(fil);
        MarkTopTree();
        return fil;
    }

    public Dir AddSubDirIfNotExist(string name)
    {
        if (!SubDirs.ContainsKey(name))
        {
            SubDirs[name] = new Dir(name, this);
        }

        return SubDirs[name];
    }

    private void MarkTopTree()
    {
        var p = this;
        while (p is not null)
        {
            p._needSizeUpdate = true;
            p = p._parent;
        }
    }
}
