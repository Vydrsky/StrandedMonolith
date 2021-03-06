using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RushingEnemy : Enemy
{

    private Vector2 distance;
    private float leaway = 0.5f; 
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        No
    }

    private Direction PlayerCrossing()
    {
        if(player.transform.position.y > this.transform.position.y - leaway && player.transform.position.y < this.transform.position.y + leaway && player.transform.position.x < this.transform.position.x)
        {
            return Direction.Left;
        }
        if (player.transform.position.y > this.transform.position.y - leaway && player.transform.position.y < this.transform.position.y + leaway && player.transform.position.x > this.transform.position.x)
        {
            return Direction.Right;
        }
        if (player.transform.position.x > this.transform.position.x - leaway && player.transform.position.x < this.transform.position.x + leaway && player.transform.position.y > this.transform.position.y)
        {
            return Direction.Up;
        }
        if (player.transform.position.x > this.transform.position.x - leaway && player.transform.position.x < this.transform.position.x + leaway && player.transform.position.y < this.transform.position.y)
        {
            return Direction.Down;
        }
        return Direction.No;
    }

    public override void rotate()
    {
        
    }

    public override void move()
    {
        if (_rigidbody.velocity.sqrMagnitude < 1 && distance.sqrMagnitude < 49)
        {
            switch (PlayerCrossing())
            {
                case Direction.Right: 
                    _rigidbody.AddForce(Vector2.right * moveSpeed * 6, ForceMode2D.Impulse);
                    break;
                case Direction.Left:
                    _rigidbody.AddForce(Vector2.left * moveSpeed * 6, ForceMode2D.Impulse);
                    break;
                case Direction.Up:
                    _rigidbody.AddForce(Vector2.up * moveSpeed * 6, ForceMode2D.Impulse);
                    break;
                case Direction.Down:
                    _rigidbody.AddForce(Vector2.down * moveSpeed * 6, ForceMode2D.Impulse);
                    break;
            }
        }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        timeToWait = Time.time + 1f;
        _audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Wait())
        {
            distance = player.transform.position - this.transform.position;
            move();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayHitSound(collider);
    }
}
