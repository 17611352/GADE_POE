using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Unit_Blue : MonoBehaviour
{
    GameObject gameManager;
    GameObject radiusCheckObject;
    public GameObject nearestObj;
    Animator anim;

    public int speed = 2;
    public float health;
    int maxHealth = 100;
    float attack = 20;
    float attackRange = 0.7f;

    bool isAttacking = false;
    public bool radiusCheckContact = false;

    GameObject runToObject = null;
    int random = 0;
    int check = 0;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        anim = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();

        if(health < 30)
        {
            RunAway();

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            anim.SetBool("canAttack", false);
        }
        else if(health >= 30)
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

                if (radiusCheckContact == false)
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    anim.SetBool("canAttack", false);
                }
                else if (radiusCheckContact == true)
                {
                    anim.SetBool("canAttack", true);
                }

                //if(nearestObj.transform.position.x - this.transform.position.x > attackRange)
                //{
                //    radiusCheckContact = false;
                //}

            }
            else
            {
                anim.SetBool("canAttack", false);
                radiusCheckContact = false;
            }

        }

    }

    void MoveTowardsEnemy()
    {
        float nearestDist = float.MaxValue;
        GameObject[] redEnemies = GameObject.FindGameObjectsWithTag("Melee Unit Red");
        GameObject[] rangedRedEnemies = GameObject.FindGameObjectsWithTag("Ranged Unit Red");

        if (redEnemies != null)
        {
            foreach (GameObject redGO in redEnemies)
            {
                float distanceToEnemy = (redGO.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < nearestDist)
                {
                    nearestDist = Vector2.Distance(transform.position, redGO.transform.position);
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
                    nearestDist = Vector2.Distance(transform.position, redGO.transform.position);
                    nearestObj = redGO;
                }

            }

            if (nearestObj != null)
            {
                Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
            }
        }



        //foreach (GameObject gO in gameManager.GetComponent<Game_Engine>().units)
        //{
        //    if(gO.tag == "Melee Unit Red")
        //    {
        //        enemyTeam.Add(gO);
        //    }
        //}


        //for(int i = 0; i < enemyTeam.Count; i++)
        //{
        //    Vector2 enemyPos = enemyTeam[i].transform.position;

        //    float distance = Vector2.Distance(transform.position, enemyPos);

        //    distanceArray = new float[enemyTeam.Count];
        //    distanceArray[i] = distance;

        //}


        //for(int i = 0; i < distanceArray.GetLength(0); i++)
        //{
        //    if(distanceArray[i] < min)
        //    {
        //        min = distanceArray[i];
        //        enemyPosInArray++;
        //    }
        //}

        //transform.LookAt(enemyTeam[enemyPosInArray].transform);
        //float distanceCheck = Vector2.Distance(transform.position, enemyTeam[enemyPosInArray].transform.position);


    }


    private void RunAway()
    {
        runToObject = GameObject.FindGameObjectWithTag("Blue Rally Point");
        transform.LookAt(runToObject.transform, Vector2.up);

        health += 0.01f;
    }


    private void DeathCheck()
    {
        if(health <= 0 && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword Red"))
        {
            //Subtract the universal sword damage from current health
            health -= 20;

            Debug.Log("Damaged Blue");
        }
    }
}
