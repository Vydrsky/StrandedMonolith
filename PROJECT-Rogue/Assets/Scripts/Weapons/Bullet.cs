using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float someRandomStuffForFun;
    float lifeTime = 2.0f;
    float bulletSpeed;
    Rigidbody2D rb;
    FightingCharacter whoAttacks;

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * ((5 + 2) + someRandomStuffForFun); // as + bonus + random
        Destroy(gameObject, lifeTime);
        
    }

    private void Start()
    {
        someRandomStuffForFun = Random.Range(-1.0f, 1.0f);
    }


    public void SetAttacker(FightingCharacter whoAttacks)
    {
        this.whoAttacks = whoAttacks;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jesli dany przeciwnik bedzie mial swoj tag to trzeba bedzie zmienic pierwszy warunek
        if (collision.gameObject.tag != whoAttacks.tag && !collision.gameObject.tag.Contains("|Bullet|"))
        {
            FightingCharacter character = collision.transform.GetComponent<FightingCharacter>();
            if (character != null)
            {
                if (character is Player)
                    ((Player)character).TakeDamage(whoAttacks.Damage);
                else
                character.TakeDamage(whoAttacks.Damage);
            }
            Destroy(gameObject);
        }
    }
}
