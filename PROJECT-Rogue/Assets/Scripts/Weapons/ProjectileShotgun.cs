using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShotgun : Projectile
{
    protected override void Shoot()
    {
        float random;
        float spread = 1 / 2.0f;
        GameObject bullet = Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjShotgun|"));
        Vector3 rotationVector;
        for (int i = -5; i <= 5; i++)
        {
            random = Random.Range(-10.0f, 10.0f);
            rotationVector = whoAttacks.transform.rotation.eulerAngles;
            //rotationVector.z += (i + (random / 10)) / spread;  // troche losowe
            rotationVector.z += Random.Range(-5.0f, 5.0f) / spread; // totalnie losowe
            //rotationVector.z += i / spread; // totalnie nie losowe
            Vector2 ownerVelocity = (whoAttacks.GetComponent<Rigidbody2D>().velocity / 5);
            var obj = Object.Instantiate(bullet, whoAttacks.firePoint.position, Quaternion.Euler(rotationVector));
            obj.GetComponent<Bullet>().SetParameters(whoAttacks, damage + whoAttacks.Damage, bulletSpeed + (random / 15), 1, ownerVelocity);
        }
    }
    public ProjectileShotgun(float attackSpeed = 0.75f, float damage = 4, float bulletSpeed = 10, float bulletSize = 1) : base(attackSpeed, damage, bulletSpeed, bulletSize) { }
}
