using System;
public class Matrix
{
    float[] values;
    int rows;
    int columns;

    public Matrix(int r, int c, float[] v)
    {
        rows = r;
        columns = c;
        values = new float[rows * columns];
        Array.Copy(v, values, (rows*columns));
    }

    public override string ToString()
    {
        string matrix = string.Empty;

        for(int r = 0; r < rows; r ++)
        {
            for(int c =0; c < rows; c++)
            {
                matrix += values[r*columns + c] + " ";
            }
            matrix += "\n";
        }

        return matrix;
    }

}
