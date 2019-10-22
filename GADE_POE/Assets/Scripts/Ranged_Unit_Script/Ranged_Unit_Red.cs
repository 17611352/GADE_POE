using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Unit_Red : MonoBehaviour
{
    GameObject gameManager;
    public GameObject nearestObj;

    public GameObject arrowSpawnPos;

    public int speed = 2;
    public float health;
    int maxHealth = 80;
    float attack = 15;
    float attackRange = 2.5f;
    bool canAttack = false;

    float arrowFireInterval = 2;
    public float currentTime = 0;
    public GameObject arrowInChamber;
    public bool canFire = false;
    int arrowCheck = 0;

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

        if (health < 30)
        {
            //RunAway();

            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (health >= 30)
        {
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

                if (Vector2.Distance(nearestObj.transform.position, this.transform.position) > attackRange)
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    canAttack = false;
                }
                else if (Vector2.Distance(nearestObj.transform.position, this.transform.position) <= attackRange)
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
            else if (nearestObj == null && canAttack == true)
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

    }

    void MoveTowardsEnemy()
    {
        float nearestDist = float.MaxValue;
        GameObject[] blueEnemies = GameObject.FindGameObjectsWithTag("Melee Unit Blue");
        GameObject[] rangedBlueEnemies = GameObject.FindGameObjectsWithTag("Ranged Unit Blue");

        if (blueEnemies != null)
        {
            foreach (GameObject blueGO in blueEnemies)
            {
                float distanceToEnemy = (blueGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector2.Distance(transform.position, blueGO.transform.position);
                    nearestObj = blueGO;
                }

            }

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }

        }
        if (rangedBlueEnemies != null)
        {
            foreach (GameObject blueGO in rangedBlueEnemies)
            {
                float distanceToEnemy = (blueGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector2.Distance(transform.position, blueGO.transform.position);
                    nearestObj = blueGO;
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
        GameObject arrow = gameManager.GetComponent<Game_Engine>().RedArrowPrefab;

        Quaternion q = new Quaternion(0, 0, 0, 0);

        Instantiate(arrow, arrowSpawnPos.transform.position, q, arrowSpawnPos.transform);

        arrowInChamber = arrow;

        Debug.Log("Spawned Arrow");
    }


    private void RunAway()
    {
        runToObject = GameObject.FindGameObjectWithTag("Red Rally Point");
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
        if (other.gameObject.CompareTag("Sword Blue"))
        {
            //Subtract the universal sword damage from current health
            health -= 20;

            Debug.Log("Damaged Ranged Red");
        }
    }
}
