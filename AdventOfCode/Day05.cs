namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string[] _input;

    public Day05()
    {
        // _input is not lines today, its "blocks"
        _input = File.ReadAllText(InputFilePath).Split(Environment.NewLine + Environment.NewLine); // Why does windows need to use '\r\n' instead of just '\n' like everyone else?
    }

    public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
    public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

    private long Solve_1_First_Approach()
    {
        // !! _input is not lines today, its "blocks" !!

        var results = new List<long>();

        var seeds = _input[0].Split(' ').Skip(1).Select(long.Parse).ToList();

        foreach (long seed in seeds)
        {
            var target = seed;
            foreach (string block in _input.Skip(1))
            {
                var map = ParseMap(block);

                foreach (var (src, dest) in map)
                {
                    if (Intersects(target, src))
                    {
                        target = dest.From + (target - src.From);
                        break;
                    }
                }
            }
            results.Add(target);
        }

        return results.Min();
    }

    private long Solve_2_First_Approach()
    {
        // !! _input is not lines today, its "blocks" !!

        var results = new List<long>();

        var seeds = _input[0].Split(' ').Skip(1).Select(long.Parse).Chunk(2).Select(x => new Range(x.First(), x.First() + x.Last())).ToList();

        throw new NotImplementedException(); // I'm not smart enough to solve this right now.

        return results.Min();
    }

    private static Dictionary<Range, Range> ParseMap(string input)
    {
        Dictionary<Range, Range> result = new();

        foreach (string line in input.Split(Environment.NewLine).Skip(1)) // Skip the header of the map
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(' ').Select(long.Parse).ToArray();
            Range src = new(parts[1], parts[1] + parts[2]);
            Range dest = new(parts[0], parts[0] + parts[2]);
            result.Add(src, dest);
        }

        return result;
    }

    bool Intersects(long l, Range r) => r.From <= l && l <= r.To;

    record Range(long From, long To);
}
