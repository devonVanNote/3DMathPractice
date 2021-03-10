using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSolidWall : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Transform C;
    public Transform D;
    public Transform E;
    public GameObject ball;

    Plane wall;
    Line ballPath;
    Line trajectory;
     float intersectT;
    float intersectS;

    // Start is called before the first frame update
    void Start()
    {
        wall = new Plane(new Coords(A.position), new Coords(B.position), new Coords(C.position));
        for (float s = 0; s < 1; s += 0.1f)
        {
            for(float t = 0; t < 1; t += 0.1f)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = wall.Lerp(s, t).ToVector();
            }
        }

        ballPath = new Line(new Coords(D.position), new Coords(E.position), LineType.Ray);
        ballPath.Draw(0.1f, Color.green);

        ball.transform.position = ballPath.A.ToVector();

        intersectT = ballPath.IntersectsAt(wall);

        if(ballPath.IntersectIsANumber(intersectT)) 
        {
            trajectory = new Line(ballPath.A, ballPath.Lerp(intersectT), LineType.Segment);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time  <= 1)
        {
            ball.transform.position = trajectory.Lerp(Time.time).ToVector();
        }
        else 
        {
            ball.transform.position += 
                trajectory.Reflect(HolisticMath.CrossProduct(wall.v, wall.u)).ToVector() * Time.deltaTime * 10;
        }
    }
}
