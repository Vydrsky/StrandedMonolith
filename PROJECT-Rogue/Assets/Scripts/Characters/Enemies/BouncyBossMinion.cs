using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBossMinion : RunningEnemy
{
    GameObject boss;
    Vector3 direction;
    Vector3 playerDirection;
    SimpleWeaponFactory weaponFactory;
    private Weapon weapon;
    [SerializeField] private AudioSource sound;

    public override void rotate()
    {
        Vector3 difference = player.transform.position - this.transform.position;
        difference = difference.normalized;
        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

    public override void move()
    {
        if (_rigidbody.velocity.magnitude < 2)
            _rigidbody.AddForce(direction * moveSpeed);
    }

    public bool CheckDistance()
    {
        float distance = Vector3.Distance(boss.transform.position, this.transform.position);
        if(distance > 2.5f)
        {
            return true;
        }
        return false;
    }
    
    public override void TakeDamage(float damage)
    {
        healthPoints -= (int)damage;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        _rigidbody = GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectsWithTag("|Enemy|Boss|")[0];
        _audioSource = GetComponent<AudioSource>();
        weaponFactory = new SimpleWeaponFactory();
        weapon = weaponFactory.CreateWeapon(WeaponsEnum.EnemyProjectilePistol);
        weapon.SetAttacker(this);
        weapon.SetWeaponSound(sound);
    }

    void FixedUpdate()
    {
        if (Wait())
        {
            playerDirection = player.transform.position - transform.position;
            direction = boss.transform.position - transform.position;
            if (CheckDistance())
                move();
            if (playerDirection.magnitude < 10)
            {
                rotate();
                weapon.CheckAttack();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayHitSound(collider);
    }
}
