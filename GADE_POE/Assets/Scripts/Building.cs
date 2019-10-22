using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    int xPosition;
    int yPosition;
    int health;
    int maxHealth;
    int team;
    string symbol;

    public abstract void Death();

}
