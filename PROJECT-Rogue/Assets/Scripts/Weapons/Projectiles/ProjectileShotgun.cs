using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShotgun : Projectile
{
    protected float spread;
    protected int numberOfPellets;
    protected override void Shoot()
    {
        float random;
        GameObject bullet = Shooting.instance.bulletPrefabs.Find(x => x.tag.Contains("|ProjShotgun|"));
        Vector3 rotationVector;
        for (int i = -numberOfPellets / 2; i <= numberOfPellets / 2; i++)
        {
            random = Random.Range(-10.0f, 10.0f);
            rotationVector = whoAttacks.transform.rotation.eulerAngles;
            // 3 rodzaje patternu shotguna
            rotationVector.z += (i + (random / 10)) / spread;  // troche losowe
            //rotationVector.z += Random.Range(-5.0f, 5.0f) / spread; // totalnie losowe
            //rotationVector.z += i / spread; // totalnie nie losowe
            Vector2 ownerVelocity = CalculateOwnersVelocity((whoAttacks.GetComponent<Rigidbody2D>().velocity / 4), 2f, 4f);

            var obj = Object.Instantiate(bullet, whoAttacks.firePoint.position, Quaternion.Euler(rotationVector));
            obj.transform.localScale = new Vector2(obj.transform.localScale.x * 1f, obj.transform.localScale.y * 1f);
            obj.GetComponent<Bullet>().SetParameters(whoAttacks, (int)damage * (int)whoAttacks.Damage, bulletSpeed + (random / 10), bulletSize, ownerVelocity, (whoAttacks.Range * rangeModifier) / 10);
        }
    }

    public ProjectileShotgun(ProjectileShotgunWeaponStats projectileShotgunWeaponStats) : base(projectileShotgunWeaponStats as ProjectileWeaponStats)
    {
        
        spread = projectileShotgunWeaponStats.Spread;
        numberOfPellets = projectileShotgunWeaponStats.NumberOfPellets;
    }
}
