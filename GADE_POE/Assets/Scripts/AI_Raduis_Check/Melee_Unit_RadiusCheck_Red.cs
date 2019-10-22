using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Unit_RadiusCheck_Red : MonoBehaviour
{
    public GameObject thisUnit;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Melee Unit Blue"))
        {
            thisUnit.GetComponent<Melee_Unit_Red>().radiusCheckContact = true;
        }
        else if (other.CompareTag("Ranged Unit Blue"))
        {
            thisUnit.GetComponent<Melee_Unit_Red>().radiusCheckContact = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Melee Unit Blue"))
        {
            thisUnit.GetComponent<Melee_Unit_Red>().radiusCheckContact = true;
        }
        else if (other.CompareTag("Ranged Unit Blue"))
        {
            thisUnit.GetComponent<Melee_Unit_Red>().radiusCheckContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Melee Unit Blue"))
        {
            thisUnit.GetComponent<Melee_Unit_Red>().radiusCheckContact = false;
        }
        else if (other.CompareTag("Ranged Unit Blue"))
        {
            thisUnit.GetComponent<Melee_Unit_Red>().radiusCheckContact = false;
        }
    }
}
