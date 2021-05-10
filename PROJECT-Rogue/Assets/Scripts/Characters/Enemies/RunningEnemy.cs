using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningEnemy : Enemy
{
    EnemyAI enemyAIscript;
    public override void move()
    {
        transform.Translate(new Vector2(1 * moveSpeed, 0) * Time.deltaTime);
        //_rigidbody.AddRelativeForce(Vector2.right * moveSpeed);
    }

    public override void rotate()
    {
        //Vector2 difference = player.transform.position - this.transform.position;
        Vector2 difference = enemyAIscript.CheckDirection();
        float x, y;
        int enemyX, enemyY;
        x = difference.x;
        y = difference.y;
        enemyX = Mathf.RoundToInt(transform.position.x) + (int)x;
        enemyY = Mathf.RoundToInt(transform.position.y) + (int)y;

        difference = new Vector2(enemyX, enemyY) - new Vector2(transform.position.x, transform.position.y); 


        difference = difference.normalized;
        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

    void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        //enemyAIscript = enemyAIobject.GetComponent<EnemyAI>();
        enemyAIscript = transform.GetChild(0).gameObject.GetComponent<EnemyAI>();//GetComponent<EnemyAI>();
    }

    void FixedUpdate()
    {
        rotate();
        move();
    }
}
