using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    private int killCount = 0;
    public KillQuest()
    {
        Room championRoom = Level.PickChampionRoom();
        championRoom.PromoteEnemy();
    }
    
    public bool Update()
    {
        killCount++;
        if (killCount >= 1)
        {
            return true;
        }

        return false;
    }

    public string JournalEntry()
    {
        return "Zabij elitarnego wroga "+killCount+"/1";
    }
}
