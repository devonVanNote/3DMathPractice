using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public  Coords A;
    public Coords B;
    public Coords V;
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
        type = LineType.Segment;
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

    public float IntersectsAt(Line L2)
    {
        if(HolisticMath.Dot(Coords.Perp(L2.V), V) == 0)
        {
            return float.NaN; // lines are parallel!
        }

        Coords c = L2.A - this.A;
        float t = HolisticMath.Dot(Coords.Perp(L2.V), c) / HolisticMath.Dot(Coords.Perp(L2.V), V);
        
        if((t < 0 || t > 1) && type == LineType.Segment)
        {
            return float.NaN;
        }
        
        return t;
    }

    public bool IntersectIsANumber(float t_or_s)
    {
        return t_or_s == t_or_s;
    }

    public float IntersectsAt(Plane plane)
    {
        Coords normal = HolisticMath.CrossProduct(plane.u, plane.v);

        if(HolisticMath.Dot(normal, V) == 0)
        {
            return float.NaN;
        }

        float t = HolisticMath.Dot(normal, plane.A-A) / HolisticMath.Dot(normal, V);

        return t;
    }

    public Coords Reflect(Coords normal)
    {
        Coords norm = normal.GetNormal();
        Coords vNorm = V.GetNormal();

        float d = HolisticMath.Dot(norm, vNorm);

        if (d == 0) //d will be zero if tryng to reflect against parallel line or wall therefore won't reflect
        {
            return V;
        }

        float vn2 =  d * 2;
        Coords reflectionVector = vNorm - norm * vn2; 

        return reflectionVector;
    }
}
