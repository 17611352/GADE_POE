using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory_Building_Controller : MonoBehaviour
{
    public string team;
    public int totalNumOfResource;
    public float health = 0;
    float maxHealth = 500;

    public GameObject gameManager;
    public Image healthBar;

    int r;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        gameManager = GameObject.FindGameObjectWithTag("Game Manager");

        if(gameObject.tag == "Factory Blue")
        {
            team = "Blue Team";
        }
        else if (gameObject.tag == "Factory Red")
        {
            team = "Red Team";
        }
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();

        healthBar.fillAmount = health / maxHealth;

        if (team == "Blue Team")
        {
            totalNumOfResource = gameManager.GetComponent<Game_Engine>().numOfBlueResourceTotal;
        }
        else if (team == "Red Team")
        {
            totalNumOfResource = gameManager.GetComponent<Game_Engine>().numOfRedResourceTotal;
        }



        //foreach(GameObject gO in gameManager.GetComponent<Game_Engine>().Building)
        //{
        //    if(gO.tag == "Resource Blue" && team == "Blue Team")
        //    {
        //        totalNumOfResource = gO.GetComponent<Resource_Building_Controller>().currentNumOfResource * 2;
        //    }
        //    else if(gO.tag == "Resource Red" && team == "Red Team")
        //    {
        //        totalNumOfResource = gO.GetComponent<Resource_Building_Controller>().currentNumOfResource * 2;
        //    }
        //}
    }

    public void SpawnUnitsAtBuilding()
    {
        //r = Random.Range(0, 5);

        if (totalNumOfResource > 0)
        {
            //Spawn Unit of choice
            if(team == "Blue Team")
            {
                GameObject gO = gameManager.GetComponent<Game_Engine>().meleeUnitBlue;

                r = Random.Range(0, 5);

                if (r < 4)
                {
                    gO = gameManager.GetComponent<Game_Engine>().meleeUnitBlue;
                }
                else if(r >= 4)
                {
                    gO = gameManager.GetComponent<Game_Engine>().rangedUnitBlue;
                }


                Vector3 unitSpawnPosBlue = new Vector3(2, 0, 0);
                unitSpawnPosBlue += transform.position;
                Instantiate(gO, unitSpawnPosBlue, Quaternion.identity);

                //gameManager.GetComponent<Game_Engine>().blueUnit.Add(gO);

                GameObject[] blueResourceBuildings = GameObject.FindGameObjectsWithTag("Resource Blue");

                foreach(GameObject bResource in blueResourceBuildings)
                {
                    if(bResource.GetComponent<Resource_Building_Controller>().currentNumOfResource <= 0)
                    {
                        bResource.GetComponent<Resource_Building_Controller>().currentNumOfResource = 0;
                    }
                    else if(bResource.GetComponent<Resource_Building_Controller>().currentNumOfResource > 0)
                    {
                        bResource.GetComponent<Resource_Building_Controller>().currentNumOfResource -= 1;
                    }
                }
            }
            else if (team == "Red Team")
            {
                GameObject gO = gameManager.GetComponent<Game_Engine>().meleeUnitRed;

                r = Random.Range(0, 5);

                if (r < 4)
                {
                    gO = gameManager.GetComponent<Game_Engine>().meleeUnitRed;
                }
                else if(r >= 4)
                {
                    gO = gameManager.GetComponent<Game_Engine>().rangedUnitRed;
                }

                Vector3 unitSpawnPosRed = new Vector3(-2, 0, 0);
                unitSpawnPosRed += transform.position;
                Instantiate(gO, unitSpawnPosRed, Quaternion.identity);

                //gameManager.GetComponent<Game_Engine>().redUnit.Add(gO);

                gameManager.GetComponent<Game_Engine>().numOfRedResourceTotal--;

                GameObject[] redResourceBuildings = GameObject.FindGameObjectsWithTag("Resource Red");

                foreach (GameObject rResource in redResourceBuildings)
                {
                    if (rResource.GetComponent<Resource_Building_Controller>().currentNumOfResource <= 0)
                    {
                        rResource.GetComponent<Resource_Building_Controller>().currentNumOfResource = 0;
                    }
                    else if (rResource.GetComponent<Resource_Building_Controller>().currentNumOfResource > 0)
                    {
                        rResource.GetComponent<Resource_Building_Controller>().currentNumOfResource -= 1;
                    }
                }
            }

        }
    }


    void DeathCheck()
    {
        if(health <= 0)
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
