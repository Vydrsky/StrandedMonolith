using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRiffle : Projectile
{
    public override void Shoot()
    {
        //Shooting.instance.ProjectileRiffleShot(whoAttacks);

            var obj = Object.Instantiate(Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjRiffle|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
            obj.GetComponent<Bullet>().SetAttacker(whoAttacks);
    }
}
