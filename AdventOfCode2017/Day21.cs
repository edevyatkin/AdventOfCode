using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017, 21)]
public class Day21 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var rules = RuleParser.Parse(input);
        
        var result1 = Solve(rules, 5);
        var result2 = Solve(rules, 18);

        return new AocDayResult(result1, result2);
    }

    internal static int Solve(Dictionary<Pattern,Pattern> rules, int iterations)
    {
        var image = new Image(".#.\n..#\n###");
        
        while (iterations-- > 0)
            image = image.Enhance(rules);

        return image.Pixels
            .SelectMany(p => p)
            .Count(p => p.Status == PixelStatus.On);
    }
}

internal static class RuleParser
{
    public static Dictionary<Pattern,Pattern> Parse(string[] input)
    {
        var dict = input
            .Select(l => l.Split(" => "))
            .ToDictionary(sp => new Pattern(sp[0]), sp => new Pattern(sp[1]));
        
        var patternFrom = dict.Keys.ToArray();
        
        foreach (var from in patternFrom)
            foreach (var equalFrom in GenerateEqualPatterns(from))
                    dict.TryAdd(equalFrom, dict[from]);
        
        return dict;
    }

    private static IEnumerable<Pattern> GenerateEqualPatterns(Pattern p)
    {
        var vf1 = p.FlipVertical();
        yield return vf1;
        var hf1 = vf1.FlipHorizontal();
        yield return hf1;
        var vf2 = hf1.FlipVertical();
        yield return vf2;
        var rtt = vf2.RotateRight();
        yield return rtt;
        var vf3 = rtt.FlipVertical();
        yield return vf3;
        var hf2 = vf3.FlipHorizontal();
        yield return hf2;
        var vf4 = hf2.FlipVertical();
        yield return vf4;
    }
}

internal class Pattern : IEquatable<Pattern>
{
    public Pixel[][] Pixels { get; }
    public int Size => Pixels.Length;
    
    public Pattern(string str)
    {
        Pixels = str
            .Split('/')
            .Select(
                (l, x) =>
                    l.Select((c, y) => new Pixel(x, y, c == '#' ? PixelStatus.On : PixelStatus.Off)).ToArray()
            ).ToArray();
    }

    public Pattern(Pixel[][] pixels)
    {
        Pixels = pixels;
    }

    public Pattern FlipHorizontal()
    {
        var pixels = Pixels.Select(r => r.Reverse().ToArray()).ToArray();
        return new Pattern(pixels);
    }

    public Pattern FlipVertical()
    {
        var p = new Pixel[Size][];
        for (var i = 0; i < p.Length; i++)
            p[i] = new Pixel[Size];
        
        for (var j = 0; j < Size; j++)
            for (var i = 0; i < Size; i++)
                p[i][j] = Pixels[^(i + 1)][j];
        
        return new Pattern(p);
    }

    public Pattern RotateRight()
    {
        var p = new Pixel[Size][];
        for (var i = 0; i < p.Length; i++)
            p[i] = new Pixel[Size];
        
        for (var i = 0; i < Size; i++)
            for (var j = 0; j < Size; j++)
                p[i][j] = Pixels[^(j + 1)][i];

        return new Pattern(p);
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public bool Equals(Pattern? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ToString().Equals(other.ToString());
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Pattern)obj);
    }
    
    public override string ToString()
    {
        return string.Concat(Pixels.SelectMany(l => l));
    }
}

internal enum PixelStatus
{
    On,
    Off
}

internal readonly record struct Pixel(int X, int Y, PixelStatus Status)
{
    public override string ToString() => Status == PixelStatus.On ? "#" : ".";
}

internal class Image
{
    public Pixel[][] Pixels { get; }
    
    public int Size => Pixels.Length;
    
    public Image(string str)
    {
        Pixels = str
            .Split('\n')
            .Select(
                (l, x) =>
                    l.Select((c, y) => new Pixel(x, y, c == '#' ? PixelStatus.On : PixelStatus.Off)).ToArray()
            ).ToArray();
    }

    private Image(Pixel[][] pixels)
    {
        Pixels = pixels;
    }

    public Image Enhance(Dictionary<Pattern,Pattern> rules)
    {
        var patternSize = Size % 2 == 0 ? 2 : 3;
        
        var patterns = ToPatterns(patternSize);

        for (var i = 0; i < patterns.Length; i++)
        {
            for (var j = 0; j < patterns[i].Length; j++)
            {
                patterns[i][j] = rules[patterns[i][j]];
            }
        }
        
        return FromPatterns(patterns);
    }

    private Image FromPatterns(Pattern[][] patterns)
    {
        var patternSize = patterns[0][0].Size;
        var imageSize = patterns[0].Length * patternSize;
        
        var pixels = new Pixel[imageSize][];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = new Pixel[imageSize];
        
        for (var i = 0; i < imageSize; i += patternSize)
        {
            for (int j = 0; j < imageSize; j += patternSize)
            {
                var patternPixels = patterns[i/patternSize][j/patternSize].Pixels;
                for (var ppi = 0; ppi < patternSize; ppi++)
                {
                    for (int ppj = 0; ppj < patternSize; ppj++)
                    {
                        pixels[i + ppi][j + ppj] = patternPixels[ppi][ppj];
                    }
                }
            }
        }

        return new Image(pixels);
    }

    private Pattern[][] ToPatterns(int patternSize)
    {
        var sizeInPatterns = Size / patternSize;
        
        var patterns = new Pattern[sizeInPatterns][];
        for (var i1 = 0; i1 < patterns.Length; i1++)
            patterns[i1] = new Pattern[sizeInPatterns];
        
        for (var i = 0; i < patterns.Length; i++)
        {
            for (var j = 0; j < patterns[i].Length; j++)
            {
                var tli = i * patternSize;
                var tlj = j * patternSize;
                var bri = i * patternSize + patternSize - 1;
                var brj = j * patternSize + patternSize - 1;
                patterns[i][j] = new Pattern(Pixels[tli..(bri + 1)]
                    .Select(l => l[tlj..(brj + 1)]).ToArray());
            }
        }
        return patterns;
    }
}
