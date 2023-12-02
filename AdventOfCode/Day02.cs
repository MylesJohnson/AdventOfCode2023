using Spectre.Console;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
    public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

    private int Solve_1_First_Approach()
    {
        var reRed = new Regex(@"(\d+) red");
        var reGreen = new Regex(@"(\d+) green");
        var reBlue = new Regex(@"(\d+) blue");

        var result = 0;

        Dictionary<int, Tuple<int, int, int>> games = new();

        foreach (string line in _input)
        {
            var split = line.Split(": ");
            int gameNumber = int.Parse(split[0].Split(' ')[1]);
            var rounds = split[1];

            var red = reRed.Matches(rounds).Select(m => int.Parse(m.Groups[1].Value)).Max();
            var green = reGreen.Matches(rounds).Select(m => int.Parse(m.Groups[1].Value)).Max();
            var blue = reBlue.Matches(rounds).Select(m => int.Parse(m.Groups[1].Value)).Max();
            
            games.Add(gameNumber, new Tuple<int, int, int>(red, green, blue));
        }

        foreach(var game in games)
        {
            var gameNumber = game.Key;
            var (red, green, blue) = game.Value;

            if (red <= 12 && green <= 13 && blue <= 14)
                result += gameNumber;
        }

        return result;
    }

    private int Solve_2_First_Approach()
    {
        var reRed = new Regex(@"(\d+) red");
        var reGreen = new Regex(@"(\d+) green");
        var reBlue = new Regex(@"(\d+) blue");

        var result = 0;

        Dictionary<int, Tuple<int, int, int>> games = new();

        foreach (string line in _input)
        {
            var split = line.Split(": ");
            int gameNumber = int.Parse(split[0].Split(' ')[1]);
            var rounds = split[1];

            var red = reRed.Matches(rounds).Select(m => int.Parse(m.Groups[1].Value)).Max();
            var green = reGreen.Matches(rounds).Select(m => int.Parse(m.Groups[1].Value)).Max();
            var blue = reBlue.Matches(rounds).Select(m => int.Parse(m.Groups[1].Value)).Max();

            games.Add(gameNumber, new Tuple<int, int, int>(red, green, blue));
        }

        foreach (var game in games)
        {
            var (red, green, blue) = game.Value;

            result += red * green * blue;
        }
        
        return result;
    }
}
