using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FulminatedMercury : PassiveItem
{
    private void Start()
    {
        this.itemName = "Fulminated Mercury";
        this.itemDescription = "Are you sure this is safe?";
        this.DamageModification = 5;
    }


    
    public override void RemoveFromInventory(Player player)
    {
        this.DamageModification = -DamageModification;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
