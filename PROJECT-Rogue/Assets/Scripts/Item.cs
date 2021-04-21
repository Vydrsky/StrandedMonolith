using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected string itemName;
    
    protected int maxHPIncrease = 0;
    protected float AttackSpeedModification = 1.0f;
    protected float DamageModification = 1.0f;
    protected float MoveSpeedModification = 1.0f;
    protected float ShotSpeedModification = 1.0f;
    protected float RangeModification = 1.0f;
    
}
