using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformations : MonoBehaviour
{
    public GameObject[] points;
    public Vector3 angle;
    public Vector3 translation;
    public Vector3 scale;
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
            Rotation r = new Rotation() 
            {
                angleX = angle.x,
                angleY = angle.y,
                angleZ = angle.z,
                clockwiseX = true,
                clockwiseY = true,
                clockwiseZ = true
            };

            Coords translationBackCoords = new Coords(new Vector3(c.x, c.y, c.z), 0);
            Coords translationToOriginCoords = new Coords(new Vector3(-c.x, -c.y, -c.z), 0);
            position = HolisticMath.Translate(position, translationToOriginCoords);
            position = HolisticMath.Rotate(position, r);
            
            p.transform.position = HolisticMath.Translate(position, translationBackCoords).ToVector();            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
