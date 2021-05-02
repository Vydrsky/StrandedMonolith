using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Ranged
{
    protected float bulletSize;
    protected float bulletSpeed;
    public Projectile(float attackSpeed, int damage, float bulletSpeed, float bulletSize) : base(attackSpeed, damage)
    {
        this.bulletSpeed = bulletSpeed;
        this.bulletSize = bulletSize;
    }
}
