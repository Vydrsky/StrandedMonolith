using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected string itemName;

    protected int maxHPIncrease = 0;
    protected float attackSpeedModification = 1.0f;
    protected float damageModification = 1.0f;
    protected float moveSpeedModification = 1.0f;
    protected float shotSpeedModification = 1.0f;
    protected float rangeModification = 1.0f;
    
}
