using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBossMinion : RunningEnemy
{
    GameObject boss;
    public override void rotate()
    {
        Vector3 difference = boss.transform.position - this.transform.position;
        difference = difference.normalized;
        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }
    public bool CheckDistance()
    {
        float distance = Vector3.Distance(boss.transform.position, this.transform.position);
        if(distance > 2.5f)
        {
            return true;
        }
        return false;
    }

    void Start()
    {
        range = 15;
        Damage = 2;
        _rigidbody = GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectsWithTag("|Enemy|Boss|")[0];
    }

    void FixedUpdate()
    {
        rotate();
        if(CheckDistance())
            move();
    }
    
}
