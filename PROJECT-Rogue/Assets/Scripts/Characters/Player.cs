using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Player : FightingCharacter, IKeyboard, IMovement
{
    private float horizontalAxis, verticalAxis;
    private bool collisionOccured = false;

    [SerializeField] private float invincibilityStart;
    [SerializeField] private float invincibilityDuration;

    [SerializeField] public LineRenderer lineRenderer;

    public float InvincibilityStart { get { return invincibilityStart; } set { invincibilityStart = value; } }
    public float InvincibilityDuration { get { return invincibilityDuration; } set { invincibilityDuration = value; } }

    bool playerRotated = false;

    Weapon weapon;

    public List<PassiveItem> Inventory = new List<PassiveItem>();

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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotDir = RotationDirectionEnum.UpDirection;     //enum opisany w RotationDirectionEnum.cs
            playerRotated = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotDir = RotationDirectionEnum.DownDirection;
            playerRotated = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotDir = RotationDirectionEnum.LeftDirection;
            playerRotated = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotDir = RotationDirectionEnum.RightDirection;
            playerRotated = true;
        }
    }

    public void Rotate()
    {
        if(playerRotated)
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
            weapon.CheckAttack();
            playerRotated = false;
        }
    }
    new public void TakeDamage(float damage)
    {
        if ( Time.time >= this.InvincibilityStart + this.InvincibilityDuration)
        {
            this.InvincibilityStart = Time.time;
            this.HealthPoints -= (int)damage;
        }
    }
    

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        characterName = "Hero";
    }
    void Start()
    {
        Damage = 10;
        //weapon = new ProjectileSniperRiffle();
        //weapon = new ProjectileShotgun();
        //weapon = new ProjectileRiffle();
        weapon = new Raycast();
        weapon.SetAttacker(this);
    }


    void Update()
    {
        if(collisionOccured == true)
        {
            collisionOccured = false;
        }
        if (Input.anyKey)
        {
            readMovementInput();
            readTurnInput();
        }
        WeaponSwap();
    }

    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            Move();
        }
            Rotate();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collisionOccured == false)
        {
            collisionOccured = true;
            switch (collider.tag)
            {
                case "DoorUp":
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                    Level.MoveCamera(0, 7.5f);
                    break;
                case "DoorBottom":
                    transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                    Level.MoveCamera(0, -7.5f);
                    break;
                case "DoorLeft":
                    transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                    Level.MoveCamera(-18, 0);
                    break;
                case "DoorRight":
                    transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                    Level.MoveCamera(18, 0);
                    break;
                default:
                    break;
            }

            if (collider.tag.Contains("Instant"))
            {
                
                InstantItem temp = collider.gameObject.GetComponent<InstantItem>();
                if (temp.CheckUsability(this))
                {
                    temp.immediateEffectOnPlayer(this);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.tag.Contains("Passive"))
            {
                
                PassiveItem temp = collider.gameObject.GetComponent<PassiveItem>();
                temp.AddToInventory(this);
                foreach(PassiveItem i in Inventory)
                {
                    Debug.Log(i.itemInfo());
                }
                Destroy(collider.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Contains("Enemy"))
        {
            TakeDamage(10);
        }
    }



    //Do testowania
    void WeaponSwap()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = new ProjectileRiffle();
            weapon.SetAttacker(this);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = new ProjectileShotgun();
            weapon.SetAttacker(this);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon = new ProjectileSniperRiffle();
            weapon.SetAttacker(this);

        }
    }

}
