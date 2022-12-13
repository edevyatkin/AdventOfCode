using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,13)]
public class Day13 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 1;

        var packets = new List<Node>();
        for (var index = 0; index < input.Length; index += 3)
        {
            var s1 = input[index];
            var s2 = input[index + 1];
            var p1 = Parse(s1);
            var p2 = Parse(s2);
            packets.AddRange(new[] { p1, p2 });
            if (p1.CompareTo(p2) <= 0)
            {
                result1 += index / 3 + 1;
            }
        }

        var dividerPacket1 = Parse("[[2]]");
        var dividerPacket2 = Parse("[[6]]");
        packets.AddRange(new[] { dividerPacket1, dividerPacket2 });
        
        packets.Sort();

        for (var i = 0; i < packets.Count; i++)
        {
            if (packets[i] == dividerPacket1 || packets[i] == dividerPacket2)
            {
                result2 *= i+1;
            }
        }

        return new AocDayResult(result1, result2);
    }

    private Node Parse(string str)
    {
        Node ParseFromIndex(string s, ref int i)
        {
            var node = new Node();
            while (i < s.Length)
            {
                if (s[i] is '[' or ',')
                {
                    i += 1;
                    node.Children.Add(ParseFromIndex(s, ref i));
                }

                if (i < s.Length && s[i] == ']')
                {
                    i++;
                    return node;
                }

                if (i < s.Length && char.IsDigit(s[i]))
                {
                    var num = 0;
                    while (i < s.Length && char.IsDigit(s[i]))
                    {
                        num += num * 10 + (s[i] - '0');
                        i++;
                    }

                    node.Value = num;
                    return node;
                }
            }

            return node;
        }

        int index = 0;
        return ParseFromIndex(str, ref index);
    }

    class Node : IComparable<Node>
    {
        public int Value { get; set; } = -1;
        public List<Node> Children { get; } = new();

        public int CompareTo(Node? other)
        {
            if (other is null)
                return 1;

            if (Value != -1 && other.Value != -1)
            {
                return Value < other.Value ? -1 : Value > other.Value ? 1 : 0;
            }
            if (Children.Count > 0 && other.Children.Count > 0)
            {
                using var enumThis = Children.GetEnumerator();
                using var enumOther = other.Children.GetEnumerator();
                var movedThis = enumThis.MoveNext();
                var movedOther = enumOther.MoveNext();
                while (movedThis || movedOther)
                {
                    if (!movedThis && movedOther)
                        return -1;
                    if (movedThis && !movedOther)
                        return 1;
                    var comp = enumThis.Current.CompareTo(enumOther.Current);
                    if (comp != 0)
                    {
                        return comp;
                    }
                    movedThis = enumThis.MoveNext();
                    movedOther = enumOther.MoveNext();
                }

                return 0;
            }
            if (Value != -1 && other.Children.Count > 0)
            {
                var node = new Node();
                node.Value = Value;
                Value = -1;
                Children.Add(node);
                return CompareTo(other);
            }
            if (Children.Count > 0 && other.Value != -1)
            {
                var node = new Node();
                node.Value = other.Value;
                other.Value = -1;
                other.Children.Add(node);
                return CompareTo(other);       
            }

            if (Value == -1 && Children.Count == 0)
            {
                return other is { Value: -1, Children.Count: 0 } ? 0 : -1;
            }

            return other is { Value: -1, Children.Count: 0 } ? 1 : 0;
        }
        
        public override string ToString()
        {
            if (Children.Count > 0)
            {
                return "[" + string.Join(',', Children) + "]";
            }
            return Value != -1 ? Value.ToString(): string.Empty;
        }
    }
}


