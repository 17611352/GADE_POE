using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Unit_Controller : MonoBehaviour
{
    GameObject gameManager;
    public GameObject nearestObj;

    public GameObject potionSpawnPos;
    public GameObject fallBackPos;

    public int speed = 2;
    public float health;
    int maxHealth = 50;
    float attack = 25;
    float attackRange = 4f;
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

            if(distance < 3.5 && canAttack == true)
            {
                //I used this bool as a way to check which impulse force to use according to the givan specifications
                impulseCategoryCheck = true;
            }
            else if(distance >= 3.5 && canAttack == true)
            {
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
        //GameObject[] wizardUnits = GameObject.FindGameObjectsWithTag("Wizard Unit");

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
        //if (wizardUnits != null)
        //{
        //    foreach (GameObject wizardGO in wizardUnits)
        //    {
        //        float distanceToEnemy = (wizardGO.transform.position - this.transform.position).sqrMagnitude;

        //        if (distanceToEnemy < nearestDist)
        //        {
        //            nearestDist = Vector2.Distance(transform.position, wizardGO.transform.position);
        //            nearestObj = wizardGO;
        //        }

        //    }

        //    if (nearestObj != null)
        //    {
        //        Debug.DrawLine(transform.position, nearestObj.transform.position, Color.red);
        //    }

        //}

    }


    private void SpawnPotionPrefab()
    {
        if(impulseCategoryCheck == false)
        {
            GameObject potion = gameManager.GetComponent<Game_Engine>().potionPrefab;

            Quaternion q = new Quaternion(0, 0, 0, 0);
            Quaternion rot = new Quaternion(potionSpawnPos.transform.rotation.x - 0.7f, potionSpawnPos.transform.rotation.y, potionSpawnPos.transform.rotation.z, potionSpawnPos.transform.rotation.w);

            potion.GetComponent<Wizard_Projectile>().impulseForce = 5;
            Instantiate(potion, potionSpawnPos.transform.position, rot);


        }
        else if(impulseCategoryCheck == true)
        {
            GameObject potion = gameManager.GetComponent<Game_Engine>().potionPrefab;

            Quaternion q = new Quaternion(0, 0, 0, 0);
            Quaternion rot = new Quaternion(potionSpawnPos.transform.rotation.x - 0.7f, potionSpawnPos.transform.rotation.y, potionSpawnPos.transform.rotation.z, potionSpawnPos.transform.rotation.w);

            potion.GetComponent<Wizard_Projectile>().impulseForce = 2;
            Instantiate(potion, potionSpawnPos.transform.position, rot);

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
