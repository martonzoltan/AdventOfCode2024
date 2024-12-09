using Core;

namespace AdventOfCode;

public sealed class Day08 : BaseDay
{
    private readonly char[,] _matrix;
    private readonly HashSet<(int i, int j)> _nullPositions = [];

    public Day08()
    {
        _matrix = InputHelper.GetMatrixInput(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        _nullPositions.Clear();
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] == '.') continue;
                VerifyAntinode(i, j, _matrix[i, j]);
            }
        }

        return new ValueTask<string>($"{_nullPositions.Count}");
    }

    public override ValueTask<string> Solve_2()
    {
        _nullPositions.Clear();
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] == '.') continue;
                VerifyRepeatingAntinode(i, j, _matrix[i, j]);
            }
        }

        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_nullPositions.Contains((i, j)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write(_matrix[i, j]);
                }
            }

            Console.WriteLine();
        }

        return new ValueTask<string>($"{_nullPositions.Count}");
    }

    private void VerifyAntinode(int startingI, int startingJ, char target)
    {
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] == target && i != startingI && j != startingJ)
                {
                    var di = i - startingI;
                    var dj = j - startingJ;
                    var potentialAntinodeI = i + di;
                    var potentialAntinodeJ = j + dj;
                    if (MatrixHelper.IsInsideMatrix(_matrix, potentialAntinodeI, potentialAntinodeJ))
                    {
                        _nullPositions.Add((potentialAntinodeI, potentialAntinodeJ));
                    }
                }
            }
        }
    }

    private void VerifyRepeatingAntinode(int startingI, int startingJ, char target)
    {
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] == target && i != startingI && j != startingJ)
                {
                    var di = i - startingI;
                    var dj = j - startingJ;
                    var potentialAntinodeI = i + di;
                    var potentialAntinodeJ = j + dj;

                    _nullPositions.Add((i, j));
                    while (MatrixHelper.IsInsideMatrix(_matrix, potentialAntinodeI, potentialAntinodeJ))
                    {
                        if (MatrixHelper.IsInsideMatrix(_matrix, potentialAntinodeI, potentialAntinodeJ))
                        {
                            _nullPositions.Add((potentialAntinodeI, potentialAntinodeJ));
                        }
                        else
                        {
                            break;
                        }

                        potentialAntinodeI += di;
                        potentialAntinodeJ += dj;
                    }
                }
            }
        }
    }
}