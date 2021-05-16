using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected string itemName;
    protected string itemDescription;

    protected int maxHPIncrease = 0;
    protected float attackSpeedModification = 0.0f;
    protected float damageModification = 0;
    protected float moveSpeedModification = 0.0f;
    protected float shotSpeedModification = 0.0f;
    protected float rangeModification = 0.0f;
}