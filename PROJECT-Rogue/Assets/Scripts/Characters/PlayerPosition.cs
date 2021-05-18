using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public static PlayerPosition instance;
    public int[,] mapInt;
    public int mapX = 0, mapY = 0;
    
    int playerNumber = 200;
    int[,] mapTemplate;
    string[] map;

    private void Start()
    {
        instance = this;
        map = new string[16];
        mapTemplate = new int[36, 15];
        mapInt = new int[36, 15];
        for (int i = 0; i < 16; i++)
        {
            map[i] = "                                    ";
        }
        ConvertMapToInt();
    }

    public void UpdateMapInfo(string[] map, int mapX, int mapY)
    {
        map.CopyTo(this.map, 0);
        this.mapX = mapX;
        this.mapY = mapY;
        ConvertMapToInt();  
    }

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

    private void ConvertMapToInt()
    {
        for (int j = 0; j < 15; j++)
        {
            for (int i = 0; i < 36; i++)
            {
                switch(map[j][i])
                {
                    case 'X':
                    case 'E':
                    case 'R':
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
        playerX = Mathf.RoundToInt(transform.position.x) - mapX;
        playerY = Mathf.RoundToInt(transform.position.y) - mapY;
        mapInt[playerX, playerY] = playerNumber;
        FillWithNumbers();
    }

    void ClearMap()
    {
        for (int j = 0; j < mapTemplate.GetLength(1); j++)
        {   
            for (int i = 0; i < mapTemplate.GetLength(0); i++)
            {
                mapInt[i, mapTemplate.GetLength(1) - 1 - j] = mapTemplate[i, j]; 
            }
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
                //if (mapInt[i, j] == 0)
                    //mapLine += "  ";
            }
            mapLine += System.Environment.NewLine;
        }
            Debug.Log(mapLine);
    }

    private void ShowArrayString()
    {
        string mapLine = "";

        for (int j = 0; j < 15; j++)
        {
            for (int i = 0; i < 36; i++)
            {
                char temp = map[j][i];
                mapLine += temp;
            }
            mapLine += System.Environment.NewLine;
        }
        Debug.Log(mapLine);
    }

    void DebugLocation()
    {
        Debug.Log("Player position(x: " + transform.position.x + " y: " + transform.position.y + ")");
        for (int j = 0; j < mapInt.GetLength(1); j++)
        {
            for (int i = 0; i < mapInt.GetLength(0); i++)
            {
                if (mapInt[i, j] == 200)
                    Debug.Log("Player position in array: x - " + i + " y -  " + j);
            }
        }
    }
}
