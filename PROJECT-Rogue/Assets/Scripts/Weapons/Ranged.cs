using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ranged : Weapon
{
    public FightingCharacter WhoAttacks { get; set; }
    public float AttackSpeed { get; set; }
    public int Damage { get; set; }

    public override void Attack()
    {
        Shoot();
    }
    public abstract void Shoot();

}
