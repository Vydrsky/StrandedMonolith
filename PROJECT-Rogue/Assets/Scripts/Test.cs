using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] public GameObject myPrefab;
    // Start is called before the first frame update
    void Start()
    {
        string sr = File.ReadAllText("Assets/Scripts/map.txt");
        string[] map = sr.Split('\n');
        int k = 0;
        int y = 0;
        while (k < map.GetLength(0))
        {
            int x = 0;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
