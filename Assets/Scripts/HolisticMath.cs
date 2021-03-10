using UnityEngine;

public class HolisticMath
{
    static public Coords GetNormalFromOrigin(Coords vector)
    {
        Coords origin = new Coords(0, 0, 0);
        float magnitude = Distance(origin, vector);
        vector.x /= magnitude;
        vector.y /= magnitude;
        vector.z /= magnitude;

        return vector;
    }

    static public float Distance(Coords point1, Coords point2)
    {
        float diffSquared = Square(point1.x - point2.x) 
                            + Square(point1.y - point2.y)
                            + Square(point1.z - point2.z);

        return Mathf.Sqrt(diffSquared);
    }

    static public float Square(float value)
    {
        return value * value;
    }

    static public float Dot(Coords vector1, Coords vector2)
    {
        return (vector1.x * vector2.x) 
                + (vector1.y * vector2.y) 
                + (vector1.z * vector2.z);
    }

    static public float Angle(Coords vector1, Coords vector2)
    {
        float dotDivide = Dot(vector1, vector2) / 
                          Distance(new Coords(0,0,0), vector1) *
                          Distance(new Coords(0,0,0), vector2);

        return Mathf.Acos(dotDivide); 
    }

    static public Coords Rotate(Coords vector, float angle, bool clockwise)
    {
        //Do an "anti-clockwise" turn to get to target
        if (clockwise)
        {
            angle = (2 * Mathf.PI) - angle;
        }

        float xValue = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        float yValue = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);

        return new Coords(xValue, yValue, 0);
    }

    //Cross Product is used to determine a perpendicular vector to two vectors
    //If the v x w < 0 it is a clockwise rotation
    //Else if v x w > 0 it is a counter-clockwise rotation
    static public Coords CrossProduct(Coords vector1, Coords vector2)
    {
        float firstCross  = ((vector1.y * vector2.z) - (vector1.z * vector2.y));
        float secondCross = ((vector1.x * vector2.z) - (vector1.z * vector2.x));
        float thirdCross  = ((vector1.x * vector2.y) - (vector1.y * vector2.x));

        return new Coords(firstCross, secondCross, thirdCross); 
    }

    static public Coords LookAt2D(Coords forwardVector, Coords position, Coords focusPoint)
    {   
        Coords direction = new Coords(focusPoint.x - position.x, focusPoint.y - focusPoint.y, position.z);
        float angle = Angle(forwardVector, direction);
        bool clockwise = false;

        if (CrossProduct(forwardVector, direction).z < 0 )
        {
            clockwise = true;
        }

        return Rotate(forwardVector, angle, clockwise);
    }

    static public Coords Translate(Coords position, Coords facing, Coords vector)
    {
        if(Distance(new Coords(0,0,0), vector) <= 0)
        {
            return position;
        }

        float angle = Angle(vector, facing);
        float worldAngle = Angle(vector, new Coords(0,1,0));

        bool clockwise = false;

        if (CrossProduct(vector, facing).z < 0 )
        {
            clockwise = true;
        }

        vector = Rotate(vector, angle + worldAngle, clockwise);

        float xValue  = position.x + vector.x;
        float yValue  = position.y + vector.y;
        float zValue  = position.z + vector.z;

        return new Coords(xValue, yValue, zValue);
    }

    static public Coords Lerp(Coords A, Coords B, float t)
    {
        Coords V = new Coords(B.x - A.x, B.y - A.y, B.z - A.z);
        t = Mathf.Clamp(t, 0, 1); //clamp to make it move along a line segment only
        float xT = A.x + V.x * t;
        float yT = A.y + V.y * t;
        float zT = A.z + V.z * t;

        return new Coords(xT, yT, zT);
    }
}
