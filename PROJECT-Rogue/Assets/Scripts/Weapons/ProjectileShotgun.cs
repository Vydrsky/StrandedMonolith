using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShotgun : Projectile
{
    public override void Shoot()
    {
        float random;
        float spread = 1 / 2.0f;
        GameObject bullet = Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjShotgun|"));
        Vector3 rotationVector;
        for (int i = -5; i <= 5; i++)
        {
            random = Random.Range(-1.0f, 1.0f);
            rotationVector = whoAttacks.transform.rotation.eulerAngles;
            rotationVector.z += i / spread;
            var obj = Object.Instantiate(bullet, whoAttacks.firePoint.position, Quaternion.Euler(rotationVector));
            obj.GetComponent<Bullet>().SetParameters(whoAttacks, damage + whoAttacks.Damage, bulletSpeed + random, 1);
        }
    }

    public ProjectileShotgun(float attackSpeed = 1, float damage = 5, float bulletSpeed = 3, float bulletSize = 1) : base(attackSpeed, damage, bulletSpeed, bulletSize)
    {

    }
}
