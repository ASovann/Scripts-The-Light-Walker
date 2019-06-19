using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pnjChat : MonoBehaviour
{
    public GameObject chatPanel;
    public Inventory inventoryPlayer;
    PlayerInventory playerInv;
    public ItemDataBaseList itemDb;
    public Text interaction;
    public Text title;
    public Text description;
    [TextArea]
    public string Title;
    [TextArea]
    public string Description;

    public int questNumber;
    public int[] rewardList;
    public float distanceToOpenChat;
    private float Distance;
    private int amountSlots;
    private int slotsChecked;
    private bool transactionDone;
    private int openCount;
    bool showChat;
    public bool isAQuest;
    
    public int xpReward;
    public int goldReward;
    public Image button;
    public Text questAdded;
    


    // Start is called before the first frame update
    void Start()
    {
        chatPanel.SetActive(false);
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Distance = Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (Distance <= distanceToOpenChat)
        {
            switch(showChat)
            {
                case false:
                    interaction.text = "Press E";
                    break;

                case true:
                    interaction.text = "";
                    break;
            }
            
            
            if (Input.GetKeyDown(KeyCode.E) && !showChat)
            {
                
                showChat = true;
                prepareChat();

            }
            
        }
        else if (distanceToOpenChat < Distance && showChat)
        {
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            showChat = false;
            chatPanel.SetActive(false);
            questAdded.text = "";
        }
        else if (distanceToOpenChat < Distance && Distance <= distanceToOpenChat + 1)
        {
            interaction.text = "";
        }
        
    }

    void prepareChat()
    {
        title.text = Title;
        description.text = Description;
        if (!isAQuest)
        {
            if (openCount == 0)
            {
                if (rewardList.Length != 0)
                {
                    for (int i = 0; i < rewardList.Length; i++)
                    {
                        Item theItem = itemDb.getItemByID(rewardList[i]);
                        button.transform.GetComponent<Button>().onClick.AddListener(delegate { giveItem(theItem); });

                    }
                }
                button.transform.GetComponent<Button>().onClick.AddListener(delegate { giveXp(xpReward); });
                button.transform.GetComponent<Button>().onClick.AddListener(delegate { giveGold(goldReward); });

            }
            else
            {
                title.text = "";
                description.text = "Au revoir";
            }
        }
        else
        {
            if (openCount == 0)
            { 
                
                button.transform.GetComponent<Button>().onClick.AddListener(delegate { quest(questNumber); });

                
                
            }
            else
            {
                description.resizeTextForBestFit = false;
                title.text = "";
                description.text = "Bonne chance aventurier";
            }
        }
        
        button.transform.GetComponent<Button>().onClick.AddListener(delegate { exitPanel(); });
        chatPanel.SetActive(true);
    }

    void giveItem(Item finalItem)
    {
        amountSlots = inventoryPlayer.transform.GetChild(1).childCount;
        transactionDone = false;
        slotsChecked = 0;
        
        foreach (Transform child in inventoryPlayer.transform.GetChild(1))
        {
            if (child.childCount == 0)
            {
                inventoryPlayer.addItemToInventory(finalItem.itemID);
                transactionDone = true;
                openCount++;
                break;
            }
            slotsChecked++;
        }
        if (slotsChecked == amountSlots && transactionDone == false)
        {
            print("No more room in the inventory");
        }
    }

    void giveXp(int xp)
    {
        playerInv.currentXp += xp;
    }
    void giveGold(int gold)
    {
        playerInv.currentMoney += gold; 
    }
    void quest(int quest)
    {
        GameObject.Find("Player").GetComponent<questSystem>().questNumber = quest;
        questAdded.text = "Une quête ajoutée ";
        openCount++;
    }
    void exitPanel()
    {
        title.text = "";
        description.text = "";
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        showChat = false;
        chatPanel.SetActive(false);
    }
}
