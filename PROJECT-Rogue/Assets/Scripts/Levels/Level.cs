using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    [SerializeField] private List<GameObject> myPrefab;
    private static List<GameObject> staticMyPrefab;
    [SerializeField] private List<GameObject> instantItems;
    private static List<GameObject> staticInstantItems;
    [SerializeField] private List<GameObject> passiveItems;
    private static List<GameObject> staticPassiveItems;
    [SerializeField] private List<GameObject> weaponItems;
    private static List<GameObject> staticWeaponItems;
    [SerializeField] private List<GameObject> activeItems;
    private static List<GameObject> staticActivetems;
    [SerializeField] private List<GameObject> enemies;
    private static List<GameObject> staticEnemies;
    [SerializeField] private List<GameObject> bosses;
    private static List<GameObject> staticBosses;
    [SerializeField] private GameObject gracz;
    private static List<GameObject> inventory;
    public static GameObject staticGracz;
    [SerializeField] private GameObject kamera;
    private static int _currentX;
    private static int _currentY;
    public static GameObject instancjaKamery;
    private static List<string> regularRooms;
    private static List<string> bossRooms;
    private static List<string> specialRooms;
    private static Dictionary<string,Room> rooms;
    private static List<Room> unclearedRooms;
    public static string[] layout;

    public static string ReplaceAtIndex(string text, int index, char c)
    {
        var stringBuilder = new System.Text.StringBuilder(text);
        stringBuilder[index] = c;
        return stringBuilder.ToString();
    }

    static string GenerateLevel(int mapSize, int wandererIterations, int numberOfWanderers)
    {
        Random.InitState((int)DateTime.Now.Ticks & 0x0000FFFF);
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
        //Debug.Log(pivotIndex);
        levelLayout = ReplaceAtIndex(levelLayout, pivotIndex, 'X');
        for (int j = 0; j < numberOfWanderers; j++)
        {
            Random.InitState((int) DateTime.Now.Ticks & 0x0000FFFF);
            pivotIndex = ((mapSideLength * ((mapSideLength + 1) / 2) - mapSideLength / 2) - 1) + (mapSideLength / 2);
            for (int i = 0; i < wandererIterations; i++)
            {
                random = UnityEngine.Random.Range(0, 3);
                switch (random)
                {
                    case 0:     //RIGHT


                            pivotIndex++;
                            if (levelLayout[pivotIndex] == '\n')
                            {
                                pivotIndex--;
                            }
                            break;

                    case 1: //UP

                            if (pivotIndex >= mapSideLength + 1)
                            {
                                pivotIndex -= mapSideLength + 1;
                            }
                            break;

                    case 2: //LEFT

                            if (pivotIndex != 0)
                            {
                                pivotIndex--;
                                if (levelLayout[pivotIndex] == '\n')
                                {

                                    pivotIndex++;
                                }
                            }
                            break;

                    case 3: //DOWN
                            if (pivotIndex <= levelLayout.Length - mapSideLength)
                            {
                                pivotIndex += mapSideLength + 1;
                            }
                            break;
                }
                levelLayout = ReplaceAtIndex(levelLayout, pivotIndex, 'X');
            }
        }
       // Debug.Log(levelLayout);
       // Debug.Log(levelLayout.Length);

        return levelLayout;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        instancjaKamery = kamera;
        staticInstantItems = instantItems;
        staticEnemies = enemies;
        staticBosses = bosses;
        staticPassiveItems = passiveItems;
        staticActivetems = activeItems;
        staticWeaponItems = weaponItems;
        staticMyPrefab = myPrefab;
        staticGracz = gracz;
        rooms = new Dictionary<string, Room>();
       regularRooms =RemoveMetaFiles(Directory
                .GetFiles("Assets/Scripts/Levels", "map*").ToList());
        bossRooms = RemoveMetaFiles(Directory
            .GetFiles("Assets/Scripts/Levels", "boss*").ToList());
        specialRooms = RemoveMetaFiles(Directory
            .GetFiles("Assets/Scripts/Levels", "special*").ToList());

        /////////////////////////////////////////////////////
        FillLevel();
    }

    public static void FillLevel()
    {
        layout = GenerateLevel(8, 6, 2).Split('\n');
        unclearedRooms = new List<Room>();
        RemoveRooms();
        PickSpecialRooms(layout);
        bool start = false;
        //Debug.Log(regularRooms.Count + " LEVEL COUNT");
        for (int i = 0; i < layout.Length; i++)
        {
            for (int j = 0; j < layout[i].Length; j++)
            {
                if (layout[i][j] != '0')
                {
                    if (layout[i][j] == 'B')
                    {
                        int rnd = Random.Range(0, bossRooms.Count);
                        rooms.Add("" + i + j,
                            new Room(staticMyPrefab, j, i, bossRooms[rnd]));
                        //rooms["" + i + j].Disable();
                    }
                    else if (layout[i][j] == 'S')
                    {
                        int rnd = Random.Range(0, specialRooms.Count);
                        rooms.Add("" + i + j,
                            new Room(staticMyPrefab, j, i, specialRooms[rnd],true));
                        //rooms["" + i + j].Disable();
                    }
                    else
                    {
                        if (!start)
                        {
                            rooms.Add("" + i + j,
                                new Room(staticMyPrefab, j, i, "Assets/Scripts/Levels/TEMPLATE.txt"));
                            rooms["" + i + j].DeActivate();
                            //unclearedRooms.Remove(rooms["" + i + j]);
                            staticGracz.transform.position = new Vector2(7 + (j * (36)), -7 + (i * (-15)));
                            instancjaKamery.transform.position = new Vector3(17.5f + (j * (36)), -7 + (i * (-15)), -10);
                            _currentX = j;
                            _currentY = i;
                        }
                        else
                        {
                            //Debug.Log("Henlo");
                            int rnd = Random.Range(0, regularRooms.Count);
                            rooms.Add("" + i + j,
                                new Room(staticMyPrefab, j, i, regularRooms[rnd]));
                            //rooms["" + i + j].Disable();
                            unclearedRooms.Add(rooms["" + i + j]);
                        }
                        
                        start = true;
                    }
                }
            }
        }
    }


    public static void MoveCamera(float x, float y)
    {
        instancjaKamery.transform.position=(new Vector3(instancjaKamery.transform.position.x + x, instancjaKamery.transform.position.y + y, -10));
    }

    public static List<GameObject> GetAllItems()
    {
        List<GameObject> allItems = new List<GameObject>();
        allItems.AddRange(staticInstantItems);
        allItems.AddRange(staticPassiveItems);
        allItems.AddRange(staticActivetems);
        allItems.AddRange(staticWeaponItems);
        return allItems;
    }

    public static void MoveFocus(int x, int y)
    {
        //rooms["" + _currentY + _currentX].Disable();
        _currentX += x;
        _currentY += y;
        //rooms["" + _currentY + _currentX].Enable();
        if (rooms.ContainsKey("" + _currentY + _currentX))
        {
            rooms["" + _currentY + _currentX].Activate();
        }
    }
    
    public static void RemoveFocus()
    {
        rooms[""+_currentY+_currentX].DeActivate();
        unclearedRooms.Remove(rooms[""+_currentY+_currentX]);
        Debug.Log("UNCLEARED"+unclearedRooms.Count);
    }
    
    public static void CheckStatus()
    {
        rooms[""+_currentY+_currentX].CheckEnemyTable();
    }

    
    public static Room PickChampionRoom()
    {
        if (unclearedRooms.Count == 0)
        {
            return null;
        }
         int rng = Random.Range(0, unclearedRooms.Count);
         Debug.Log("RNG="+rng);
         return unclearedRooms[rng];
    }

    public static GameObject GetItem(ItemClass itemClass)
    {
        int rnd;
        switch (itemClass)
        {
            case ItemClass.Instant:
                rnd=Random.Range(0, staticInstantItems.Count);
                return staticInstantItems[rnd];
            case ItemClass.Passive:
                rnd=Random.Range(0, staticPassiveItems.Count);
                return staticPassiveItems[rnd];
            case ItemClass.Active:
                rnd = Random.Range(0, staticActivetems.Count);
                return staticActivetems[rnd];
            case ItemClass.Weapon:
                rnd = Random.Range(0, staticWeaponItems.Count);
                return staticWeaponItems[rnd];
            default:
                return null;
        }
    }
    
    public static GameObject GetEnemy(EnemyType type)
    {
        int rnd;
        //Random.InitState((int)DateTime.Now.Ticks & 0x0000FFFF);
        switch (type)
        {
            case EnemyType.Regular:
                rnd=Random.Range(0, staticEnemies.Count);
                return staticEnemies[rnd];
            case EnemyType.Boss:
                rnd=Random.Range(0, staticBosses.Count);
                return staticBosses[rnd];
            default:
                return null;
        }
    }

    static void RemoveRooms()
    {
        if (rooms.Count > 0)
        {
            foreach (var i in rooms.Values.ToArray())
            {
                i.Delete();
            }
            unclearedRooms.Clear();
            rooms.Clear();
        }

        List<GameObject> temp;
        temp = staticGracz.GetComponent<Player>().tempInventory;
        for (int i = temp.Count-1; i >= 0; i--)
        {
            if (temp[i].activeInHierarchy)
            {
                Destroy(temp[i]);
                temp.RemoveAt(i);
            }
        }
    }

    static void PickSpecialRooms(string[] layout)
    {
        int cordY=0;
        int cordX=0;
        int countS = 0;
        for (int i = 0; i < layout.Length-1; i++)
        {
            for (int j = 0; j < layout[i].Length; j++)
            {
                int connectedRooms = 0;
                if (layout[i][j] == 'X')
                {
                    cordX = i;
                    cordY = j;
                    if (i < layout.Length - 2)
                    {
                        if (layout[i + 1][j] != '0')
                        {
                            connectedRooms++;
                        }
                    }

                    if (i > 0)
                    {
                        if (layout[i - 1][j] != '0')
                        {
                            connectedRooms++;
                        }
                    }

                    if (j < layout[i].Length - 1)
                    {
                        if (layout[i][j + 1] != '0')
                        {
                            connectedRooms++;
                        }
                    }

                    if (j > 0)
                    {
                        if (layout[i][j - 1] != '0')
                        {
                            connectedRooms++;
                        }
                    }
                }

                if (connectedRooms == 1)
                {
                    countS++;
                    layout[i] = ReplaceAtIndex(layout[i],j,'S');
                }
            }
        }

        if (layout[cordX][cordY] == 'S')
        {
            layout[cordX] = ReplaceAtIndex(layout[cordX],cordY,'X');
        }

       // Debug.Log("TUTAJ COUNTS "+countS);
        while (countS < 2)
        {
            bool interrupt = false;
            for (int i = 0; (i < layout.Length-1) && !interrupt; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    int connectedRooms = 0;
                    if (layout[i][j] == '0')
                    {
                        if (i < layout.Length - 2)
                        {
                            if (layout[i + 1][j] != '0')
                            {
                                connectedRooms++;
                            }
                        }

                        if (i > 0)
                        {
                            if (layout[i - 1][j] != '0')
                            {
                                connectedRooms++;
                            }
                        }

                        if (j < layout[i].Length - 1)
                        {
                            if (layout[i][j + 1] != '0')
                            {
                                connectedRooms++;
                            }
                        }

                        if (j > 0)
                        {
                            if (layout[i][j - 1] != '0')
                            {
                                connectedRooms++;
                            }
                        }

                        if (connectedRooms == 1)
                        {
                            int rng = Random.Range(0, 10);
                            if (rng == 0)
                            {
                                layout[i] = ReplaceAtIndex(layout[i], j, 'S');
                                countS++;
                                interrupt = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (Random.Range(0, 2)==0)
        {
            cordX++;
            if (cordX >= layout.Length)
            {
                string pH = "";
                for (int i = 0; i <= cordY; i++)
                {
                    pH += '0';
                }
                layout.Append(pH);
            }
        }
        else
        {
            cordY++;
            if (cordY >= layout[cordX].Length)
            {
                for (int i = 0; i < layout.Length; i++)
                {
                    layout[i]+='0';
                }
            }
        }
        layout[cordX]=ReplaceAtIndex(layout[cordX], cordY, 'B');
    }

    List<string> RemoveMetaFiles(List<string> files)
    {
        bool flag = false;
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

        return files;
    }
}