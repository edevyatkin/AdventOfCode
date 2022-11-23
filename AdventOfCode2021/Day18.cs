using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,18)]
public class Day18 : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);

        // PART 1
        long result1 = 0;
        Node tree1 = TreeParser.Parse(input[0]);
        Node resultTree = null;
        for (int i = 1; i < input.Length; i++) {
            Node tree2 = TreeParser.Parse(input[i]);
            resultTree = TreeCalculator.Add(tree1, tree2);
            tree1 = resultTree;
        }
        
        result1 = TreeCalculator.Magnitude(resultTree);

        // PART 2
        int result2 = 0;
        for (int i = 0; i < input.Length-1; i++) {
            for (int j = i+1; j < input.Length; j++) {
                var addTwo = TreeCalculator.Add(TreeParser.Parse(input[i]), TreeParser.Parse(input[j]));
                var addTwoReversed = TreeCalculator.Add(TreeParser.Parse(input[j]), TreeParser.Parse(input[i]));
                var magnitude1 = TreeCalculator.Magnitude(addTwo);
                var magnitude2 = TreeCalculator.Magnitude(addTwoReversed);
                result2 = Math.Max(result2, magnitude1);
                result2 = Math.Max(result2, magnitude2);
            }
        }
        
        return new AocDayResult(result1, result2);
    }

    public class Node {
        public Node? Left { get; internal set; }
        public Node? Right { get; internal set; }
        public int Value { get; internal set; }
        public Node Parent { get; internal set; }

        public Node(Node left, Node right, Node parent = null) {
            Left = left;
            Right = right;
            Parent = parent;
            Value = 1;
        }

        public Node(int value, Node parent) {
            Parent = parent;
            Value = value;
        }

        public override string ToString() {
            if (Left is null && Right is null)
                return Value.ToString();
            return $"[{Left?.ToString()},{Right?.ToString()}]";
        }
    }
    
    public static class TreeParser {
        public static Node Parse(string s) {
            var tempParent = new Node(-1, null);
            int i = 0;
            var head = Parse(s, ref i, tempParent);
            tempParent.Left = head;
            return tempParent.Left;
        }
        
        private static Node Parse(string s, ref int i, Node parent) {
            Node node = new Node(-1, parent);
            if (i == s.Length)
                return node;
            if (s[i] == '[') {
                i++;
                node.Left = Parse(s, ref i, node);
            }
            if (char.IsDigit(s[i])) {
                var num = 0;
                while (Char.IsDigit(s[i])) {
                    var d = s[i] - '0';
                    num *= 10;
                    num += d;
                    i++;
                }
                node.Value = num;
                return node;
            }
            if (s[i] == ',') {
                i++;
                node.Right = Parse(s, ref i, node);
            }

            i++;
            return node;
        }
    }
    
    public static class TreeCalculator {
        internal enum ReduceOperation {
            Addition
        }

        public static Node Add(Node tree1, Node tree2) {
            var root = new Node(tree1, tree2);
            tree1.Parent = root;
            tree2.Parent = root;
            Reduce(root, ReduceOperation.Addition);
            return root;
        }

        internal static void Reduce(Node root, ReduceOperation op) {
            while (Explode(root, op) || Split(root)) { }
        }
        
        internal static bool Explode(Node node, ReduceOperation op) {
            var leaves = new List<LeafData>();
            CollectLeaves(node, leaves,0);
            var leftLeafIndex = leaves.FindIndex(l => l.Height > 4);
            if (leftLeafIndex == -1)
                return false;
            
            var leftLeafToExplode = leaves[leftLeafIndex].Node;
            var rightLeafToExplode = leaves[leftLeafIndex+1].Node;
            var prevLeaf = (leftLeafIndex-1 >= 0) ? leaves[leftLeafIndex-1].Node : null;
            var postLeaf = (leftLeafIndex+2 <= leaves.Count-1) ? leaves[leftLeafIndex + 2].Node : null;
            switch (op) {
                case ReduceOperation.Addition:
                    if (prevLeaf is not null)
                        prevLeaf.Value += leftLeafToExplode.Value;
                    if (postLeaf is not null)
                        postLeaf.Value += rightLeafToExplode.Value;
                    break;
            }
            
            var leafParent = leftLeafToExplode.Parent;
            leafParent.Left = null;
            leafParent.Right = null;
            leafParent.Value = 0;

            return true;
        }
        
        internal static bool Split(Node node) {
            var leaves = new List<LeafData>();
            CollectLeaves(node, leaves,0);
            var leafIndex = leaves.FindIndex(l => l.Node.Value >= 10);
            if (leafIndex == -1)
                return false;
            var leaf = leaves[leafIndex].Node;
            var leftValue = leaf.Value / 2;
            var rightValue = (leaf.Value % 2 == 0) ? leftValue : leftValue + 1;
            leaf.Value = 1;
            leaf.Left = new Node(leftValue, leaf);
            leaf.Right = new Node(rightValue, leaf);
            return true;
        }

        private static void CollectLeaves(Node node, List<LeafData> leaves, int height) {
            if (node.Left == null && node.Right == null) {
                leaves.Add(new LeafData(node, height));
                return;
            }
            CollectLeaves(node.Left, leaves, height + 1);
            CollectLeaves(node.Right, leaves, height + 1);
        }

        struct LeafData {
            public Node Node;
            public int Height;

            public LeafData(Node node, int height) {
                Node = node;
                Height = height;
            }
        }

        public static int Magnitude(Node? node) {
            if (node == null)
                return 0;
            if (node.Left == null && node.Right == null)
                return node.Value;
            var leftMagnitude = 3 * Magnitude(node.Left);
            var rightMagnitude = 2 * Magnitude(node.Right);
            return leftMagnitude + rightMagnitude;
        }
    }
}



