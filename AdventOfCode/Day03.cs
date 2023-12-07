using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;

    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
    public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

    private int Solve_1_First_Approach()
    {
        int result = 0;

        // Lets build a set of "cordinates" of all symbols
        var symbols = new HashSet<(int, int)>();
        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (_input[i][j] != '.' && !char.IsDigit(_input[i][j]))
                {
                    symbols.Add((i, j));
                }
            }
        }

        var re = new Regex(@"\d+");

        for (int i = 0; i < _input.Length; i++)
        {
            var matches = re.Matches(_input[i]);
            foreach (Match match in matches)
            {
                var num = int.Parse(match.Value);
                for (int j = match.Index - 1; j < match.Index + match.Length + 1; j++)
                {
                    if (symbols.Contains((i - 1, j)) || symbols.Contains((i, j)) || symbols.Contains((i + 1, j)))
                    {
                        result += num;
                        break;
                    }
                }
            }
        }

        return result;
    }

    private int Solve_2_First_Approach()
    {
        int result = 0;

        var gears = new HashSet<(int, int)>();
        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (_input[i][j] == '*')
                {
                    gears.Add((i, j));
                }
            }
        }

        Dictionary<(int, int), List<int>> gearsMap = new(); // gear location -> list of adjacent numbers

        var re = new Regex(@"\d+");

        for (int i = 0; i < _input.Length; i++)
        {
            var matches = re.Matches(_input[i]);
            foreach (Match match in matches)
            {
                var num = int.Parse(match.Value);
                for (int j = match.Index - 1; j < match.Index + match.Length + 1; j++)
                {
                    if (gears.Contains((i - 1, j)))
                    {
                        if (gearsMap.ContainsKey((i - 1, j)))
                        {
                            gearsMap[(i - 1, j)].Add(num);
                        }
                        else
                        {
                            gearsMap.Add((i - 1, j), new List<int> { num });
                        }
                        break;
                    }
                    else if (gears.Contains((i, j)))
                    {
                        if (gearsMap.ContainsKey((i, j)))
                        {
                            gearsMap[(i, j)].Add(num);
                        }
                        else
                        {
                            gearsMap.Add((i, j), new List<int> { num });
                        }
                        break;
                    } 
                    else if (gears.Contains((i + 1, j)))
                    {
                        if (gearsMap.ContainsKey((i + 1, j)))
                        {
                            gearsMap[(i + 1, j)].Add(num);
                        }
                        else
                        {
                            gearsMap.Add((i + 1, j), new List<int> { num });
                        }
                        break;
                    }
                }
            }
        }

        foreach ((var gear, var nums) in gearsMap)
        {
            if (nums.Count != 2)
            {
                continue;
            }

            result += nums[0] * nums[1];
        }

        return result;
    }
}
