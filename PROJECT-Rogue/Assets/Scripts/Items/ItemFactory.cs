using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : Item
{
    public Item CreateItem(ItemClass item, ItemsEnum type)
    {
        switch (item)
        {
            case ItemClass.Instant:
                switch (type)
                {
                    case ItemsEnum.Milk:
                        return new Milk();
                    case ItemsEnum.ColdBullets:
                        return new ColdBullets();
                    case ItemsEnum.BarrelGrease:
                        return new BarrelGrease();
                }
                break;
            default:
                return null;
        }
        return null;
    }
}
