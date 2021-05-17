using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeaponFactory
{
    public Weapon CreateWeapon(WeaponsEnum weaponType)
    {
        switch(weaponType)
        {
            case WeaponsEnum.ProjectileStartingWeapon:
                ProjectileWeaponStats projWeaponStats = new ProjectileWeaponStats();
                projWeaponStats.Name = "";
                projWeaponStats.Description = "";
                projWeaponStats.AttackSpeed = 3f;
                projWeaponStats.Damage = 15;
                projWeaponStats.RangeModifier = 0.6f;
                projWeaponStats.BulletSize = 0.8f;
                projWeaponStats.BulletSpeed = 18;
                return new ProjectileRifle(projWeaponStats);

            case WeaponsEnum.ProjectileRifleA:
                projWeaponStats = new ProjectileWeaponStats();
                projWeaponStats.Name = "";
                projWeaponStats.Description = "";
                projWeaponStats.AttackSpeed = 7f;
                projWeaponStats.Damage = 16;
                projWeaponStats.RangeModifier = 0.8f;
                projWeaponStats.BulletSize = 1f;
                projWeaponStats.BulletSpeed = 20;
                return new ProjectileRifle(projWeaponStats);

            case WeaponsEnum.ProjectileRifleB:
                projWeaponStats = new ProjectileWeaponStats();
                projWeaponStats.Name = "";
                projWeaponStats.Description = "";
                projWeaponStats.AttackSpeed = 15f;
                projWeaponStats.Damage = 7;
                projWeaponStats.RangeModifier = 0.5f;
                projWeaponStats.BulletSize = 0.75f;
                projWeaponStats.BulletSpeed = 23;
                return new ProjectileRifle(projWeaponStats);

            case WeaponsEnum.ProjectileShotgunA:
                ProjectileShotgunWeaponStats projShotgunStats = new ProjectileShotgunWeaponStats();
                projShotgunStats.Name = "";
                projShotgunStats.Description = "";
                projShotgunStats.AttackSpeed = 1.5f;
                projShotgunStats.Damage = 3;
                projShotgunStats.RangeModifier = 0.45f;
                projShotgunStats.BulletSize = 1f;
                projShotgunStats.BulletSpeed = 15;
                projShotgunStats.NumberOfPellets = 11;
                projShotgunStats.Spread = 0.5f;
                return new ProjectileShotgun(projShotgunStats);

            case WeaponsEnum.ProjectileShotgunB:
                projShotgunStats = new ProjectileShotgunWeaponStats();
                projShotgunStats.Name = "";
                projShotgunStats.Description = "";
                projShotgunStats.AttackSpeed = 1.5f;
                projShotgunStats.Damage = 5;
                projShotgunStats.RangeModifier = 0.5f;
                projShotgunStats.BulletSize = 1.5f;
                projShotgunStats.BulletSpeed = 20;
                projShotgunStats.NumberOfPellets = 5;
                projShotgunStats.Spread = 1f;
                return new ProjectileShotgun(projShotgunStats);

            case WeaponsEnum.ProjectileSniperRifleA:
                projWeaponStats = new ProjectileWeaponStats();
                projWeaponStats.Name = "";
                projWeaponStats.Description = "";
                projWeaponStats.AttackSpeed = 0.45f;
                projWeaponStats.Damage = 45;
                projWeaponStats.RangeModifier = 1.5f;
                projWeaponStats.BulletSize = 0.75f;
                projWeaponStats.BulletSpeed = 28;
                return new ProjectileSniperRifle(projWeaponStats);

            case WeaponsEnum.ProjectileSniperRifleB:
                projWeaponStats = new ProjectileWeaponStats();
                projWeaponStats.Name = "";
                projWeaponStats.Description = "";
                projWeaponStats.AttackSpeed = 0.2f;
                projWeaponStats.Damage = 70;
                projWeaponStats.RangeModifier = 1.5f;
                projWeaponStats.BulletSize = 1.25f;
                projWeaponStats.BulletSpeed = 28;
                return new ProjectileSniperRifle(projWeaponStats);

            case WeaponsEnum.RaycastRifleA:
                WeaponStats weaponStats = new WeaponStats();
                weaponStats.Name = "";
                weaponStats.Description = "";
                weaponStats.AttackSpeed = 5f;
                weaponStats.Damage = 15;
                weaponStats.RangeModifier = 1.3f;
                return new RaycastRifle(weaponStats);

            case WeaponsEnum.RaycastRifleB:
                weaponStats = new WeaponStats();
                weaponStats.Name = "";
                weaponStats.Description = "";
                weaponStats.AttackSpeed = 7f;
                weaponStats.Damage = 10;
                weaponStats.RangeModifier = 1.2f;
                return new RaycastRifle(weaponStats);

            case WeaponsEnum.RaycastSniperRifle:
                weaponStats = new WeaponStats();
                weaponStats.Name = "";
                weaponStats.Description = "";
                weaponStats.AttackSpeed = 0.5f;
                weaponStats.Damage = 55;
                weaponStats.RangeModifier = 3;
                return new RaycastSniperRifle(weaponStats);
            case WeaponsEnum.EnemyProjectileRifle:
                projWeaponStats = new ProjectileWeaponStats();
                projWeaponStats.Name = "";
                projWeaponStats.Description = "";
                projWeaponStats.AttackSpeed = 0.7f;
                projWeaponStats.Damage = 10;
                projWeaponStats.RangeModifier = 0.6f;
                projWeaponStats.BulletSize = 1f;
                projWeaponStats.BulletSpeed = 7;
                return new ProjectileSniperRifle(projWeaponStats);
            case WeaponsEnum.EnemyProjectilePistol:
                projWeaponStats = new ProjectileWeaponStats();
                projWeaponStats.Name = "";
                projWeaponStats.Description = "";
                projWeaponStats.AttackSpeed = 0.01f;
                projWeaponStats.Damage = 10;
                projWeaponStats.RangeModifier = 1f;
                projWeaponStats.BulletSize = 0.5f;
                projWeaponStats.BulletSpeed = 6;
                return new ProjectileSniperRifle(projWeaponStats);

            default:
                return null;
        }
    }
}
