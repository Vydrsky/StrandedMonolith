using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantItem : Item     //pieniadze,klucze,leczenie etc.
{
    [SerializeField] protected int HPrestoration;

    public abstract bool CheckUsability(Player player); //zwraca czy jest sens podnosic item, jesli nie to trzeb zrobic zeby nie naruszalo itema w grze
    public abstract void ImmediateEffectOnPlayer(Player player);    //wplyw na gracza
}
