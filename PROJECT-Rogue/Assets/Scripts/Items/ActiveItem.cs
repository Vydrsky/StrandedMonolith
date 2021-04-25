using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveItem : Item
{

    protected int maxItemCooldown;
    [SerializeField] protected int currentItemCooldown;
    [SerializeField] protected int durationOfEffect;
    protected float timeWhenUsed;
    protected bool isActive = false;

    public int ItemCooldown { get { return currentItemCooldown; } set { currentItemCooldown = value; } }
    public int DurationOfEffect { get { return durationOfEffect; } set { durationOfEffect = value; } }
    public float TimeWhenUsed { get { return timeWhenUsed; } set { timeWhenUsed = value; } }
    public bool IsActive { get { return isActive; } set { isActive = value; } }


    public abstract void Effect(Player player);
    public abstract void RemoveEffect(Player player);

    public bool EffectCanBeUsed()
    {
        if (currentItemCooldown == 0)
        {
            return true;
        }
        return false;
    }
    public bool EffectRunnedOut()
    {
        
        if(Time.time >= timeWhenUsed+durationOfEffect  && isActive == true)
        {
            return true;
        }
        return false;
    }

    public void ReduceCooldown() 
    {
        if (currentItemCooldown > 0)
        {
            currentItemCooldown--;
        }
    }

    private void OnDisable()            //po podniesieniu przedmiotu aka wylaczeniu go w przestrzeni gry, zacznij odliczac cooldown co sekunde
    {
        InvokeRepeating("ReduceCooldown", 1f, 1f);
    }

    private void OnEnable()         //po odlozeniu
    {
        CancelInvoke("ReduceCooldown");
    }
}
