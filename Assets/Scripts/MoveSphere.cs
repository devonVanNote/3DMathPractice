using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    public Transform start;
    public Transform end;
    Line line;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = HolisticMath.Lerp(new Coords(start.position), new Coords(end.position), Time.time * 0.25f).ToVector();
    }
}
