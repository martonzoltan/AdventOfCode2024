using Core;

namespace AdventOfCode;

public sealed class Day09 : BaseDay
{
    private readonly List<int> _storage = [];
    private int _startingPosition;

    public Day09()
    {
    }

    private void InitStorage()
    {
        var input = InputHelper.GetInput(InputFilePath).FirstOrDefault();
        _storage.Clear();
        var storage = input!.Select(x => int.Parse(x.ToString())).ToList();
        var index = 0;
        for (var i = 0; i < storage.Count - 1; i += 2)
        {
            if (index == 0)
            {
                _startingPosition = storage[i];
            }

            _storage.AddRange(Enumerable.Repeat(index, storage[i]));
            var totalEmptySpace = storage[i + 1];
            _storage.AddRange(Enumerable.Repeat(0, totalEmptySpace));
            index++;
        }

        _storage.AddRange(Enumerable.Repeat(index, storage[^1]));
    }

    public override ValueTask<string> Solve_1()
    {
        InitStorage();
        for (var i = _storage.Count - 1; i > 1; i--)
        {
            if (_storage[i] != 0)
            {
                AddValueAtFirstEmpty(_storage[i]);
                _storage[i] = 0;
            }

            var emptyStorage = false;
            for (var j = _startingPosition; j < i; j++)
            {
                if (_storage[j] != 0) continue;
                emptyStorage = true;
                break;
            }

            if (!emptyStorage)
            {
                break;
            }
        }

        return new ValueTask<string>($"{CalculateCheckSum()}");
    }

    public override ValueTask<string> Solve_2()
    {
        InitStorage();
        var i = _storage.Count - 1;
        while (true)
        {
            if (_storage[i] != 0)
            {
                var lastFileLength = GetContinuousSameValuesAtEnd(i);
                MoveFileToFirstEmptySpace(i, lastFileLength);
                i -= lastFileLength;
            }
            else
            {
                i--;
            }

            var emptyStorage = false;
            for (var j = _startingPosition; j < i; j++)
            {
                if (_storage[j] != 0) continue;
                emptyStorage = true;
                break;
            }

            if (!emptyStorage)
            {
                break;
            }
        }

        return new ValueTask<string>($"{CalculateCheckSum()}");
    }

    private void AddValueAtFirstEmpty(int value)
    {
        for (var i = _startingPosition; i < _storage.Count; i++)
        {
            if (_storage[i] != 0) continue;
            _storage[i] = value;
            break;
        }
    }

    private void MoveFileToFirstEmptySpace(int index, int lastFileLength)
    {
        var emptyCounter = 0;
        var indexWhereItEnds = 0;
        for (var i = _startingPosition; i < index; i++)
        {
            if (_storage[i] != 0)
            {
                emptyCounter = 0;
                continue;
            }

            if (emptyCounter + 1 == lastFileLength)
            {
                indexWhereItEnds = i;
                break;
            }

            emptyCounter++;
        }

        if (indexWhereItEnds == 0) return;
        for (var i = index; i > index - lastFileLength; i--)
        {
            _storage[indexWhereItEnds] = _storage[i];
            indexWhereItEnds--;
            _storage[i] = 0;
        }
    }

    private int GetContinuousSameValuesAtEnd(int index)
    {
        var count = 0;
        var lastValue = _storage[index];

        for (var i = index; i >= 0; i--)
        {
            if (_storage[i] == 0 || _storage[i] != lastValue)
            {
                break;
            }

            count++;
        }

        return count;
    }

    private long CalculateCheckSum()
    {
        long checkSum = 0;
        for (var i = 0; i < _storage.Count; i++)
        {
            checkSum += _storage[i] * i;
        }

        return checkSum;
    }
}