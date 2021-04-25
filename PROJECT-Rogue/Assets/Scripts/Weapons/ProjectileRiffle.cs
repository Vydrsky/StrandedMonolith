using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRiffle : Projectile
{
    public override void Shoot()
    {
        var obj = Object.Instantiate(Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjRiffle|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
        Vector2 ownerVelocity = (whoAttacks.GetComponent<Rigidbody2D>().velocity / 1);
        obj.GetComponent<Bullet>().SetParameters(whoAttacks, damage + whoAttacks.Damage, bulletSpeed, 1, ownerVelocity);
    }

    public ProjectileRiffle(float attackSpeed = 3, float damage = 15, float bulletSpeed = 8, float bulletSize = 1) : base(attackSpeed, damage, bulletSpeed, bulletSize)
    {

    }
}
