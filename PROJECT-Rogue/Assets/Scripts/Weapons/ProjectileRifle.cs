using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRifle : Projectile
{
    protected override void Shoot()
    {
        var obj = Object.Instantiate(Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjRiffle|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
        Vector2 ownerVelocity = (whoAttacks.GetComponent<Rigidbody2D>().velocity / 5);
        obj.GetComponent<Bullet>().SetParameters(whoAttacks, damage * whoAttacks.Damage, bulletSpeed, 1, ownerVelocity, (whoAttacks.Range * 0.8f)/ 10);
    }
    public ProjectileRifle(float attackSpeed = 10, float damage = 15, float bulletSpeed = 20, float bulletSize = 1) : base(attackSpeed, damage, bulletSpeed, bulletSize) { }
}
