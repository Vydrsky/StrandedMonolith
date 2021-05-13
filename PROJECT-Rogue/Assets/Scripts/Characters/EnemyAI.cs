using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    int currentX, currentY;
    int[,] map;
    int mapX, mapY;
    Vector2 debug;
    
    private void Start()
    {
        mapX = PlayerPosition.instance.mapX;
        mapY = PlayerPosition.instance.mapY;
        //InvokeRepeating("ShowDebug", 0, 3f);
    }

    private void CheckPosition()
    {
        currentX = Mathf.RoundToInt(transform.position.x) - mapX;
        currentY = Mathf.RoundToInt(transform.position.y) - mapY;   
    }

    public Vector2 CheckDirection()
    {
        CheckPosition();
        map = PlayerPosition.instance.mapInt;
        int max = 0;
        int max2nd = 0;
        int maxX = currentX, maxY = currentY;
        int maxX2nd = currentX, maxY2nd = currentY;

        if (currentX - 1 > 0)
        {
            if (max <= map[currentX - 1, currentY])
            {
                max2nd = max;
                maxX2nd = maxX;
                maxY2nd = maxY;

                max = map[currentX - 1, currentY];
                maxX = currentX - 1;
                maxY = currentY;
            }
        }
        if (currentX + 1 < map.GetLength(0))
        {
            if (max <= map[currentX + 1, currentY])
            {
                max2nd = max;
                maxX2nd = maxX;
                maxY2nd = maxY;

                max = map[currentX + 1, currentY];
                maxX = currentX + 1;
                maxY = currentY;
            }
        }
        if (currentY - 1 > 0)
        {
            if (max <= map[currentX, currentY - 1])
            {
                max2nd = max;
                maxX2nd = maxX;
                maxY2nd = maxY;

                max = map[currentX, currentY - 1];
                maxX = currentX;
                maxY = currentY - 1;
            }
        }
        if (currentY + 1 < map.GetLength(1))
        {
            if (max <= map[currentX, currentY + 1])
            {
                max2nd = max;
                maxX2nd = maxX;
                maxY2nd = maxY;

                max = map[currentX, currentY + 1];
                maxX = currentX;
                maxY = currentY + 1;
            }
        }
        if (max > 1)
        {
            if (max2nd > 1)
            {
                int tempX, tempY;
                tempX = (maxX - currentX) + (maxX2nd - currentX);
                tempY = (maxY - currentY) + (maxY2nd - currentY);
                if (max < map[tempX + currentX, tempY + currentY])
                {
                    debug = new Vector2(tempX, tempY);
                    return debug;
                }
            }

            debug = new Vector2(maxX - currentX, maxY - currentY);
            return debug;
        }
        debug = new Vector2(0, 0);
        return debug;
           
    }

    private void ShowDebug()
    {
        Debug.Log("Current position: " + currentX + " " + currentY);
        Debug.Log("Rotation vector: " + debug.x + " " + debug.y);
    }
}
