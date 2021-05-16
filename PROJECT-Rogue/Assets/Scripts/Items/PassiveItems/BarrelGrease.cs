using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelGrease : PassiveItem
{
    private void Start()
    {
        this.itemName = "Barrel Grease";
        this.itemDescription = "Just slide into the action";
        this.shotSpeedModification = 0.15f;
    }


    
    public override void RemoveFromInventory(Player player)
    {
        this.shotSpeedModification = -shotSpeedModification;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
