using Core;

namespace AdventOfCode;

public sealed class Day06 : BaseDay
{
    private readonly char[,] _matrix;
    private readonly (int i, int j) _startingPosition;

    public Day06()
    {
        _matrix = InputHelper.GetMatrixInput(InputFilePath);
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] != '^') continue;
                _startingPosition = (i, j);
                break;
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>($"{SimulateGuardMovement().movesToExit}");
    }

    public override ValueTask<string> Solve_2()
    {
        var totalLoops = 0;
        for (var i = 0; i < _matrix.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
            {
                if (_matrix[i, j] != '^' && _matrix[i, j] != '#')
                {
                    _matrix[i, j] = '#';
                    if (SimulateGuardMovement().isLoop)
                    {
                        totalLoops++;
                    }

                    _matrix[i, j] = '.';
                }
            }
        }

        return new ValueTask<string>($"{totalLoops}");
    }

    private (int movesToExit, bool isLoop) SimulateGuardMovement()
    {
        var i = _startingPosition.i;
        var j = _startingPosition.j;
        HashSet<(int, int)> visited = [];
        HashSet<((int, int), Direction)> loopPositions = [];
        var direction = Direction.Up;
        var outOfBounds = false;
        visited.Add((i, j));
        while (true)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (!MatrixHelper.IsInsideMatrix(_matrix, i - 1, j))
                    {
                        outOfBounds = true;
                        break;
                    }

                    if (_matrix[i - 1, j] == '#')
                    {
                        direction++;
                    }
                    else
                    {
                        i--;
                    }

                    break;
                case Direction.Right:
                    if (!MatrixHelper.IsInsideMatrix(_matrix, i, j + 1))
                    {
                        outOfBounds = true;
                        break;
                    }

                    if (_matrix[i, j + 1] == '#')
                    {
                        direction++;
                    }
                    else
                    {
                        j++;
                    }

                    break;
                case Direction.Down:
                    if (!MatrixHelper.IsInsideMatrix(_matrix, i + 1, j))
                    {
                        outOfBounds = true;
                        break;
                    }

                    if (_matrix[i + 1, j] == '#')
                    {
                        direction++;
                    }
                    else
                    {
                        i++;
                    }

                    break;
                case Direction.Left:
                    if (!MatrixHelper.IsInsideMatrix(_matrix, i, j - 1))
                    {
                        outOfBounds = true;
                        break;
                    }

                    if (_matrix[i, j - 1] == '#')
                    {
                        direction = Direction.Up;
                    }
                    else
                    {
                        j--;
                    }

                    break;
            }

            if (outOfBounds)
            {
                break;
            }

            if (loopPositions.Contains(((i, j), direction)))
            {
                return (visited.Count, true);
            }

            visited.Add((i, j));
            loopPositions.Add(((i, j), direction));
        }

        return (visited.Distinct().Count(), false);
    }

    private enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}