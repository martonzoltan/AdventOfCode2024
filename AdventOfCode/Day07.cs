using Core;

namespace AdventOfCode;

public sealed class Day07 : BaseDay
{
    private readonly List<(long result, List<long> numbers)> _equations = [];

    public Day07()
    {
        var input = InputHelper.GetInput(InputFilePath);
        foreach (var equation in input)
        {
            var equationParts = equation.Split(":", StringSplitOptions.RemoveEmptyEntries);
            var result = long.Parse(equationParts[0]);
            var numbers = equationParts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            _equations.Add((result, numbers));
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var totalSum = _equations.Where(equation => CanCompute(equation.numbers, equation.result))
            .Sum(equation => equation.result);
        return new ValueTask<string>($"{totalSum}");
    }

    public override ValueTask<string> Solve_2()
    {
        var totalSum = _equations.Where(equation => CanCompute(equation.numbers, equation.result, true))
            .Sum(equation => equation.result);
        return new ValueTask<string>($"{totalSum}");
    }

    private static bool CanCompute(List<long> numbers, long target, bool useOrOperator = false)
    {
        return Backtrack(numbers, target, numbers[0], 1, useOrOperator);
    }

    private static bool Backtrack(List<long> numbers, long target, long current, int index, bool useOrOperator = false)
    {
        if (index == numbers.Count)
        {
            return current == target;
        }

        return Backtrack(numbers, target, current + numbers[index], index + 1, useOrOperator) ||
               Backtrack(numbers, target, current * numbers[index], index + 1, useOrOperator) ||
               (useOrOperator && Backtrack(numbers, target, Convert.ToInt64(current + numbers[index].ToString()),
                   index + 1, true));
    }
}