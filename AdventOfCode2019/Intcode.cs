namespace AdventOfCode2019;

public class Intcode
{
    public readonly Dictionary<long, long> ProgramMemory = new();
    private long _relativeBase = 0;

    public Intcode(int[] instructions)
    {
        for (var index = 0; index < instructions.Length; index++)
            ProgramMemory[index] = instructions[index];
    }
    
    public List<long> Execute(params int[] systemIds)
    {
        var i = 0L;
        var systemIdIndex = 0;
        var output = new List<long>();
        while (true)
        {
            var (modes, opCode) = ParseInstruction(ProgramMemory[i]);
            if (opCode == 1)
            {
                var ix = GetDataIndex(i, 3, modes);
                ProgramMemory[ix[2]] = ProgramMemory.GetValueOrDefault(ix[0]) + ProgramMemory.GetValueOrDefault(ix[1]);
                i += 4;
            } 
            else if (opCode == 2)
            {
                var ix = GetDataIndex(i, 3, modes);
                ProgramMemory[ix[2]] = ProgramMemory.GetValueOrDefault(ix[0]) * ProgramMemory.GetValueOrDefault(ix[1]);
                i += 4;
            }
            else if (opCode == 3)
            {
                var ix = GetDataIndex(i, 1, modes);
                ProgramMemory[ix[0]] = systemIds[systemIdIndex++];
                i += 2;
            }
            else if (opCode == 4)
            {
                var ix = GetDataIndex(i, 1, modes);
                output.Add(ProgramMemory.GetValueOrDefault(ix[0]));
                i += 2;
            }
            else if (opCode == 5)
            {
                var ix = GetDataIndex(i, 2, modes);
                if (ProgramMemory.GetValueOrDefault(ix[0]) != 0)
                    i = ProgramMemory.GetValueOrDefault(ix[1]);
                else
                    i += 3;
            }
            else if (opCode == 6)
            {
                var ix = GetDataIndex(i, 2, modes);
                if (ProgramMemory.GetValueOrDefault(ix[0]) == 0)
                    i = ProgramMemory.GetValueOrDefault(ix[1]);
                else
                    i += 3;
            }
            else if (opCode == 7)
            {
                var ix = GetDataIndex(i, 3, modes);
                ProgramMemory[ix[2]] = ProgramMemory.GetValueOrDefault(ix[0]) < ProgramMemory.GetValueOrDefault(ix[1]) ? 1 : 0;
                i += 4;
            }
            else if (opCode == 8)
            {
                var ix = GetDataIndex(i, 3, modes);
                ProgramMemory[ix[2]] = ProgramMemory.GetValueOrDefault(ix[0]) == ProgramMemory.GetValueOrDefault(ix[1]) ? 1 : 0;
                i += 4;
            }
            else if (opCode == 9)
            {
                var ix = GetDataIndex(i, 1, modes);
                _relativeBase += ProgramMemory.GetValueOrDefault(ix[0]);
                i += 2;
            }
            else if (opCode == 99)
            {
                break;
            }
        }
        return output;
    }
    
    private List<long> GetDataIndex(long index, int operandsCount, int[] modes)
    {
        var result = new List<long>();
        for (var op = 0; op < operandsCount; op++)
        {
            var nextIndex = modes[op] switch {
                0 => ProgramMemory[index+op+1],
                1 => index+op+1,
                2 => ProgramMemory[index+op+1] + _relativeBase
            };
            result.Add(nextIndex);
        }
        return result;
    }

    private static (int[] modes, int opCode) ParseInstruction(long instruction)
    {
        instruction = Math.DivRem(instruction, 100, out var oc);
        instruction = Math.DivRem(instruction, 10, out var fm);
        instruction = Math.DivRem(instruction, 10, out var sm);
        Math.DivRem(instruction, 10, out var tm);
        return ([(int)fm, (int)sm, (int)tm], (int)oc);
    }
}
