using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSniperRiffle : Projectile
{
    public override void Shoot()
    {
        var obj = Object.Instantiate(Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjSniper|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
        Vector2 ownerVelocity = (whoAttacks.GetComponent<Rigidbody2D>().velocity / 5);
        obj.GetComponent<Bullet>().SetParameters(whoAttacks, damage + whoAttacks.Damage, bulletSpeed, 1, ownerVelocity);
    }

    public ProjectileSniperRiffle(float attackSpeed = 0.4f, float damage = 50, float bulletSpeed = 15, float bulletSize = 1) : base(attackSpeed, damage, bulletSpeed, bulletSize)
    {

    }
}
