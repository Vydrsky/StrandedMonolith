using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody2D _rigidbody;


    new public void Move()  //przes�oni�cie metody z Character, ta u�ywa si�y do poruszania
    {
        _rigidbody.AddForce(new Vector2(horizontalAxis*moveSpeed,0),ForceMode2D.Impulse);
        _rigidbody.AddForce(new Vector2(0,verticalAxis*moveSpeed),ForceMode2D.Impulse);
    }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        characterName = "Hero";
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            readMovementInput();
            readTurnInput();
        }
    }

    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            Move();
            Rotate();
        }
    }

    // void OnTriggerEnter2D()
    // {
    //     transform.position = new Vector2(0, 0);
    // }
}
