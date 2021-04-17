using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IKeyboard,IMovement
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float invincibilityDelay = 0;


    public void readMovementInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
    }
    public void Move()
    {
        _rigidbody.AddForce(new Vector2(horizontalAxis*moveSpeed,0),ForceMode2D.Impulse);
        _rigidbody.AddForce(new Vector2(0,verticalAxis*moveSpeed),ForceMode2D.Impulse);
    }

    public void readTurnInput()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotDir = RotationDirectionEnum.UpDirection;     //enum opisany w RotationDirectionEnum.cs
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RotDir = RotationDirectionEnum.DownDirection;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotDir = RotationDirectionEnum.LeftDirection;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotDir = RotationDirectionEnum.RightDirection;
        }
    }

    public void Rotate()
    {
        switch (RotDir)
        {
            case RotationDirectionEnum.UpDirection:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    break;
                }
            case RotationDirectionEnum.LeftDirection:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    break;
                }
            case RotationDirectionEnum.DownDirection:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                    break;
                }
            case RotationDirectionEnum.RightDirection:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Enemy" && Time.time>=invincibilityDelay+1.0f)
        {
            invincibilityDelay = Time.time;
            this.healthPoints -= 10;
        }
        invincibilityDelay = 0;
    }
}
