using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShotgun : Projectile
{
    public override void Shoot()
    {
        Shooting.instance.ProjectileShotgunShot(whoAttacks);
    }

}
