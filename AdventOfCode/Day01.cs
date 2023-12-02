using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{Solve_1_Second_Approach()}");
    public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

    private int Solve_1_First_Approach()
    {
        var result = 0;

        foreach (string line in _input)
        {
            char first = default;
            char last = default;
            foreach (char c in line)
            {
                if (char.IsDigit(c))
                {
                    if (first == default)
                    {
                        first = c;
                    }
                    last = c;
                }
            }
            result += int.Parse($"{first}{last}");
        }

        return result;
    }

    private int Solve_1_Second_Approach()
    {
        var result = 0;

        foreach (string line in _input)
        {
            int first = default;
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    first = line[i] - '0'; // this is faster than using int.Parse
                    break;
                }
            }

            // its faster to start from the end and work backwards rather than keep looking from above
            int last = default;
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    last = line[i] - '0'; // this is faster than using int.Parse
                    break;
                }
            }

            // this is faster than using string interpolation
            result += first * 10 + last;
        }

        return result;
    }

    private int Solve_2_First_Approach()
    {
        Dictionary<string, int> digits = new Dictionary<string, int>() {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        int findFirst(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    return line[i] - '0';
                }

                for (int j = i + 3; j < Math.Min(i + 6, line.Length); j++) // shortest word is 3 chars, longest is 5
                {
                    if (digits.TryGetValue(line[i..j], out var result))
                    {
                        return result;
                    }
                }
            }

            throw new SolvingException();
        }

        int findLast(string line)
        {
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    return line[i] - '0';
                }

                for (int j = Math.Max(0, i - 2); j >= Math.Max(0, i - 5); j--)
                {
                    if (digits.TryGetValue(line[j..(i + 1)], out var result))
                    {
                        return result;
                    }
                }
            }

            throw new SolvingException();
        }

        var result = 0;

        foreach (string line in _input)
        {
            int first = findFirst(line);
            int last = findLast(line);

            result += first * 10 + last;
        }

        return result;
    }

    private int Solve_2_regex()
    {
        static int ParseMatch(string st) => st switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            var d => int.Parse(d)
        };

        var result = 0;

        foreach (string line in _input)
        {
            var firstMatch = Regex.Match(line, @"\d|one|two|three|four|five|six|seven|eight|nine");
            var lastSecond = Regex.Match(line, @"\d|one|two|three|four|five|six|seven|eight|nine", RegexOptions.RightToLeft);

            result += int.Parse($"{ParseMatch(firstMatch.Value)}{ParseMatch(lastSecond.Value)}");
        }

        return result;
    }
}
