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
        player.Damage *= DamageModification;
        player.Range *= RangeModification;
        player.MoveSpeed *= MoveSpeedModification;
        player.AttackSpeed *= AttackSpeedModification;
        player.ShotSpeed *= ShotSpeedModification;
    }
}
