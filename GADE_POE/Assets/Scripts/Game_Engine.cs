using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Engine : MonoBehaviour
{
   //public List<GameObject> units;
    public List<GameObject> blueUnit;
    public List<GameObject> redUnit;
    public List<GameObject> buildings;

    //public List<GameObject> Unit
    //{
    //    get { return units; }
    //    set { units = value; }
    //}

    public List<GameObject> Building
    {
        get { return buildings; }
        set { buildings = value; }
    }


    public int numOfFactoryBuildingsPerTeam;
    public int numOfResourceBuildingsPerTeam;

    public GameObject factoryBuildingRed;
    public GameObject factoryBuildingBlue;

    public GameObject resourceBuildingRed;
    public GameObject resourceBuildingBlue;

    public float nextWaveInterval;
    public float nextWave;
    public float currentTime;

    public int resourceGatherAmountPerInterval = 1;
    public float resourceGatherSec = 10;

    public int numOfBlueResourceTotal;
    public int numOfRedResourceTotal;

    public int numOfBlueUnitsInGame;
    public int numOfRedUnitsInGame;

    public GameObject meleeUnitBlue;
    public GameObject meleeUnitRed;

    public GameObject rangedUnitBlue;
    public GameObject rangedUnitRed;

    public GameObject RedArrowPrefab;
    public GameObject BlueArrowPrefab;
    public float arrowFireInterval = 3;

    public List<Vector3> blueSpawnPos;
    public List<Vector3> redSpawnPos;

    public GameObject blueRallyPoint;
    public GameObject redRallyPoint;

  

    // Start is called before the first frame update
    void Start()
    {
        if (numOfResourceBuildingsPerTeam > 2 || numOfFactoryBuildingsPerTeam > 2)
        {
            
        }
        else
        {
            SpawnBuildings(numOfFactoryBuildingsPerTeam, numOfResourceBuildingsPerTeam);
        }

        AssignResourceBuildingInstance();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        TotalNumOfResourceCheck();

        if (currentTime >= nextWave)
        {
            //SpawnUnitsAtBuilding
            nextWave += nextWaveInterval;

            //I want this to call the spawn method of all the factory buildings in the game
            foreach(GameObject gO in Building)
            {
                if(gO.tag == "Factory Blue" || gO.tag == "Factory Red")
                {
                    gO.GetComponent<Factory_Building_Controller>().SpawnUnitsAtBuilding();
                }
            }
        }

    }


    private void NumOfUnitsInGame()
    {

    }


    private void AssignResourceBuildingInstance()
    {
        int instanceCheckBlue = 1;
        int instanceCheckRed = 3;

        for(int i = 0; 0 < buildings.Count; i++)
        {
            if(buildings[i].tag == "Resource Blue")
            {
                buildings[i].GetComponent<Resource_Building_Controller>().Instance = instanceCheckBlue;
                instanceCheckBlue++;
            }else if (buildings[i].tag == "Resource Red")
            {
                buildings[i].GetComponent<Resource_Building_Controller>().Instance = instanceCheckRed;
                instanceCheckRed++;
            }
        }
    }


    //Checks for the total num of resource a Team has
    public void TotalNumOfResourceCheck()
    {
        int blueTemp1 = 0;
        int blueTemp2 = 0;

        int redTemp1 = 0;
        int redTemp2 = 0;

        foreach(GameObject gO in Building)
        {
            if(gO.tag == "Resource Blue" || gO.tag == "Resource Red")
            {
                if (gO.GetComponent<Resource_Building_Controller>().Instance == 1)
                {
                    blueTemp1 = gO.GetComponent<Resource_Building_Controller>().currentNumOfResource;
                }
                if (gO.GetComponent<Resource_Building_Controller>().Instance == 2)
                {
                    blueTemp2 = gO.GetComponent<Resource_Building_Controller>().currentNumOfResource;
                }

                if (gO.GetComponent<Resource_Building_Controller>().Instance == 3)
                {
                    redTemp1 = gO.GetComponent<Resource_Building_Controller>().currentNumOfResource;
                }
                if (gO.GetComponent<Resource_Building_Controller>().Instance == 4)
                {
                    redTemp2 = gO.GetComponent<Resource_Building_Controller>().currentNumOfResource;
                }
            }
            
        }

        numOfBlueResourceTotal = blueTemp1 + blueTemp2;
        numOfRedResourceTotal = redTemp1 + redTemp2;

    }


    public void SpawnBuildings(int Fbuildings, int Rbuildings)
    {
        Instantiate(blueRallyPoint as GameObject, new Vector3(-4.5f, 0, 0), Quaternion.identity);
        Instantiate(redRallyPoint as GameObject, new Vector3(4.5f, 0, 0), Quaternion.identity);

        //Spawns Factory Buildings
        for (int i = 0; i < 1; i++)
        {
            if(Fbuildings == 2)
            {
                Instantiate(factoryBuildingBlue as GameObject, blueSpawnPos[2], Quaternion.identity);
                Instantiate(factoryBuildingBlue as GameObject, blueSpawnPos[3], Quaternion.identity);

                Instantiate(factoryBuildingRed as GameObject, redSpawnPos[2], Quaternion.identity);
                Instantiate(factoryBuildingRed as GameObject, redSpawnPos[3], Quaternion.identity);
            }
            else if(Fbuildings < 2)
            {
                Instantiate(factoryBuildingBlue as GameObject, blueSpawnPos[3], Quaternion.identity);

                Instantiate(factoryBuildingRed as GameObject, redSpawnPos[3], Quaternion.identity);
            }

        }


        //Spawns Resource Buildings
        for (int i = 0; i < 1; i++)
        {
            if (Rbuildings == 2)
            {
                Instantiate(resourceBuildingBlue as GameObject, blueSpawnPos[0], Quaternion.identity);
                Instantiate(resourceBuildingBlue as GameObject, blueSpawnPos[1], Quaternion.identity);

                Instantiate(resourceBuildingRed as GameObject, redSpawnPos[0], Quaternion.identity);
                Instantiate(resourceBuildingRed as GameObject, redSpawnPos[1], Quaternion.identity);
            }
            else if (Rbuildings < 2)
            {
                Instantiate(resourceBuildingBlue as GameObject, blueSpawnPos[1], Quaternion.identity);

                Instantiate(resourceBuildingRed as GameObject, redSpawnPos[1], Quaternion.identity);
            }

        }

        buildings.AddRange(GameObject.FindGameObjectsWithTag("Factory Blue"));
        buildings.AddRange(GameObject.FindGameObjectsWithTag("Factory Red"));

        buildings.AddRange(GameObject.FindGameObjectsWithTag("Resource Blue"));
        buildings.AddRange(GameObject.FindGameObjectsWithTag("Resource Red"));

    }

}
