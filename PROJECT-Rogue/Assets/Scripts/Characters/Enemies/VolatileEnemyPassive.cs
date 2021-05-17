using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolatileEnemyPassive : VolatileEnemy
{

    private float turnTimer;
    public override void rotate()
    {
        
    }

    public override void move()
    {
        _rigidbody.AddForce(randomMovement * 1, ForceMode2D.Impulse);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        timeToWait = Time.time + 1f;
        _audioSource = GetComponent<AudioSource>();
        Random.InitState((int)System.DateTime.Now.Ticks & 0x0000FFFF);
        turnTimer = Time.time;
    }


    void Update()
    {
        randomMovement.x = Random.Range(-1, 2);
        randomMovement.y = Random.Range(-1, 2);
    }

    private void FixedUpdate()
    {
        if (Wait())
        {
            move();
            rotate();
        }
        if(Time.time > turnTimer + 2f)
        {
            
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            _rigidbody.AddRelativeForce(Vector2.right*15, ForceMode2D.Impulse);
            turnTimer = Time.time;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayHitSound(collider);
    }
}
