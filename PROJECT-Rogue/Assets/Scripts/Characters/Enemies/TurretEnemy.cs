using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    
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
        range = 15;
        Damage = 2;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        // ustawianie broni
        weapon = new ProjectileRifle(4, 10, 10);
        weapon.SetAttacker(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = player.transform.position - this.transform.position;
        if(distance.magnitude<10)
            weapon.CheckAttack();
        move();
        rotate();
    }
}
