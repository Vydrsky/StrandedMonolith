using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : PassiveItem
{
    private void Start()
    {
        this.itemName = "Milk";
        this.itemDescription = "Good for Your bones";
        this.maxHPIncrease = 25;
    }


    
    public override void RemoveFromInventory(Player player)
    {
        this.maxHPIncrease = -maxHPIncrease;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
