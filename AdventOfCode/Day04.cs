using Core;

namespace AdventOfCode;

public sealed class Day04 : BaseDay
{
    private readonly char[,] _matrix;

    public Day04()
    {
        _matrix = InputHelper.GetMatrixInput(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var totalXmas = 0;
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                foreach (var (di, dj) in MatrixHelper.Directions)
                {
                    if (Verify(i, j, di, dj, "XMAS"))
                    {
                        totalXmas++;
                    }
                }
            }
        }

        return new ValueTask<string>($"{totalXmas}");
    }

    public override ValueTask<string> Solve_2()
    {
        var totalMas = 0;
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] != 'A') continue;
                if (VerifyDiagonalMas(i, j))
                {
                    totalMas++;
                }
            }
        }

        return new ValueTask<string>($"{totalMas}");
    }

    private bool Verify(int i, int j, int di, int dj, string target, int index = 0)
    {
        while (true)
        {
            if (index >= target.Length) return true;
            if (!MatrixHelper.IsInsideMatrix(_matrix, i, j)) return false;
            if (_matrix[i, j] != target[index]) return false;
            i += di;
            j += dj;
            index += 1;
        }
    }

    private bool VerifyDiagonalMas(int i, int j)
    {
        if (!MatrixHelper.IsInsideMatrix(_matrix, i - 1, j - 1) ||
            !MatrixHelper.IsInsideMatrix(_matrix, i + 1, j + 1) ||
            !MatrixHelper.IsInsideMatrix(_matrix, i - 1, j + 1) ||
            !MatrixHelper.IsInsideMatrix(_matrix, i + 1, j - 1))
            return false;

        if (_matrix[i - 1, j - 1] == 'M' &&
            _matrix[i + 1, j + 1] == 'S' &&
            _matrix[i - 1, j + 1] == 'M' &&
            _matrix[i + 1, j - 1] == 'S')
        {
            return true;
        }

        if (_matrix[i - 1, j - 1] == 'S' &&
            _matrix[i + 1, j + 1] == 'M' &&
            _matrix[i - 1, j + 1] == 'S' &&
            _matrix[i + 1, j - 1] == 'M')
        {
            return true;
        }

        if (_matrix[i - 1, j - 1] == 'S' &&
            _matrix[i + 1, j + 1] == 'M' &&
            _matrix[i - 1, j + 1] == 'M' &&
            _matrix[i + 1, j - 1] == 'S')
        {
            return true;
        }

        if (_matrix[i - 1, j - 1] == 'M' &&
            _matrix[i + 1, j + 1] == 'S' &&
            _matrix[i - 1, j + 1] == 'S' &&
            _matrix[i + 1, j - 1] == 'M')
        {
            return true;
        }

        return false;
    }
}