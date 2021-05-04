using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSniperRifle : Projectile
{
    protected override void Shoot()
    {
        var obj = Object.Instantiate(Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjSniper|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
        Vector2 ownerVelocity = (whoAttacks.GetComponent<Rigidbody2D>().velocity / 10);
        obj.GetComponent<Bullet>().SetParameters(whoAttacks, damage * whoAttacks.Damage, bulletSpeed, 1, ownerVelocity, (whoAttacks.Range * 1.5f) / 10);
    }
    public ProjectileSniperRifle(float attackSpeed = 0.4f, int damage = 50, float bulletSpeed = 30, float bulletSize = 1) : base(attackSpeed, damage, bulletSpeed, bulletSize) { }
}