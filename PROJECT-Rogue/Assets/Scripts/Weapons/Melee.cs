using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Melee : Weapon
{

    public abstract void MeleeHit(FightingCharacter WhoAttacks);

    public override void Attack()
    {
        MeleeHit(whoAttacks);
    }

}
