namespace AdventOfCode2023;

[AocDay(2023, 15)]
public class Day15 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = input[0].Split(',').Sum(Utils.Hash);

        var facility = new Facility();
        Array.ForEach(input[0].Split(','), step => {
            facility.PerformStep(step);
            // facility.Dump();
        });
        var result2 = facility.FocusingPower;

        return new AocDayResult(result1, result2);
    }
}

public static class Utils
{
    public static int Hash(string s) =>
        s.Aggregate(0, (acc, c) => (acc + (byte)c) * 17 % 256);
}

public class Facility
{
    private readonly Box[] _boxes = new Box[256];

    public Facility()
    {
        for (var index = 0; index < _boxes.Length; index++)
            _boxes[index] = new Box();
    }

    public long FocusingPower => _boxes
        .Select((b, i) => (Box: b, Index: i))
        .Aggregate(0L, (power, box) => power + (box.Index + 1) * box.Box.FocusingPower);

    public void PerformStep(string step)
    {
        var sp = step.Split(new[] { '=', '-' });
        var boxIx = Utils.Hash(sp[0]);
        var box = _boxes[boxIx];
        var label = sp[0];
        var lensIx = -1;
        for (var i = 0; i < box.Count; i++)
        {
            if (box[i].Label == label)
            {
                lensIx = i;
                break;
            }
        }

        if (step.Contains('='))
        {
            var focalLength = int.Parse(sp[1]);
            if (lensIx >= 0)
                box[lensIx] = box[lensIx] with { FocalLength = focalLength };
            else
                box.Add(new(label, focalLength));
        }
        else
        {
            if (lensIx >= 0)
                box.RemoveAt(lensIx);
        }
    }

    public void Dump()
    {
        for (var i = 0; i < _boxes.Length; i++)
        {
            if (_boxes[i].Count > 0)
            {
                Console.WriteLine($"Box {i}: {_boxes[i]}");
            }
        }

        Console.WriteLine();
    }

    private class Box : List<Lens>
    {
        public long FocusingPower => this
            .Select((l, index) => (Lens: l, Index: index))
            .Aggregate(0L, (power, lens) => power + (lens.Index + 1) * lens.Lens.FocalLength);

        public override string ToString() => string.Join(' ', this);
    }

    private readonly record struct Lens(string Label, int FocalLength)
    {
        public override string ToString() => $"[{Label} {FocalLength}]";
    }
}


