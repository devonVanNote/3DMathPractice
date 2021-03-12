using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    float speed = 10.0f;
    float rotationSpeed = 100.0f;

    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        Move(0,0, translation);

        transform.Rotate(0, rotation, 0);
    }

    void Move(float x, float y, float z)
    {
        Matrix4x4 worldTransform = transform.localToWorldMatrix;
        Vector4 airplaneWorldForwardVector = worldTransform.GetColumn(2) * z;
        transform.position += new Vector3(airplaneWorldForwardVector.x
                                                                ,airplaneWorldForwardVector.y 
                                                                ,airplaneWorldForwardVector.z);
    }
}
