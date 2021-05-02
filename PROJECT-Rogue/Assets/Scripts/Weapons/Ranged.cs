using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ranged : Weapon
{
    protected override void Attack() { Shoot(); }
    protected abstract void Shoot();

    public Ranged(float attackSpeed, int damage) : base(attackSpeed, damage) { }
}
