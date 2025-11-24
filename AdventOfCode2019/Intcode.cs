namespace AdventOfCode2019;

public static class Intcode
{
    public static List<int> Execute(int[] instructions, params int[] systemIds)
    {
        var i = 0;
        var systemIdIndex = 0;
        var output = new List<int>();
        while (true)
        {
            var (secondMode, firstMode, opCode) = ParseInstruction(instructions[i]);
            if (opCode == 1)
            {
                var operand1 = firstMode == 0 ? instructions[instructions[i + 1]] : instructions[i + 1];
                var operand2 = secondMode == 0 ? instructions[instructions[i + 2]] : instructions[i + 2];
                instructions[instructions[i + 3]] = operand1 + operand2;
                i += 4;
            } 
            else if (opCode == 2)
            {
                var operand1 = firstMode == 0 ? instructions[instructions[i + 1]] : instructions[i + 1];
                var operand2 = secondMode == 0 ? instructions[instructions[i + 2]] : instructions[i + 2];
                instructions[instructions[i + 3]] = operand1 * operand2;
                i += 4;
            }
            else if (opCode == 3)
            {
                instructions[instructions[i + 1]] = systemIds[systemIdIndex++];
                i += 2;
            }
            else if (opCode == 4)
            {
                var operand1 = firstMode == 0 ? instructions[instructions[i + 1]] : instructions[i + 1];
                output.Add(operand1);
                i += 2;
            }
            else if (opCode == 5)
            {
                var operand1 = firstMode == 0 ? instructions[instructions[i + 1]] : instructions[i + 1];
                var operand2 = secondMode == 0 ? instructions[instructions[i + 2]] : instructions[i + 2];
                if (operand1 != 0)
                    i = operand2;
                else
                    i += 3;
            }
            else if (opCode == 6)
            {
                var operand1 = firstMode == 0 ? instructions[instructions[i + 1]] : instructions[i + 1];
                var operand2 = secondMode == 0 ? instructions[instructions[i + 2]] : instructions[i + 2];
                if (operand1 == 0)
                    i = operand2;
                else
                    i += 3;
            }
            else if (opCode == 7)
            {
                var operand1 = firstMode == 0 ? instructions[instructions[i + 1]] : instructions[i + 1];
                var operand2 = secondMode == 0 ? instructions[instructions[i + 2]] : instructions[i + 2];
                instructions[instructions[i + 3]] = operand1 < operand2 ? 1 : 0;
                i += 4;
            }
            else if (opCode == 8)
            {
                var operand1 = firstMode == 0 ? instructions[instructions[i + 1]] : instructions[i + 1];
                var operand2 = secondMode == 0 ? instructions[instructions[i + 2]] : instructions[i + 2];
                instructions[instructions[i + 3]] = operand1 == operand2 ? 1 : 0;
                i += 4;
            }
            else if (opCode == 99)
            {
                break;
            }
        }
        return output;
    }

    private static (int secondMode, int firstMode, int opCode) ParseInstruction(long instruction)
    {
        instruction = Math.DivRem(instruction, 100, out var oc);
        instruction = Math.DivRem(instruction, 10, out var fm);
        Math.DivRem(instruction, 10, out var sm);
        return ((int)sm, (int)fm, (int)oc);
    }
}
