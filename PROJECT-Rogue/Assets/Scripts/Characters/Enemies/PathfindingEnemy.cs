using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfindingEnemy : Enemy
{
    GameObject EnemyAIobj;
    protected EnemyAI enemyAIscript;
    //public override void move()
    //{
    //    //transform.Translate(new Vector2(1 * moveSpeed, 0) * Time.deltaTime);
    //    _rigidbody.AddRelativeForce(Vector2.right * moveSpeed);
    //    //_rigidbody.AddRelativeForce(difference * moveSpeed);
    //}

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

            // srodek kratki na mapie
            float x, y;
            int enemyX, enemyY;
            x = temp.x;
            y = temp.y;
            enemyX = Mathf.RoundToInt(transform.position.x) + (int)x;
            enemyY = Mathf.RoundToInt(transform.position.y) + (int)y;
            temp = new Vector2(enemyX, enemyY) - new Vector2(transform.position.x, transform.position.y);
            //

        }
        return temp.normalized;
    }

    // void Start()
    //{
    //    _rigidbody = GetComponent<Rigidbody2D>();
    //    player = GameObject.FindGameObjectsWithTag("Player")[0];
    //    EnemyAIobj = transform.GetChild(0).gameObject;
    //    enemyAIscript = transform.GetChild(0).gameObject.GetComponent<EnemyAI>();
    //    timeToWait = Time.time + 1f;
    //    _audioSource = GetComponent<AudioSource>();
    //}

    //void FixedUpdate()
    //{
    //    if (Wait())
    //    {
    //        rotate();
    //        move();
    //    }
    //}
}
