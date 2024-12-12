using Core;

namespace AdventOfCode;

public sealed class Day11 : BaseDay
{
    private List<long> _meteors = [];
    private readonly Dictionary<long, long> _cache = new();

    public Day11()
    {
    }

    private void GetInput()
    {
        _meteors.Clear();
        _cache.Clear();
        var input = InputHelper.GetInput(InputFilePath).FirstOrDefault();
        _meteors = input!.Split(" ").Select(x => long.Parse(x.ToString())).ToList();
        foreach (var stone in _meteors)
        {
            var count = _cache.GetValueOrDefault(stone, 0);
            _cache[stone] = count + 1;
        }
    }

    public override ValueTask<string> Solve_1()
    {
        GetInput();
        var counter = RunBlink(25);
        return new ValueTask<string>($"{counter}");
    }

    public override ValueTask<string> Solve_2()
    {
        GetInput();
        var counter = RunBlink(75);
        return new ValueTask<string>($"{counter}");
    }

    private long RunBlink(int totalBlinks)
    {
        long counter = 0;
        for (var i = 0; i < totalBlinks; i++)
        {
            var list = _cache.ToList();
            _cache.Clear();
            counter = 0;
            foreach (var (meteor, count) in list)
            {
                if (meteor == 0)
                {
                    AddToCache(1, count);
                    counter += count;
                    continue;
                }

                if (Math.Floor(Math.Log10(meteor) + 1) % 2 == 0)
                {
                    var numDigits = (int) Math.Floor(Math.Log10(meteor) + 1);
                    var halfDigits = numDigits / 2;
                    var divisor = (long) Math.Pow(10, halfDigits);

                    var firstHalf = meteor / divisor;
                    var secondHalf = meteor % divisor;

                    AddToCache(firstHalf, count);
                    AddToCache(secondHalf, count);
                    counter += count * 2;
                }
                else
                {
                    AddToCache(meteor * 2024, count);
                    counter += count;
                }
            }
        }

        return counter;
    }

    private void AddToCache(long number, long newToAdd)
    {
        var count = _cache.GetValueOrDefault(number, 0);
        _cache[number] = count + newToAdd;
    }
}