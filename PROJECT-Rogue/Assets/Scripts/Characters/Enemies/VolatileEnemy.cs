using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolatileEnemy : Enemy
{
    protected Vector2 randomMovement;
    public override void rotate()
    {
        Vector3 difference = player.transform.position - this.transform.position;
        difference = difference.normalized;
        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

    public override void move()
    {
        _rigidbody.AddForce(randomMovement * 1, ForceMode2D.Impulse);
        _rigidbody.AddRelativeForce(Vector2.right * 20);
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
        PlayHitSound(collider);
    }
}
