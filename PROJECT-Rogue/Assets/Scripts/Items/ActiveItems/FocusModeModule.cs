using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusModeModule : ActiveItem
{
    public override void Effect(Player player)  //jesli mozna uzyc przedmiotu, uzyj przedmiotu
    {

        if (EffectCanBeUsed())
        {
            isActive = true;
            player.MoveSpeed *= moveSpeedModification;
            player.AttackSpeed *= attackSpeedModification;
            currentItemCooldown = maxItemCooldown;
            timeWhenUsed = Time.time;
        }
    }

    public override void RemoveEffect(Player player)    //cofnij efekt na graczu
    {
        isActive = false;
        player.MoveSpeed *= 1.0f / moveSpeedModification;
        player.AttackSpeed *= 1.0f / attackSpeedModification;
    }

    private void Start()
    {
        itemName = "Focus Mode Module";
        itemDescription = "Ready. Aim. Fire";
        maxItemCooldown = currentItemCooldown;
        moveSpeedModification = 0.5f;
        attackSpeedModification = 2.0f;
    }
}
