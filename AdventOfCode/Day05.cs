using Core;

namespace AdventOfCode;

public sealed class Day05 : BaseDay
{
    private readonly List<(int before, int after)> _beforeAfterPairs;
    private readonly List<List<int>> _updates;

    public Day05()
    {
        IEnumerable<string> input = InputHelper.GetInput(InputFilePath);
        var parts = string.Join("\n", input).Split(["\n\n"], StringSplitOptions.None);

        _beforeAfterPairs = parts[0].Split('\n')
            .Select(line => line.Split('|'))
            .Select(split => (int.Parse(split[0]), int.Parse(split[1])))
            .ToList();

        _updates = parts[1].Split('\n')
            .Select(line => line.Split(',').Select(int.Parse).ToList())
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 0;
        foreach (var update in _updates)
        {
            var badOne = false;
            for (var i = 0; i < update.Count; i++)
            {
                if (CheckRestOfItems(i, update)) continue;
                badOne = true;
                break;
            }

            if (badOne) continue;
            var middleItem = update[update.Count / 2];
            total += middleItem;
        }

        return new ValueTask<string>($"{total}");
    }

    public override ValueTask<string> Solve_2()
    {
        var total = 0;
        foreach (var update in _updates)
        {
            var badOne = false;
            for (var i = 0; i < update.Count; i++)
            {
                if (CheckRestOfItems(i, update, true)) continue;
                badOne = true;
                break;
            }

            if (badOne) continue;
            var middleItem = update[update.Count / 2];
            total += middleItem;
        }

        return new ValueTask<string>($"{total}");
    }

    private bool CheckRestOfItems(int index, List<int> update, bool fix = false)
    {
        var item = update[index];
        var itemsBeforeIndexed = _beforeAfterPairs.Where(x => x.before == item).Select(x => x.after).ToList();
        for (var before = 0; before < index; before++)
        {
            if (itemsBeforeIndexed.Contains(update[before]))
            {
                if (fix)
                {
                    FixOrder(update[before], update);
                    return true;
                }

                return false;
            }
        }

        var itemsAfterIndexed = _beforeAfterPairs.Where(x => x.after == item).Select(x => x.before).ToList();
        for (var after = index; after < update.Count; after++)
        {
            if (itemsAfterIndexed.Contains(update[after]))
            {
                if (fix)
                {
                    FixOrder(update[after], update);
                    return true;
                }

                return false;
            }
        }

        return true;
    }

    private void FixOrder(int item, List<int> update)
    {
        var index = update.IndexOf(item);
        var itemsBeforeIndexed = _beforeAfterPairs.Where(x => x.before == item).Select(x => x.after).ToList();
        for (var before = 0; before < index; before++)
        {
            if (itemsBeforeIndexed.Contains(update[before]))
            {
                var temp = update[before];
                update[before] = item;
                update[index] = temp;
                return;
            }
        }

        var itemsAfterIndexed = _beforeAfterPairs.Where(x => x.after == item).Select(x => x.before).ToList();
        for (var after = index; after < update.Count; after++)
        {
            if (itemsAfterIndexed.Contains(update[after]))
            {
                var temp = update[after];
                update[after] = item;
                update[index] = temp;
                return;
            }
        }
    }
}