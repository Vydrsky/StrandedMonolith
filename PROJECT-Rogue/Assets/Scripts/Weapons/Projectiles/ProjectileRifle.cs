using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRifle : Projectile
{
    protected override void Shoot()
    {
        var obj = Object.Instantiate(Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjRiffle|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
        Vector2 ownerVelocity = CalculateOwnersVelocity((whoAttacks.GetComponent<Rigidbody2D>().velocity / 3), 1f, 4f);
        obj.GetComponent<Bullet>().SetParameters(whoAttacks, (int)damage * (int)whoAttacks.Damage, bulletSpeed, bulletSize, ownerVelocity, (whoAttacks.Range * rangeModifier) / 10);
    }

    public ProjectileRifle(ProjectileWeaponStats projectileWeaponStats) : base(projectileWeaponStats) { }
}
