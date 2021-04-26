using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Melee : Weapon
{
    protected abstract void MeleeHit();
    protected override void Attack()
    {
        MeleeHit();
    }
    public Melee(float attackSpeed, float damage) : base(attackSpeed, damage) { }

}
