using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impactAnimation;
    int damage;
    float range;
    Vector2 velocity;
    public string attackerTag;
    float time;

    private void Start()
    {
        time = Time.time;
    }
    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        if(Time.time > time + range)
        {
            PlayImpactAnimation();
        }
    }
    public void SetParameters(FightingCharacter whoAttacks, int damage, float bulletSpeed, float bulletSize, Vector2 ownerVelocity, float range)
    {
        float bonusBulletSpeed = whoAttacks.ShotSpeed;
        this.damage = damage;
        attackerTag = whoAttacks.tag;
        ownerVelocity = Quaternion.Euler(0f, 0f, -whoAttacks.firePoint.transform.rotation.eulerAngles.z) * ownerVelocity;
        Vector2 baseBulletVelocity = (Vector3.right * (bulletSpeed + bonusBulletSpeed));
        velocity = (baseBulletVelocity + ownerVelocity);
        this.transform.localScale = new Vector2(this.transform.localScale.x * bulletSize, this.transform.localScale.y * bulletSize);
        this.range = range;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" && attackerTag == "Player") ||
            (collision.gameObject.tag.Contains("Enemy") && attackerTag.Contains("Enemy")) ||
            collision.gameObject.tag.Contains("|Bullet|") ||
            collision.gameObject.tag.Contains("|Item|"))
            return;

            FightingCharacter character = collision.transform.GetComponent<FightingCharacter>();
            if (character != null)
            {
                character.TakeDamage(damage);
            }
        PlayImpactAnimation();
    }


    private void PlayImpactAnimation()
    {
        gameObject.SetActive(false);
        GameObject obj = Instantiate(impactAnimation, gameObject.transform.position, gameObject.transform.rotation);
        obj.transform.parent = null;
        obj.SetActive(true);
        obj.transform.localScale = new Vector2(this.transform.localScale.x * 3, this.transform.localScale.x * 3);
        Destroy(obj, 0.17f);
        Destroy(gameObject);
    }
}
