using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{Solve_1_No_Regex()}");
    public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

    private int Solve_1_First_Approach()
    {
        int result = 0;

        var re = new Regex(@"\d+");

        foreach (var line in _input)
        {
            // First number is always the card number (we don't care)
            // Next 10 numbers are the winning numbers
            // The rest (25) are the card numbers
            var matches = re.Matches(line);
            var winningNumbers = matches.Skip(1).Take(10).Select(m => int.Parse(m.Value)).ToHashSet();
            var cardNumbers = matches.Skip(11).Select(m => int.Parse(m.Value)).ToHashSet();

            result += (int)Math.Pow(2, winningNumbers.Intersect(cardNumbers).Count() - 1);
        }

        return result;
    }

    private static (HashSet<int> WinningNumbers, HashSet<int> CardNumbers) SolveCard(string input)
    {
        var card = input.Split(':', 2); // Specifying the number of splits could be faster?
        var splitCard = card[1].Split('|', 2); // Specifying the number of splits could be faster?
        HashSet<int> winningNumbers = splitCard[0].Split(' ', StringSplitOptions.RemoveEmptyEntries ^ StringSplitOptions.TrimEntries).Select(int.Parse).ToHashSet();
        HashSet<int> cardNumbers = splitCard[1].Split(' ', StringSplitOptions.RemoveEmptyEntries ^ StringSplitOptions.TrimEntries).Select(int.Parse).ToHashSet();

        return (winningNumbers, cardNumbers);
    }

    private int Solve_1_No_Regex()
    {
        int result = 0;

        foreach (var line in _input)
        {
            (var winningNumbers, var cardNumbers) = SolveCard(line);

            result += (int)Math.Pow(2, winningNumbers.Intersect(cardNumbers).Count() - 1);
        }

        return result;
    }

    private int Solve_2_First_Approach()
    {
        int[] cardCounts = new int[_input.Length];

        for (int cardIndex = 0; cardIndex < _input.Length; cardIndex++)
        {
            ++cardCounts[cardIndex];

            (var winningNumbers, var cardNumbers) = SolveCard(_input[cardIndex]);

            var cardsWon = winningNumbers.Intersect(cardNumbers).Count();
            for (int i = cardIndex + 1; i <= cardIndex + cardsWon; i++)
            {
                cardCounts[i] += cardCounts[cardIndex];
            }
        }

        return cardCounts.Sum();
    }
}
