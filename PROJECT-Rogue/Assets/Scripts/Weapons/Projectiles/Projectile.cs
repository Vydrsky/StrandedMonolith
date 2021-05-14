using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Ranged
{
    protected float bulletSize;
    protected float bulletSpeed;
    public Projectile(float attackSpeed, int damage, float bulletSpeed, float bulletSize) : base(attackSpeed, damage)
    {
        this.bulletSpeed = bulletSpeed;
        this.bulletSize = bulletSize;
    }

    public Projectile(ProjectileWeaponStats projectileWeaponStats) : base(projectileWeaponStats as WeaponStats)
    {
        bulletSize = projectileWeaponStats.BulletSize;
        bulletSpeed = projectileWeaponStats.BulletSpeed;
    }

    protected Vector2 CalculateOwnersVelocity(Vector2 rawVelocity, float limitX, float limitY)
    {
        int signX, signY;
        float velocityX = rawVelocity.x;
        float velocityY = rawVelocity.y;

        if (velocityX != 0)
            signX = (int)(velocityX / Mathf.Abs(velocityX));
        else
            signX = 1;
        if (velocityY != 0)
            signY = (int)(velocityY / Mathf.Abs(velocityY));
        else
            signY = 1;
        velocityX = Mathf.Min(limitX, Mathf.Abs(velocityX)) * signX;
        velocityY = Mathf.Min(limitY, Mathf.Abs(velocityY)) * signY;
        Debug.Log($"x vel: {velocityX} x limit: {limitX} y vel: {velocityY} y limit: {limitY}");
        return new Vector2(velocityX, velocityY);
    }
}
