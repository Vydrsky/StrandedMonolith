using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolatileEnemyPassive : VolatileEnemy
{
    public override void rotate()
    {
        
    }

    public override void move()
    {
        _rigidbody.AddForce(randomMovement * 1, ForceMode2D.Impulse);
    }

    void Start()
    {
        range = 15;
        Damage = 2;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        timeToWait = Time.time + 1f;
        _audioSource = GetComponent<AudioSource>();
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
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Contains("Bullet"))
        {
            AudioSource.PlayClipAtPoint(_audioSource.clip, this.transform.position, _audioSource.volume);
        }
    }
}
