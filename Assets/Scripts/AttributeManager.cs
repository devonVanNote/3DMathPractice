
using UnityEngine;
using UnityEngine.UI;
using System;

public class AttributeManager : MonoBehaviour
{
    static public int MAGIC = 1 << 5;
    static public int INTELLIGENCE = 1 << 4;
    static public int CHARISMA = 1 << 3;
    static public int FLY = 1 << 2;
    static public int INVISIBLE = 1;

    public Text attributeDisplay;
    public int attributes = 0;

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case Tag.Magic:
            case Tag.MagicKey:
                attributes |= MAGIC;
                Destroy(other.gameObject);
                break;
            case Tag.Intelligence:
                attributes |= INTELLIGENCE;
                break;
            case Tag.Charisma:
            case Tag.CharismaKey:
                attributes |= CHARISMA;
                Destroy(other.gameObject);
                break;
            case Tag.Fly:
                attributes |= FLY;
                break;
            case Tag.Invisible:
            case Tag.InvisibleKey:
                attributes |= INVISIBLE;
                Destroy(other.gameObject);
                break;
            case Tag.Add:
                attributes |= (INTELLIGENCE | MAGIC | CHARISMA);
                break;
            case Tag.Remove:
                attributes &= ~(INTELLIGENCE | MAGIC);
                break;
            case Tag.Reset:
                attributes = 0;
                break;
            case Tag.GoldenKey:
                attributes |= (MAGIC | INVISIBLE | CHARISMA);
                Destroy(other.gameObject);
                break;
            default:
                break;

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        attributeDisplay.transform.position = screenPoint + new Vector3(0, -50, 0);
        attributeDisplay.text = Convert.ToString(attributes, 2).PadLeft(8, '0');
    }

}
