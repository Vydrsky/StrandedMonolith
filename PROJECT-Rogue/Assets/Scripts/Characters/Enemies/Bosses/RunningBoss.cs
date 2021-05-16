using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBoss : Enemy
{
    private Vector2 distance;
    private float originalMS;
    private Vector2 direction;
    private void Charge()
    {
        moveSpeed = originalMS*4f;
        move();
    }

    void Start()
    {
        originalMS = moveSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        timeToWait = Time.time + 1f;
        _audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        direction = player.transform.position - transform.position;
        if (Wait())
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
    public override void move()
    {
        _rigidbody.AddRelativeForce(Vector2.right * moveSpeed);
    }

    public override void rotate()
    {
        Vector2 difference = player.transform.position - this.transform.position;
        difference = difference.normalized;
        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayHitSound(collider);
    }
}
