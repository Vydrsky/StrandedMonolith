using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Player : CanAttack, IKeyboard, IMovement
{
    protected float horizontalAxis, verticalAxis;
    [SerializeField] private float invincibilityStart;
    public float InvincibilityStart { get { return invincibilityStart; } set { invincibilityStart = value; } }

    [SerializeField] private float invincibilityDuration;
    public float InvincibilityDuration { get { return invincibilityDuration; } set { invincibilityDuration = value; } }

    Weapon weapon;

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
        weapon.CheckShot();
    }
    public void takeDamage(int damage)
    {
        if ( Time.time >= this.InvincibilityStart + this.InvincibilityDuration)
        {
            this.InvincibilityStart = Time.time;
            this.HealthPoints -= damage;
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
        weapon = new Weapon();
        weapon.SetAttacker(this);
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
    void OnTriggerEnter2D(Collider2D drzwi)
    {
        switch (drzwi.tag)
        {
            case "DoorUp":
                transform.position = new Vector2(transform.position.x,transform.position.y+1);
                Level.MoveCamera(0,7.5f);
                break;
            case "DoorBottom":
                transform.position = new Vector2(transform.position.x,transform.position.y-1);
                Level.MoveCamera(0,-7.5f);
                break;
            case "DoorLeft":
                transform.position = new Vector2(transform.position.x-1,transform.position.y);
                Level.MoveCamera(-18,0);
                break;
            case "DoorRight":
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                Level.MoveCamera(18,0);
                break;
            default:
                break;
        }
    }
}
