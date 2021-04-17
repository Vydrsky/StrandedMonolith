using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected string characterName;
    protected float horizontalAxis, verticalAxis;
    protected RotationDirectionEnum RotDir = RotationDirectionEnum.RightDirection;
    [SerializeField] private int maxHealth;     //do ustawieniea w inspektorze dla debugowania 
    [SerializeField] protected int healthPoints;
    [SerializeField] protected float moveSpeed;

    
    
}
