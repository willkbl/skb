using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private GameObject[] walls;
    private GameObject[] boxes;
    private GameObject[] winzones;
    private int numOfWinZones;

    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        //find number of win spots
        winzones = GameObject.FindGameObjectsWithTag("PDS");
        numOfWinZones = winzones.Length;

        //populate knowledge of walls
        walls = GameObject.FindGameObjectsWithTag("Wall");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerMoved()
    {
        //populate knowledge of boxes (update w player movement in case boxes are added)
        //if the ability to add walls or winzones is included, those finds should be moved here
        boxes = GameObject.FindGameObjectsWithTag("Box");

        int filledWZs = 0;

        //for each box, check whether it is in the same position as any of the winzones
        foreach (var box in boxes) {
            foreach (var wz in winzones)
            {
                if (box.transform.position.x == wz.transform.position.x
                    && box.transform.position.y == wz.transform.position.y)
                {
                    filledWZs++;
                }
            }
        }

        //if all winzones filled
        if (filledWZs == numOfWinZones)
        {
            Debug.Log("Winning position"); //win the game

            if (nextScene != null)
            {
                Invoke("NextLevel", 1f); //load the next level after 1 second
            }
            else
            {
                Debug.Log("No next level to load");
            }
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
}
