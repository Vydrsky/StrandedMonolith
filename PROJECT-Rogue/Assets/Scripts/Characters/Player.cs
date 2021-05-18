using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : FightingCharacter
{
    public static Player instance;
    
    private bool collisionOccured = false;

    private PlayerMovement playerMovement; //podklasa do chodzenia

    [SerializeField] private float itemPickupTime = 0f;
    [SerializeField] private float invincibilityStart;
    [SerializeField] private float invincibilityDuration;
    public float InvincibilityStart { get { return invincibilityStart; } set { invincibilityStart = value; } }
    public float InvincibilityDuration { get { return invincibilityDuration; } set { invincibilityDuration = value; } }

    [SerializeField] public HealthBar healthBar;
    [SerializeField] private PlayerJournal activeQuest;
    [SerializeField] private PlayerStats statsUI;
    [SerializeField] private ActiveUI activeUI;
    //[SerializeField] private LineRenderer lineRenderer;
    [SerializeField] public ActiveItem activeItem;
    [SerializeField] public WeaponItem weaponItem;
    public List<GameObject> tempInventory=new List<GameObject>();

    [SerializeField] public List<PassiveItem> Inventory = new List<PassiveItem>();

    public ActiveItem ActiveItem { get { return activeItem; } set { activeItem = value; } }
    public WeaponItem WeaponItem { get { return weaponItem; } set { weaponItem = value; } }

    SimpleWeaponFactory weaponFactory;
    private Quest journal;

    bool hasStartingWeapon = true;

    private int money;
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            if (money < 0)
            {
                money = 0;
            }
        }
    }


    private AudioSource _audioSource;
    [SerializeField] AudioClip[] clipArray;
    [SerializeField] AudioClip takeDamageSound;

    public override void TakeDamage(float damage)
    {
        
        if ( Time.time >= this.InvincibilityStart + this.InvincibilityDuration)
        {
            this.InvincibilityStart = Time.time;
            this.HealthPoints -= (int)damage;
            healthBar.SetHealth(HealthPoints);
            healthBar.SetText(HealthPoints, MaxHealth);
            AudioSource.PlayClipAtPoint(takeDamageSound, transform.position);
        }
        if (healthPoints <= 0)
        {
            SceneManager.LoadScene("GameOver");
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
            if(hasStartingWeapon)
            {
                hasStartingWeapon = false;
                Destroy(WeaponItem.gameObject);
            }
            if (WeaponItem != null)
            {
                WeaponItem temp = collider.gameObject.GetComponent<WeaponItem>();
                WeaponItem.gameObject.SetActive(true);
                WeaponItem.gameObject.transform.position = collider.gameObject.transform.position;
                Vector2 force = transform.position - WeaponItem.transform.position;
                WeaponItem.GetComponent<Rigidbody2D>().AddForce(-force * 2000, ForceMode2D.Force);
                WeaponItem = temp;
                tempInventory.Add(WeaponItem.gameObject);
                
                Debug.Log(tempInventory.Count);
                WeaponItem.weapon.SetAttacker(this);
                collider.gameObject.SetActive(false);
                itemPickupTime = Time.time;
            }
            else
            {
                WeaponItem = collider.gameObject.GetComponent<WeaponItem>();
                tempInventory.Add(WeaponItem.gameObject);
                collider.gameObject.SetActive(false);
                
                Debug.Log(tempInventory.Count);
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
                tempInventory.Add(activeItem.gameObject);
                activeUI.SetCurrentActive(activeItem);
                collider.gameObject.SetActive(false);
                itemPickupTime = Time.time;
            }
            else
            {
                activeItem = collider.gameObject.GetComponent<ActiveItem>();
                tempInventory.Add(activeItem.gameObject);
                Debug.Log(tempInventory.Count);
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
        instance = this;
        weaponFactory = new SimpleWeaponFactory();
        
        characterName = "Hero";
        Invoke("UpdateStats", 0f);

        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(HealthPoints);
        //Debug.Log(attackSpeed);
        healthBar.SetText(HealthPoints, MaxHealth);
        playerMovement = new PlayerMovement(this);
        WeaponItem = Instantiate(WeaponItem);
        WeaponItem.gameObject.SetActive(false);
        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileStartingWeapon);
        WeaponItem.weapon.SetAttacker(this);
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        Money = 10;
        //sound
        weaponItem.weapon.SetWeaponSound(weaponItem.sound);
        //
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
            _audioSource.clip = clipArray[Random.Range(0, clipArray.Length)];
            if (!_audioSource.isPlaying && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                _audioSource.PlayOneShot(_audioSource.clip);
            }
            playerMovement.move(this);
        }
        playerMovement.rotate(this);
        //WeaponSwap();
    }

    public void JournalUpdate()
    {
        if (journal.Update())
        {
            activeQuest.SetColor(Color.green);
            Money += Random.Range(5, 10);
        }

        activeQuest.SetText(journal.JournalEntry());
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
                    activeQuest.SetText("");
                    activeQuest.SetColor(Color.white);
                    journal = null;
                    break;
                case "NPC":
                    if (Level.PickChampionRoom() != null && journal==null)
                    {
                        journal = new KillQuest();
                        activeQuest.SetText(journal.JournalEntry());
                    }

                    break;
                case "ShopItem":
                    ShopItem shopItem = collider.gameObject.GetComponent<ShopItem>();
                    if (shopItem.Buy() <= money)
                    {
                        money -= shopItem.Buy();
                        shopItem.Sell();
                    }
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
            if (collider.tag.Contains("Bullet"))
            {
                Bullet bulletCollision = collider.gameObject.GetComponent<Bullet>();
                if(bulletCollision.attackerTag != "Player")
                    AudioSource.PlayClipAtPoint(takeDamageSound, transform.position);
            }
        }
    }


    //void WeaponSwap()
    //{     
    //    if(Input.GetKey(KeyCode.Alpha1))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileRifleA);
    //        WeaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha2))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileRifleB);
    //        weaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha3))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileShotgunA);
    //        weaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha4))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileShotgunB);
    //        weaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha5))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileSniperRifleA);
    //        weaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha6))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.ProjectileSniperRifleB);
    //        weaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha7))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.RaycastRifleA);
    //        weaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha8))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.RaycastRifleB);
    //        weaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha9))
    //    {
    //        WeaponItem.weapon = weaponFactory.CreateWeapon(WeaponsEnum.RaycastSniperRifle);
    //        weaponItem.weapon.SetAttacker(this);
    //    }
    //    else if (Input.GetKey(KeyCode.Space))
    //    {
    //        Level.FillLevel();
            
    //    }
    //    weaponItem.weapon.SetWeaponSound(weaponItem.sound);
    //}



    private void OnCollisionStay2D(Collision2D collision)
    {
        UpdateHealth();
        if(collision.gameObject.tag.Contains("Enemy"))
        {
            Enemy enemyCollision = collision.gameObject.GetComponent<Enemy>();
            TakeDamage(enemyCollision.Damage);
            
        }
    }

}
