namespace Core;

public class MatrixHelper
{
    public static readonly (int di, int dj)[] Directions =
    [
        (-1, 0), // Up
        (-1, 1), // DiagTopRight
        (0, 1), // Right
        (1, 1), // DiagBottomRight
        (1, 0), // Down
        (1, -1), // DiagBottomLeft
        (0, -1), // Left
        (-1, -1) // DiagTopLeft
    ];

    public static bool IsInsideMatrix<T>(T[,] matrix, int i, int j)
    {
        return i >= 0 && j >= 0 && i < matrix.GetLength(0) && j < matrix.GetLength(1);
    }

    public static void PrintMatrix(char[,] matrix)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j]);
            }

            Console.WriteLine();
        }
    }
}