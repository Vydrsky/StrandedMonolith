using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stimpak : ActiveItem
{
    
    public override void Effect(Player player)  //jesli mozna uzyc przedmiotu, uzyj przedmiotu
    {
        
        if (EffectCanBeUsed())
        {
            isActive = true;
            player.MoveSpeed *= moveSpeedModification;
            currentItemCooldown = maxItemCooldown;
            timeWhenUsed = Time.time;
        }
    }

    public override void RemoveEffect(Player player)    //cofnij efekt na graczu
    {
        isActive = false;
        player.MoveSpeed *= 1.0f/moveSpeedModification;
    }

    private void Start()
    {
        itemName = "Stimpak";
        itemDescription = "Works like coffee but better";
        maxItemCooldown = currentItemCooldown;
        moveSpeedModification = 2.0f;
    }
}
