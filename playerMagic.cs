using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerMagic : MonoBehaviour
{
    public GameObject UiPanel;
    public Text pointsText;
    public Text pointsFireball;
    public Text pointsHeal;
    public Text pointsLightning;
    public Text pointsEnergy;
    public Text pointsWhiteBomb;
    public Text pointsBoostAttack;
    public Text pointsShield;
    public Text pointsFlameThrower;
    public Text pointsIce;
    public Text pointsSpeed;
    public Text pointsSlow;
    public Text pointsOneForAll;
    private int levelFireball;
    private int levelHeal;
    private int levelLightning;
    private int levelEnergy;
    private int levelWhiteBomb;
    private int levelBoostAttack;
    private int levelShield;
    private int levelFlameThrower;
    private int levelIce;
    private int levelSpeed;
    private int levelSlow;
    private int levelOneForAll;
    public int availablePoints;
    public string openKey;
    private bool isOpen;
    
    private PlayerInventory playerInv;

    CharacterMotor characterMotor;

    // Start is called before the first frame update
    void Start()
    {
        playerInv = gameObject.GetComponent<PlayerInventory>();
        characterMotor = gameObject.GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(openKey))
        {
            pointsText.text = "Points: " + availablePoints;
            isOpen = !isOpen;
        }
        if (isOpen)
        {
            UiPanel.SetActive(true);

           
        }
        else
        {
            UiPanel.SetActive(false);
            
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            
            isOpen = !isOpen;

        }
        if (playerInv.playerLvl >= 5)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_Energy_NoLevel"));
                Destroy(GameObject.Find("Background_Energy_NoLevel"));
            }
        }
        if (playerInv.playerLvl >= 10)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_WhiteBomb_NoLevel"));
                Destroy(GameObject.Find("Background_WhiteBomb_NoLevel"));
            }
        }
        if (playerInv.playerLvl >= 20)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_BoostAttack_NoLevel"));
                Destroy(GameObject.Find("Background_BoostAttack_NoLevel"));
            }
        }
        if (playerInv.playerLvl >= 30)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_Shield_NoLevel"));
                Destroy(GameObject.Find("Background_Shield_NoLevel"));
            }
        }
        if (playerInv.playerLvl >= 40)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_FlameThrower_NoLevel"));
                Destroy(GameObject.Find("Background_FlameThrower_NoLevel"));

            }
        }
        if (playerInv.playerLvl >= 50)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_Ice_NoLevel"));
                Destroy(GameObject.Find("Background_Ice_NoLevel"));
            }

        }

        if (playerInv.playerLvl >= 60)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_Speed_NoLevel"));
                Destroy(GameObject.Find("Background_Speed_NoLevel"));
            }
        }
        if (playerInv.playerLvl >= 70)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_Slow_NoLevel"));
                Destroy(GameObject.Find("Background_Slow_NoLevel"));
            }
        }
        if (playerInv.playerLvl >= 80)
        {
            if (GameObject.Find("Panel - Sorts"))
            {
                Destroy(GameObject.Find("Button_OneForAll"));
                Destroy(GameObject.Find("Background_OneForAll"));
            }
        }
    }
    public void upgradeFireBall(float FireBall)
    {
        if (availablePoints >= 1)
        {
            
            levelFireball += 1;
            characterMotor.FireBallDamage += FireBall;
            availablePoints -= 1;
            pointsFireball.text = "" + levelFireball;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void upgradeHeal(float Heal)
    {
        if (availablePoints >= 1)
        {
            
            levelHeal += 1;
            characterMotor.HealSpellAmount += Heal;
            availablePoints -= 1;
            pointsHeal.text = "" + levelHeal;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void upgradeLightning(float upgradeLightning)
    {
        if (availablePoints >= 1)
        {
            
            levelLightning += 1;
            characterMotor.LightningSpellDamage += upgradeLightning;
            availablePoints -= 1;
            pointsLightning.text = "" + levelLightning;
            pointsText.text = "Points: " + availablePoints;
        }
    }

    public void upgradeEnergy(float upgradeEnergy)
    {
        if (playerInv.playerLvl >= 5 && availablePoints >= 1)
        {
            
            levelEnergy += 1;
            characterMotor.EnergySpellDamage += upgradeEnergy;
            availablePoints -= 1;
            pointsEnergy.text = "" + levelEnergy;
            pointsText.text = "Points: " + availablePoints;
        }
    }

    public void upgradeWhiteBomb(float upgradeWhiteBomb)
    {
        if (playerInv.playerLvl >= 10 && availablePoints >= 1)
        {
            
            levelWhiteBomb += 1;
            characterMotor.WhiteBombSpellDamage += upgradeWhiteBomb;
            availablePoints -= 1;
            pointsWhiteBomb.text = "" + levelWhiteBomb;
            pointsText.text = "Points: " + availablePoints;
            
        }
    }
    
    public void upgradeBoostAttack(float upgradeBoostA)
    {
        if (playerInv.playerLvl >= 20 && availablePoints >= 1)
        {
            
            levelBoostAttack += 1;
            characterMotor.BoostAttackSpellPercentage += upgradeBoostA;
            availablePoints -= 1;
            pointsBoostAttack.text = "" + levelBoostAttack;
            pointsText.text = "Points: " + availablePoints;
        }
    }

    public void upgradeShield (float upgradeTime)
    {
        if (playerInv.playerLvl >= 30 && availablePoints >= 1)
        {
            
            levelShield += 1;
            characterMotor.ShieldSpellTime += upgradeTime;
            availablePoints -= 1;
            pointsShield.text = "" + levelShield;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void upgradeFlameThrower(float upgradeFlame)
    {
        if (playerInv.playerLvl >= 40 && availablePoints >= 1)
        {
            levelFlameThrower += 1;
            characterMotor.FlameThrowerDamage += upgradeFlame;
            availablePoints -= 1;
            pointsFlameThrower.text = "" + levelFlameThrower;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void upgradeIce(float Ice)
    {
        if (playerInv.playerLvl >= 50 && availablePoints >= 1)
        {
            levelIce += 1;
            characterMotor.IceSpellDamage += Ice;
            availablePoints -= 1;
            pointsIce.text = "" + levelIce;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void upgradeSpeed(float speed)
    {
        if (playerInv.playerLvl >= 60 && availablePoints >= 1)
        {
            levelSpeed += 1;
            characterMotor.SpeedSpellAmount += speed;
            availablePoints -= 1;
            pointsSpeed.text = "" + levelSpeed;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void upgradeSlow(float slow)
    {
        if (playerInv.playerLvl >= 70 && availablePoints >= 1 )
        {
            levelSlow += 1;
            characterMotor.SlowSpellTime += slow;
            availablePoints -= 1;
            pointsSlow.text = "" + levelSlow;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void upgradeOneForAll(float oneForAll)
    {
        if (playerInv.playerLvl >= 80 && availablePoints >= 1)
        {
            levelOneForAll += 1;
            characterMotor.OneForAllPercentage += oneForAll;
            characterMotor.OneForAllTime += oneForAll;
            pointsOneForAll.text = "" + levelOneForAll;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void Open()
    {
        isOpen = true;
    }
}
