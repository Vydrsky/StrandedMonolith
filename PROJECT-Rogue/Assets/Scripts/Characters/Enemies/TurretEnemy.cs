using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    public AudioSource sound;
    SimpleWeaponFactory weaponFactory;   
    private Weapon weapon;
    private Vector2 distance;
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

    // Start is called before the first frame update
    void Start()
    {
        weaponFactory = new SimpleWeaponFactory();
        range = 15;
        Damage = 2;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        // ustawianie broni
        weapon = weaponFactory.CreateWeapon(WeaponsEnum.EnemyProjectileRifle);
        weapon.SetAttacker(this);
        weapon.SetWeaponSound(sound);
        timeToWait = Time.time + 1f;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Wait())
        {
            distance = player.transform.position - this.transform.position;
            move();
            rotate();
            if (distance.magnitude < 10)
                weapon.CheckAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayHitSound(collider);
    }
}
