using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WeaponItem : MonoBehaviour
{
    public Weapon weapon;
    SimpleWeaponFactory weaponFactory = new SimpleWeaponFactory();
    public WeaponsEnum weaponType;
    public Sprite weaponSprite;
    public AudioSource sound;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        weaponSprite = sr.sprite;
        weapon = weaponFactory.CreateWeapon(weaponType);
        weapon.SetWeaponSound(sound);
    }
}
