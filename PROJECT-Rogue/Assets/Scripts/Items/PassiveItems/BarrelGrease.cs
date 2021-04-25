using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelGrease : PassiveItem
{
    private void Start()
    {
        this.itemName = "Barrel Grease";
        this.itemDescription = "Just slide into the action";
        this.ShotSpeedModification = 0.1f;
    }


    
    public override void RemoveFromInventory(Player player)
    {
        this.ShotSpeedModification = -ShotSpeedModification;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
