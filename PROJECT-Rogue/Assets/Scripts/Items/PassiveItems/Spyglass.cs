using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyglass : PassiveItem
{
    private void Start()
    {
        this.itemName = "Spyglass";
        this.itemDescription = "Be careful where you look";
        this.RangeModification = 0.2f;
    }


    
    public override void RemoveFromInventory(Player player)
    {
        this.RangeModification = -RangeModification;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
