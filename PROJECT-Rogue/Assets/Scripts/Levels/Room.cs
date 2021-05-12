using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Transactions;
using UnityEngine;
using UnityEngine.WSA;

public class Room
{
    private string[] map;
    private List<GameObject> myPrefab;
    private List<GameObject> doors;
    private List<GameObject> allObjects;
    private List<GameObject> enemies;
    private List<GameObject> enemyPool;
    private List<List<int>> enemySpawns;
    private List<List<int>> itemSpawns;
    private List<GameObject> itemPool;
    private List<List<int>> geoSpawns;
    private bool _active = false;
    private bool _disarmed = false;
    public string roomType;
    private bool Promotion = false;
    public Room(List<GameObject> myPrefab,int x, int y, string filePath, bool isSafe=false)
    {
        string sr = File.ReadAllText(filePath);
        map=sr.Split('\n');
        roomType = filePath;
        this.myPrefab = myPrefab;
        doors = new List<GameObject>();
        allObjects = new List<GameObject>();
        enemies = new List<GameObject>();
        enemySpawns = new List<List<int>>();
        enemyPool = new List<GameObject>();
        itemPool = new List<GameObject>();
        itemSpawns = new List<List<int>>();
        geoSpawns = new List<List<int>>();
        int spawnIndx = 0;
        int itemIndx = 0;
        int geoIndx = 0;
        int k = 0;
        int y1 = y*(-15);

        while (k < map.GetLength(0))
        {
            int x1 = x*36;
            int i = 0;
            while (i < map[k].Length)
            {
                char pntr = map[k][i];
                switch (pntr)
                {
                    case 'X':
                        allObjects.Add(Object.Instantiate(myPrefab[0], new Vector2(x1, y1), Quaternion.identity));
                        break;
                    case 'E':
                        //////
                        bool isDoor=false;
                       // Debug.Log(Level.layout.Length);// HERE WEWOOWEWOOO
                        if (y < Level.layout.Length - 1 && k == map.GetLength(0) - 2)
                        {
                            if (Level.layout[y + 1][x] != '0')
                            {
                                myPrefab[1].tag = "DoorBottom";
                                isDoor = true;
                            }
                        }

                        if (y > 0 && k == 0)
                        {
                            if (Level.layout[y - 1][x] != '0')
                            {
                                myPrefab[1].tag = "DoorUp";
                                isDoor = true;
                            }
                        }

                        if (x < Level.layout[y].Length - 1 && i == map[k].Length - 1)
                        {
                            if (Level.layout[y][x + 1] != '0')
                            {
                                myPrefab[1].tag = "DoorRight";
                                isDoor = true;
                            }
                        }

                        if (x > 0 && i == 0)
                        {
                            if (Level.layout[y][x - 1] != '0')
                            {
                                myPrefab[1].tag = "DoorLeft";
                                isDoor = true;
                            }
                        }

                        if (isDoor)
                        {
                            doors.Add(Object.Instantiate(myPrefab[1], new Vector2(x1, y1), Quaternion.identity));
                        }
                        else
                        {
                            allObjects.Add(
                                Object.Instantiate(myPrefab[0], new Vector2(x1, y1), Quaternion.identity));
                        }

                        break;
                    case 'I':
                        itemSpawns.Add(new List<int>());
                        itemSpawns[itemIndx].Add(x1);
                        itemSpawns[itemIndx].Add(y1);
                        itemPool.Add(Level.GetItem(ItemClass.Instant));
                        itemIndx++;
                        break;
                    case 'W':
                        enemySpawns.Add(new List<int>());
                        enemySpawns[spawnIndx].Add(x1);
                        enemySpawns[spawnIndx].Add(y1);
                        enemyPool.Add(Level.GetEnemy(EnemyType.Regular));
                        spawnIndx++;
                        break;
                    case 'Q':
                        enemySpawns.Add(new List<int>());
                        enemySpawns[spawnIndx].Add(x1);
                        enemySpawns[spawnIndx].Add(y1);
                        enemyPool.Add(Level.GetEnemy(EnemyType.Boss));
                        spawnIndx++;
                        break;
                    case 'B':
                        allObjects.Add(Object.Instantiate(myPrefab[2], new Vector2(x1, y1), Quaternion.identity));
                        break;
                    case 'R':
                        allObjects.Add(Object.Instantiate(myPrefab[3], new Vector2(x1, y1), Quaternion.identity));
                        break;
                    case 'T':
                        geoSpawns.Add(new List<int>());
                        geoSpawns[geoIndx].Add(x1);
                        geoSpawns[geoIndx].Add(y1);
                        geoIndx++;
                        break;
                    case '!':
                        allObjects.Add(Object.Instantiate(myPrefab[5], new Vector2(x1, y1), Quaternion.identity));
                        break;
                    case 'A':
                        itemSpawns.Add(new List<int>());
                        itemSpawns[itemIndx].Add(x1);
                        itemSpawns[itemIndx].Add(y1);
                        int rng = Random.Range(0, 3);
                        switch (rng)
                        {
                            case 0:
                                itemPool.Add(Level.GetItem(ItemClass.Active));
                                break;
                            case 1:
                                itemPool.Add(Level.GetItem(ItemClass.Passive));
                                break;
                            case 2: 
                                itemPool.Add(Level.GetItem(ItemClass.Weapon));
                                break;
                        }
                        itemIndx++;
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
        if (isSafe)
        {
            DeActivate();
        }
    }

    public void Activate()
    {
        if (!_active && !_disarmed)
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
                    enemies.Add(Object.Instantiate(enemyPool[i], new Vector2(enemySpawns[i][0], enemySpawns[i][1]),
                        Quaternion.identity));
                    if (Promotion)
                    {
                        enemies[i].GetComponent<Enemy>().IsTarget = true;
                        Promotion = false;
                    }
            }
        }


    }

    public void DeActivate()
    {
        if (!_disarmed)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].GetComponent<Collider2D>().isTrigger = true;
                doors[i].GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f);
                doors[i].GetComponent<SpriteRenderer>().color = new Color(0f,29f/255,106f/255);
            }
            for (int i = 0; i < itemSpawns.Count; i++)
            {
                allObjects.Add(Object.Instantiate(itemPool[i], new Vector2(itemSpawns[i][0], itemSpawns[i][1]), Quaternion.identity));
            }
            for (int i = 0; i < geoSpawns.Count; i++)
            {
                allObjects.Add(Object.Instantiate(myPrefab[4], new Vector2(geoSpawns[i][0], geoSpawns[i][1]), Quaternion.identity));
            }
        }
        _disarmed = true;
    }

    public void CheckEnemyTable()
    {
        int cnt = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
           // Debug.Log(i+" "+enemies[i]);
            if (enemies[i] == null)
            {
                cnt++;
            }
        }

        if (cnt == enemies.Count-1)
        {
            DeActivate();
            Level.RemoveFocus();
        }
    }

    public void PromoteEnemy()
    {
        Promotion = true;
    }

    public void Delete()
    {
        for (int i = 0; i < allObjects.Count; i++)
        {
            Object.Destroy(allObjects[i]);
        }

        for (int i = 0; i < doors.Count; i++)
        {
            Object.Destroy(doors[i]);
        }
        
        for (int i = 0; i < enemies.Count; i++)
        {
            Object.Destroy(enemies[i]);
        }
    }
}
