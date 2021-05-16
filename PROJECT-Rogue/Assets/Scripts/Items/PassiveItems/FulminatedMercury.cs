using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FulminatedMercury : PassiveItem
{
    private void Start()
    {
        this.itemName = "Fulminated Mercury";
        this.itemDescription = "Are you sure this is safe?";
        this.damageModification = 0.1f;
    }


    
    public override void RemoveFromInventory(Player player)
    {
        this.damageModification = -damageModification;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
