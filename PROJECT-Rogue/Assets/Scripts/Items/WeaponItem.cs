using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public Weapon weapon;

    public int identifier = 0;
    
    public void SetWeaponType()
    {
        
        switch (identifier)
        {
            case 0:
                weapon = new ProjectileRiffle();
                break;
            case 1:
                weapon = new ProjectileShotgun();
                break;
            default:
                weapon = new ProjectileRiffle();
                break;
        }
    }

}
