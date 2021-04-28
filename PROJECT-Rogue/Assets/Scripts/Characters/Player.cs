using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class Player : FightingCharacter, IKeyboard, IMovement
{
    private float horizontalAxis, verticalAxis;
    private bool collisionOccured = false;
    private bool playerRotated = false;
    [SerializeField]private float itemPickupTime = 0f;

    [SerializeField] private float invincibilityStart;
    [SerializeField] private float invincibilityDuration;

    [SerializeField] public LineRenderer lineRenderer;

    public float InvincibilityStart { get { return invincibilityStart; } set { invincibilityStart = value; } }
    public float InvincibilityDuration { get { return invincibilityDuration; } set { invincibilityDuration = value; } }


    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerStats statsUI;
    [SerializeField] private ActiveUI activeUI;
    Weapon weapon;

    public List<PassiveItem> Inventory = new List<PassiveItem>();

    [SerializeField]private ActiveItem activeItem;
    public ActiveItem ActiveItem { get; set; }

    public void readMovementInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
    }
    public void move()
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

    public void rotate()
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

    
    public override void TakeDamage(float damage)
    {
        if ( Time.time >= this.InvincibilityStart + this.InvincibilityDuration)
        {
            this.InvincibilityStart = Time.time;
            this.HealthPoints -= (int)damage;
            healthBar.SetHealth(HealthPoints);
            healthBar.SetText(HealthPoints, MaxHealth);
        }
    }

    private void UpdateHealth()
    {
        healthBar.SetHealth(HealthPoints);
        healthBar.SetText(HealthPoints, MaxHealth);
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
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(HealthPoints);
        healthBar.SetText(HealthPoints, MaxHealth);
    }


    void Update()
    {
        UpdateStats();

        if (Input.anyKey)           //INPUT RUCH
        {
            readMovementInput();
            readTurnInput();
        }
        WeaponSwap();
        if (activeItem != null && Input.GetKeyDown(KeyCode.E))  //INPUT PRZEDMIOT AKTYWNY    
        {
            activeItem.Effect(this);
        }
        if (activeItem != null && activeItem.EffectRanOut())
        {
            activeItem.RemoveEffect(this);
        }
    }

    private void UpdateStats()
    {
        statsUI.SetStat("MS", this.MoveSpeed);
        statsUI.SetStat("DAMAGE", this.Damage);
        statsUI.SetStat("AS", this.AttackSpeed);
        statsUI.SetStat("SHOTSPEED", this.ShotSpeed);
        statsUI.SetStat("RANGE", this.Range);
    }

    void FixedUpdate()
    {

        if (collisionOccured == true)
        {
            collisionOccured = false;
        }

        if (Input.anyKey)
        {
            move();
        }
            rotate();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collisionOccured == false)
        {
            collisionOccured = true;
            switch (collider.tag)
            {
                case "DoorUp":
                    transform.position = new Vector2(transform.position.x, transform.position.y + 3f);
                    Level.MoveCamera(0, 15);
                    Level.MoveFocus(0,-1);
                    break;
                case "DoorBottom":
                    transform.position = new Vector2(transform.position.x, transform.position.y - 3f);
                    Level.MoveCamera(0, -15);
                    Level.MoveFocus(0,1);
                    break;
                case "DoorLeft":
                    transform.position = new Vector2(transform.position.x - 3f, transform.position.y);
                    Level.MoveCamera(-36, 0);
                    Level.MoveFocus(-1,0);
                    break;
                case "DoorRight":
                    transform.position = new Vector2(transform.position.x + 3f, transform.position.y);
                    Level.MoveCamera(36, 0);
                    Level.MoveFocus(1,0);
                    break;
                case "Button":
                    Level.RemoveFocus();
                    break;
                default:
                    break;
            }

            if (collider.tag.Contains("Instant"))
            {
                
                InstantItem temp = collider.gameObject.GetComponent<InstantItem>();
                if (temp.CheckUsability(this))
                {
                    temp.ImmediateEffectOnPlayer(this);
                    UpdateHealth();
                    Destroy(collider.gameObject);
                }
            }
            if (collider.tag.Contains("Passive"))
            {
                
                PassiveItem temp = collider.gameObject.GetComponent<PassiveItem>();
                temp.AddToInventory(this);
                foreach(PassiveItem i in Inventory)
                {
                    Debug.Log(i.ItemInfo());
                }
                collider.gameObject.SetActive(false);
                healthBar.SetMaxHealth(MaxHealth);
                UpdateHealth();
            }
            if (collider.tag.Contains("Active"))
            {
                if (Time.time >= itemPickupTime + 0.1f)
                {
                    if (activeItem != null)
                    {
                        if (activeItem.IsActive)
                            activeItem.RemoveEffect(this);
                        ActiveItem temp = collider.gameObject.GetComponent<ActiveItem>();
                        activeItem.gameObject.SetActive(true);
                        activeItem.gameObject.transform.position = collider.gameObject.transform.position;
                        Vector2 force = transform.position - activeItem.transform.position;
                        activeItem.GetComponent<Rigidbody2D>().AddForce(-force * 2000, ForceMode2D.Force);
                        activeItem = temp;
                        activeUI.SetCurrentActive(activeItem);
                        collider.gameObject.SetActive(false);
                        itemPickupTime = Time.time;
                    }
                    else
                    {
                        activeItem = collider.gameObject.GetComponent<ActiveItem>();
                        activeUI.SetCurrentActive(activeItem);
                        Debug.Log(activeItem);
                        collider.gameObject.SetActive(false);
                        itemPickupTime = Time.time;
                    }
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        UpdateHealth();
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
