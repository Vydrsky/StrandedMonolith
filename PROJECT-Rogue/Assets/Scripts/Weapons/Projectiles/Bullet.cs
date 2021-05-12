using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{
    public GameObject impactAnimation;
    int damage;
    Vector2 velocity;
    string attackerTag;
    private AudioSource sound;
    public AudioMixer mixer;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        //float rand = Random.Range(0.75f, 1.25f);
        //sound.pitch = rand;
        AudioSource.PlayClipAtPoint(sound.clip, transform.position,sound.volume);
    }
    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
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
        Destroy(gameObject, range);
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
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject obj = Instantiate(impactAnimation, gameObject.transform.position, gameObject.transform.rotation);
        obj.transform.localScale = new Vector2(this.transform.localScale.x * 3, this.transform.localScale.x * 3);
        Destroy(obj, 0.17f);
    }
}
