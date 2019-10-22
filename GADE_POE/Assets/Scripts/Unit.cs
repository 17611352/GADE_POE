using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit
{
    protected int xPos;
    protected int yPos;
    protected int health;
    protected int maxHealth;
    protected int speed;
    protected int attack;
    protected int attackRange;
    protected string team;
    protected string symbol;
    protected bool isAttacking;
    protected bool isDead;


    public abstract void Move(int dir);

    public abstract void Combat(Unit attacker);

    public abstract bool Range(Unit other);

    public abstract (Unit, int) ClosestUnit(List<Unit> units);

    public abstract void Death();
}
