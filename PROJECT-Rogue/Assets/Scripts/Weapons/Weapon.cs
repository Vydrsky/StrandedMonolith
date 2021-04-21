using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected FightingCharacter whoAttacks;
    protected float attackSpeed;
    protected int damage;
    float delay; // delay = (1 / attackSpeed)
    float lastShot;
    public abstract void Attack();

    public void SetAttacker(FightingCharacter whoAttacks)
    {
        this.whoAttacks = whoAttacks;
    }


    public Weapon()
    {
        lastShot = 0;
        delay = 0.5f;
    }
    public void CheckAttack()
    {
        if (Time.time > lastShot + delay)
        {
            Attack();
            lastShot = Time.time;
        }
    }
}
