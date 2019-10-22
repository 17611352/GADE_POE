using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Building_Controller : MonoBehaviour
{
    int resourcePerInterval = 1;
    public int currentNumOfResource = 0;

    public float currentTime;
    float resourceGatherCheck;

    public int Instance;

    public float health = 0;
    float maxHealth = 300;

    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");

        resourceGatherCheck = gameManager.GetComponent<Game_Engine>().resourceGatherSec;
        resourcePerInterval = gameManager.GetComponent<Game_Engine>().resourceGatherAmountPerInterval;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= resourceGatherCheck)
        {
            currentNumOfResource += resourcePerInterval;
            currentTime = 0;

            if (gameObject.tag == "Resource Blue")
            {

            }
            //if (gameObject.tag == "Resource Blue")
            //{
            //    gameManager.GetComponent<Game_Engine>().blueResourceCount1 = currentNumOfResource;
            //}
            //else if (gameObject.tag == "Resource Red")
            //{
            //    gameManager.GetComponent<Game_Engine>().numOfRedResourceTotal += currentNumOfResource;
            //}
        }

 
    }
}
