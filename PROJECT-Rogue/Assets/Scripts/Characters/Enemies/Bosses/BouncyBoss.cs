using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBoss : BouncingEnemy
{
    public override void move()
    {
        if (velocity.magnitude < 2)
            _rigidbody.AddRelativeForce(Vector2.right * moveSpeed);
    }

    void Start()
    {
        range = 15;
        Damage = 2;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        _rigidbody = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        direction = new Vector2(1f, 0);
    }


    void Update()
    {
        velocity = _rigidbody.velocity;
    }

    void FixedUpdate()
    {
        move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Contains("Player") && !collision.gameObject.tag.Contains("Enemy"))
        {
            float speed = velocity.magnitude;
            direction = Vector2.Reflect(velocity.normalized, collision.contacts[0].normal);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _rigidbody.velocity = direction * Mathf.Max(speed, 0f);
        }
    }
}
