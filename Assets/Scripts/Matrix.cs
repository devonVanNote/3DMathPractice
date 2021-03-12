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

    public Coords AsCoords()
    {
        if(rows == 4 && columns == 1)
        {
            return(new Coords(values[0], values[1], values[2], values[3]));
        }
        else 
        {
            return null;
        }
    }

    public override string ToString()
    {
        string matrix = string.Empty;

        for(int r = 0; r < rows; r ++)
        {
            for(int c =0; c < columns; c++)
            {
                matrix += values[r * columns + c] + " ";
            }
            matrix += "\n";
        }

        return matrix;
    }

    static public Matrix operator + (Matrix a, Matrix b)
    {
        if(a.rows != b.rows || a.columns != b.columns)
        {
            return null;
        }

        Matrix sum = new Matrix(a.rows, a.columns, a.values);

        int length = a.rows * a.columns;
        for (int i =0; i < length; i++)
        {
            sum.values[i] += b.values[i];
        }

        return sum;
    }

     static public Matrix operator - (Matrix a, Matrix b)
    {
        if(a.rows != b.rows || a.columns != b.columns)
        {
            return null;
        }

        Matrix difference = new Matrix(a.rows, a.columns, a.values);

        int length = a.rows * a.columns;
        for (int i =0; i < length; i++)
        {
            difference.values[i] -= b.values[i];
        }

        return difference;
    }

    static public Matrix operator * (Matrix a, Matrix b)
    {
        if (a.columns != b.rows)
        {
            return null;
        }

        float[] resultValues = new float[a.rows * b.columns];
        for(int i = 0; i < a.rows; i++)
        {
            for(int j = 0; j < b.columns; j++)
            {
                for(int k = 0; k < a.columns; k++)
                {
                    resultValues[i * b.columns + j] += a.values[i * a.columns + k] *
                                                    b.values[k * b.columns + j];
                }
            }
        }

        return new Matrix(a.rows, b.columns, resultValues);
    }
}
