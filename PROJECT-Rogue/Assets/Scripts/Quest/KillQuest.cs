using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    // Update is called once per frame
    public KillQuest()
    {
        Room championRoom = Level.PickChampionRoom();
        championRoom.PromoteEnemy();
        Debug.Log("Zabij elitarnego wroga 0/1");
    }
    
    public void Update()
    {
    }
}
