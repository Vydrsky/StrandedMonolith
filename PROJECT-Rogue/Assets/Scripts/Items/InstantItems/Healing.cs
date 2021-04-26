using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : InstantItem
{
    
    public override void ImmediateEffectOnPlayer(Player player)
    {
        
        player.HealthPoints += HPrestoration;
        if (player.HealthPoints >= player.MaxHealth)
        {
            player.HealthPoints = player.MaxHealth;
        }
        
    }

    public override bool CheckUsability(Player player)
    {
        bool usable = true;
        if (player.HealthPoints >=player.MaxHealth)
        {
            usable = false;
        }
        return usable;
    }
}
