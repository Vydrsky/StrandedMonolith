using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : FightingCharacter
{
    public float timeToWait;
    protected GameObject player;
    private bool isTarget;
    protected AudioSource _audioSource;
    public bool IsTarget
    {
        get
        {
            return isTarget;
        }
        set
        {
            isTarget = value;
            this.GetComponent<SpriteRenderer>().color=Color.blue;
            this.attackSpeed *= 2f;
            this.maxHealth *= 2;
            this.damage *= 2;
            this.healthPoints *= 2;
            this.GetComponent<Transform>().localScale = new Vector2(this.transform.localScale.x*1.5f, this.transform.localScale.y*1.5f);
        }
    }

    protected float delay;

    public abstract void move();

    public abstract void rotate();

    public override void TakeDamage(float damage)
    {
        healthPoints -= (int)damage;
        if (healthPoints <= 0)
        {
            Level.CheckStatus();
            if (IsTarget)
            {
                Level.staticGracz.GetComponent<Player>().JournalUpdate();
                IsTarget = false;
            }
            Destroy(gameObject);
            Destroy(this);
        }
    }

    protected bool Wait()
    {
        if (Time.time > timeToWait)
        {
            return true;
        }
        return false;
    }

    protected LayerMask MakeMask(string[] layerName)
    {
        LayerMask mask = 0;
        foreach (var item in layerName)
        {
            mask |= 1 << LayerMask.NameToLayer(item);
        }
        return ~mask;
    }

    protected bool IsPlayerInSight(LayerMask mask, int distance = 100, bool showRay = false)
    {
        Vector2 temp = player.transform.position - this.firePoint.transform.position;
        float rotation = (Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg) - (Mathf.Atan2(this.transform.rotation.y, this.transform.rotation.x) * Mathf.Rad2Deg);
        this.firePoint.rotation = Quaternion.Euler(0f, 0f, rotation);

        RaycastHit2D hitInfo = Physics2D.Raycast(this.firePoint.position, this.firePoint.right, distance, mask);

        if(showRay)
        {
            LineRenderer lineRenderer = Shooting.instance.raycastPrefabs.Find(x => x.tag.Contains("|RayRiffle|"));

            var obj = Object.Instantiate(lineRenderer);
            obj.SetPosition(0, this.firePoint.position);
            obj.SetPosition(1, hitInfo.transform.position);
            obj.enabled = true;
        }

        return hitInfo.transform.gameObject.tag == "Player";

    }


    protected void PlayHitSound(Collider2D collider)
    {
        Bullet bulletCollision = collider.gameObject.GetComponent<Bullet>();

        if (collider.gameObject.tag.Contains("Bullet") && bulletCollision.attackerTag == "Player")
        {
            AudioSource.PlayClipAtPoint(_audioSource.clip, this.transform.position, _audioSource.volume);
        }
    }

}
