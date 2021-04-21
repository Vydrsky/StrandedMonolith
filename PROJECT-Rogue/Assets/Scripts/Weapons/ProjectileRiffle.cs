using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRiffle : Projectile
{
    public override void Shoot()
    {
        Shooting.instance.ProjectileRiffleShot(whoAttacks);
    }
}
