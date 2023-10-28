using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,11)]
public class Day11 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, false);
        var result2 = SolvePart(input, true);
        return new AocDayResult(result1, result2);
    }

    private static int SolvePart(string[] input, bool isPartTwo)
    {
        var result = 0;
        var initialArrangement = 0L;
        var floors = new List<List<string>>();
        var itemsList = new List<string>();
        foreach (var f in input)
        {
            floors.Add(Regex
                .Matches(f, @"(a .*?) ")
                .Select(m => m.Groups[1].Value)
                .ToList());
        }

        if (isPartTwo)
        {
            floors[0].AddRange(new []
            {
                "An elerium generator.",
                "An elerium-compatible microchip.",
                "A dilithium generator.",
                "A dilithium-compatible microchip."
            });
        }

        itemsList = floors.SelectMany(fi => fi).ToList();
        itemsList.Sort();
        for (var floorNum = 0; floorNum < floors.Count; floorNum++)
        {
            var floorItems = floors[floorNum];
            foreach (var item in floorItems)
            {
                var shift = (floorNum * itemsList.Count + itemsList.IndexOf(item));
                initialArrangement |= 1L << shift;
            }
        }

        // Dump(initialArrangement, itemsList, floors);

        var elevatorShift = itemsList.Count * floors.Count;
        var elevatorMask = 3L << elevatorShift;
        var floorMask = (1L << itemsList.Count) - 1;
        var rtgMask = floorMask;
        int fc = itemsList.Count;
        var mask = 1;
        while (fc-- > 0)
        {
            rtgMask &= ~mask;
            mask <<= 2;
        }

        var chipMask = rtgMask >> 1;
        var cache = new HashSet<long>();
        var q = new Queue<long>();
        q.Enqueue(initialArrangement);
        var steps = 0;
        while (q.Count > 0)
        {
            var count = q.Count;
            while (count-- > 0)
            {
                var arng = q.Dequeue();
                // Dump(arng, itemsList, floors);
                if (!cache.Add(arng))
                    continue;
                if ((arng >> itemsList.Count * (floors.Count - 1) & floorMask) == floorMask)
                {
                    result = steps;
                    break;
                }

                var elevator = (int)((elevatorMask & arng) >> elevatorShift);
                var floorShift = itemsList.Count * elevator;
                var floor = (arng >> floorShift) & floorMask;
                for (int firstItemShift = 0; firstItemShift < itemsList.Count; firstItemShift++)
                {
                    for (int secondItemShift = firstItemShift; secondItemShift < itemsList.Count; secondItemShift++)
                    {
                        var itemsForElevator = (1L << firstItemShift) | (1L << secondItemShift);
                        if ((itemsForElevator & floor) != itemsForElevator)
                            continue;
                        for (var newElevator = elevator - 1; newElevator <= elevator + 1; newElevator++)
                        {
                            if (newElevator == elevator || newElevator > 3 || newElevator < 0)
                                continue;
                            var newFloorShift = itemsList.Count * newElevator;
                            var newFloor = (((floorMask << newFloorShift) & arng) >> newFloorShift) | itemsForElevator;
                            var floorChips = newFloor & chipMask;
                            var floorRtgs = newFloor & rtgMask;

                            long CreateNewArrangement()
                            {
                                var a = arng | (itemsForElevator << newFloorShift); // add to new floor
                                a &= ~(itemsForElevator << floorShift); // remove from previous floor
                                a &= ~elevatorMask; // reset elevator position
                                a |= (long)newElevator << elevatorShift; // set new elevator position
                                return a;
                            }

                            if (floorChips > 0 && floorRtgs > 0)
                            {
                                var rtgsForChips = (floorChips << 1) & floorRtgs;
                                if (rtgsForChips >> 1 != floorChips)
                                    continue;
                            }

                            var newArng = CreateNewArrangement();
                            q.Enqueue(newArng);
                        }
                    }
                }
            }

            steps++;
        }

        return result;
    }

    private static void Dump(long arng, List<string> itemsList, List<List<string>> floors)
    {
        var line = Convert.ToString(arng, 2)
            .PadLeft(itemsList.Count * floors.Count + 2, '0');
        var ix = line.Length;
        while (ix-itemsList.Count > 0)
        {
            ix -= itemsList.Count;
            line = line.Insert(ix, " ");
        }
        Console.WriteLine(line);
    }
}
