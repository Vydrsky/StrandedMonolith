using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifeTime = 2.0f;
    Rigidbody2D rb;
    CanAttack whoAttacks;
    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * (5 + 2);
        Destroy(gameObject, lifeTime);
    }

    public void SetAttacker(CanAttack whoAttacks)
    {
        this.whoAttacks = whoAttacks;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != whoAttacks.tag && collision.gameObject.tag != "Bullet")
            Destroy(gameObject);
    }
}
