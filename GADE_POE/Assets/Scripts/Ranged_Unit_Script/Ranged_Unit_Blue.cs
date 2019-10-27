using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Unit_Blue : MonoBehaviour
{
    GameObject gameManager;
    public GameObject nearestObj;

    public GameObject arrowSpawnPos;

    public int speed = 2;
    public float health;
    int maxHealth = 80;
    float attack = 15;
    float attackRange = 2.5f;

    float arrowFireInterval = 2;
    public float currentTime = 0;
    public GameObject arrowInChamber;
    public bool canFire = false;
    int arrowCheck = 0;

    bool canAttack = false;

    GameObject runToObject = null;
    int random = 0;
    int check = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");

        health = maxHealth;

        SpawnArrowPrefab();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        DeathCheck();

       
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

            //if (nearestObj.transform.position.x - this.transform.position.x > attackRange)
            //{
            //    radiusCheckContact = false;
            //}
            arrowCheck = 0;
        }
        else if(nearestObj == null && canAttack == true)
        {
            currentTime = 0;
            canFire = false;

            if (arrowCheck == 0)
            {
                SpawnArrowPrefab();
                arrowCheck++;
            }

        }


        if (currentTime >= gameManager.GetComponent<Game_Engine>().arrowFireInterval)
        {
            currentTime = 0;
            canFire = true;
            SpawnArrowPrefab();
        }

        

    }

    void MoveTowardsEnemy()
    {
        float nearestDist = float.MaxValue;
        GameObject[] redEnemies = GameObject.FindGameObjectsWithTag("Melee Unit Red");
        GameObject[] rangedRedEnemies = GameObject.FindGameObjectsWithTag("Ranged Unit Red");
        GameObject[] wizardUnits = GameObject.FindGameObjectsWithTag("Wizard Unit");

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
        if (wizardUnits != null)
        {
            foreach (GameObject wizardGO in wizardUnits)
            {
                float distanceToEnemy = (wizardGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector3.Distance(transform.position, wizardGO.transform.position);
                    nearestObj = wizardGO;
                }

            }

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }

        }

    }


    private void SpawnArrowPrefab()
    {
        GameObject arrow = gameManager.GetComponent<Game_Engine>().BlueArrowPrefab;

        Quaternion q = new Quaternion(0, 0, 0, 0);

        Instantiate(arrow, arrowSpawnPos.transform.position, q, arrowSpawnPos.transform);

        arrowInChamber = arrow;

        Debug.Log("Spawned Arrow");
    }


    private void RunAway()
    {
        runToObject = GameObject.FindGameObjectWithTag("Blue Rally Point");
        transform.LookAt(runToObject.transform, Vector2.up);

        health += 0.01f;
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
        if (other.gameObject.CompareTag("Sword Red"))
        {
            //Subtract the universal Sword damage from current health
            health -= 20;

            Debug.Log("Damaged Ranged Blue");
        }
        else if (other.gameObject.CompareTag("Red Arrow"))
        {
            health -= 15;

            Debug.Log("Damaged Blue Ranged Unit With Arrow");
        }
        else if (other.gameObject.CompareTag("Wizard Projectile"))
        {
            health -= 25;
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
