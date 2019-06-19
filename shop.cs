using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shop : MonoBehaviour
{
    public Inventory inventoryPlayer;
    public GameObject shopPanel;
    public ItemDataBaseList itemDb;
    PlayerInventory playerInv;
    public float distanceToOpenShop;
    public Text interaction;
    [Header("Id des items du shop")]
    public int item1Id;
    public int item2Id;
    public int item3Id;

    public Text itemText1;
    public Text itemText2;
    public Text itemText3;

    public Image iconItem1;
    public Image iconItem2;
    public Image iconItem3;

    private int amountSlots;
    private int slotsChecked;
    private bool transactionDone;
    private float Distance;

    bool showShop;
    
    Item theItem1;
    Item theItem2;
    Item theItem3;

    // Start is called before the first frame update
    void Start()
    {
        
        shopPanel.SetActive(false);
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (Distance <= distanceToOpenShop)
        {
            switch(showShop)
            {
                case false:
                    interaction.text = "Press E";
                    break;
                case true:
                    interaction.text = "";
                    break;
            }
                
          
           
            if (Input.GetKeyDown(KeyCode.E) && !showShop)
            {
                
                showShop = true;
                prepareShop();
            }
            
            
        }
        else if ((distanceToOpenShop < Distance || Input.GetKeyDown(KeyCode.Escape)) && showShop )
        {
            
            iconItem1.GetComponent<Button>().onClick.RemoveAllListeners();
            iconItem2.GetComponent<Button>().onClick.RemoveAllListeners();
            iconItem3.GetComponent<Button>().onClick.RemoveAllListeners();
            shopPanel.SetActive(false);
            showShop = false;
        }
        else if (distanceToOpenShop < Distance && Distance <= distanceToOpenShop + 1)
        {
            interaction.text = "";
        }
        
        

    }

    void prepareShop()
    {
        
        theItem1 = itemDb.getItemByID(item1Id);
        theItem2 = itemDb.getItemByID(item2Id);
        theItem3 = itemDb.getItemByID(item3Id);

        itemText1.text = theItem1.itemName + " ( Prix: " + theItem1.itemValue + " golds ) ";
        itemText2.text = theItem2.itemName + " ( Prix: " + theItem2.itemValue + " golds ) ";
        itemText3.text = theItem3.itemName + " ( Prix: " + theItem3.itemValue + " golds ) ";

        iconItem1.sprite = theItem1.itemIcon;
        iconItem2.sprite = theItem2.itemIcon;
        iconItem3.sprite = theItem3.itemIcon;

        iconItem1.transform.GetComponent<Button>().onClick.AddListener(delegate { buyItem(theItem1); });

        iconItem2.transform.GetComponent<Button>().onClick.AddListener(delegate { buyItem(theItem2); });
        iconItem3.transform.GetComponent<Button>().onClick.AddListener(delegate { buyItem(theItem3); });
        shopPanel.SetActive(true);
        
    }
    
    void buyItem(Item finalItem)
    {
        amountSlots = inventoryPlayer.transform.GetChild(1).childCount;
        transactionDone = false;
        slotsChecked = 0;
        
        foreach(Transform child in inventoryPlayer.transform.GetChild(1))
        {
            if (child.childCount == 0)
            {
                
                if (playerInv.currentMoney >= finalItem.itemValue)
                {
                    inventoryPlayer.addItemToInventory(finalItem.itemID);
                    playerInv.currentMoney -= finalItem.itemValue;
                    transactionDone = true;
                    print("Player bought: " + finalItem.itemName);
                    break;
                }
                else
                {
                    print("Not enought money");
                }
            }
            slotsChecked++;
            
        }
        if (slotsChecked == amountSlots && transactionDone == false)
        {
            print("No more room in the inventory");
        }
    }

}
