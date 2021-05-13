using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : FightingCharacter
{
    protected GameObject player;
    private bool isTarget;
    public bool IsTarget
    {
        get
        {
            return isTarget;
        }
        set
        {
            isTarget = value;
            this.GetComponent<SpriteRenderer>().color=Color.blue;
            this.GetComponent<Transform>().localScale = new Vector2(this.transform.localScale.x*1.5f, this.transform.localScale.y*1.5f);
        }
    }

    Weapon weapon; // Do wywalenie, tylko na czas testowania xD

    protected float delay;

    public abstract void move();

    public abstract void rotate();

    public override void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Level.CheckStatus();
            if (IsTarget)
            {
                Level.staticGracz.GetComponent<Player>().JournalUpdate();
                IsTarget = false;
            }
            Destroy(gameObject);
            Destroy(this);
        }
    }

    
}
