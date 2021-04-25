using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.WSA;

public class Room
{
    private string[] map;
    private List<GameObject> myPrefab;
    private List<GameObject> doors;
    private List<GameObject> allObjects;
    private List<GameObject> enemies;
    private List<List<int>> enemySpawns;
    private List<List<int>> itemSpawns;
    private bool _active = false;
    private bool _disarmed = false;
    public Room(List<GameObject> myPrefab,int x, int y,bool top, bool left, bool right, bool bottom, string filePath)
    {
        string sr = File.ReadAllText(filePath);
        map= sr.Split('\n');
        this.myPrefab = myPrefab;
        doors = new List<GameObject>();
        allObjects = new List<GameObject>();
        enemies = new List<GameObject>();
        enemySpawns = new List<List<int>>();
        itemSpawns = new List<List<int>>();
        int spawnIndx = 0;
        int itemIndx = 0;
        int k = 0;
        int y1 = y;
        while (k < map.GetLength(0))
        {
            int x1 = x;
            int i = 0;
            while (i < map[k].Length)
            {
                char pntr = map[k][i];
                switch (pntr)
                {
                    case 'X':
                        allObjects.Add(Level.Instantiate(myPrefab[0], new Vector2(x1, y1), Quaternion.identity));
                        break;
                    case 'E':
                        if ((i == map[k].Length - 1 && right) || (i == 0 && left) || (k == 0 && top) ||
                            (k == map.GetLength(0) - 2 && bottom))
                        {
                            if (k == 0 && top)
                            {
                                myPrefab[1].tag = "DoorUp";
                            }
                            else if (k == map.GetLength(0) - 2 && bottom)
                            {
                                myPrefab[1].tag = "DoorBottom";
                            }
                            else if (i == 0 && left)
                            {
                                myPrefab[1].tag = "DoorLeft";
                            }
                            else if (i == map[k].Length - 1 && right)
                            {
                                myPrefab[1].tag = "DoorRight";
                            }

                            doors.Add(Level.Instantiate(myPrefab[1], new Vector2(x1, y1), Quaternion.identity));
                        }
                        else
                        {
                            allObjects.Add(Object.Instantiate(myPrefab[0], new Vector2(x1, y1), Quaternion.identity));
                        }
                        break;
                    case 'I':
                        //Level.Instantiate(Level.GetItem(), new Vector2(x1, y1), Quaternion.identity);
                        itemSpawns.Add(new List<int>());
                        itemSpawns[itemIndx].Add(x1);
                        itemSpawns[itemIndx].Add(y1);
                        itemIndx++;
                        break;
                    case 'W':
                        //enemies.Add(Level.Instantiate(Level.GetEnemy(), new Vector2(x1, y1), Quaternion.identity));
                        enemySpawns.Add(new List<int>());
                        enemySpawns[spawnIndx].Add(x1);
                        enemySpawns[spawnIndx].Add(y1);
                        spawnIndx++;
                        break;
                    case 'B':
                        allObjects.Add(Level.Instantiate(myPrefab[2], new Vector2(x1, y1), Quaternion.identity));
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
        if (!_active)
        {
            _active = true;
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].GetComponent<Collider2D>().isTrigger = false;
                doors[i].GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                doors[i].GetComponent<SpriteRenderer>().color = Color.red;
            }
            for (int i = 0; i < enemySpawns.Count; i++)
            {
                enemies.Add(Level.Instantiate(Level.GetEnemy(), new Vector2(enemySpawns[i][0], enemySpawns[i][1]), Quaternion.identity));
            }
        }


    }

    public void DeActivate()
    {
        if (_active && !_disarmed)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].GetComponent<Collider2D>().isTrigger = true;
                doors[i].GetComponent<BoxCollider2D>().size = new Vector2(0.01f, 0.01f);
                doors[i].GetComponent<SpriteRenderer>().color = new Color(0f,29f/255,106f/255);
            }
            for (int i = 0; i < itemSpawns.Count; i++)
            {
                allObjects.Add(Level.Instantiate(Level.GetItem(ItemClass.Instant), new Vector2(itemSpawns[i][0], itemSpawns[i][1]), Quaternion.identity));
            }
        }
        _disarmed = true;
    }

    public void CheckEnemyTable()
    {
        int cnt = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            Debug.Log(i+" "+enemies[i]);
            if (enemies[i] == null)
            {
                cnt++;
            }
        }

        if (cnt == enemies.Count-1)
        {
            DeActivate();
        }
    }

    public void Delete()
    {
        for (int i = 0; i < allObjects.Count; i++)
        {
            Level.Destroy(allObjects[i]);
        }

        for (int i = 0; i < doors.Count; i++)
        {
            Level.Destroy(doors[i]);
        }
        
        for (int i = 0; i < enemies.Count; i++)
        {
            Level.Destroy(enemies[i]);
        }
    }
}
