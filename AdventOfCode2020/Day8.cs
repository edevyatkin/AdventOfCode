using System;
using System.IO;

namespace AdventOfCode2020
{
    public class Day8
    {
        public static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines("Day8_input");
            //int accumulator = 0;
            bool[] visited = new bool[instructions.Length];
            //int currentInst = 0;

            static (string Operation, int Argument) ParseInstruction(string instruction)
            {
                var split = instruction.Split(' ');
                return (split[0], int.Parse(split[1]));
            }

            int Traverse1(int currentInst, int accumulator)
            {
                if (visited[currentInst])
                    return accumulator;
                visited[currentInst] = true;
                var inst = ParseInstruction(instructions[currentInst]);
                switch (inst.Operation)
                {
                    case "acc":
                        return Traverse1(currentInst + 1, accumulator + inst.Argument);
                    case "jmp":
                        return Traverse1(currentInst + inst.Argument, accumulator);
                    case "nop":
                        return Traverse1(currentInst + 1, accumulator);
                }
                return -1;
            }
            
            // while (!visited[currentInst])
            // {
            //     visited[currentInst] = true;
            //     var inst = ParseInstruction(instructions[currentInst]);
            //     switch (inst.Operation)
            //     {
            //         case "acc":
            //             accumulator += inst.Argument;
            //             currentInst++;
            //             break;
            //         case "jmp":
            //             currentInst += inst.Argument;
            //             break;
            //         case "nop":
            //             currentInst++;
            //             break;
            //     }
            // }
            
            int Traverse2(int currentInst, int accumulator, bool changed)
            {
                if (currentInst >= instructions.Length)
                    return accumulator;
                if (visited[currentInst])
                    return -1;
                visited[currentInst] = true;
                var inst = ParseInstruction(instructions[currentInst]);
                switch (inst.Operation)
                {
                    case "acc":
                        return Traverse2(currentInst + 1, accumulator + inst.Argument, changed);
                    case "jmp":
                        if (!changed)
                            return Math.Max(
                                Traverse2(currentInst + inst.Argument, accumulator, changed),
                                Traverse2(currentInst + 1, accumulator, true)
                            );
                        return Traverse2(currentInst + inst.Argument, accumulator, changed);
                    case "nop":
                        if (!changed)
                            return Math.Max(
                                Traverse2(currentInst + 1, accumulator, changed),
                                Traverse2(currentInst + inst.Argument, accumulator, true)
                            );                            
                        return Traverse2(currentInst + 1, accumulator, changed);
                }
                return -1;
            }
            Console.WriteLine($"Day 8 part 1: {Traverse1(0, 0)}");
            
            visited = new bool[instructions.Length];
            
            Console.WriteLine($"Day 8 part 2: {Traverse2(0, 0, false)}");

        }
    }
}