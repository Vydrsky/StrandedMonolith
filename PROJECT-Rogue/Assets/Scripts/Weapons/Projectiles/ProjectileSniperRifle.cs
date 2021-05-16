using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSniperRifle : Projectile
{
    protected override void Shoot()
    {
        var obj = Object.Instantiate(Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjSniper|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
        Vector2 ownerVelocity = CalculateOwnersVelocity((whoAttacks.GetComponent<Rigidbody2D>().velocity / 6), 2f, 3f);
        obj.GetComponent<Bullet>().SetParameters(whoAttacks, (int)damage * (int)whoAttacks.Damage, bulletSpeed, bulletSize, ownerVelocity, (whoAttacks.Range * rangeModifier) / 10);
    }
    //public ProjectileSniperRifle(float attackSpeed = 0.4f, int damage = 50, float bulletSpeed = 30, float bulletSize = 1) : base(attackSpeed, damage, bulletSpeed, bulletSize) { }

    public ProjectileSniperRifle(ProjectileWeaponStats projectileWeaponStats) : base(projectileWeaponStats)
    {

    }
}
