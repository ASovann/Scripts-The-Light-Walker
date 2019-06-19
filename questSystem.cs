using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class questSystem : MonoBehaviour
{
    public GameObject panelQuest;
    public bool showPanel;
    PlayerInventory playerInv;
    public Text description;
    public Text description2;
    public Text description3;
    public Text description4;
    public Text interaction;
    public Text AddQuest;
    public int questNumber;
    public int counterSpider;
    public int counterDragon;
    public int counterGolem;
    public int counterDemon;
    public string nameKill;
    private float textRepeatTime;
    public float textTime;
    
    

    // Start is called before the first frame update
    void Start()
    {
        playerInv = GetComponent<PlayerInventory>();
        panelQuest.SetActive(false);
        textRepeatTime = textTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            showPanel = !showPanel;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && showPanel)
        {

            showPanel = !showPanel;

        }
        if(showPanel)
        {
            panelQuest.SetActive(true);
        }
        else
        {
            panelQuest.SetActive(false);
        }
        
        
        switch(questNumber)
        {
            case 1:
                textRepeatTime -= Time.deltaTime;
                if(textRepeatTime <= 0)
                {
                    AddQuest.text = "";
                    textRepeatTime = textTime;
                }
                if (nameKill == "Spider")
                {
                    nameKill = ""; 
               
                    counterSpider -= 1;
                }
                description.text = questNumber + "- " + "Tuez " + counterSpider + " Spiders";
                if (counterSpider <= 0)
                {
                    
                    playerInv.currentXp += 500;
                    
                    interaction.text = "Quest Completed +500 XP";
                    
                    questNumber++;
                    counterSpider = 0;
                }
                
                break;
            case 2:
                textRepeatTime -= Time.deltaTime;
                if (textRepeatTime <= 0)
                {
                    interaction.text = "";
                    textRepeatTime = textTime;
                }
                if (nameKill == "Dragon")
                {
                    nameKill = "";
                    counterSpider -= 1;
                }
                description2.text = questNumber + "- " + "Tuez " + counterDragon + " Dragons";
                if (counterDragon <= 0)
                {
                    textRepeatTime -= Time.deltaTime;
                    playerInv.currentXp += 700;
                    interaction.text = "Quest Completed +700 XP";
                    
                    questNumber++;
                    counterDragon = 0;
                    

                }
                break;
            case 3:
                if (textRepeatTime <= 0)
                {
                    interaction.text = "";
                    textRepeatTime = textTime;
                }
                if (nameKill == "golem")
                {

                    nameKill = "";
                    counterGolem--;

                }
                description3.text = questNumber + "- " + "Tuez " + counterGolem + " Golems";
                if(counterGolem <= 0)
                {
                    textRepeatTime -= Time.deltaTime;
                    playerInv.currentXp += 300;
                    interaction.text = "Quest Completed +300 XP";
                    
                    questNumber++;
                    counterGolem = 0;
                    

                }
                break;
            case 4:
                if (textRepeatTime <= 0)
                {
                    interaction.text = "";
                    textRepeatTime = textTime;
                }
                if (nameKill == "demon")
                {
                    nameKill = "";
                    counterDemon--;

                }
                description4.text = questNumber + "- " + "Tuez " + counterDemon + " Demons";
                if (counterDemon <= 0)
                {
                    textRepeatTime -= Time.deltaTime;
                    playerInv.currentXp += 1000;
                    interaction.text = "Quest Completed +1000 XP";
                    
                    counterDemon = 0;
                    questNumber++;
                }
                
                break;
            case 5:
                if (textRepeatTime <= 0)
                {
                    interaction.text = "";
                    textRepeatTime = textTime;
                }
                description4.text = "TUEZ LE SEIGNEUR DEMON en prenant le portail dans la zone morte";
                break;
            default:
                description.text = "Vous n'avez pas de quête";
                break;


        }



    }
}
