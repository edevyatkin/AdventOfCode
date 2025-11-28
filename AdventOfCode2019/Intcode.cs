namespace AdventOfCode2019;

public class Intcode
{
    public readonly Dictionary<long, long> ProgramMemory = new();
    private long _relativeBase = 0;
    public IntcodeState State { get; private set; } = IntcodeState.NotRunning;
    private readonly List<long> _output = [];
    private long _currentInstruction = 0;

    public Intcode(long[] instructions)
    {
        for (var index = 0; index < instructions.Length; index++)
            ProgramMemory[index] = instructions[index];
    }
    
    public Intcode(int[] instructions) : this(instructions.Select(n => (long)n).ToArray()) { }

    public Intcode(string[] input) : this(input[0].Split(',').Select(long.Parse).ToArray()) { }
    
    public List<long> Execute(params long[] systemIds)
    {
        if (State == IntcodeState.Completed)
            return _output;
        var systemIdIndex = 0;
        while (true)
        {
            var (modes, opCode) = ParseInstruction(ProgramMemory[_currentInstruction]);
            if (opCode == 1)
            {
                var ix = GetDataIndex(_currentInstruction, 3, modes);
                ProgramMemory[ix[2]] = ProgramMemory.GetValueOrDefault(ix[0]) + ProgramMemory.GetValueOrDefault(ix[1]);
                _currentInstruction += 4;
            } 
            else if (opCode == 2)
            {
                var ix = GetDataIndex(_currentInstruction, 3, modes);
                ProgramMemory[ix[2]] = ProgramMemory.GetValueOrDefault(ix[0]) * ProgramMemory.GetValueOrDefault(ix[1]);
                _currentInstruction += 4;
            }
            else if (opCode == 3)
            {
                if (State == IntcodeState.NotRunning)
                    State = IntcodeState.Running;
                else if (State == IntcodeState.Running && systemIdIndex == systemIds.Length)
                    break;
                var ix = GetDataIndex(_currentInstruction, 1, modes);
                ProgramMemory[ix[0]] = systemIds[systemIdIndex++];
                _currentInstruction += 2;
            }
            else if (opCode == 4)
            {
                var ix = GetDataIndex(_currentInstruction, 1, modes);
                _output.Add(ProgramMemory.GetValueOrDefault(ix[0]));
                _currentInstruction += 2;
            }
            else if (opCode == 5)
            {
                var ix = GetDataIndex(_currentInstruction, 2, modes);
                if (ProgramMemory.GetValueOrDefault(ix[0]) != 0)
                    _currentInstruction = ProgramMemory.GetValueOrDefault(ix[1]);
                else
                    _currentInstruction += 3;
            }
            else if (opCode == 6)
            {
                var ix = GetDataIndex(_currentInstruction, 2, modes);
                if (ProgramMemory.GetValueOrDefault(ix[0]) == 0)
                    _currentInstruction = ProgramMemory.GetValueOrDefault(ix[1]);
                else
                    _currentInstruction += 3;
            }
            else if (opCode == 7)
            {
                var ix = GetDataIndex(_currentInstruction, 3, modes);
                ProgramMemory[ix[2]] = ProgramMemory.GetValueOrDefault(ix[0]) < ProgramMemory.GetValueOrDefault(ix[1]) ? 1 : 0;
                _currentInstruction += 4;
            }
            else if (opCode == 8)
            {
                var ix = GetDataIndex(_currentInstruction, 3, modes);
                ProgramMemory[ix[2]] = ProgramMemory.GetValueOrDefault(ix[0]) == ProgramMemory.GetValueOrDefault(ix[1]) ? 1 : 0;
                _currentInstruction += 4;
            }
            else if (opCode == 9)
            {
                var ix = GetDataIndex(_currentInstruction, 1, modes);
                _relativeBase += ProgramMemory.GetValueOrDefault(ix[0]);
                _currentInstruction += 2;
            }
            else if (opCode == 99)
            {
                State = IntcodeState.Completed;
                _currentInstruction = 0;
                break;
            }
        }
        return _output;
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

public enum IntcodeState
{
    NotRunning, 
    Running,
    Completed
}
