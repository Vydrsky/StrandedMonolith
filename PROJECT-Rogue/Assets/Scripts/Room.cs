using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Room: MonoBehaviour
{
    public Room(GameObject myPrefab){
        string sr = File.ReadAllText("Assets/Scripts/map.txt");
        string[] map = sr.Split('\n');
        int k = 0;
        int y = 7;
        while (k < map.GetLength(0))
        {
            int x = -7;
            int i = 0;
            while (i < map[k].Length)
            {
                char pntr = map[k][i];
                if (pntr == 'X')
                {
                    Instantiate(myPrefab, new Vector2(x, y), Quaternion.identity);
                }

                x++;
                i++;
            }

            y--;
            k++;
        }
    }
}
