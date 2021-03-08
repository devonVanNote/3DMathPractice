using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    Coords A;
    Coords B;
    Coords V;
    string type;

    public Line(Coords _A, Coords _B, string _type)
    {
        A = _A;
        B = _B;
        type = _type;
        V = new Coords(B.x - A.x, B.y - A.y, B.z - A.z);
    }

    public Line(Coords _A, Coords v)
    {
        A = _A;
        B = _A + v;
        V = v;
    }

    //t is sometimes referred to as time
    public Coords Lerp(float t)
    {
        t = AdjustForLineType(t, type);
        float xT = A.x + V.x * t;
        float yT = A.y + V.y * t;
        float zT = A.z + V.z * t;

        return new Coords(xT, yT, zT);
    }

    private float AdjustForLineType(float t, string type)
    {
        switch(type)
        {
            case LineType.Segment:
                t = Mathf.Clamp(t, 0, 1);
                break;
            case LineType.Ray:
                t = t < 0 ? 0 : t; 
                break;
            default:
                break;
        }

        return t;   
    }

    public void Draw(float width, Color color)
    {
        Coords.DrawLine(A, B, width, color);
    }
}
