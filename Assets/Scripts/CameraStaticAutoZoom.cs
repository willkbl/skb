using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A system for dynamically choosing the right zoom level for the camera.
//The "static" refers to the fact that the camera does not move during gameplay -- all these calculations are done when the level is loaded.


/*
 * current flaws:
 *      assumes all wall sprites are the same size
 *      assumes all wall sprites are square
 *      assumes the level itself is vertically centered
 */

//DISCLAIMER: this system may very well have to be adjusted if the way sprites/levels are created changes.
public class CameraStaticAutoZoom : MonoBehaviour
{

    //assume every level is surrounded by walls on all sides
    private GameObject[] walls;

    //default screen info: lowest tile is at y = -4.5 with a height of 1
    //                     highest tile is at y = 3.5 with a height of 1
    //                     difference of 8
    //default camera info: size is 5
    //this info is subject to change if I figure out a better way to align tiles

    Camera thisCamera;
    [SerializeField]
    [Tooltip("Default zoom of 5, leave unchanged for dynamic camera. Change zoom value for specific zoom.")]
    public float cameraSize = 5; //default value of 5, leave unchanged for dynamic camera

    // Start is called before the first frame update
    void Start()
    {
        //populate knowledge of walls
        walls = GameObject.FindGameObjectsWithTag("Wall");

        thisCamera = GetComponent<Camera>();

        if (cameraSize == 5)
        {
            float lowestY = walls[0].transform.position.y;
            float highestY = walls[0].transform.position.y;
            foreach (var wall in walls)
            {
                if (wall.transform.position.y < lowestY)
                {
                    lowestY = wall.transform.position.y;
                }
                if (wall.transform.position.y > highestY)
                {
                    highestY = wall.transform.position.y;
                }
            }

            float heightDifference = highestY - lowestY;

            //assume same sprite size for all walls
            float spriteSize = walls[0].GetComponent<SpriteRenderer>().size.y;

            cameraSize = (heightDifference / 2) + spriteSize;
        }

        thisCamera.orthographicSize = cameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
