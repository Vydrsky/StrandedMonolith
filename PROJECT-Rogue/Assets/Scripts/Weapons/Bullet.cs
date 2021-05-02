using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impactAnimation;
    int damage;
    Vector2 velocity;
    string attackerTag;
    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
    public void SetParameters(FightingCharacter whoAttacks, int damage, float bulletSpeed, float bulletSize, Vector2 ownerVelocity, float range)
    {
        float bonusBulletSpeed = whoAttacks.AttackSpeed;
        this.damage = damage;
        attackerTag = whoAttacks.tag;
        ownerVelocity = Quaternion.Euler(0f, 0f, -whoAttacks.firePoint.transform.rotation.eulerAngles.z) * ownerVelocity;
        Vector2 baseBulletVelocity = (Vector3.right * (bulletSpeed + bonusBulletSpeed));
        velocity = (baseBulletVelocity + ownerVelocity);
        Destroy(gameObject, range);
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

    private void OnDestroy()
    {
        GameObject obj = Instantiate(impactAnimation, gameObject.transform.position, gameObject.transform.rotation);
        obj.transform.localScale = new Vector2(this.transform.localScale.x * 3, this.transform.localScale.x * 3);
        Destroy(obj, 0.17f);
    }
}
