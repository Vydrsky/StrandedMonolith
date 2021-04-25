using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected string itemName;
    protected string itemDescription;

    protected int maxHPIncrease = 0;
    protected float AttackSpeedModification = 0.0f;
    protected int DamageModification = 0;
    protected float MoveSpeedModification = 0.0f;
    protected float ShotSpeedModification = 0.0f;
    protected float RangeModification = 0.0f;
    
}
