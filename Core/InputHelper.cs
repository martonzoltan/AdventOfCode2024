namespace Core;

public static class InputHelper
{
    public static IEnumerable<string> GetInput(string path)
    {
        var input = File.ReadAllLines(path);
        return input;
    }

    public static char[,] GetMatrixInput(string path)
    {
        var input = File.ReadAllLines(path).ToList();
        var array = new char[input.Count, input.Count];
        var rows = 0;
        foreach (var line in input)
        {
            for (var i = 0; i < line.Length; i++)
            {
                array[rows, i] = line[i];
            }

            rows++;
        }

        return array;
    }

    public static int[,] GetIntMatrixInput(string path)
    {
        var input = File.ReadAllLines(path).ToList();
        var array = new int[input.Count, input.Count];
        var rows = 0;
        foreach (var line in input)
        {
            for (var i = 0; i < line.Length; i++)
            {
                array[rows, i] = int.Parse(line[i].ToString());
            }

            rows++;
        }

        return array;
    }

    public static List<List<string>> GetListMatrixInput()
    {
        var input = File.ReadAllLines("input.txt").ToList();
        List<List<string>> matrices = [];
        List<string> array = [];
        var newArray = true;
        foreach (var line in input)
        {
            if (newArray)
            {
                array = [];
                newArray = false;
            }

            if (line == "")
            {
                matrices.Add(array);
                newArray = true;
            }
            else
            {
                array.Add(line);
            }
        }

        matrices.Add(array);
        return matrices;
    }
}