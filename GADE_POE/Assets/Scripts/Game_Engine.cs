using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Engine : MonoBehaviour
{
    public bool startGame = false;
    public bool endGame = false;
    public bool mainMenu = false;
    public float endScore = 0;

    public string victorTeam;
    public TextMeshProUGUI victoryTextBox;

    public float currentBlueTeamScore = 0;
    public float currentRedTeamScore = 0;


    //public List<GameObject> units;
    public GameObject[] blueRallyPoints;
    public GameObject[] redRallyPoints;
    public GameObject[] meleeBlueUnit;
    public GameObject[] rangedBlueUnit;
    public GameObject[] meleeRedUnit;
    public GameObject[] rangedRedUnit;
    public GameObject[] wizardUnits;
    public GameObject[] tempFactoryBluebuildings;
    public GameObject[] tempFactoryRedbuildings;
    public GameObject[] tempResourceBluebuildings;
    public GameObject[] tempResourceRedbuildings;
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

    public bool blueBuildingUnderAttack = false;
    public bool redBuildingUnderAttack = false;

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

    public GameObject wizardUnit;
    public GameObject potionPrefab;
    public float potionThrowInterval = 3;
    public float wizardSpawnInterval = 14;
    float wizardSpawnCheck = 0;

    public Transform[] wizardSpawnPos;

    public List<Vector3> blueSpawnPos;
    public List<Vector3> redSpawnPos;

    public GameObject blueRallyPoint;
    public GameObject redRallyPoint;

  

    // Start is called before the first frame update
    void Start()
    {
        endScore = 100;
        wizardSpawnCheck = wizardSpawnInterval;
        startGame = false;
        mainMenu = true;
    }

    public void StartGame()
    {
        endScore = 100;
        wizardSpawnCheck = wizardSpawnInterval;
        currentTime = 0;
        nextWave = 0;
        startGame = true;
        mainMenu = false;

        if (numOfResourceBuildingsPerTeam > 2 || numOfFactoryBuildingsPerTeam > 2)
        {

        }
        else
        {
            SpawnBuildings(numOfFactoryBuildingsPerTeam, numOfResourceBuildingsPerTeam);
        }

        AssignResourceBuildingInstance();
    }


    public void MainMenu()
    {
        mainMenu = true;
        startGame = false;
        endGame = false;

        currentBlueTeamScore = 0;
        currentRedTeamScore = 0;

        DestroyAllUnits();
    }


    void DestroyAllUnits()
    {
        tempFactoryBluebuildings = GameObject.FindGameObjectsWithTag("Factory Blue");
        tempFactoryRedbuildings = GameObject.FindGameObjectsWithTag("Factory Red");
        tempResourceBluebuildings = GameObject.FindGameObjectsWithTag("Resource Blue");
        tempResourceRedbuildings = GameObject.FindGameObjectsWithTag("Resource Red");

        blueRallyPoints = GameObject.FindGameObjectsWithTag("Blue Rally Point");
        redRallyPoints = GameObject.FindGameObjectsWithTag("Red Rally Point");

        meleeBlueUnit = GameObject.FindGameObjectsWithTag("Melee Unit Blue");
        rangedBlueUnit = GameObject.FindGameObjectsWithTag("Ranged Unit Blue");

        meleeRedUnit = GameObject.FindGameObjectsWithTag("Melee Unit Red");
        rangedRedUnit = GameObject.FindGameObjectsWithTag("Ranged Unit Red");

        wizardUnits = GameObject.FindGameObjectsWithTag("Wizard Unit");

        for(int i = 0; i < tempFactoryBluebuildings.Length; i++)
        {
            Destroy(tempFactoryBluebuildings[i]);
        }

        for (int i = 0; i < tempFactoryRedbuildings.Length; i++)
        {
            Destroy(tempFactoryRedbuildings[i]);
        }

        for (int i = 0; i < tempResourceBluebuildings.Length; i++)
        {
            Destroy(tempResourceBluebuildings[i]);
        }

        for (int i = 0; i < tempResourceRedbuildings.Length; i++)
        {
            Destroy(tempResourceRedbuildings[i]);
        }

        for (int i = 0; i < meleeBlueUnit.Length; i++)
        {
            Destroy(meleeBlueUnit[i]);
        }

        for (int i = 0; i < meleeRedUnit.Length; i++)
        {
            Destroy(meleeRedUnit[i]);
        }

        for (int i = 0; i < rangedBlueUnit.Length; i++)
        {
            Destroy(rangedBlueUnit[i]);
        }

        for (int i = 0; i < rangedRedUnit.Length; i++)
        {
            Destroy(rangedRedUnit[i]);
        }

        for (int i = 0; i < wizardUnits.Length; i++)
        {
            Destroy(wizardUnits[i]);
        }

        for(int i = 0; i < blueRallyPoints.Length; i++)
        {
            Destroy(blueRallyPoints[i]);
        }

        for (int i = 0; i < redRallyPoints.Length; i++)
        {
            Destroy(redRallyPoints[i]);
        }

        buildings.Clear();
    }


    public void EndGame(string victoryTeam)
    {
        startGame = false;
        mainMenu = false;
        endGame = true;

        victorTeam = victoryTeam;

        if(victoryTeam == "Blue Won")
        {
            victoryTextBox.text = "Victory \n -Blue Team-";
        }
        if(victoryTeam == "Red Won")
        {
            victoryTextBox.text = "Victory \n -Red Team-";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(startGame == true)
        {
            currentTime += Time.deltaTime;

            TotalNumOfResourceCheck();

            TotalNumOfUnitsInGame();

            TotalNumOfBuildingsInGame();

            if(currentBlueTeamScore >= endScore)
            {
                EndGame("Blue Won");
            }
            if(currentRedTeamScore >= endScore)
            {
                EndGame("Red Won");
            }

            if (currentTime >= nextWave)
            {
                //SpawnUnitsAtBuilding
                nextWave += nextWaveInterval;

                //I want this to call the spawn method of all the factory buildings in the game
                foreach (GameObject gO in Building)
                {
                    if (gO != null)
                    {
                        if (gO.tag == "Factory Blue" || gO.tag == "Factory Red")
                        {
                            gO.GetComponent<Factory_Building_Controller>().SpawnUnitsAtBuilding();
                        }
                    }

                }
            }

            if (currentTime >= wizardSpawnCheck)
            {
                wizardSpawnCheck += wizardSpawnInterval;
                SpawnWizardUnits();
            }
        }
        

    }


    private void SpawnWizardUnits()
    {
        Instantiate(wizardUnit, wizardSpawnPos[Random.Range(0, 3)].position, Quaternion.identity);
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


    void TotalNumOfBuildingsInGame()
    {
        tempFactoryBluebuildings = GameObject.FindGameObjectsWithTag("Factory Blue");
        tempFactoryRedbuildings = GameObject.FindGameObjectsWithTag("Factory Red");
        tempResourceBluebuildings = GameObject.FindGameObjectsWithTag("Resource Blue");
        tempResourceRedbuildings = GameObject.FindGameObjectsWithTag("Resource Red");

        if(tempFactoryBluebuildings == null)
        {
            EndGame("Red Won");
        }
        if(tempResourceBluebuildings == null)
        {
            EndGame("Red Won");
        }
        if(tempFactoryRedbuildings == null)
        {
            EndGame("Blue Won");
        }
        if (tempResourceRedbuildings == null)
        {
            EndGame("Blue Won");
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
            if(gO != null)
            {
                if (gO.tag == "Resource Blue" || gO.tag == "Resource Red")
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
  
            
        }

        numOfBlueResourceTotal = blueTemp1 + blueTemp2;
        numOfRedResourceTotal = redTemp1 + redTemp2;

    }


    void TotalNumOfUnitsInGame()
    {
        meleeBlueUnit = GameObject.FindGameObjectsWithTag("Melee Unit Blue");
        rangedBlueUnit = GameObject.FindGameObjectsWithTag("Ranged Unit Blue");

        meleeRedUnit = GameObject.FindGameObjectsWithTag("Melee Unit Red");
        rangedRedUnit = GameObject.FindGameObjectsWithTag("Ranged Unit Red");

        wizardUnits = GameObject.FindGameObjectsWithTag("Wizard Unit");
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
