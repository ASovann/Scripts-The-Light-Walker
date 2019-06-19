
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMotor : MonoBehaviour
{
    //script player inventory
    PlayerInventory playerInv;

    public GameObject[] enemyList;
    private float Distance;
    public GameObject UIMenu;
    public GameObject UIInv;
    public GameObject UIEquip;

    //audio
    public AudioClip attack;
    private AudioSource source;

    public int itemId;
    public List<GameObject> itemList = new List<GameObject>();
    public List<GameObject> secondList;
    public GameObject bodyPartHand;
    public GameObject bodyPartBack;

    public float regenHealth = 1;
    public float regenMana = 5;
    private float regenReset;
    public float regenTime;
    private float resetTime;
    public Vector3 basePosition;
    public float rebirthTime;
    public int CheatCount = 0;
    public Vector3 jumpsSpeed;
    CapsuleCollider playerCollider;
    public bool isDead = false;

    //animation du perso
    Animation animations;

    //vistesse du perso
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;


    //inputs
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    //variable concernant l'attaque
    public float attackCooldown;
    public bool isAttacking;
    public bool isCasting;
    private float currentCooldown;
    public float attackRange;
    private GameObject rayHit;


    //spell
    [Header("Paramètre des sorts")]
    public GameObject rayCast;
    public GameObject rayCast2;
    private GameObject spellHolderImg;
    public int currentSpell = 1;
    private int totalSpell;
    public bool isLeveling = false;
    public List<GameObject> spellList = new List<GameObject>();
    public List<GameObject> OnSelfSpellList = new List<GameObject>();


    //fire spell
    [Header("Paramètre des sorts de feu")]
    public GameObject FireBallSpellGO;
    public float FireBallSpellCost;
    public float FireBallSpellSpeed;
    public int FireBallSpellID;
    public Sprite FireBallSpellImage;
    public float FireBallDamage;
    public AudioClip fireBallSound;

    //heal spell
    [Header("Paramètre des sorts de heal")]
    public GameObject HealSpellGO;
    public float HealSpellCost;
    public float HealSpellAmount;
    public int HealSpellID;
    public Sprite HealSpellImage;
    public AudioClip healSound;

    //lightning spell
    [Header("Paramètre des sorts de foudre")]
    public GameObject LightningSpellGO;
    public float LightningSpellCost;
    public int LightningSpellID;
    public float LightningSpellDamage;
    public Sprite LightningSpellImage;
    public AudioClip electricSound;

    //energy spell
    [Header("Paramètre des sort d'energie")]
    public GameObject EnergySpellGO;
    public float EnergySpellCost;
    public int EnergySpellID;
    public float EnergySpellDamage;
    public Sprite EnergySpellImage;
    public float EnergySpellRangeDistance;
    public AudioClip energySound;

    //whitebomb spell
    [Header("Paramètre des sort bombe blanche")]
    public GameObject WhiteBombSpellGO;
    public float WhiteBombSpellCost;
    public int WhiteBombSpellID;
    public float WhiteBombSpellDamage;
    public Sprite WhiteBombSpellImage;
    public float WhiteBombSpellRangeDistance;
    public AudioClip whiteBombSound;

    //boostattack spell
    [Header("Paramètre des sorts boost attaque")]
    public GameObject BoostAttackSpellGO;
    public float BoostAttackSpellCost;
    public int BoostAttackSpellID;
    public float BoostAttackSpellPercentage;
    public Sprite BoostAttackSpellImage;
    public float BoostAttackTime;
    private bool isBoostAttack = false;
    private float BoostAttackReset;
    private float currentDamage;
    public AudioClip boostattackSound;

    //shield spell
    [Header("Paramètres du sort de bouclier")]
    public GameObject ShieldSpellGO;
    public float ShieldSpellCost;
    public int ShieldSpellID;
    public Sprite ShieldSpellImage;
    public float ShieldSpellTime;
    private bool isShielded = false;
    private float ShieldSpellReset;
    private float currentLife;
    public AudioClip shieldSound;


    //flamethrower spell
    [Header("Paramètres du sort lance flamme")]
    public GameObject FlameThrowerGO;
    public float FlameThrowerCost;
    public int FlameThrowerID;
    public Sprite FlameThrowerImage;
    public float FlameThrowerTime;
    public bool isThrowing = false;
    public float FlameThrowerReset;
    public float FlameThrowerDamage;
    public AudioClip flamethrowerSound;

    //ice spell
    [Header("Paramètres du sort de glace")]
    public GameObject IceSpellGO;
    public float IceSpellCost;
    public int IceSpellID;
    public Sprite IceSpellImage;
    public float IceSpellSpeed;
    public float IceSpellDamage;
    public AudioClip iceSound;

    //speed spell
    [Header("Paramètres du sort de vitesse")]
    public GameObject SpeedSpellGO;
    public float SpeedSpellCost;
    public int SpeedSpellID;
    public Sprite SpeedSpellImage;
    public float SpeedSpellTime;
    private float SpeedSpellReset;
    private bool IsSpeedster = false;
    public float SpeedSpellAmount;
    public AudioClip speedSound;

    //slow spell
    [Header("Paramètres du sort de ralentissement")]
    //public GameObject SlowSpellGO;
    public float SlowSpellCost;
    public int SlowSpellID;
    public Sprite SlowSpellImage;
    public float SlowSpellTime;
    private float SlowSpellReset;
    private bool IsSlow = false;
    private Vector3 currentPos;


    //oneForAll spell
    [Header("paramètres du OneForAll")]
    public GameObject OneForAllGO;
    public float OneForAllCost;
    public int OneForAllID;
    public Sprite OneForAllImage;
    public float OneForAllTime;
    private bool isBadass = false;
    private float OneForAllReset;
    public float OneForAllPercentage;
    private float currentSpeed;
    public AudioClip oneforallSound;

    
    


    void Start()
    {
        animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        playerInv = gameObject.GetComponent<PlayerInventory>();
        basePosition = transform.position;
        rayHit = GameObject.Find("RayHit");
        rayCast = GameObject.Find("RayCast");
        rayCast2 = GameObject.Find("RayCast2");
        spellHolderImg = GameObject.Find("SpellHolderImg");
        totalSpell = 3;
        currentSpeed = walkSpeed;
        OneForAllReset = OneForAllTime;
        BoostAttackReset = BoostAttackTime;
        ShieldSpellReset = ShieldSpellTime;
        FlameThrowerReset = FlameThrowerTime;
        ShieldSpellReset = ShieldSpellTime;
        SlowSpellReset = SlowSpellTime;
        source = GetComponent<AudioSource>();
    }

    bool IsGrounded()
    {

        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.08f, layerMask: 9);

    }


    void Update()
    {


        if (!isDead)
        {
            //glitch protction
            if(gameObject.transform.position.y <= -5)
            {
                gameObject.transform.position = basePosition;
            }

            //cheatcode
            switch (CheatCount)
            {
                case 0:
                    if(Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 5;
                        playerInv.maxHealth = 105;
                        playerInv.currentDamage = 10;
                        playerInv.currentHealth = 105;
                        playerInv.maxMana = 105;
                        playerInv.currentMana = 105;
                        CheatCount++;
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 10;
                        playerInv.currentHealth = 110;
                        playerInv.currentDamage = 15;
                        playerInv.maxHealth = 110;
                        playerInv.maxMana = 110;
                        playerInv.currentMana = 110;
                        CheatCount++;
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 20;
                        playerInv.currentHealth = 120;
                        playerInv.currentMana = 120;
                        playerInv.currentDamage = 20;
                        playerInv.maxHealth = 120;
                        playerInv.maxMana = 120;
                        CheatCount++;
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 30;
                        playerInv.currentHealth = 130;
                        playerInv.currentMana = 130;
                        playerInv.currentDamage = 30;
                        playerInv.maxHealth = 130;
                        playerInv.maxMana = 130;
                        CheatCount++;
                    }
                    break;
                case 4:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 40;
                        playerInv.currentHealth = 140;
                        playerInv.currentMana = 140;
                        playerInv.currentDamage = 40;
                        playerInv.maxHealth = 140;
                        playerInv.maxMana = 140;
                        CheatCount++;
                    }
                    break;
                case 5:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 50;
                        playerInv.currentHealth = 150;
                        playerInv.currentMana = 150;
                        playerInv.currentDamage = 50;
                        playerInv.maxHealth = 150;
                        playerInv.maxMana = 150;
                        CheatCount++;
                    }
                    break;
                case 6:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 60;
                        playerInv.currentHealth = 160;
                        playerInv.currentMana = 160;
                        playerInv.currentDamage = 60;
                        playerInv.maxHealth = 160;
                        playerInv.maxMana = 160;
                        CheatCount++;
                    }
                    break;
                case 7:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 70;
                        playerInv.currentDamage = 70;
                        playerInv.maxHealth = 170;
                        playerInv.maxMana = 170;
                        playerInv.currentHealth = 170;
                        playerInv.currentMana = 170;
                        CheatCount++;
                    }
                    break;
                case 8:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.playerLvl = 80;
                        playerInv.currentHealth = 10000;
                        playerInv.currentDamage = 1000;
                        playerInv.maxHealth = 10000;
                        playerInv.maxMana = 1000;
                        playerInv.currentMana = 1000;
                        CheatCount++;
                    }
                    break;
                default:
                    if (Input.GetKeyDown(KeyCode.Equals))
                    {
                        playerInv.currentHealth = 10000;
                        playerInv.currentMana = 1000;
                    }
                    break;
            }
            regenReset -= Time.deltaTime;
            if (regenReset <= 0)
            {
                regenReset = resetTime;
                playerInv.currentHealth += regenHealth;
                playerInv.currentMana += regenMana;
                if (playerInv.currentHealth > playerInv.maxHealth)
                {
                    playerInv.currentHealth = playerInv.maxHealth;
                }
                if (playerInv.currentMana > playerInv.maxMana)
                {
                    playerInv.currentMana = playerInv.maxMana;
                }


            }
            if (GameObject.Find("Slot - InventorySword(Clone)"))
            {
                itemId = GameObject.Find("Slot - InventorySword(Clone)").GetComponent<CheckItem>().itemID;
                itemList = GameObject.Find("Slot - InventorySword(Clone)").GetComponent<CheckItem>().itemList;
                secondList = GameObject.Find("Slot - InventorySword(Clone)").GetComponent<CheckItem>().secondList;
                bodyPartHand = GameObject.Find("Slot - InventorySword(Clone)").GetComponent<CheckItem>().bodyPart;

                if (bodyPartHand.transform.childCount > 1)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        itemList[i].SetActive(false);

                    }
                    for (int i = 0; i < spellList.Count; i++)
                    {
                        spellList[i].SetActive(false);

                    }
                }
                if (bodyPartBack.transform.childCount > 1)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        secondList[i].SetActive(false);

                    }
                }
            }
            if (isAttacking && !isCasting)
            {
                //iron sword
                if (itemId == 1)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 0)
                        {
                            itemList[i].SetActive(true);
                            secondList[i].SetActive(false);
                        }
                    }
                }

                //crystal sword
                if (itemId == 4)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 1)
                        {
                            itemList[i].SetActive(true);
                            secondList[i].SetActive(false);
                        }
                    }
                }

                //katana
                if (itemId == 6)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 2)
                        {
                            itemList[i].SetActive(true);
                            secondList[i].SetActive(false);
                        }
                    }
                }

                //longsword
                if (itemId == 7)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 3)
                        {
                            itemList[i].SetActive(true);
                            secondList[i].SetActive(false);
                        }
                    }
                }

                //saber sword
                if (itemId == 8)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 4)
                        {
                            itemList[i].SetActive(true);
                            secondList[i].SetActive(false);
                        }
                    }
                }

                //ulfberht
                if (itemId == 9)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 5)
                        {
                            itemList[i].SetActive(true);
                            secondList[i].SetActive(false);
                        }
                    }
                }
            }
            else
            {
                //iron sword
                if (itemId == 1)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 0)
                        {
                            itemList[i].SetActive(false);
                            secondList[i].SetActive(true);
                        }
                    }
                }

                //crystal sword
                if (itemId == 4)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 1)
                        {
                            itemList[i].SetActive(false);
                            secondList[i].SetActive(true);
                        }
                    }
                }

                //katana
                if (itemId == 6)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 2)
                        {
                            itemList[i].SetActive(false);
                            secondList[i].SetActive(true);
                        }
                    }
                }

                //longsword
                if (itemId == 7)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 3)
                        {
                            itemList[i].SetActive(false);
                            secondList[i].SetActive(true);
                        }
                    }
                }

                //saber sword
                if (itemId == 8)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 4)
                        {
                            itemList[i].SetActive(false);
                            secondList[i].SetActive(true);
                        }
                    }
                }

                //ulfberht
                if (itemId == 9)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i == 5)
                        {
                            itemList[i].SetActive(false);
                            secondList[i].SetActive(true);
                        }
                    }
                }
            }
            if (isAttacking && isCasting)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    itemList[i].SetActive(false);

                }
                //heal spell
                if (currentSpell == HealSpellID)
                {
                    for (int i = 0; i < spellList.Count; i++)
                    {
                        if (i == 0)
                        {
                            spellList[i].SetActive(true);
                        }
                    }
                }
                //fireball spell
                if (currentSpell == FireBallSpellID)
                {
                    for (int i = 0; i < spellList.Count; i++)
                    {
                        if (i == 1)
                        {
                            spellList[i].SetActive(true);
                        }
                    }
                }
                //lightning spell
                if (currentSpell == LightningSpellID)
                {
                    for (int i = 0; i < spellList.Count; i++)
                    {
                        if (i == 2)
                        {
                            spellList[i].SetActive(true);
                        }
                    }
                }

                //energy spell
                if (playerInv.playerLvl >= 5)
                {
                    if (currentSpell == EnergySpellID)
                    {
                        for (int i = 0; i < spellList.Count; i++)
                        {
                            if (i == 3)
                            {
                                spellList[i].SetActive(true);
                            }
                        }
                    }
                }

                //whitebomb spell
                if (playerInv.playerLvl >= 10)
                {
                    if (currentSpell == WhiteBombSpellID)
                    {
                        for (int i = 0; i < spellList.Count; i++)
                        {
                            if (i == 4)
                            {
                                spellList[i].SetActive(true);
                            }
                        }
                    }
                }

                //boostattack spell
                if (playerInv.playerLvl >= 20)
                {
                    if (currentSpell == BoostAttackSpellID)
                    {
                        for (int i = 0; i < spellList.Count; i++)
                        {
                            if (i == 4)
                            {
                                spellList[i].SetActive(true);
                            }
                        }
                    }
                }

                //shield spell
                if (playerInv.playerLvl >= 30)
                {
                    if (currentSpell == ShieldSpellID)
                    {
                        for (int i = 0; i < spellList.Count; i++)
                        {
                            if (i == 5)
                            {
                                spellList[i].SetActive(true);
                            }
                        }
                    }
                }

                //flamethrower spell
                if (playerInv.playerLvl >= 40)
                {
                    if (currentSpell == FlameThrowerID)
                    {
                        for (int i = 0; i < spellList.Count; i++)
                        {
                            if (i == 6)
                            {
                                spellList[i].SetActive(true);
                            }
                        }
                    }
                }
                
            }
            else
            {
                for (int i = 0; i < spellList.Count; i++)
                {

                    spellList[i].SetActive(false);

                }
            }

            //si on avance 
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);

                if (!isAttacking)
                {

                    animations.Play("walk");


                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && !GameObject.Find("Panel - Inventory(Clone)") && !GameObject.Find("Panel - EquipmentSystem(Clone)") && !GameObject.Find("Panel - Competences") && !GameObject.Find("Panel - CraftSystem(Clone)") && !GameObject.Find("Panel - Storage(Clone)") && !GameObject.Find("Panel - Sorts") && !GameObject.Find("Panel - Menu") && !GameObject.Find("Panel - Shop") && !GameObject.Find("Panel - Option") && !GameObject.Find("Panel - Chat"))
                {
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1) && !GameObject.Find("Panel - Inventory(Clone)") && !GameObject.Find("Panel - EquipmentSystem(Clone)") && !GameObject.Find("Panel - Competences") && !GameObject.Find("Panel - CraftSystem(Clone)") && !GameObject.Find("Panel - Storage(Clone)") && !GameObject.Find("Panel - Sorts") && !GameObject.Find("Panel - Menu") && !GameObject.Find("Panel - Shop") && !GameObject.Find("Panel - Option") && !GameObject.Find("Panel - Chat"))
                {
                    Spell();
                }
            }
            //si on sprint
            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
            {

                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animations.Play("run");


            }
            //si on recule
            if (Input.GetKey(inputBack))
            {
                transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
                if (!isAttacking)
                {

                    animations.Play("walk");

                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && !GameObject.Find("Panel - Inventory(Clone)") && !GameObject.Find("Panel - EquipmentSystem(Clone)") && !GameObject.Find("Panel - Competences") && !GameObject.Find("Panel - CraftSystem(Clone)") && !GameObject.Find("Panel - Storage(Clone)") && !GameObject.Find("Panel - Sorts") && !GameObject.Find("Panel - Menu") && !GameObject.Find("Panel - Shop") && !GameObject.Find("Panel - Option") && !GameObject.Find("Panel - Chat"))
                {
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1) && !GameObject.Find("Panel - Inventory(Clone)") && !GameObject.Find("Panel - EquipmentSystem(Clone)") && !GameObject.Find("Panel - Competences") && !GameObject.Find("Panel - CraftSystem(Clone)") && !GameObject.Find("Panel - Storage(Clone)") && !GameObject.Find("Panel - Sorts") && !GameObject.Find("Panel - Menu") && !GameObject.Find("Panel - Shop") && !GameObject.Find("Panel - Option") && !GameObject.Find("Panel - Chat"))
                {
                    Spell();
                }
            }
            //rotation a gauche
            if (Input.GetKey(inputLeft))
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            }
            //rotation a droit
            if (Input.GetKey(inputRight))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }
            //si on avance pas et que on recule pas aussi 
            if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))
            {
                if (!isAttacking)
                {

                    animations.Play("idle");


                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && !GameObject.Find("Panel - Inventory(Clone)") && !GameObject.Find("Panel - EquipmentSystem(Clone)") && !GameObject.Find("Panel - Competences") && !GameObject.Find("Panel - CraftSystem(Clone)") && !GameObject.Find("Panel - Storage(Clone)") && !GameObject.Find("Panel - Sorts") && !GameObject.Find("Panel - Menu") && !GameObject.Find("Panel - Shop") && !GameObject.Find("Panel - Option") && !GameObject.Find("Panel - Chat"))
                {
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.Mouse1) && !GameObject.Find("Panel - Inventory(Clone)") && !GameObject.Find("Panel - EquipmentSystem(Clone)") && !GameObject.Find("Panel - Competences") && !GameObject.Find("Panel - CraftSystem(Clone)") && !GameObject.Find("Panel - Storage(Clone)") && !GameObject.Find("Panel - Sorts") && !GameObject.Find("Panel - Menu") && !GameObject.Find("Panel - Shop") && !GameObject.Find("Panel - Option") && !GameObject.Find("Panel - Chat"))
                {
                    Spell();
                }
            }
            //si on saute

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {

                //preparation du saut (necessaire en c#)
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpsSpeed.y;
                //saut
                gameObject.GetComponent<Rigidbody>().velocity = jumpsSpeed;

            }
            
        }
        else
        {
            resetTime -= Time.deltaTime;

        }
        if (resetTime <= 0)

        {
            resetTime = rebirthTime;
            isDead = false;

            Rebirth();
        }
        if (isAttacking)
        {
            currentCooldown -= Time.deltaTime;

        }
        if (currentCooldown <= 0)
        {
            currentCooldown = attackCooldown;
            
            isAttacking = false;
            isCasting = false;
        }

        //changement de sort
        //limite de niveau
        if (playerInv.playerLvl == 5 && !isLeveling)
        {
            totalSpell += 1;
            isLeveling = true;
        }
        if (playerInv.playerLvl == 10 && isLeveling)
        {
            totalSpell += 1;
            isLeveling = false;
        }
        if (playerInv.playerLvl == 20 && !isLeveling)
        {
            totalSpell += 1;
            isLeveling = true;
        }
        if (playerInv.playerLvl == 30 && isLeveling)
        {
            totalSpell += 1;
            isLeveling = false;
        }
        if (playerInv.playerLvl == 40 && !isLeveling)
        {
            totalSpell += 1;
            isLeveling = true;
        }
        if (playerInv.playerLvl == 50 && isLeveling)
        {
            totalSpell += 1;
            isLeveling = false;
        }
        if (playerInv.playerLvl == 60 && !isLeveling)
        {
            totalSpell += 1;
            isLeveling = true;
        }
        if (playerInv.playerLvl == 70 && isLeveling)
        {
            totalSpell += 1;
            isLeveling = false;
        }
        if (playerInv.playerLvl == 80 && !isLeveling)
        {
            totalSpell += 1;
            isLeveling = true;
        }
        //up
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentSpell <= totalSpell && currentSpell != 1 )
            {
                currentSpell -= 1;
            }
        }

        //down
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            
            if (currentSpell >= 0 && currentSpell != totalSpell)
            {
                currentSpell += 1;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !GameObject.Find("Panel - Inventory(Clone)") && !GameObject.Find("Panel - EquipmentSystem(Clone)") && !GameObject.Find("Panel - Competences") && !GameObject.Find("Panel - CraftSystem(Clone)") && !GameObject.Find("Panel - Storage(Clone)") && !GameObject.Find("Panel - Sorts") && !GameObject.Find("Panel - Menu") && !GameObject.Find("Panel - Shop") && !GameObject.Find("Panel - Chat") && !GameObject.Find("Panel - Quest"))
        {
            UIMenu.SetActive(true);
        }



         // changement d'image à recopier pour creer un nouveau sort
        if (currentSpell == FireBallSpellID)
        {
            spellHolderImg.GetComponent<Image>().sprite = FireBallSpellImage;
        }
        if (currentSpell == HealSpellID)
        {
            spellHolderImg.GetComponent<Image>().sprite = HealSpellImage;
        }
        if (currentSpell == LightningSpellID)
        {
            spellHolderImg.GetComponent<Image>().sprite = LightningSpellImage;
        }
        if (playerInv.playerLvl >= 5)
        {
            if (currentSpell == EnergySpellID)
            {
                spellHolderImg.GetComponent<Image>().sprite = EnergySpellImage;
            }
        }
        if (playerInv.playerLvl >= 10)
        {
            if (currentSpell == WhiteBombSpellID)
            {
                spellHolderImg.GetComponent<Image>().sprite = WhiteBombSpellImage;
            }
        }
        if (playerInv.playerLvl >= 20)
        {
            if (currentSpell == BoostAttackSpellID)
            {
                spellHolderImg.GetComponent<Image>().sprite = BoostAttackSpellImage;
            }
        }
        if (playerInv.playerLvl >= 30)
        {
            if (currentSpell == ShieldSpellID)
            {
                spellHolderImg.GetComponent<Image>().sprite = ShieldSpellImage;
            }
        }
        if (playerInv.playerLvl >= 40)
        {
            if (currentSpell == FlameThrowerID)
            {
                spellHolderImg.GetComponent<Image>().sprite = FlameThrowerImage;
            }
        }
        if (playerInv.playerLvl >= 50)
        {
            if (currentSpell == IceSpellID)
            {
                spellHolderImg.GetComponent<Image>().sprite = IceSpellImage;
            }
        }
        if (playerInv.playerLvl >= 60)
        {
            if (currentSpell == SpeedSpellID)
            {
                spellHolderImg.GetComponent<Image>().sprite = SpeedSpellImage;
            }
        }
        if (playerInv.playerLvl >= 70)
        {
            if (currentSpell == SlowSpellID)
            { 
                spellHolderImg.GetComponent<Image>().sprite = SlowSpellImage;

            }
        }
        if (playerInv.playerLvl >= 80)
        {
            if (currentSpell == OneForAllID)
            {
                spellHolderImg.GetComponent<Image>().sprite = OneForAllImage;
            }
        }



        //boost attack time
        if (isBoostAttack)
        {
            BoostAttackReset -= Time.deltaTime;
        }
        if (BoostAttackReset <= 0)
        {
            BoostAttackReset = BoostAttackTime;
            for (int i = 0; i < OnSelfSpellList.Count; i++)
            {
                if (i == 1)
                {

                    OnSelfSpellList[i].SetActive(false);
                    playerInv.currentDamage = currentDamage;
                    isBoostAttack = false;
                }

            }
        }

        //shield spell time
        if (isShielded)
        {
            playerInv.currentHealth = currentLife;
            ShieldSpellReset -= Time.deltaTime;
             
        }
        if (ShieldSpellReset <= 0)
        {
            ShieldSpellReset = ShieldSpellTime;
            for (int i = 0; i < OnSelfSpellList.Count; i++)
            {
                if (i == 2)
                {
                    
                    OnSelfSpellList[i].SetActive(false);
                    isShielded = false;
                }
            }
        }

        //flamethrower time
        if (isThrowing)
        {
            
            FlameThrowerReset -= Time.deltaTime;
            enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            
            for (int i = 0; i < enemyList.Length; i++)
            {
                Distance = Vector3.Distance(enemyList[i].transform.position, transform.position);
                if (Distance <= 3)
                {
                    enemyList[i].GetComponent<ennemyAI>().ApplyDamage(FlameThrowerDamage);
                }
            }
        }
        if (FlameThrowerReset <= 0)
        {
            FlameThrowerReset = FlameThrowerTime;
            for (int i = 0; i < OnSelfSpellList.Count; i++)
            {
                if (i == 3)
                {
                    OnSelfSpellList[i].SetActive(false);
                    
                    isThrowing = false;
                }
            }
        }

        //speedspell time
        if (IsSpeedster)
        {
            SpeedSpellReset -= Time.deltaTime;
        }
        if (SpeedSpellReset <= 0)
        {
            SpeedSpellReset = SpeedSpellTime;
            for (int i = 0; i < OnSelfSpellList.Count; i++)
            {
                if (i == 4)
                {
                    OnSelfSpellList[i].SetActive(false);
                    walkSpeed = currentSpeed;
                    runSpeed = currentSpeed + 7;
                    IsSpeedster = false;
                }
            }
        }

        //slowspell time
        if (IsSlow)
        {
            SlowSpellReset -= Time.deltaTime;
            enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            
           for (int i = 0; i < enemyList.Length; i++)
            {
                Distance = Vector3.Distance(enemyList[i].transform.position, transform.position);
                if (Distance <= 10)
                {
                    enemyList[i].transform.position = currentPos;
                }
            }
        }
        if (SlowSpellReset <= 0)
        {
            SlowSpellReset = SlowSpellTime;
            IsSlow = false;
            
        }

        //oneforall time
        if (isBadass)
        {
            
            OneForAllReset -= Time.deltaTime;
        }
        if (OneForAllReset <= 0)
        {
            OneForAllReset = OneForAllTime;
            for (int i = 0; i < OnSelfSpellList.Count; i++)
            {
                if (i == 5)
                {
                    OnSelfSpellList[i].SetActive(false);
                    playerInv.currentDamage = currentDamage;
                    walkSpeed = currentSpeed;
                    runSpeed = currentSpeed + 7;
                    isBadass = false;
                }
            }
        }

      
    }
    //fonction d'attaque

    public void Attack()
    {
        if (!isAttacking)
        {

            animations.Play("attack");
            source.PlayOneShot(attack);
            RaycastHit hit;
            if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<ennemyAI>().ApplyDamage(playerInv.currentDamage);
                    print(hit.transform.name + "detected");
                }
            }
            isAttacking = true;
            isCasting = false;

        }

    }

    public void Spell()
    {
        //fire spell
        if (currentSpell == FireBallSpellID && !isAttacking && playerInv.currentMana >= FireBallSpellCost)
        {

            animations.Play("attack");
            source.PlayOneShot(fireBallSound);
            GameObject theSpell = Instantiate(FireBallSpellGO, rayCast.transform.position, transform.rotation);
            theSpell.GetComponent<Rigidbody>().AddForce(transform.forward * FireBallSpellSpeed);
            playerInv.currentMana -= FireBallSpellCost;
            isAttacking = true;
            isCasting = true;
        }

        //heal spell
        if (currentSpell == HealSpellID && !isAttacking && playerInv.currentMana >= HealSpellCost && playerInv.currentHealth < playerInv.maxHealth)
        {
            
            for (int i = 0; i < OnSelfSpellList.Count; i++)
            {
                if (i == 0)
                {
                    OnSelfSpellList[i].SetActive(true);
                    
                }
            }
            playerInv.currentMana -= HealSpellCost;
            playerInv.currentHealth += HealSpellAmount;
            if (playerInv.currentHealth > playerInv.maxHealth)
            {
                playerInv.currentHealth = playerInv.maxHealth;
            }
            source.PlayOneShot(healSound);
            isAttacking = true;
            isCasting = true;
        }

        //Lightning spell
        if (currentSpell == LightningSpellID && !isAttacking && playerInv.currentMana >= LightningSpellCost)
        {
            animations.Play("attack");
            source.PlayOneShot(electricSound);
            Instantiate(LightningSpellGO, rayCast2.transform.position, transform.rotation);
            
            playerInv.currentMana -= LightningSpellCost;
            isAttacking = true;
            isCasting = true;
        }

        //Energy spell
        if (playerInv.playerLvl >= 5)
        {
            if (currentSpell == EnergySpellID && !isAttacking && playerInv.currentMana >= EnergySpellCost)
            {
                enemyList = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemyList.Length; i++)
                {

                    Distance = Vector3.Distance(enemyList[i].transform.position, transform.position);
                    if (Distance <= EnergySpellRangeDistance && enemyList[i].GetComponent<ennemyAI>().isDead != true)
                    {
                        animations.Play("attack");
                        source.PlayOneShot(energySound);
                        //Instantiate(EnergySpellGO, new Vector3(enemyList[i].transform.position.x, enemyList[i].transform.position.y + 2, enemyList[i].transform.position.z), transform.rotation);
                        enemyList[i].GetComponent<ennemyAI>().onSpellList[0].SetActive(true);
                        enemyList[i].GetComponent<ennemyAI>().onSpellList[0].GetComponent<ParticleSystem>().Play();
                        enemyList[i].GetComponent<ennemyAI>().ApplyDamage(EnergySpellDamage);
                        playerInv.currentMana -= EnergySpellCost;
                        isAttacking = true;
                        isCasting = true;
                    }
                }
            }
        }

        //WhiteBomb spell
        if (playerInv.playerLvl >= 10)
        {
            if (currentSpell == WhiteBombSpellID && !isAttacking && playerInv.currentMana >= WhiteBombSpellCost)

            {
                enemyList = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemyList.Length; i++)
                {
                    Distance = Vector3.Distance(enemyList[i].transform.position, transform.position);
                    if (Distance <= WhiteBombSpellRangeDistance && enemyList[i].GetComponent<ennemyAI>().isDead != true)
                    {
                        animations.Play("attack");
                        source.PlayOneShot(whiteBombSound);
                        enemyList[i].GetComponent<ennemyAI>().onSpellList[1].SetActive(true);
                        enemyList[i].GetComponent<ennemyAI>().onSpellList[1].GetComponent<ParticleSystem>().Play();
                        enemyList[i].GetComponent<ennemyAI>().ApplyDamage(WhiteBombSpellDamage);
                        playerInv.currentMana -= EnergySpellCost;
                        isAttacking = true;
                        isCasting = true;
                    }
                    
                }
            }

        }
        
        //BoostAttack spell
        if (playerInv.playerLvl >= 20)
        {
            if (currentSpell == BoostAttackSpellID && !isAttacking && playerInv.currentMana >= BoostAttackSpellCost && !isBoostAttack)
            {
                //animations.Play("attack");
                for (int i = 0; i < OnSelfSpellList.Count; i++)
                {
                    if (i == 1)
                    {

                        OnSelfSpellList[i].SetActive(true);
                        currentDamage = playerInv.currentDamage;
                        playerInv.currentDamage += BoostAttackSpellPercentage * playerInv.currentDamage / 100;
                        isBoostAttack = true;
                        source.PlayOneShot(boostattackSound);
                        playerInv.currentMana -= BoostAttackSpellCost;
                        
                    }
                }
                //isAttacking = true;
                //isCasting = true;
            }
        }

        //shield spell
        if (playerInv.playerLvl >= 30)
        {
            if (currentSpell == ShieldSpellID && !isAttacking && playerInv.currentMana >= ShieldSpellCost && !isShielded)
            {
                for (int i = 0; i < OnSelfSpellList.Count; i++)
                {
                    if (i == 2)
                    {
                        OnSelfSpellList[i].SetActive(true);
                        currentLife = playerInv.currentHealth;
                        isShielded = true;
                        playerInv.currentMana -= ShieldSpellCost;
                        source.PlayOneShot(shieldSound);
                        
                    }
                }
            }
        }

        //flamethrower spell
        if (playerInv.playerLvl >= 40)
        {
            if (currentSpell == FlameThrowerID && !isAttacking && playerInv.currentMana >= FlameThrowerCost && !isThrowing)
            {
                source.PlayOneShot(flamethrowerSound);
                for (int i = 0; i < OnSelfSpellList.Count; i++)
                {
                    if (i == 3)
                    {
                        
                        OnSelfSpellList[i].SetActive(true);
                        //OnSelfSpellList[i].GetComponentInChildren<ParticleSystem>().Play();
                        isThrowing = true;
                        playerInv.currentMana -= FlameThrowerCost;
                    }
                }
                
            }
        }

        //ice spell
        if (playerInv.playerLvl >= 50)
        {
            if (currentSpell == IceSpellID && !isAttacking && playerInv.currentMana >= IceSpellCost)
            {
                animations.Play("attack");
                source.PlayOneShot(iceSound);
                GameObject theSpell = Instantiate(IceSpellGO, rayCast.transform.position, transform.rotation);
                //theSpell.SetActive(true);
                theSpell.GetComponent<Rigidbody>().AddForce(transform.forward * IceSpellSpeed);
                
                playerInv.currentMana -= IceSpellCost;
                isAttacking = true;
                isCasting = true;

            }
        }

        //SpeedSpell
        if (playerInv.playerLvl >= 60)
        {
            if (currentSpell == SpeedSpellID && !isAttacking && playerInv.currentMana >= SpeedSpellCost && ! IsSpeedster)
            {
                for (int i = 0; i < OnSelfSpellList.Count; i++)
                {
                    if (i == 4)
                    {
                        source.PlayOneShot(speedSound);
                        OnSelfSpellList[i].SetActive(true);
                        currentSpeed = walkSpeed;
                        walkSpeed += SpeedSpellAmount;
                        runSpeed += SpeedSpellAmount + 7;
                        IsSpeedster = true;
                        playerInv.currentMana -= SpeedSpellCost;
                    }
                }
            }
        }

        //slow spell
        if (playerInv.playerLvl >= 70)
        {
            if (currentSpell == SlowSpellID && !isAttacking && playerInv.currentMana >= SlowSpellCost && !IsSlow)
            {
                animations.Play("attack");
                enemyList = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemyList.Length; i++)
                {
                    currentPos = enemyList[i].transform.position;
                }
                IsSlow = true;
                playerInv.currentMana -= SlowSpellCost;
                isAttacking = true;
                isCasting = true;
            }
        }

        //oneforall spell
        if (playerInv.playerLvl >= 80)
        {
            if (currentSpell == OneForAllID && !isAttacking && playerInv.currentMana >= OneForAllCost && !isBadass)
            {
                for (int i = 0; i < OnSelfSpellList.Count; i++)
                {
                    if (i == 5)
                    {
                        source.PlayOneShot(oneforallSound);
                        OnSelfSpellList[i].SetActive(true);
                        currentDamage = playerInv.currentDamage;
                        currentSpeed = walkSpeed;
                        playerInv.currentDamage += OneForAllPercentage * playerInv.currentDamage / 100;
                        walkSpeed += OneForAllPercentage * walkSpeed / 100;
                        runSpeed += OneForAllPercentage * runSpeed / 100;
                        isBadass = true;
                        playerInv.currentMana -= OneForAllCost;
                        
                    }
                }
            }
        }


    }
    public void OpenInv()
    {
        UIInv.SetActive(true);

    }
    public void OpenEquip()
    {
        UIEquip.SetActive(true);
    }
    public void OpenQuest()
    {
        gameObject.GetComponent<questSystem>().showPanel = true;
    }

    public void OpenOptions()
    {
        UIMenu.SetActive(true);
    }
    public void Rebirth()
    {

        GameObject.Find("Player").transform.position = basePosition;
        animations.Play("idle");
        playerInv.currentHealth = playerInv.maxHealth;

    }
}