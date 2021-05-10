using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBoss : RunningEnemy
{
    private Vector2 distance;
    private float originalMS;
    private void Charge()
    {
        moveSpeed = originalMS*8f;
        move();
    }

    void Start()
    {
        originalMS = moveSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void FixedUpdate()
    {
        distance = player.transform.position - transform.position;
        if (distance.sqrMagnitude > 50)
        {
            moveSpeed = originalMS;
            rotate();
            move();
        }
        else
        {
            if (distance.sqrMagnitude > 20)
            {
                rotate();
            }
            Charge();
        }
    }
}
