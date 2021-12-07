using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020 {
    public class Day18 {
        public static void Main(string[] args) {
            long sum = 0;
            foreach (var line in File.ReadLines("Day18_input")) {
                var tokensArr = ParseExpressionForPart2(line);
                Console.WriteLine(string.Join("", tokensArr));
                var index = tokensArr.Count - 1;
                sum += Evaluate(tokensArr, ref index);
            }
            Console.WriteLine(sum);
        }

        private static long Evaluate(List<string> tokensArr, ref int i) {
            if (i == tokensArr.Count) {
                return 0;
            }
            var opStack = new Stack<string>();
            var valStack = new Stack<long>();
            long result = 0;
            while (i >= 0) {
                if (tokensArr[i] == ")") {
                    i--;
                    valStack.Push(Evaluate(tokensArr, ref i));
                    i--;
                    if (i < 0)
                        break;
                }
                
                if (tokensArr[i] == "(") {
                    break;
                }
                if (long.TryParse(tokensArr[i], out long value)) {
                    valStack.Push(value);
                }
                else {
                    opStack.Push(tokensArr[i]);
                }

                i--;
            }
            opStack.Push("+");
            while (opStack.Count > 0 && valStack.Count > 0) {
                var val = valStack.Pop();
                var op = opStack.Pop();
                result = op switch { 
                    "+" => result + val,
                    _ => result * val
                };
            }

            return result;
        }

        private static List<string> ParseExpressionForPart1(string line) {
            var arr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> tokens = new List<string>();
            foreach (var s in arr) {
                var tempToken = s;
                while (tempToken.StartsWith("(")) {
                    tempToken = tempToken.Substring(1);
                    tokens.Add("(");
                }
                Stack<string> braceStack = new Stack<string>();
                while (tempToken.EndsWith(")")) {
                    tempToken = tempToken.Substring(0, tempToken.Length-1);
                    braceStack.Push(")");
                }
                tokens.Add(tempToken);
                while (braceStack.Count > 0) {
                    tokens.Add(braceStack.Pop());
                }
            }

            return tokens;
        }
        
        private static List<string> ParseExpressionForPart2(string line) {
            var tokens = ParseExpressionForPart1(line).ToList();
            Console.WriteLine(string.Join("", tokens));
            for (var j = 0; j < tokens.Count; j++) {
                if (tokens[j] == "+") {
                    int bracesCount = 0;
                    for (var i = j+1; i < tokens.Count; i++) {
                        if (tokens[i] == "(")
                            bracesCount++;
                        if (tokens[i] == ")")
                            bracesCount--;
                        if (bracesCount <= 0 && (int.TryParse(tokens[i], out int _) || tokens[i] == ")")) {
                            tokens.Insert(i+1, ")");
                            break;
                        }
                    }
                    bracesCount = 0;
                    for (var i = j-1; i >= 0; i--) {
                        if (tokens[i] == ")")
                            bracesCount++;
                        if (tokens[i] == "(")
                            bracesCount--;
                        if (bracesCount <= 0 && (int.TryParse(tokens[i], out int _) || tokens[i] == "(")) {
                            tokens.Insert(i, "(");
                            break;
                        }
                    }

                    j += 2;
                }
            }
            
            
            return tokens;
        }
    }
}