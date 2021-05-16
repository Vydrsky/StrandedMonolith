using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightingCharacter : Character
{
    public Transform firePoint;

    [SerializeField] protected int maxHealth;     //do ustawieniea w inspektorze dla debugowania 
    [SerializeField] protected int healthPoints;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected float shotSpeed;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float Damage { get { return damage; } set { damage = value; } }
    public float Range { get { return range; } set { range = value; } }
    public float ShotSpeed { get { return shotSpeed; } set { shotSpeed = value; } }

    public abstract void TakeDamage(float damage);  
}
