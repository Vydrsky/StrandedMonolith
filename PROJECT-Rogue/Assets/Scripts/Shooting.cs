using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static void Shoot(CanAttack canAttack)
    {
        var obj = Instantiate(canAttack.bulletPrefab, canAttack.firePoint.position, canAttack.firePoint.rotation);
        obj.GetComponent<Bullet>().SetAttacker(canAttack);
    }
}
