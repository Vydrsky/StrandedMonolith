using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public static PlayerPosition instance;
    public int[,] mapInt;
    public int mapX, mapY;


    
    int playerNumber = 200;
    int[,] mapTemplate;
    string[] map;


    private void Start()
    {
        instance = this;
        map = new string[16];
        mapTemplate = new int[15, 36];
        mapInt = new int[15, 36];

        //FillStringMap();
        //ShowArray();
        //ConvertMapToInt();
        //ShowArrayInt();
        //InvokeRepeating("UpdateMapArray", 0.0f, 0.5f);
        //InvokeRepeating("DebugLocation", 0.0f, 0.5f);
    }

    public void UpdateMapInfo(string[] map, int mapX, int mapY)
    {
        this.map = map;
        this.mapX = mapX;
        this.mapY = mapY;
        Debug.Log(mapX + " " + mapY);
    }

    //private void FillStringMap()
    //{
    //    for (int j = 0; j < map.GetLength(1); j++)
    //    {
    //        for (int i = 0; i < map.GetLength(0); i++)
    //        {
    //            if (j == 0 || j == map.GetLength(1) - 1)
    //            {
    //                map[i, j] = "X";
    //            }
    //            else if (i == 0 || i == map.GetLength(0) - 1)
    //            {
    //                map[i, j] = "X";
    //            }
    //            else
    //                map[i, j] = " ";
    //        }
    //    }
    //    map[3, 2] = "X";
    //    map[2, 3] = "X";
    //    map[2, 2] = "X";
    //}

    //private void ShowArray()
    //{
    //    for (int j = 0; j < map.GetLength(1); j++)
    //    {
    //        string mapLine = "";
    //        for (int i = 0; i < map.GetLength(0); i++)
    //        {
    //            mapLine += string.Format("{0, -4}", map[i, j]);
    //        }
    //        Debug.Log(mapLine);
    //    }
    //}

    private void FillWithNumbers()
    {
        int currentPlayerNumber = playerNumber;
        bool anyEmpty = true;
        while(anyEmpty)
        {
            anyEmpty = false;
            for (int j = 0; j < mapInt.GetLength(1); j++)
            {
                for (int i = 0; i < mapInt.GetLength(0); i++)
                {
                    if (mapInt[i, j] == currentPlayerNumber)
                    {
                        if(i - 1 > 0)
                        {
                            if(mapInt[i - 1, j] == 1)
                            {
                                mapInt[i - 1, j] = currentPlayerNumber - 1;
                                anyEmpty = true;
                            }
                        }
                        if (i + 1 < mapInt.GetLength(0))
                        {
                            if (mapInt[i + 1, j] == 1)
                            {
                                mapInt[i + 1, j] = currentPlayerNumber - 1;
                                anyEmpty = true;
                            }
                        }
                        if (j - 1 > 0)
                        {
                            if (mapInt[i, j - 1] == 1)
                            {
                                mapInt[i, j - 1] = currentPlayerNumber - 1;
                                anyEmpty = true;
                            }
                        }
                        if (j + 1 < mapInt.GetLength(1))
                        {
                            if (mapInt[i, j + 1] == 1)
                            {
                                mapInt[i, j + 1] = currentPlayerNumber - 1;
                                anyEmpty = true;
                            }
                        }
                    }
                }
            }
            currentPlayerNumber--;
        }
    }

    private void ShowArrayInt()
    {
            string mapLine = "";
        for (int j = mapInt.GetLength(1) - 1; j >= 0 ; j--)
        {
            for (int i = 0; i < mapInt.GetLength(0); i++)
            {
                mapLine += string.Format("{0, 5}", mapInt[i, j].ToString());
                if (mapInt[i, j] == 0)
                    mapLine += "  ";
            }
            mapLine += System.Environment.NewLine;
        }
            Debug.Log(mapLine);
    }


    private void ConvertMapToInt()
    {
        for (int j = 0; j < 15; j++)
        {
            for (int i = 0; i < 37; i++)
            {
                switch(map[j][i])
                {
                    case 'X':
                    case 'E':
                        mapTemplate[i, j] = 0;
                        break;
                    default:
                        mapTemplate[i, j] = 1;
                        break;
                }
            }
        }
    }

    void UpdateMapArray()
    {
        ClearMap();
        int playerX, playerY;
        playerX = Mathf.RoundToInt(transform.position.x);
        playerY = Mathf.RoundToInt(transform.position.y);
        mapInt[playerX, playerY] = playerNumber;
        FillWithNumbers();
        //ShowArrayInt();
    }
    
    void ClearMap()
    {
        for (int j = 0; j < mapTemplate.GetLength(1); j++)
        {   
            for (int i = 0; i < mapTemplate.GetLength(0); i++)
            {
                mapInt[i, j] = mapTemplate[i, j]; 
            }
        }
    }

    void DebugLocation()
    {
        Debug.Log("Player position(x: " + transform.position.x + " y: " + transform.position.y + ")");
        for (int j = 0; j < map.GetLength(1); j++)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                if (mapInt[i, j] == 200)
                    Debug.Log("Player position in array: x - " + i + " y -  " + j);
            }
        }
    }
}
