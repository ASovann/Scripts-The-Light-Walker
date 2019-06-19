using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject characterSystem;
    public GameObject craftSystem;
    private Inventory craftSystemInventory;
    private CraftSystem cS;
    private Inventory mainInventory;
    private Inventory characterSystemInventory;
    private Tooltip toolTip;

    private InputManager inputManagerDatabase;

    public float damageScreenTime;
    private float damageScreenRepeatTime;
    public GameObject damageScreenGO;
    private bool isDamaged;

    Image hpImage;
    Image manaImage;

    public float maxHealth = 100;
    public float maxMana = 100;
    
    float maxDamage = 0;
    float maxArmor = 0;

    public float currentHealth = 60;
    public float currentMana = 100;
    public float currentDamage = 0;
    public float currentArmor = 0;

    public int questNumber;
    int normalSize = 3;

    //experience
    Image experienceBar;
    Text playerLevelTxt;
    public int playerLvl = 1;
    public float currentXp = 0;
    public float maxXp = 100;
    public float rateXp;
    public int pointCompetence;
    public float currentMoney = 0;
    public Text AmountMoney;
    playerSkills playerSkill;
    playerMagic playerMagics;

    public CharacterMotor charactermotor;
    public Animation playerAnimations;

    



    public void OnEnable()
    {
        Inventory.ItemEquip += OnBackpack;
        Inventory.UnEquipItem += UnEquipBackpack;

        Inventory.ItemEquip += OnGearItem;
        Inventory.ItemConsumed += OnConsumeItem;
        Inventory.UnEquipItem += OnUnEquipItem;

        Inventory.ItemEquip += EquipWeapon;
        Inventory.UnEquipItem += UnEquipWeapon;
    }

    public void OnDisable()
    {
        Inventory.ItemEquip -= OnBackpack;
        Inventory.UnEquipItem -= UnEquipBackpack;

        Inventory.ItemEquip -= OnGearItem;
        Inventory.ItemConsumed -= OnConsumeItem;
        Inventory.UnEquipItem -= OnUnEquipItem;

        Inventory.UnEquipItem -= UnEquipWeapon;
        Inventory.ItemEquip -= EquipWeapon;
    }

    void EquipWeapon(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {
            //add the weapon if you unequip the weapon
        }
    }

    void UnEquipWeapon(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {
            //delete the weapon if you unequip the weapon
        }
    }

    void OnBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
        {
            for (int i = 0; i < item.itemAttributes.Count; i++)
            {
                if (mainInventory == null)
                    mainInventory = inventory.GetComponent<Inventory>();
                mainInventory.sortItems();
                if (item.itemAttributes[i].attributeName == "Slots")
                    changeInventorySize(item.itemAttributes[i].attributeValue);
            }
        }
    }

    void UnEquipBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
            changeInventorySize(normalSize);
    }

    void changeInventorySize(int size)
    {
        dropTheRestItems(size);

        if (mainInventory == null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (size == 3)
        {
            mainInventory.width = 3;
            mainInventory.height = 1;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        if (size == 6)
        {
            mainInventory.width = 3;
            mainInventory.height = 2;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 12)
        {
            mainInventory.width = 4;
            mainInventory.height = 3;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 16)
        {
            mainInventory.width = 4;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 24)
        {
            mainInventory.width = 6;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
    }

    void dropTheRestItems(int size)
    {
        if (size < mainInventory.ItemsInInventory.Count)
        {
            for (int i = size; i < mainInventory.ItemsInInventory.Count; i++)
            {
                GameObject dropItem = (GameObject)Instantiate(mainInventory.ItemsInInventory[i].itemModel);
                dropItem.AddComponent<PickUpItem>();
 
                dropItem.GetComponent<PickUpItem>().item = mainInventory.ItemsInInventory[i];
                dropItem.transform.localPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.localPosition.x +2, GameObject.FindGameObjectWithTag("Player").transform.localPosition.y+2, GameObject.FindGameObjectWithTag("Player").transform.localPosition.z + 2);
            }
        }
    }

    void Start()
    {

        hpImage = GameObject.Find("currentHP").GetComponent<Image>();
        manaImage = GameObject.Find("currentMana").GetComponent<Image>();

        experienceBar = GameObject.Find("currentXp").GetComponent<Image>();
        playerLevelTxt = GameObject.Find("playerLevelTxt").GetComponent<Text>();

        charactermotor = gameObject.GetComponent<CharacterMotor>();
        playerAnimations = gameObject.GetComponent<Animation>();

        damageScreenRepeatTime = damageScreenTime;
        playerSkill = gameObject.GetComponent<playerSkills>();
        playerMagics = gameObject.GetComponent<playerMagic>();

        if (inputManagerDatabase == null)
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");

        if (craftSystem != null)
            cS = craftSystem.GetComponent<CraftSystem>();

        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        if (inventory != null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (characterSystem != null)
            characterSystemInventory = characterSystem.GetComponent<Inventory>();
        if (craftSystem != null)
            craftSystemInventory = craftSystem.GetComponent<Inventory>();
    }


    public void ApplyDamage(float TheDamage)
    {
        damageScreenGO.SetActive(true);
        isDamaged = true;
        currentHealth = currentHealth - (TheDamage - ((currentArmor * TheDamage) / 100));
        if (currentHealth <= 0 && charactermotor.isDead == false)
        {
            Dead();
        }
       
    }

    public void Dead()
    {
        charactermotor.isDead = true;
        playerAnimations.Play("die");


    }

    public void OnConsumeItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
            {
                if ((currentHealth + item.itemAttributes[i].attributeValue) > maxHealth)
                    currentHealth = maxHealth;
                else
                    currentHealth += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Mana")
            {
                if ((currentMana + item.itemAttributes[i].attributeValue) > maxMana)
                    currentMana = maxMana;
                else
                    currentMana += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Armor")
            {
                if ((currentArmor + item.itemAttributes[i].attributeValue) > maxArmor)
                    currentArmor = maxArmor;
                else
                    currentArmor += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Damage")
            {
                if ((currentDamage + item.itemAttributes[i].attributeValue) > maxDamage)
                    currentDamage = maxDamage;
                else
                    currentDamage += item.itemAttributes[i].attributeValue;
            }
        }
  
    }

    public void OnGearItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
                maxHealth += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Mana")
                maxMana += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Armor")
                currentArmor += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Damage")
                currentDamage += item.itemAttributes[i].attributeValue;
        }
       
    }

    public void OnUnEquipItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
                maxHealth -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Mana")
                maxMana -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Armor")
                currentArmor -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Damage")
                currentDamage -= item.itemAttributes[i].attributeValue;
        }

    }



    // Update is called once per frame
    void Update()
    {
        //damage screen
        if(isDamaged)
        {
            damageScreenRepeatTime -= Time.deltaTime;
            if (damageScreenRepeatTime <= 0)
            {
                damageScreenGO.SetActive(false);
                damageScreenRepeatTime = damageScreenTime;
                isDamaged = false;
            }
        }

        //Pour la barre de vie
        float percentageHP = ((currentHealth * 100) / maxHealth) / 100;
        hpImage.fillAmount = percentageHP;

        //Pour la barre de mana
        float percentageMana = ((currentMana * 100) / maxMana) / 100;
        manaImage.fillAmount = percentageMana;

        //Pour la barre d'xp
        //assez d'xp
        if (currentXp >= maxXp)
        {
            float reste = currentXp - maxXp;
            playerLvl += 1;
            playerSkill.availablePoints += pointCompetence;
            playerMagics.availablePoints += pointCompetence;
            currentXp = 0 + reste; 
            maxXp = maxXp * rateXp;
        }

        playerLevelTxt.text = "" + playerLvl;
        float percentageXp = ((currentXp * 100) / maxXp) / 100;
        experienceBar.fillAmount = percentageXp;

        //money
        AmountMoney.text = "gold: " + currentMoney;

        if (Input.GetKeyDown(inputManagerDatabase.CharacterSystemKeyCode))
        {
            if (!characterSystem.activeSelf)
            {
                characterSystemInventory.openInventory();
            }
            else
            {
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                characterSystemInventory.closeInventory();
            }
        }

        if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
        {
            if (!inventory.activeSelf)
            {
                mainInventory.openInventory();
            }
            else
            {
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                mainInventory.closeInventory();
            }
        }
       

        if (Input.GetKeyDown(inputManagerDatabase.CraftSystemKeyCode))
        {
            if (!craftSystem.activeSelf)
                craftSystemInventory.openInventory();
            else
            {
                if (cS != null)
                    cS.backToInventory();
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                craftSystemInventory.closeInventory();
            }
        }

    }

}
