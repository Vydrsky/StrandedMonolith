using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CanAttack, IMovement
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

    void collideWithPlayerHandle(Collision2D playerCollision)
    {
        if (playerCollision.gameObject.tag == "Player" && Time.time >= player.InvincibilityStart + player.InvincibilityDuration)
        {
            player.InvincibilityStart = Time.time;
            player.HealthPoints -= 10;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        // ustawianie broni
        weapon = new Weapon();
        weapon.SetAttacker(this);
    }

    private void Update()
    {
        weapon.CheckShot();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collideWithPlayerHandle(collision);
    }
}
