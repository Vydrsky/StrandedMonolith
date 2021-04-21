using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingCharacter : Character
{
    public Transform firePoint;
    public void takeDamage(int damage)
    {
        healthPoints -= damage;
    }
}
