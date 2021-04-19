using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    [SerializeField] private List<GameObject> myPrefab;
    [SerializeField] private GameObject gracz;
    [SerializeField] private GameObject kamera;
    public static GameObject instancjaKamery;
    private List<string> files;
    private List<Room> pokoje;

    public string ReplaceAtIndex(string text, int index, char c)
    {
        var stringBuilder = new System.Text.StringBuilder(text);
        stringBuilder[index] = c;
        return stringBuilder.ToString();
    }

    string Wander(int mapSize, int wandererIterations, int numberOfWanderers)
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
        for (int j = 0; j < numberOfWanderers; j++)
        {
            UnityEngine.Random.InitState(UnityEngine.Random.Range(-10000, 10000));
            pivotIndex = ((mapSideLength * ((mapSideLength + 1) / 2) - mapSideLength / 2) - 1) + (mapSideLength / 2);
            for (int i = 0; i < wandererIterations; i++)
            {
                random = UnityEngine.Random.Range(0, 3);
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
                            if (pivotIndex >= mapSideLength + 1)
                            {
                                pivotIndex -= mapSideLength + 1;
                            }
                            break;
                        }
                    case 2: //LEFT
                        {
                            if (pivotIndex != 0)
                            {
                                pivotIndex--;
                                if (levelLayout[pivotIndex] == '\n')
                                {

                                    pivotIndex++;
                                }
                            }
                            break;
                        }
                    case 3: //DOWN
                        {
                            if (pivotIndex <= levelLayout.Length - mapSideLength)
                            {
                                pivotIndex += mapSideLength + 1;
                            }
                            break;
                        }
                }
                levelLayout = ReplaceAtIndex(levelLayout, pivotIndex, 'X');
            }
        }
        Debug.Log(levelLayout);
        Debug.Log(levelLayout.Length);

        return levelLayout;
    }
    // Start is called before the first frame update
    void Start()
    {
        instancjaKamery = kamera;
        //string test = Wander(7);
        pokoje = new List<Room>();
        bool start = false;
        bool flag = false;
        string layout1D = Wander(30,30,2);
        string[] layout = layout1D.Split('\n');
        files = Directory.GetFiles("Assets/Scripts","map*").ToList();
        string compare="meta";
        for (int i = 0; i < files.Count; i++)
        {
            
            flag = false;
            for (int j = 0; j < files[i].Length; j++)
            {
                int k = 0;
                while(((j+k)<files[i].Length)&&(k<compare.Length)&&(files[i][j+k] == compare[k]))
                {
                    k++;
                    if (k == compare.Length - 1)
                    {
                        flag = true;
                    }
                }
            }
            if(flag)
            { 
                files.RemoveAt(i);
            }
        }
        Debug.Log(files.Count+" LEVEL COUNT");
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
                    int rnd=Random.Range(0,files.Count);
                    pokoje.Add(new Room(myPrefab,7+(j*(36)),-7+(i*(-15)),top,left,right,bottom,files[rnd]));
                    if (!start)
                    {
                        gracz.transform.position = new Vector2(14 + (j * (36)), -14 + (i * (-15)));
                        instancjaKamery.transform.position = new Vector3(26 + (j * (36)), -14 + (i * (-15)),-10);
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

    public static void MoveCamera(float x, float y)
    {
        instancjaKamery.transform.position=(new Vector3(instancjaKamery.transform.position.x + x, instancjaKamery.transform.position.y + y, -10));
    }
}
