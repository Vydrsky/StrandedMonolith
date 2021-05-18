using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfindingEnemy : Enemy
{
    GameObject EnemyAIobj;
    protected EnemyAI enemyAIscript;

    protected float GetRotation(string[] layerName)
    {
        Vector2 temp = GetDirection(layerName);
        return Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
    }

    protected Vector2 GetDirection(string[] layerName)
    {
        LayerMask mask = MakeMask(layerName);
        Vector2 temp;

        if (IsPlayerInSight(mask))
            temp = Player.instance.transform.position - this.transform.position;
        else
        {
            temp = enemyAIscript.CheckDirection();

            // z ta czescia kodu przeciwnik idze na srodek kratki ale za czesto sie obraca, nie wyglada to dobrze :(
            //float x, y;
            //int enemyX, enemyY;
            //x = temp.x;
            //y = temp.y;
            //enemyX = Mathf.RoundToInt(transform.position.x) + (int)x;
            //enemyY = Mathf.RoundToInt(transform.position.y) + (int)y;
            //temp = new Vector2(enemyX, enemyY) - new Vector2(transform.position.x, transform.position.y);
            //

        }
        return temp.normalized;
    }

}
