using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveItem : Item
{
    public string itemInfo()
    {
        return this.itemName;
    }
    public abstract void AddToInventory(Player player);
    public abstract void RemoveFromInventory(Player player);


    public void SetPlayerStats(Player player)
    {
        player.MaxHealth += maxHPIncrease;
        player.HealthPoints += maxHPIncrease;
        player.Damage *= damageModification;
        player.Range *= rangeModification;
        player.MoveSpeed *= moveSpeedModification;
        player.AttackSpeed *= attackSpeedModification;
        player.ShotSpeed *= shotSpeedModification;
    }
}
