using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<GameObject> myPrefab;
    [SerializeField] private GameObject gracz;
    private List<Room> pokoje;

    public string ReplaceAtIndex(string text, int index, char c)
    {
        var stringBuilder = new System.Text.StringBuilder(text);
        stringBuilder[index] = c;
        return stringBuilder.ToString();
    }

    string Wander(int mapSize, int wandererIterations)
    {
        UnityEngine.Random.InitState(UnityEngine.Random.Range(-10000,10000));
        string levelLayout="";
        int random;     //w ktora strone pojdzie pies
        int pivotIndex; //indeks w kt�rym jest pies, zaczyna od srodka
        int mapSideLength = mapSize;
        if (mapSize % 2 == 0)
        {
            mapSideLength++;
        }
        for (int i = 0; i < mapSideLength; i++)
        {
            for (int j = 0; j < mapSideLength; j++)
            {
                levelLayout += "0";
            }
            levelLayout += "\n";
        }

        pivotIndex = ((mapSideLength * ((mapSideLength+1) / 2)-mapSideLength/2)-1)+(mapSideLength/2);
        Debug.Log(pivotIndex);
        levelLayout = ReplaceAtIndex(levelLayout, pivotIndex, 'X');

        for (int i = 0; i < wandererIterations; i++)
        {
            random = UnityEngine.Random.Range(0,3);
            switch (random)
            {
                case 0:     //RIGHT
                    {
                        
                        pivotIndex++;
                        if (levelLayout[pivotIndex] == '\n')
                        {
                            pivotIndex--;
                        }
                        break;
                    }
                case 1: //UP
                    {
                        if (pivotIndex>=mapSideLength+1)
                        {
                            pivotIndex -= mapSideLength + 1;
                        }
                        break;
                    }
                case 2: //LEFT
                    {
                        pivotIndex--;
                        if (levelLayout[pivotIndex] == '\n')
                        {
                            pivotIndex++;
                        }

                        break;
                    }
                case 3: //DOWN
                    {
                        if (pivotIndex <= levelLayout.Length-mapSideLength) 
                        { 
                            pivotIndex += mapSideLength + 1;
                        }
                        break;
                    }
            }
            levelLayout = ReplaceAtIndex(levelLayout, pivotIndex, 'X');
        }
        Debug.Log(levelLayout);

        return levelLayout;
    }
    // Start is called before the first frame update
    void Start()
    {
        //string test = Wander(7);
        pokoje = new List<Room>();
        bool start = false;
        string layout1D = Wander(10,30);
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
                            bottom = true;
                        }
                    }
                    if (i > 0)
                    {
                        if (layout[i - 1][j] == 'X')
                        {
                            top = true;
                        }
                    }
                    if (j < layout[i].Length - 1)
                    {
                        //Debug.Log(layout[i][j + 1]);
                        if (layout[i][j + 1] == 'X')
                        {
                            right = true;
                        }
                    }
                    if (j > 0)
                    {
                        if (layout[i][j - 1] == 'X')
                        {
                            left = true;
                        }
                    }
                    pokoje.Add(new Room(myPrefab,7+(j*(36)),-7+(i*(-15)),top,left,right,bottom));
                    if (!start)
                    {
                        gracz.transform.position = new Vector2(14 + (j * (36)), -14 + (i * (-15)));
                    }

                    start = true;
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
