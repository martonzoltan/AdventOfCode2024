using System.Text.RegularExpressions;
using Core;

namespace AdventOfCode;

public sealed class Day03 : BaseDay
{
    private readonly List<string> _memory;

    public Day03()
    {
        var input = InputHelper.GetInput(InputFilePath);
        _memory = input.ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        const string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        Regex regex = new Regex(pattern);
        var result = 0;

        foreach (var memoryLine in _memory)
        {
            foreach (Match match in regex.Matches(memoryLine))
            {
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                result += x * y;
            }
        }

        return new ValueTask<string>($"{result}");
    }

    public override ValueTask<string> Solve_2()
    {
        const string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        Regex regex = new Regex(pattern);
        var result = 0;

        foreach (var memoryLine in _memory)
        {
            var parts = Regex.Split(memoryLine, @"(?=do\(\)|don't\(\))");
            var partsToDo = parts.Where(x => x.StartsWith("do()")).ToList();
            partsToDo = partsToDo.Prepend(parts[0]).ToList();
            foreach (var partToDo in partsToDo)
            {
                foreach (Match match in regex.Matches(partToDo))
                {
                    var x = int.Parse(match.Groups[1].Value);
                    var y = int.Parse(match.Groups[2].Value);
                    result += x * y;
                }
            }
            
        }

        return new ValueTask<string>($"{result}");
    }
}