using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : FightingCharacter
{
    public float timeToWait;
    protected GameObject player;
    private bool isTarget;
    protected AudioSource _audioSource;
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

    Weapon weapon; // Do wywalenie, tylko na czas testowania

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

    protected bool Wait()
    {
        if (Time.time > timeToWait)
        {
            return true;
        }
        return false;
    }

}
