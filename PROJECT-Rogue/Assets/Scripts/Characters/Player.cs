using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class Player : FightingCharacter
{
    private bool collisionOccured = false;

    private PlayerMovement playerMovement; //podklasa do chodzenia

    [SerializeField] private float itemPickupTime = 0f;
    [SerializeField] private float invincibilityStart;
    [SerializeField] private float invincibilityDuration;
    public float InvincibilityStart { get { return invincibilityStart; } set { invincibilityStart = value; } }
    public float InvincibilityDuration { get { return invincibilityDuration; } set { invincibilityDuration = value; } }

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerStats statsUI;
    [SerializeField] private ActiveUI activeUI;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ActiveItem activeItem;
    [SerializeField] private WeaponItem weaponItem;

    [SerializeField] public List<PassiveItem> Inventory = new List<PassiveItem>();

    public ActiveItem ActiveItem { get { return activeItem; } set { activeItem = value; } }
    public WeaponItem WeaponItem { get { return weaponItem; } set { weaponItem = value; } }

    SimpleWeaponFactory weaponFactory;
    private Quest journal;


    public override void TakeDamage(int damage)
    {
        if ( Time.time >= this.InvincibilityStart + this.InvincibilityDuration)
        {
            this.InvincibilityStart = Time.time;
            this.HealthPoints -= damage;
            healthBar.SetHealth(HealthPoints);
            healthBar.SetText(HealthPoints, MaxHealth);
        }
    }

    private void UpdateHealth()
    {
        healthBar.SetHealth(HealthPoints);
        healthBar.SetText(HealthPoints, MaxHealth);
    }

    public void SwapWeapon(Collider2D collider)
    {
        if (Time.time >= itemPickupTime + 1f)
        {
            if (WeaponItem != null)
            {
                WeaponItem temp = collider.gameObject.GetComponent<WeaponItem>();
                WeaponItem.gameObject.SetActive(true);
                WeaponItem.gameObject.transform.position = collider.gameObject.transform.position;
                Vector2 force = transform.position - WeaponItem.transform.position;
                WeaponItem.GetComponent<Rigidbody2D>().AddForce(-force * 2000, ForceMode2D.Force);
                WeaponItem = temp;
                WeaponItem.weapon.SetAttacker(this);
                collider.gameObject.SetActive(false);
                itemPickupTime = Time.time;
            }
            else
            {
                WeaponItem = collider.gameObject.GetComponent<WeaponItem>();
                collider.gameObject.SetActive(false);
                WeaponItem.weapon.SetAttacker(this);
                itemPickupTime = Time.time;
            }
        }
    }

    public void SwapActiveItem(Collider2D collider)
    {
        if (Time.time >= itemPickupTime + 1f)
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
                collider.gameObject.SetActive(false);
                itemPickupTime = Time.time;
            }
        }
    }

    private void UpdateStats()
    {
        statsUI.SetStat("MS", MoveSpeed);
        statsUI.SetStat("DAMAGE", Damage);
        statsUI.SetStat("AS", AttackSpeed);
        statsUI.SetStat("SHOTSPEED", ShotSpeed);
        statsUI.SetStat("RANGE", Range);
    }

    private void Start()
    {
        weaponFactory = new SimpleWeaponFactory();
        
        characterName = "Hero";
        Invoke("UpdateStats", 0f);

        Damage = 2;
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(HealthPoints);
        range = 10;
        attackSpeed = 1;
        Debug.Log(attackSpeed);
        healthBar.SetText(HealthPoints, MaxHealth);
        playerMovement = new PlayerMovement(this);
        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.RaycastRifleA);
        WeaponItem.weapon.SetAttacker(this);
    }
    void Update()
    {
        if (Input.anyKey)           //INPUT RUCH
        {
            playerMovement.readMovementInput();
            playerMovement.readTurnInput();
        }
        //WeaponSwap();
        if (activeItem != null && Input.GetKeyDown(KeyCode.E))  //INPUT PRZEDMIOT AKTYWNY    
        {
            activeItem.Effect(this);
            Invoke("UpdateStats", 0f);
        }
        if (activeItem != null && activeItem.EffectRanOut())
        {
            activeItem.RemoveEffect(this);
            Invoke("UpdateStats", 0f);
        }
    }

    void FixedUpdate()
    {
        if (collisionOccured == true)
        {
            collisionOccured = false;
        }

        if (Input.anyKey)
        {
            playerMovement.move(this);
        }
        playerMovement.rotate(this);
        WeaponSwap();
    }

    public void JournalUpdate()
    {
        journal.Update();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Invoke("UpdateStats",0f);
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
                case "Trapdoor":
                    Level.FillLevel();
                    break;
                case "NPC":
                    journal=new KillQuest();
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
                SwapActiveItem(collider);
            }
            if (collider.tag.Contains("Weapon"))
            {
                SwapWeapon(collider);
            }
        }
    }


    void WeaponSwap()
    {     
        if(Input.GetKey(KeyCode.Alpha1))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileRifleA);
            WeaponItem.weapon.SetAttacker(this);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileRifleB);
            weaponItem.weapon.SetAttacker(this);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileShotgunA);
            weaponItem.weapon.SetAttacker(this);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileShotgunB);
            weaponItem.weapon.SetAttacker(this);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileSniperRifleA);
            weaponItem.weapon.SetAttacker(this);
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileSniperRifleB);
            weaponItem.weapon.SetAttacker(this);
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.RaycastRifleA);
            weaponItem.weapon.SetAttacker(this);
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.RaycastRifleB);
            weaponItem.weapon.SetAttacker(this);
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.RaycastSniperRifle);
            weaponItem.weapon.SetAttacker(this);
        }
    }

    

    private void OnCollisionStay2D(Collision2D collision)
    {
        UpdateHealth();
        if(collision.gameObject.tag.Contains("Enemy"))
        {
            TakeDamage(15);
        }
    }

}
