using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    CanAttack whoAttacks;
    float delay; // delay = (1 / attackSpeed)
    float lastShot;
    float bulletSpeed;

    public float BulletSpeed { get => bulletSpeed; }

    public Weapon()
    {
        lastShot = 0;
        bulletSpeed = 5;
        delay = 0.5f;
    }
    public void SetAttacker(CanAttack whoAttacks)
    {
        this.whoAttacks = whoAttacks;
    }

    public void CheckShot()
    {

        if (Time.time > lastShot + delay)
        {
            Shooting.Shoot(whoAttacks);
            lastShot = Time.time;
        }
    }
}
