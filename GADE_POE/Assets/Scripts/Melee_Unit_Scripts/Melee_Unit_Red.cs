using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Melee_Unit_Red : MonoBehaviour
{
    GameObject gameManager;
    public GameObject nearestObj;
    Animator anim;

    public Image healthBar;

    public int speed = 2;
    public float health;
    int maxHealth = 100;
    float attack = 20;
    float attackRange = 2f;
    public float distance = 0;

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

        healthBar.fillAmount = health / maxHealth;

        if (health < 30)
        {
            RunAway();

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            anim.SetBool("canAttack", false);
        }
        else if (health >= 30)
        {
            MoveTowardsEnemy();

            if (nearestObj != null)
            {
                transform.LookAt(nearestObj.transform, Vector2.up);

                distance = Vector3.Distance(nearestObj.transform.position, this.transform.position);

                if (Vector3.Distance(nearestObj.transform.position, this.transform.position) > attackRange)
                {
                    radiusCheckContact = false;
                }
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

                //if (nearestObj.transform.position.x - this.transform.position.x > attackRange)
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
        GameObject[] blueEnemies = GameObject.FindGameObjectsWithTag("Melee Unit Blue");
        GameObject[] rangedBlueEnemies = GameObject.FindGameObjectsWithTag("Ranged Unit Blue");
        GameObject[] wizardUnits = GameObject.FindGameObjectsWithTag("Wizard Unit");

        if (blueEnemies != null)
        {
            foreach (GameObject blueGO in blueEnemies)
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
        if (rangedBlueEnemies != null)
        {
            foreach (GameObject blueGO in rangedBlueEnemies)
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

            Debug.Log("Damaged Red");

            if (health <= 0 && this.gameObject != null)
            {
                gameManager.GetComponent<Game_Engine>().currentBlueTeamScore++;
            }
        }
        else if (other.gameObject.CompareTag("Blue Arrow"))
        {
            health -= 15;

            Debug.Log("Damaged Red Melee Unit With Arrow");

            if (health <= 0 && this.gameObject != null)
            {
                gameManager.GetComponent<Game_Engine>().currentBlueTeamScore++;
            }
        }
        else if (other.gameObject.CompareTag("Wizard Projectile"))
        {
            health -= 25;
        }
    }


}
