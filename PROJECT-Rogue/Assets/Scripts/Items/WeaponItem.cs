using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public Weapon weapon;
    SimpleWeaponFactory weaponFactory = new SimpleWeaponFactory();
    public WeaponsEnum weaponType;

    private void Start()
    {
        weapon = weaponFactory.CreateWeapon(weaponType);
    }

    private void Update()
    {
        
    }

}
