using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Unit_RadiusCheck_Blue : MonoBehaviour
{
    public GameObject thisUnit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee Unit Red"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = true;
        }
        else if (other.CompareTag("Ranged Unit Red"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = true;
        }
        else if (other.CompareTag("Wizard Unit"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Melee Unit Red"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = true;
        }
        else if (other.CompareTag("Ranged Unit Red"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = true;
        }
        else if (other.CompareTag("Wizard Unit"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Melee Unit Red"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = false;
        }
        else if (other.CompareTag("Ranged Unit Red"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = false;
        }
        else if (other.CompareTag("Wizard Unit"))
        {
            thisUnit.GetComponent<Melee_Unit_Blue>().radiusCheckContact = false;
        }
    }
}
