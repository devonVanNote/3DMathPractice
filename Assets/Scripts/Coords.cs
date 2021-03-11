using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coords
{
    public float x;
    public float y;
    public float z;

    public float w;

    public Coords(float _X, float _Y)
    {
        x = _X;
        y = _Y;
        z = -1;
    }

    public Coords(float _X, float _Y, float _Z)
    {
        x = _X;
        y = _Y;
        z = _Z;
    }

     public Coords(float _X, float _Y, float _Z, float _W)
    {
        x = _X;
        y = _Y;
        z = _Z;
        w = _W;
    }

    public Coords(Vector3  vectorPostion, float _W)
    {
        x = vectorPostion.x;
        y = vectorPostion.y;
        z = vectorPostion.z;
        w = _W;
    }

    public float[] AsFloats()
    {
        float[] values = {x , y, x, w};
        return values;
    }

    public Coords(Vector3 vecpos)
    {
        x = vecpos.x;
        y = vecpos.y;
        z = vecpos.z;
    }

    public Coords GetNormal()
    {
        float magnitude = HolisticMath.Distance(new Coords(0,0,0), new Coords(x,y,z));
        return new Coords(x / magnitude, y / magnitude, z / magnitude);
    }

    public override string ToString()
    {
        return "(" + x + "," + y + "," + z + ")";
    }

    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }

    static public void DrawPoint(Coords position, float width, Color color)
    {
        GameObject line = new GameObject("Point_" + position.ToString());
        CreatePoint(position, line, width, color);
    }

    static public void DrawLine(Coords start, Coords end, float width, Color color)
    {
        GameObject line = new GameObject("Line_" + start.ToString() + "_" + end.ToString());
        CreateLine(start, end, line, width, color);
    }

    static private void CreatePoint(Coords position, GameObject line, float width, Color color)
    {
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = color;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(position.x - width / 3.0f, position.y - width / 3.0f, position.z));
        lineRenderer.SetPosition(1, new Vector3(position.x + width / 3.0f, position.y - width / 3.0f, position.z));
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    static private void CreateLine(Coords startPosition, Coords endPosition, GameObject line, float width, Color color)
    {
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = color;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(startPosition.x, startPosition.y, startPosition.z));
        lineRenderer.SetPosition(1, new Vector3(endPosition.x, endPosition.y, endPosition.z));
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    static public Coords operator + (Coords a, Coords b)
    {
        Coords sum = new Coords(a.x + b.x, a.y + b.y, a.z + b.z);
        return sum;
    }

    static public Coords operator * (Coords a, float b)
    {
        Coords product = new Coords(a.x  * b, a.y * b, a.z * b);
        return product;
    }

    static public Coords operator / (Coords a, float b)
    {
        Coords quotient = new Coords(a.x / b, a.y / b, a.z / b);
        return quotient;
    }

    static public Coords operator - (Coords a, Coords b)
    {
        Coords difference = new Coords(a.x - b.x, a.y - b.y, a.z - b.z);
        return difference;
    }

    static public Coords Perp(Coords v)
    {
        return new Coords(-v.y, v.x, 0); //z value is zero for 2D only!
    }
}
