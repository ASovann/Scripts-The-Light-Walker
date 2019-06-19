using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerSkills : MonoBehaviour
{
    public GameObject Uipanel;
    public Text pointsText;
    public Text pointsEndurance;
    public Text pointsForce;
    public Text pointsIntelligence;
    public Text pointsArmor;
    public Text pointsHaste;
    public Text pointsPolyvalence;
    public Text pointsCritic;
    public Text pointsEsquive;
    public Text pointsParade;
    public int availablePoints;
    public string openKey;
    private bool isOpen;
    private PlayerInventory playerInv;
    private CharacterMotor characterMotor;

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
            pointsText.text = "Points: "+ availablePoints;
            isOpen = !isOpen;
        }
        if (isOpen)
        {
            Uipanel.SetActive(true);

        }
        else
        {   
            Uipanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape)  && isOpen)
        {

            isOpen = !isOpen;
        }

    }
    public void addHealthMax(float amountHp)
    {
        if (availablePoints >= 1)
        {
            int level = 0;
            level += 1;
            playerInv.maxHealth += amountHp;
            availablePoints -= 1;
            pointsEndurance.text = "" + level;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    public void addForce(float amountForce)
    {
        if (availablePoints >= 1)
        {
            int level = 0;
            level += 1;
            playerInv.currentDamage += amountForce;
            availablePoints -= 1;
            pointsForce.text = "" + level;
            pointsText.text = "Points: " + availablePoints;
        }
    }

    public void addIntelligence(float amountIntel)
    {
        if (availablePoints >= 1)
        {
            int level = 0;
            level += 1;
            playerInv.maxMana += amountIntel;
            availablePoints -= 1;
            pointsIntelligence.text = "" + level;
            pointsText.text = "Points: " + availablePoints;
        }
    }

    public void addArmor (float amountArmor)
    {
        if (availablePoints >= 1)
        {
            int level = 0;
            level += 1;
            playerInv.currentArmor += amountArmor;
            availablePoints -= 1;
            pointsArmor.text = "" + level;
            pointsText.text = "Points: " + availablePoints;
        }
    }
    
    public void speedAttack(float speed)
    {
        if (availablePoints >= 1)
        {
            int level = 0;
            level += 1;
            characterMotor.attackCooldown -= speed;
            availablePoints -= 1;
            pointsHaste.text = "" + level;
            pointsText.text = "Points: " + availablePoints;

        }
    }

    public void addPolyvalence(float poly)
    {
        if (availablePoints >= 1)
        {
            int level = 0;
            level += 1;
            characterMotor.HealSpellAmount += poly;
            availablePoints -= 1;
            pointsPolyvalence.text = "" + level;
            pointsText.text = "Points: " + availablePoints;
        }
    }

    public void addChanceCritic(float critic)
    {
        if (availablePoints >= 1)
        {
            
        }
    }

    public void addChanceEsquive(float esquive)
    {
        if (availablePoints >= 1)
        {

        }
    }

    public void addChanceParade(float parade)
    {
        if (availablePoints >= 1)
        {

        }
    }
    public void Open()
    {
        isOpen = true;
    }
}
