namespace AdventOfCode2023;

[AocDay(2023,20)]
public class Day20 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input);
        var result2 = SolvePart2(input);

        return new AocDayResult(result1, result2);
    }

    internal static long SolvePart1(string[] input)
    {
        var machines = new Machines(input);
        
        var count = 1000;
        while (count-- > 0)
            machines.PressButton();

        return machines.PulseCounterLow * machines.PulseCounterHigh;
    }

    internal static long SolvePart2(string[] input)
    {
        var machines = new Machines(input);
        var cycles = machines.DetectCyclesForRx();
        return MathExtensions.FindLcm(cycles);
    }

    public class Machines
    {
        private static readonly Dictionary<string, Module> _modulesDict = new();
        private static readonly Queue<(Module Source, Module Dest, Pulse Pulse)> _q = new();
        public long PulseCounterLow { get; private set; } = 0;
        public long PulseCounterHigh { get; private set; } = 0;


        public Machines(string[] input)
        {
            _modulesDict["button"] = new ButtonModule("button");
            foreach (var line in input)
            {
                var sp = line.Split(" -> ");
                InitModule(sp[0]);
            }
            
            foreach (var line in input)
            {
                var sp = line.Split(" -> ");
                var name = char.IsLetter(sp[0][0]) ? sp[0] : sp[0][1..];
                AddWires(name, sp[1]);
            }
        }
        
        void InitModule(string str)
        {
            if (str == "broadcaster")
                _modulesDict[str] = new BroadcasterModule(str);
            else if (str[0] == '%')
                _modulesDict[str[1..]] = new FlipFlopModule(str[1..]);
            else if (str[0] == '&')
                _modulesDict[str[1..]] = new ConjunctionModule(str[1..]);
        }

        private void AddWires(string name, string destsStr)
        {
            var source = _modulesDict[name];
            foreach (var destStr in destsStr.Split(", "))
            {
                if (!_modulesDict.ContainsKey(destStr))
                {
                    _modulesDict[destStr] = new OutputModule(destStr);
                }
                var dest = _modulesDict[destStr];
                source.DestinationModules.Add(dest);
                if (dest is ConjunctionModule conjunctionModule)
                {
                    conjunctionModule.Inputs.TryAdd(source, new Pulse(0));
                }
            }
        }
        
        internal abstract class Module
        {
            public string Name { get; }
            internal List<Module> DestinationModules { get; set; } = new();
            internal abstract void Send(Pulse pulse, Module source);

            internal Module(string name)
            {
                Name = name;
            }

            public override string ToString()
            {
                return $"{Name}";
            }
        }

        class BroadcasterModule : Module
        {
            internal override void Send(Pulse pulse, Module source)
            {
                foreach (var dest in DestinationModules)
                {
                    _q.Enqueue((this, dest, pulse));
                }
            }

            public BroadcasterModule(string name) : base(name) { }
        }

        class ButtonModule : Module
        {
            internal override void Send(Pulse pulse, Module source) { }

            public ButtonModule(string name) : base(name) { }
        }


        class FlipFlopModule : Module
        {
            private int _state = 0; // 0 - off, 1 - on

            internal override void Send(Pulse pulse, Module source)
            {
                if (pulse.Value == 1)
                    return;
                
                foreach (var dest in DestinationModules)
                {
                    _q.Enqueue((this, dest, new Pulse(_state == 0 ? 1 : 0)));
                }

                _state ^= 1;
            }

            public FlipFlopModule(string name) : base(name) { }
        }

        class ConjunctionModule : Module
        {
            internal Dictionary<Module, Pulse> Inputs { get; } = new();
            internal override void Send(Pulse pulse, Module source)
            {
                Inputs[source] = pulse;
                var allOn = Inputs.Values.All(p => p.Value == 1);
                foreach (var dest in DestinationModules)
                {
                    _q.Enqueue((this, dest, new Pulse(allOn ? 0 : 1)));
                }
            }

            public ConjunctionModule(string name) : base(name) { }
        }
        
        class OutputModule : Module
        {
            internal override void Send(Pulse pulse, Module source) { }

            public OutputModule(string name) : base(name) { }
        }

        internal struct Pulse
        {
            public Pulse(int value)
            {
                Value = value;
            }

            internal int Value { get; } = 0;

            public override string ToString()
            {
                return $"{Value}";
            }
        }

        public void PressButton()
        {
            var buttonModule = _modulesDict["button"];
            var broadcasterModule = _modulesDict["broadcaster"];
            _q.Enqueue((buttonModule, broadcasterModule, new Pulse(0)));
            while (_q.Count > 0)
            {
                var (source, dest, pulse) = _q.Dequeue();
                if (pulse.Value == 0)
                    PulseCounterLow++;
                else
                    PulseCounterHigh++;
                dest.Send(pulse, source);
            }
        }


        public int[] DetectCyclesForRx()
        {
            var conj = _modulesDict.Values
                .First(m => m.DestinationModules.Find(dm => dm.Name == "rx") is not null);
            if (conj is not ConjunctionModule cm) 
                return Array.Empty<int>();
            
            var counter = 0;
            var counts = new Dictionary<Module, int>();
            while (true)
            {
                counter++;
                var buttonModule = _modulesDict["button"];
                var broadcasterModule = _modulesDict["broadcaster"];
                _q.Enqueue((buttonModule, broadcasterModule, new Pulse(0)));
                while (_q.Count > 0)
                {
                    var (source, dest, pulse) = _q.Dequeue();
                    if (dest == cm && pulse.Value == 1)
                    {
                        counts.TryAdd(source, counter);
                        if (counts.Count == cm.Inputs.Count && counts.Values.All(v => v > 0))
                            return counts.Values.ToArray();
                    }
                    dest.Send(pulse, source);
                }
            }
        }
    }
}
