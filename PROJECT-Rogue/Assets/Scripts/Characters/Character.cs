using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected string characterName;
    protected RotationDirectionEnum RotDir = RotationDirectionEnum.RightDirection;

    [SerializeField] private int maxHealth;     //do ustawieniea w inspektorze dla debugowania 
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    [SerializeField] protected int healthPoints;
    public int HealthPoints { get { return healthPoints; } set { healthPoints = value; } }

    [SerializeField] protected float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }



}
