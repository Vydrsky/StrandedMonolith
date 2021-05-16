using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioReagent : ActiveItem
{
    public override void Effect(Player player)  //jesli mozna uzyc przedmiotu, uzyj przedmiotu
    {

        if (EffectCanBeUsed() && player.HealthPoints <= player.MaxHealth/2)
        {
            isActive = true;
            player.HealthPoints += maxHPIncrease;
            player.healthBar.SetText(player.HealthPoints, player.MaxHealth);
            player.healthBar.SetHealth(player.HealthPoints);
            currentItemCooldown = maxItemCooldown;
            timeWhenUsed = Time.time;
        }
    }

    public override void RemoveEffect(Player player)    //cofnij efekt na graczu
    {
        isActive = false;
        
    }

    private void Start()
    {
        itemName = "Bio Reagent";
        itemDescription = "Closes severe wounds";
        maxItemCooldown = currentItemCooldown;
        maxHPIncrease = 30;
        
    }
}
