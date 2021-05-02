using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected FightingCharacter whoAttacks;
    protected float attackSpeed;
    protected float damage;
    float delay; // delay = (1 / attackSpeed)
    float lastShot;
    protected abstract void Attack();
    public void SetAttacker(FightingCharacter whoAttacks) { this.whoAttacks = whoAttacks; }
    public Weapon(float attackSpeed, float damage)
    {
        this.attackSpeed = attackSpeed;
        this.damage = damage;
        lastShot = Time.time + 0.5f;
        delay = 0.5f;
    }
    public void CheckAttack()
    {
        delay = (1 / (attackSpeed + whoAttacks.AttackSpeed)); 
        if(Time.time > lastShot + delay)
        {
            Attack();
            lastShot = Time.time;
        }
    }
}
