using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformations : MonoBehaviour
{
    public GameObject[] points;
    public Vector3 angle;
    public Vector3 translation;
    public Vector3 scale;
    public Vector3 shear;
    public GameObject center;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 c = new Vector3(center.transform.position.x,
                                               center.transform.position.y,
                                               center.transform.position.z );

        angle = angle * Mathf.Deg2Rad;

        foreach( GameObject p in points)
        {
            Coords position = new Coords(p.transform.position, 1);
            p.transform.position = HolisticMath.Shear(position, shear.x, shear.y, shear.z).ToVector();            
        }

        DrawWireFrame();
    }

    void DrawWireFrame()
    {
        Coords.DrawLine(new Coords(points[0].transform.position), new Coords(points[1].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[1].transform.position), new Coords(points[2].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[2].transform.position), new Coords(points[3].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[3].transform.position), new Coords(points[0].transform.position), 0.05f, Color.yellow);
        
        Coords.DrawLine(new Coords(points[4].transform.position), new Coords(points[5].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[5].transform.position), new Coords(points[6].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[6].transform.position), new Coords(points[7].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[7].transform.position), new Coords(points[4].transform.position), 0.05f, Color.yellow);
        
        Coords.DrawLine(new Coords(points[2].transform.position), new Coords(points[6].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[7].transform.position), new Coords(points[3].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[5].transform.position), new Coords(points[1].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[4].transform.position), new Coords(points[0].transform.position), 0.05f, Color.yellow);
        
        Coords.DrawLine(new Coords(points[8].transform.position), new Coords(points[9].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[9].transform.position), new Coords(points[4].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[9].transform.position), new Coords(points[5].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[8].transform.position), new Coords(points[6].transform.position), 0.05f, Color.yellow);
        Coords.DrawLine(new Coords(points[8].transform.position), new Coords(points[7].transform.position), 0.05f, Color.yellow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
