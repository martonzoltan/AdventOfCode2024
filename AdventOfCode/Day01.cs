using Core;

namespace AdventOfCode;

public sealed class Day01 : BaseDay
{
    private readonly List<int> _listA = [];
    private readonly List<int> _listB = [];

    public Day01()
    {
        var input = InputHelper.GetInput(InputFilePath);
        foreach (var line in input)
        {
            var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            _listA.Add(Convert.ToInt32(split[0]));
            _listB.Add(Convert.ToInt32(split[1]));
        }
    }

    public override ValueTask<string> Solve_1()
    {
        _listA.Sort();
        _listB.Sort();
        var totalDifference = _listA.Select((t, i) => Math.Abs(t - _listB[i])).Sum();
        return new ValueTask<string>($"{totalDifference}");
    }

    public override ValueTask<string> Solve_2()
    {
        var totalDifference = 0;
        foreach (var item in _listA)
        {
            var found = _listB.Count(x => x == item);
            totalDifference += item * found;
        }

        return new ValueTask<string>($"{totalDifference}");
    }
}