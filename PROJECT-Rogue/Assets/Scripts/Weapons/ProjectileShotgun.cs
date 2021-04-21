using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShotgun : Projectile
{
    public override void Shoot()
    {
        float spread = 1 / 2.0f;
        GameObject bullet = Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjShotgun|"));
        Vector3 rotationVector;
        for (int i = -5; i <= 5; i++)
        {
            rotationVector = whoAttacks.transform.rotation.eulerAngles;
            rotationVector.z += i / spread;
            var obj = Object.Instantiate(bullet, whoAttacks.firePoint.position, Quaternion.Euler(rotationVector));
            obj.GetComponent<Bullet>().SetAttacker(whoAttacks);
        }
    }

}
