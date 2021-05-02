using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public Weapon weapon;

    public WeaponsEnum weaponType;

    public void FabricateWeapon()
    {

        switch (weaponType)
        {
            case WeaponsEnum.Rifle:
                weapon = new ProjectileRifle();
                break;
            case WeaponsEnum.Shotgun:
                weapon = new ProjectileShotgun();
                break;
            case WeaponsEnum.Raycast:
                weapon = new RaycastRifle();
                break;
            default:
                weapon = new ProjectileRifle();
                break;
        }
    }

    private void Start()
    {
        FabricateWeapon();
    }



}
