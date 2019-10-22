using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Unit : Unit
{
    public Melee_Unit(int xpos, int ypos, int health, int speed, int attack, int attackRange, string symbol, string team)
    {
        Xpos = xpos;
        Ypos = ypos;
        Health = health;
        base.maxHealth = health;
        Speed = speed;
        Attack = attack;
        AttackRange = attackRange;
        Symbol = symbol;
        Team = team;
    }

    public int Xpos
    {
        get { return base.xPos; }
        set { base.xPos = value; }
    }

    public int Ypos
    {
        get { return base.yPos; }
        set { base.yPos = value; }
    }

    public int Health
    {
        get { return base.health; }
        set { base.health = value; }
    }

    public int MaxHealth
    {
        get { return base.maxHealth; }
    }

    public int Speed
    {
        get { return base.speed; }
        set { base.speed = value; }
    }

    public int Attack
    {
        get { return base.attack; }
        set { base.attack = value; }
    }

    public int AttackRange
    {
        get { return base.attackRange; }
        set { base.attackRange = value; }
    }

    public string Symbol
    {
        get { return base.symbol; }
        set { base.symbol = value; }
    }

    public string Team
    {
        get { return base.team; }
        set { base.team = value; }
    }

    public bool IsAttacking
    {
        get { return base.isAttacking; }
        set { isAttacking = value; }
    }

    public bool IsDead { get; set; }





    public override (Unit, int) ClosestUnit(List<Unit> units)
    {
        throw new System.NotImplementedException();
    }

    public override void Combat(Unit attacker)
    {
        throw new System.NotImplementedException();
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }

    public override void Move(int dir)
    {
        throw new System.NotImplementedException();
    }

    public override bool Range(Unit other)
    {
        throw new System.NotImplementedException();
    }
}
