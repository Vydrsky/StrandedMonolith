using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRiffle : Projectile
{
    public override void Shoot()
    {
        var obj = Object.Instantiate(Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjRiffle|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
        obj.GetComponent<Bullet>().SetParameters(whoAttacks, damage + whoAttacks.Damage, bulletSpeed, 1);
    }

    public ProjectileRiffle(float attackSpeed = 1, float damage = 5, float bulletSpeed = 1, float bulletSize = 1) : base(attackSpeed, damage, bulletSpeed, bulletSize)
    {

    }
}
