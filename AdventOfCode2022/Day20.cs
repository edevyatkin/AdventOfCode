using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 20)]
public class Day20 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = CalculateResult(input, 1, 1);
        var result2 = CalculateResult(input, 811589153, 10);

        return new AocDayResult(result1, result2);
    }

    private long CalculateResult(string[] input, int mul, int rounds)
    {
        var result = 0L;
        var ints = input.Select(v => long.Parse(v) * mul).ToArray();
        var dict = new Dictionary<int, LinkedListNode<long>>();
        var ll = new LinkedList<long>();
        for (var i = 0; i < ints.Length; i++)
        {
            dict[i] = ll.AddLast(ints[i]);
        }

        while (--rounds >= 0)
        {
            for (var i = 0; i < ints.Length; i++)
            {
                var lln = dict[i];
                var shift = lln.Value;
                if (shift == 0)
                {
                    continue;
                }

                var node = lln;
                var j = shift % (input.Length - 1);
                if (j == 0)
                {
                    continue;
                }

                if (j > 0)
                {
                    while (--j >= 0)
                    {
                        node = node.Next ?? ll.First;
                    }

                    dict[i] = node == ll.Last ? ll.AddBefore(ll.First, shift) : ll.AddAfter(node, shift);
                }
                else if (j < 0)
                {
                    while (++j <= 0)
                    {
                        node = node.Previous ?? ll.Last;
                    }

                    dict[i] = node == ll.First ? ll.AddAfter(ll.Last, shift) : ll.AddBefore(node, shift);
                }

                ll.Remove(lln);
            }
        }


        var js = new[] { 1000, 2000, 3000 };
        var zeroNode = dict.Values.First(kv => kv.Value == 0);

        foreach (var j in js)
        {
            var nodeToFind = zeroNode;
            var i = j;
            while (--i >= 0)
            {
                nodeToFind = nodeToFind.Next ?? ll.First;
            }

            result += nodeToFind.Value;
        }

        return result;
    }
}
