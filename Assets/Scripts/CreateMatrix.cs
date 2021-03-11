using UnityEngine;

public class CreateMatrix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float[] values = {1, 2, 3, 4, 5, 6};
        Matrix m = new Matrix(2, 3, values);

        float[] nvalues = {1, 2, 3, 4, 5, 6};
        Matrix n = new Matrix(3, 2, nvalues);

        Matrix product = m * n;

        Debug.Log(m.ToString()     + "\n");
        Debug.Log(n.ToString()      + "\n");
        Debug.Log(product.ToString() + "\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
