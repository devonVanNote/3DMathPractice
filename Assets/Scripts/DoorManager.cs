using UnityEngine;

public class DoorManager : MonoBehaviour
{
    int doorType = 0;
    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<AttributeManager>().attributes & doorType) != 0)
        {
            SetDoorTrigger(true); //"open door"
        }
    }

    void OnTriggerExit(Collider other)
    {
        SetDoorTrigger(false); //"close door"
        other.gameObject.GetComponent<AttributeManager>().attributes &= ~doorType; //Used key, remove it from attributes
    }

    private void SetDoorTrigger(bool isTrigger)
    {
        this.GetComponent<BoxCollider>().isTrigger = isTrigger;
    }

    void Start()
    {
        switch (this.gameObject.tag)
        {
            case Tag.MagicDoor:
                doorType = AttributeManager.MAGIC;
                break;
            case Tag.InvisibleDoor:
                doorType = AttributeManager.INVISIBLE;
                break;
            case Tag.CharismaDoor:
                doorType = AttributeManager.CHARISMA;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
