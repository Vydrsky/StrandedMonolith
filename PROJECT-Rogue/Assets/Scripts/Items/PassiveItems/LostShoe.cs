using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostShoe : PassiveItem
{
    private void Start()
    {
        this.itemName = "Lost Shoe";
        this.itemDescription = "There you are";
        this.moveSpeedModification = 0.3f;
    }


    
    public override void RemoveFromInventory(Player player)
    {
        this.moveSpeedModification = -moveSpeedModification;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
