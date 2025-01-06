using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script represents the behavior for a default pushable object
 */
public class Push : MonoBehaviour
{

    private GameObject[] walls;
    private GameObject[] boxes;

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        boxes = GameObject.FindGameObjectsWithTag("Box");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Move(Vector2 direction)
    {
        if (PushBlocked(transform.position, direction))
        {
            return false;
        } else
        {
            transform.Translate(direction);
            return true;
        }
    }


    public bool PushBlocked(Vector3 pos, Vector2 direction)
    {
        Vector2 newPos = new Vector2(pos.x, pos.y) + direction;

        //pushable object blocked by wall
        foreach (var obj in walls)
        {
            if (obj.transform.position.x == newPos.x
                && obj.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        
        //pushable object blocked by pushable object
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x
               && box.transform.position.y == newPos.y)
            {
                Push objpush = box.GetComponent<Push>(); //get the push behavior of the other object
                if (objpush && objpush.Move(direction)) //if the object has push behavior and isn't blocked
                {
                    return false; //this box is not blocked (also the other box should move in the direction of pushing)
                }
                else
                {
                    return true;
                }


                //if screwed for boxes pushing boxes:
                //return true;
            }
        }

        return false;

    }
}
