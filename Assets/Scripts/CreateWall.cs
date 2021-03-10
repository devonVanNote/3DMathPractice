using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    Line wall;
    Line ballPath;
    public GameObject ball;
    float intersectT;
    float intersectS;
    Line trajectory;

    // Start is called before the first frame update
    void Start()
    {
        wall = new Line(new Coords(5, -2, 0), new Coords(0, 5, 0));
        wall.Draw(1, Color.blue);

        ballPath = new Line(new Coords(-6, 2, 0), new Coords(100, -20, 0));
        ballPath.Draw(0.1f, Color.yellow);

        ball.transform.position = ballPath.A.ToVector();

        intersectT = ballPath.IntersectsAt(wall);
        intersectS = wall.IntersectsAt(ballPath);

        if(ballPath.IntersectIsANumber(intersectT) && wall.IntersectIsANumber(intersectS)) 
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
            ball.transform.position += trajectory.Reflect(Coords.Perp(wall.V)).ToVector() * Time.deltaTime * 5;
        }
      
    }
}
