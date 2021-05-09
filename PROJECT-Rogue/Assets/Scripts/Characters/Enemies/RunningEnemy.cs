using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningEnemy : Enemy
{
    public override void move()
    {
        _rigidbody.AddRelativeForce(Vector2.right * moveSpeed);
    }

    public override void rotate()
    {
        Vector3 difference = player.transform.position - this.transform.position;
        difference = difference.normalized;
        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

    void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void FixedUpdate()
    {
        rotate();
        move();
    }
}
