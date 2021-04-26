using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBullets : PassiveItem
{
    private void Start()
    {
        this.itemName = "Cold Bullets";
        this.itemDescription = "Keep Your cool";
        attackSpeedModification = 0.2f;
    }


    
    public override void RemoveFromInventory(Player player)
    {
        attackSpeedModification = -attackSpeedModification;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
