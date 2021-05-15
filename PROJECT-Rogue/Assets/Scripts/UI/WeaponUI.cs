using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] Sprite startSprite;
    private Image image;
    private Player player;
    private Weapon weapon;

    void Start()
    {
        image = GetComponent<Image>();
        player = FindObjectOfType<Player>();
        weapon = player.WeaponItem.weapon;
    }

    // Update is called once per frame
    void Update()
    {
        if(image.sprite == null)
        {
            image.sprite = startSprite;
        }
        if (weapon != player.WeaponItem.weapon)
        {
            weapon = player.WeaponItem.weapon;
            image.sprite = player.WeaponItem.weaponSprite;
        }
    }
}
