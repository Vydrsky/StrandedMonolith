using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FightingCharacter, IMovement
{
    [SerializeField] private Player player;
    private float delay;
    Weapon weapon; // Do wywalenie, tylko na czas testowania

    public void Move()
    {
        _rigidbody.AddRelativeForce(Vector2.right*moveSpeed);
    }

    public void Rotate()
    {
        Vector3 difference = player.transform.position - this.transform.position;
        difference = difference.normalized;
        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

   

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        // ustawianie broni
        weapon = new ProjectileRiffle();
        weapon.SetAttacker(this);
    }

    private void Update()
    {
        weapon.CheckAttack();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    
}
