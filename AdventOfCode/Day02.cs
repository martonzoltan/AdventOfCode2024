using Core;

namespace AdventOfCode;

public sealed class Day02 : BaseDay
{
    private readonly List<List<int>> _reports = [];

    public Day02()
    {
        var input = InputHelper.GetInput(InputFilePath);
        foreach (var line in input)
        {
            var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            _reports.Add(split.Select(int.Parse).ToList());
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var safeReports = _reports.Count(IsReportSafe);
        return new ValueTask<string>($"{safeReports}");
    }

    public override ValueTask<string> Solve_2()
    {
        var safeReports = 0;
        foreach (var report in _reports)
        {
            if (IsReportSafe(report))
            {
                safeReports++;
            }
            else
            {
                for (var i = 0; i < report.Count; i++)
                {
                    var copy = new List<int>(report);
                    copy.RemoveAt(i);
                    if (!IsReportSafe(copy)) continue;
                    safeReports++;
                    break;
                }
            }
        }

        return new ValueTask<string>($"{safeReports}");
    }

    private static bool IsReportSafe(List<int> levels)
    {
        var isIncreasing = true;
        var isDecreasing = true;

        for (var i = 1; i < levels.Count; i++)
        {
            var diff = levels[i] - levels[i - 1];
            if (diff == 0)
            {
                isIncreasing = false;
                isDecreasing = false;
                break;
            }

            isIncreasing &= diff is > 0 and <= 3;
            isDecreasing &= diff is < 0 and >= -3;
        }

        return isIncreasing || isDecreasing;
    }
}