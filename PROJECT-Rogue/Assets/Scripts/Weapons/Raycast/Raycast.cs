using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Raycast : Ranged
{
    LineRenderer lineRenderer;
    protected override void Shoot() { whoAttacks.StartCoroutine(ShootRaycast()); }
    protected abstract IEnumerator ShootRaycast();
    //public Raycast(float attackSpeed = 10, int damage = 20) : base(attackSpeed, damage) { }

    public Raycast(WeaponStats weaponStats) : base(weaponStats)
    {
        
    }
}
