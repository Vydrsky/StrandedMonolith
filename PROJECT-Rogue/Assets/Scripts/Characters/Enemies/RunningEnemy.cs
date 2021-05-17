using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningEnemy : PathfindingEnemy
{
    public override void move()
    {
        //transform.Translate(new Vector2(1 * moveSpeed, 0) * Time.deltaTime);
        _rigidbody.AddRelativeForce(Vector2.right * moveSpeed);
        //_rigidbody.AddRelativeForce(difference * moveSpeed);
    }

    public override void rotate()
    {
        Vector2 difference = player.transform.position - this.transform.position;
        string[] layerName = { "Raycast Ignore", "Enemy", "Flying", "MiddleGround" };
        
        float rotationOnZ = GetRotation(layerName);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        timeToWait = Time.time + 1f;
        _audioSource = GetComponent<AudioSource>();
        enemyAIscript = transform.GetChild(0).gameObject.GetComponent<EnemyAI>();
    }

    void FixedUpdate()
    {
        if (Wait())
        {
            rotate();
            move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayHitSound(collider);
    }
}
