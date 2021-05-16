using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBoss : BouncingEnemy
{

    [SerializeField] private GameObject minion;
    private GameObject[] minions=new GameObject[30];
    public override void move()
    {
        if (velocity.magnitude < 2)
            _rigidbody.AddRelativeForce(direction * moveSpeed);
    }

    void Start()
    {
        InstantiateMinions();
        InvokeRepeating("SpawnMinion",2,2);
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        _rigidbody = GetComponent<Rigidbody2D>();
        //transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        direction = new Vector2(1f, 1f);
        timeToWait = Time.time + 1f;
        _audioSource = GetComponent<AudioSource>();
    }

    public override void TakeDamage(float damage)
    {
        healthPoints -= (int)damage;
        if (healthPoints <= 0)
        {
            Level.CheckStatus();

            for (int i = 0; i < minions.Length; i++)
            {
                if (minions[i] != null)
                {
                    Destroy(minions[i]);
                }
            }
            Destroy(gameObject);
            Destroy(this);
        }
    }

    void FixedUpdate()
    {
        if (Wait())
        {
            velocity = _rigidbody.velocity;
            move();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Contains("Player") && !collision.gameObject.tag.Contains("Enemy"))
        {
            float speed = velocity.magnitude;
            direction = Vector2.Reflect(velocity.normalized, collision.contacts[0].normal);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _rigidbody.velocity = direction * Mathf.Max(speed, 0f);
        }
    }

    private void SpawnMinion()
    {
        bool temp = false;
        int tempIteration = 0;
        for (int i=0; i < minions.Length; i++)
        {
            if (minions[i]==null)
            {
                temp = true;
                tempIteration = i;
                break;
            }

        }
        if (temp)
        {
            minions[tempIteration] = Instantiate(minion,
                        new Vector2(this.transform.localPosition.x, this.transform.localPosition.y), Quaternion.identity);
        }
    }

    private void InstantiateMinions()
    {
        for (int i = 0; i < minions.Length; i++)
        {
                minions[i] = Instantiate(minion,
                        new Vector2(this.transform.localPosition.x, this.transform.localPosition.y), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayHitSound(collider);
    }
}
