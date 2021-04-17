using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    protected string characterName;
    [SerializeField] protected int maxHealth;      //do ustawieniea w inspektorze dla debugowania 
    [SerializeField] protected int healthPoints;
    [SerializeField] protected float moveSpeed;
    

}
