using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifeTime = 2.0f;
    float damage;
    Rigidbody2D rb, charRb;
    Vector2 velocity;
    string attackerTag;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
    public void SetParameters(FightingCharacter whoAttacks, float damage, float bulletSpeed, float bulletSize, Vector2 ownerVelocity)
    {
        float bonusBulletSpeed = whoAttacks.AttackSpeed;
        this.damage = damage;
        attackerTag = whoAttacks.tag;
        rb = GetComponent<Rigidbody2D>();
        ownerVelocity = Quaternion.Euler(0f, 0f, -whoAttacks.firePoint.transform.rotation.eulerAngles.z) * ownerVelocity;
        Vector2 baseBulletVelocity = (Vector3.right * (bulletSpeed + bonusBulletSpeed));
        velocity = (baseBulletVelocity + ownerVelocity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jesli dany przeciwnik bedzie mial swoj tag to trzeba bedzie zmienic pierwszy warunek
        if (collision.gameObject.tag != attackerTag &&
            !collision.gameObject.tag.Contains("|Bullet|") &&
            !collision.gameObject.tag.Contains("|Item|"))
        {
            FightingCharacter character = collision.transform.GetComponent<FightingCharacter>();
            if (character != null)
            {
                character.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
