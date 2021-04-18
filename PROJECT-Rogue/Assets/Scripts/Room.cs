using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Room
{
    private string[] map;
    private List<GameObject> myPrefab;
    public Room(List<GameObject> myPrefab,int x, int y,bool top, bool left, bool right, bool bottom){
        string sr = File.ReadAllText("Assets/Scripts/map.txt");
        map= sr.Split('\n');
        this.myPrefab = myPrefab;
        int k = 0;
        int y1 = y;
        while (k < map.GetLength(0))
        {        
            int x1 = x;
            int i = 0;
            Debug.Log(map[k].Length);
            while (i < map[k].Length)
            {
                char pntr = map[k][i];
                switch(pntr){
                    case 'X':
                        Level.Instantiate(myPrefab[0], new Vector2(x1, y1), Quaternion.identity);
                        break;
                    case 'E':
                        if ((i == map[k].Length-1 && right)||(i==0 && left)||(k==0 && top)||(k==map.GetLength(0)-2 && bottom))
                        {
                            Level.Instantiate(myPrefab[1], new Vector2(x1, y1), Quaternion.identity);
                        }
                        else
                        {                        
                            Level.Instantiate(myPrefab[0], new Vector2(x1, y1), Quaternion.identity);
                        }
                        break;
                    default:
                        break;
                }
                x1++;
                i++;
            }

            y1--;
            k++;
        }
    }

    public void Activate()
    {

    }
}
