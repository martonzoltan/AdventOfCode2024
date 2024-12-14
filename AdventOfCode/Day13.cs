using Core;

namespace AdventOfCode;

public sealed class Day13 : BaseDay
{
    private readonly List<Machine> _machines = [];

    public Day13()
    {
        var input = InputHelper.GetInput(InputFilePath).ToList();
        for (var i = 0; i < input.Count; i++)
        {
            var line = input[i];
            if (!line.StartsWith("Button A")) continue;
            var machine = new Machine();
            var buttonACoordinates = line.Split(["X+", "Y+"], StringSplitOptions.TrimEntries);
            machine.ButtonA = (int.Parse(buttonACoordinates[1].Trim(',')), int.Parse(buttonACoordinates[2]));
            var buttonBLine = input[++i];
            var buttonBCoordinates = buttonBLine.Split(["X+", "Y+"], StringSplitOptions.TrimEntries);
            machine.ButtonB = (int.Parse(buttonBCoordinates[1].Trim(',')), int.Parse(buttonBCoordinates[2]));
            var prizeLine = input[++i];
            var prizeCoordinates = prizeLine.Split(["X=", "Y="], StringSplitOptions.TrimEntries);
            machine.Prize = (long.Parse(prizeCoordinates[1].Trim(',')), long.Parse(prizeCoordinates[2]));
            _machines.Add(machine);
        }
    }

    public override ValueTask<string> Solve_1()
    {
        foreach (var machine in _machines)
        {
            FindButtonCombination(machine);
        }

        var totalPrice = _machines.Where(x => x.MinPrice != long.MaxValue).Sum(x => x.MinPrice);
        return new ValueTask<string>($"{totalPrice}");
    }

    public override ValueTask<string> Solve_2()
    {
        const long newPrizeLocationOffset = 10_000_000_000_000;
        foreach (var machine in _machines)
        {
            machine.MinPrice = long.MaxValue;
            machine.Prize = (machine.Prize.X + newPrizeLocationOffset, machine.Prize.Y + newPrizeLocationOffset);
            FindButtonCombinationPart2(machine);
        }

        var totalPrice = _machines.Where(x => x.MinPrice != long.MaxValue).Sum(x => x.MinPrice);
        return new ValueTask<string>($"{totalPrice}");
    }

    private static void FindButtonCombination(Machine machine)
    {
        const int maxPresses = 100;
        for (var pressesA = 0; pressesA < maxPresses; pressesA++)
        {
            for (var pressesB = 0; pressesB < maxPresses; pressesB++)
            {
                var currentX = pressesA * machine.ButtonA.X + pressesB * machine.ButtonB.X;
                var currentY = pressesA * machine.ButtonA.Y + pressesB * machine.ButtonB.Y;

                if (currentX == machine.Prize.X && currentY == machine.Prize.Y)
                {
                    var cost = pressesA * 3 + pressesB;
                    if (cost < machine.MinPrice)
                    {
                        machine.MinPrice = cost;
                    }
                }

                if (currentX > machine.Prize.X || currentY > machine.Prize.Y)
                {
                    break;
                }
            }
        }
    }

    private static void FindButtonCombinationPart2(Machine machine)
    {
        long determinant = machine.ButtonA.X * machine.ButtonB.Y - machine.ButtonB.X * machine.ButtonA.Y;

        // Calculate the determinants for A and B
        var determinantA = machine.Prize.X * machine.ButtonB.Y - machine.Prize.Y * machine.ButtonB.X;
        var determinantB = machine.ButtonA.X * machine.Prize.Y - machine.ButtonA.Y * machine.Prize.X;

        // Calculate the number of presses for A and B
        var pressesA = (double) determinantA / determinant;
        var pressesB = (double) determinantB / determinant;

        // Check if the solutions are non-negative integers
        if (pressesA >= 0 && pressesB >= 0 && IsInteger(pressesA) && IsInteger(pressesB))
        {
            var cost = (long) pressesA * 3 + (long) pressesB;
            if (cost < machine.MinPrice)
            {
                machine.MinPrice = cost;
            }
        }
    }

    private static bool IsInteger(double value)
    {
        return Math.Abs(value - Math.Round(value)) < 1e-9;
    }

    private class Machine
    {
        public (int X, int Y) ButtonA { get; set; }
        public (int X, int Y) ButtonB { get; set; }
        public (long X, long Y) Prize { get; set; }
        public long MinPrice { get; set; } = long.MaxValue;
    }
}