using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character,IMovement
{
    [SerializeField] private Player player;

    public void Move()
    {

        transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
    }

    public void Rotate()
    {
        Vector3 difference = player.transform.position - this.transform.position;

        difference = difference.normalized;

        float rotationOnZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationOnZ);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Rotate();

    }
}
