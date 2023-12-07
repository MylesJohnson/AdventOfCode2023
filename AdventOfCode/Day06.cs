namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _input;

    public Day06()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{Solve_1_Equation()}");
    public override ValueTask<string> Solve_2() => new($"{Solve_2_Equation()}");

    private int Solve_1_First_Approach()
    {
        var times = _input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        var recordDistances = _input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();

        var result = 1;

        foreach (var (time, recordDistance) in times.Zip(recordDistances))
        {
            var subResult = 0;

            for (var i = 0; i < time; i++)
            {
                var distance = (time - i) * i;
                if (distance >= recordDistance)
                {
                    subResult++;
                }
                else if (subResult > 0)
                {
                    break; // We can return early, we know once the distance is less than the record distance, it will always be less
                }
            }

            result *= subResult;
        }

        return result;
    }

    private int Solve_1_Equation()
    {
        var times = _input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        var recordDistances = _input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();

        var result = 1;

        foreach (var (time, recordDistance) in times.Zip(recordDistances))
        {       
            result *= SolveEquationMethod(time, recordDistance);
        }

        return result;
    }

    private int Solve_2_First_Approach()
    {
        var time = int.Parse(_input[0].Replace(" ", string.Empty).Split(':')[1]);
        var recordDistance = long.Parse(_input[1].Replace(" ", string.Empty).Split(':')[1]);

        var result = 0;

        for (var i = 0; i < time; i++)
        {
            var distance = (time - i) * i;
            if (distance >= recordDistance)
            {
                result++;
            }
            else if (result > 0)
            {
                break; // We can return early, we know once the distance is less than the record distance, it will always be less
            }
        }

        return result;
    }

    private int Solve_2_Equation()
    {
        var time = int.Parse(_input[0].Replace(" ", string.Empty).Split(':')[1]);
        var recordDistance = long.Parse(_input[1].Replace(" ", string.Empty).Split(':')[1]);

        return SolveEquationMethod(time, recordDistance);
    }

    internal static int SolveEquationMethod(int inputTime, long inputDistance)
    {
        // distance = (inputTime - x) * x
        // solving for x
        // x = (inputTime +- sqrt(inputTime^2 - 4(distance)))/2

        // If inputTime is large (like part 2), then we can rewrite
        // x = (inputTime +- sqrt(inputTime - 2(sqrt(inputDistance))) * sqrt(inputTime + 2(sqrt(inputDistance))))/2

        //var high = Math.Floor((inputTime + Math.Sqrt(inputTime * inputTime - 4 * inputDistance)) / 2);
        //var low = Math.Ceiling((inputTime - Math.Sqrt(inputTime * inputTime - 4 * inputDistance)) / 2);

        var high = Math.Floor((inputTime + Math.Sqrt(inputTime - 2 * Math.Sqrt(inputDistance)) * Math.Sqrt(inputTime + 2 * Math.Sqrt(inputDistance))) / 2);
        var low = Math.Ceiling((inputTime - Math.Sqrt(inputTime - 2 * Math.Sqrt(inputDistance)) * Math.Sqrt(inputTime + 2 * Math.Sqrt(inputDistance))) / 2);
        return (int)(high - low) + 1;
    }
}
