using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : FightingCharacter
{
    protected GameObject player;

    protected float delay;

    public abstract void move();

    public abstract void rotate();

    public override void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Level.CheckStatus();
            Destroy(gameObject);
            Destroy(this);
        }
    }

    
}
