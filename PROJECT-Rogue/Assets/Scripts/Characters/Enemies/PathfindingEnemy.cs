using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingEnemy : Enemy
{
    GameObject EnemyAIobj;
    EnemyAI enemyAIscript;
    Vector2 difference;
    public override void move()
    {
        //transform.Translate(new Vector2(1 * moveSpeed, 0) * Time.deltaTime);
        _rigidbody.AddRelativeForce(Vector2.right * moveSpeed);
        //_rigidbody.AddRelativeForce(difference * moveSpeed);
    }

    public override void rotate()
    {
        LayerMask newMask = 1 << LayerMask.NameToLayer("Enemy");
        newMask |= 1 << LayerMask.NameToLayer("Flying");
        newMask |= 1 << LayerMask.NameToLayer("Raycast Ignore");
        newMask = ~newMask;


        Vector2 temp = player.transform.position - this.firePoint.transform.position;
        float rotation = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg; ;
        this.firePoint.rotation = Quaternion.Euler(0f, 0f, rotation);

        RaycastHit2D hitInfo = Physics2D.Raycast(this.firePoint.position, this.firePoint.right, 100, newMask);

        //do usuniecia
        //LineRenderer lineRenderer = Shooting.instance.raycastPrefabs.Find(x => x.tag.Contains("|RayRiffle|"));

        //var obj = Object.Instantiate(lineRenderer);
        //obj.SetPosition(0, this.firePoint.position);
        //obj.SetPosition(1, hitInfo.transform.position);
        //obj.enabled = true;
        //

        if (hitInfo.transform.gameObject.tag == "Player")
        {
            difference = Player.instance.transform.position - this.transform.position;
            //Debug.Log("Player hit");
        }
        else
        {    
            difference = enemyAIscript.CheckDirection();
            float x, y;
            int enemyX, enemyY;
            x = difference.x;
            y = difference.y;
            enemyX = Mathf.RoundToInt(transform.position.x) + (int)x;
            enemyY = Mathf.RoundToInt(transform.position.y) + (int)y;
            difference = new Vector2(enemyX, enemyY) - new Vector2(transform.position.x, transform.position.y);
            //Debug.Log("Not player hit");
        }

        difference = difference.normalized;
        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        EnemyAIobj = transform.GetChild(0).gameObject;
        enemyAIscript = transform.GetChild(0).gameObject.GetComponent<EnemyAI>();
        timeToWait = Time.time + 1f;
        _audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Wait())
        {
            rotate();
            move();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Contains("Bullet"))
        {
            AudioSource.PlayClipAtPoint(_audioSource.clip, this.transform.position, _audioSource.volume);
        }
    }
}
