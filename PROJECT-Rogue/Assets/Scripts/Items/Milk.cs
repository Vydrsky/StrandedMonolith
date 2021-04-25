using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : PassiveItem
{
    private void Start()
    {
        this.itemName = "Milk";
        this.maxHPIncrease = 25;
        this.AttackSpeedModification = 1.3f;
    }


    public override void AddToInventory(Player player)
    {
        SetPlayerStats(player);
        player.Inventory.Add(this);
    }
    public override void RemoveFromInventory(Player player)
    {
        this.maxHPIncrease = -25;
        this.AttackSpeedModification = 1.0f/1.3f;
        SetPlayerStats(player);
        player.Inventory.Remove(this);
    }
}
