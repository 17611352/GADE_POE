using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard_Unit_Controller : MonoBehaviour
{
    GameObject gameManager;
    public GameObject nearestObj;

    public Image healthBar;

    public GameObject potionSpawnPos;
    public GameObject fallBackPos;

    public int speed = 2;
    public float health;
    int maxHealth = 50;
    float attack = 25;
    float attackRange = 3.5f;
    public float distance = 0;

    float potionFireInterval = 2;
    public float currentTime = 0;

    public bool canAttack = false;
    public bool impulseCategoryCheck = false;

    GameObject runToObject = null;
    int random = 0;
    int check = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        DeathCheck();

        healthBar.fillAmount = health / maxHealth;

        MoveTowardsEnemy();

        if (nearestObj != null)
        {
            transform.LookAt(nearestObj.transform, Vector2.up);

            //if (Vector2.Distance(transform.position, nearestObj.transform.position) > attackRange)
            //{
            //    transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //    anim.SetBool("canAttack", false);
            //}
            //else if (Vector2.Distance(transform.position, nearestObj.transform.position) <= attackRange)
            //{
            //    anim.SetBool("canAttack", true);
            //}

            if (Vector3.Distance(nearestObj.transform.position, this.transform.position) > attackRange)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                canAttack = false;
            }
            else if (Vector3.Distance(nearestObj.transform.position, this.transform.position) <= attackRange)
            {
                //Attack
                canAttack = true;
            }

            if(distance < 2.5f && canAttack == true)
            {
                //I used this bool as a way to check which impulse force to use according to the givan specifications
                impulseCategoryCheck = true;
            }
            if(distance >= 3.5f && canAttack == true)
            {
                //I used this bool as a way to check which impulse force to use according to the givan specifications
                impulseCategoryCheck = false;
            }
            else if(distance >= 2.5f && distance <3.5f && canAttack == true)
            {
                //I used this bool as a way to check which impulse force to use according to the givan specifications
                impulseCategoryCheck = false;
            }

            //if (nearestObj.transform.position.x - this.transform.position.x > attackRange)
            //{
            //    radiusCheckContact = false;
            //}
        }


        if (currentTime >= gameManager.GetComponent<Game_Engine>().potionThrowInterval && canAttack == true)
        {
            currentTime = 0;
                
            SpawnPotionPrefab();
        }


        

        distance = Vector3.Distance(nearestObj.transform.position, this.transform.position);

    }

    void MoveTowardsEnemy()
    {
        float nearestDist = float.MaxValue;
        GameObject[] redEnemies = GameObject.FindGameObjectsWithTag("Melee Unit Red");
        GameObject[] rangedRedEnemies = GameObject.FindGameObjectsWithTag("Ranged Unit Red");

        GameObject[] blueFactoryBuildings = GameObject.FindGameObjectsWithTag("Factory Blue");
        GameObject[] redFactoryBuildings = GameObject.FindGameObjectsWithTag("Factory Red");
        GameObject[] blueResourceBuildings = GameObject.FindGameObjectsWithTag("Resource Blue");
        GameObject[] redResourceBuildings = GameObject.FindGameObjectsWithTag("Resource Red");

        if(blueFactoryBuildings != null)
        {
            foreach (GameObject blueGO in blueFactoryBuildings)
            {
                float distanceToEnemy = (blueGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector3.Distance(transform.position, blueGO.transform.position);
                    nearestObj = blueGO;
                }

            }

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }
        }
        if(blueResourceBuildings != null)
        {
            foreach (GameObject blueGO in blueResourceBuildings)
            {
                float distanceToEnemy = (blueGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector3.Distance(transform.position, blueGO.transform.position);
                    nearestObj = blueGO;
                }

            }

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }
        }
        if(redFactoryBuildings != null)
        {
            foreach (GameObject redGO in redFactoryBuildings)
            {
                float distanceToEnemy = (redGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector3.Distance(transform.position, redGO.transform.position);
                    nearestObj = redGO;
                }

            } 

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }
        }
        if(redResourceBuildings != null)
        {
            foreach (GameObject redGO in redResourceBuildings)
            {
                float distanceToEnemy = (redGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector3.Distance(transform.position, redGO.transform.position);
                    nearestObj = redGO;
                }

            }

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }
        }
        if (redEnemies != null)
        {
            foreach (GameObject redGO in redEnemies)
            {
                float distanceToEnemy = (redGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector3.Distance(transform.position, redGO.transform.position);
                    nearestObj = redGO;
                }

            }

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }

        }
        if (rangedRedEnemies != null)
        {
            foreach (GameObject redGO in rangedRedEnemies)
            {
                float distanceToEnemy = (redGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector3.Distance(transform.position, redGO.transform.position);
                    nearestObj = redGO;
                }

            }

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }
        }

    }


    private void SpawnPotionPrefab()
    {
        if(impulseCategoryCheck == false)
        {
            GameObject potion = gameManager.GetComponent<Game_Engine>().potionPrefab;

            potion.GetComponent<Wizard_Projectile>().impulseForce = 4;
            Instantiate(potion, potionSpawnPos.transform.position, potionSpawnPos.transform.rotation);


        }
        else if(impulseCategoryCheck == true)
        {
            GameObject potion = gameManager.GetComponent<Game_Engine>().potionPrefab;

            potion.GetComponent<Wizard_Projectile>().impulseForce = 2;
            Instantiate(potion, potionSpawnPos.transform.position, potionSpawnPos.transform.rotation);

        }



        //arrowInChamber = potion;

        Debug.Log("Spawned Potion");
    }


    private void DeathCheck()
    {
        if (health <= 0 && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword Red") || other.gameObject.CompareTag("Sword Blue"))
        {
            //Subtract the universal Sword damage from current health
            health -= 20;

            Debug.Log("Damaged Wizard Unit");
        }
        else if (other.gameObject.CompareTag("Blue Arrow") || other.gameObject.CompareTag("Red Arrow"))
        {
            health -= 15;

            Debug.Log("Damaged Wizard Unit With Arrow");
        }
        //if(other.gameObject.CompareTag("Red Arrow"))
        //{
        //    //Subtract the universal Arrow damage from current health
        //    health -= 15;

        //    Destroy(other.gameObject);

        //    Debug.Log("Arrow Hit Blue");
        //}
    }
}
