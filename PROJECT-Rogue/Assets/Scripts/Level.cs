using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<GameObject> myPrefab;
    private List<Room> pokoje;
    // Start is called before the first frame update
    void Start()
    {
        pokoje = new List<Room>();
        string layout1D = "0X0\nXXX\n0X0";
        string[] layout = layout1D.Split('\n');
        for (int i = 0; i < layout.Length; i++)
        {
            for (int j = 0; j < layout[i].Length; j++)
            {   
                bool left = false;
                bool right = false;
                bool top = false;
                bool bottom = false;
                if (layout[i][j] == 'X')
                {
                    if (i < layout.Length-1)
                    {
                        if (layout[i + 1][j] == 'X')
                        {
                            right = true;
                        }
                    }
                    if (i > 0)
                    {
                        if (layout[i - 1][j] == 'X')
                        {
                            left = true;
                        }
                    }
                    if (j < layout[i].Length - 1)
                    {
                        Debug.Log(layout[i][j + 1]);
                        if (layout[i][j + 1] == 'X')
                        {
                            top = true;
                        }
                    }
                    if (j > 0)
                    {
                        if (layout[i][j - 1] == 'X')
                        {
                            bottom = true;
                        }
                    }
                    Debug.Log(right);
                    pokoje.Add(new Room(myPrefab,-7+(i*18),-7+(j*11),top,left,right,bottom));
                }
            }
        }

        for (int k = 0; k < pokoje.Count; k++)
        {
            pokoje[k].Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
