using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private GameObject[] walls;
    private GameObject[] boxes;

    public bool readyToMove = true;

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        boxes = GameObject.FindGameObjectsWithTag("Box");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();


        if (moveInput.sqrMagnitude > 0.5)
        {
            if (readyToMove)
            {
                readyToMove = false;
                Move(moveInput);
            }
        }
        else
        {
            readyToMove = true;
        }
    }

    public bool Move(Vector2 direction)
    {
        Debug.Log("moving " + direction);
        if (Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();

        if(Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    public bool Blocked(Vector3 pos, Vector2 direction)
    {
        Vector2 newPos = new Vector2(pos.x, pos.y) + direction;

        foreach (var obj in walls)
        {
            if (obj.transform.position.x == newPos.x
                && obj.transform.position.y == newPos.y)
            {
                Debug.Log("blocked by a wall");
                return true;
            }
        }

        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x
                && box.transform.position.y == newPos.y)
            {
                Push objpush = box.GetComponent<Push>();
                if(objpush && objpush.Move(direction))
                {
                    return false;
                } else
                {
                    return true;
                }
            }
        }

        return false;

    }
}
