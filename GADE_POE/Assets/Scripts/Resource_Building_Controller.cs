using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        gameManager = GameObject.FindGameObjectWithTag("Game Manager");

        resourceGatherCheck = gameManager.GetComponent<Game_Engine>().resourceGatherSec;
        resourcePerInterval = gameManager.GetComponent<Game_Engine>().resourceGatherAmountPerInterval;
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();

        healthBar.fillAmount = health / maxHealth;

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


    void DeathCheck()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wizard Projectile"))
        {
            health -= 5;
        }
    }
}
