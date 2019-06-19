using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    //Id de l'arme actuelle
    public int itemID;
    //membre de notre personnage
    public GameObject bodyPart;

    //Liste de nos armes (objet se trouvant dans la main du personnage)
    [SerializeField]
    public List<GameObject> itemList = new List<GameObject>();
    public List<GameObject> secondList = new List<GameObject>();
    



    // Update is called once per frame
    void Update()
    {

        
        if (transform.childCount > 0)
        {
            itemID = gameObject.GetComponentInChildren<ItemOnObject>().item.itemID;
            
        }
        else
        {
            itemID = 0;

            for (int i = 0; i < itemList.Count; i++)
            {

                itemList[i].SetActive(false);
                

            }
        }
        //Si le jeu detecte plusieurs armes dans la main on les désactives sauf celle équipé
        if (bodyPart.transform.childCount > 1)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].SetActive(false);
                
            }
        }
        //copier /coller le bloc suivant pour chacune de vos armes 
        //weaponid correspondant à l'ID de l'arme dans la BDD
        //i = X correspond à l'ID (ou l'index) de l'arme dans la liste

        
        //iron helmet

        if (itemID == 2 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 0)
                {
                    itemList[i].SetActive(true);


                }
            }
        }

        

        
        //iron bracelet

        if (itemID == 3 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 0 || i == 1)
                {
                    itemList[i].SetActive(true);


                }
            }
        }

        //knight helmet
        if(itemID == 10 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 1)
                {
                    itemList[i].SetActive(true);
                }
            }
        }

        //rusty knight helmet
        if (itemID == 11 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 2)
                {
                    itemList[i].SetActive(true);
                }
            }
        }


        //metal helmet
        if (itemID == 12 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 3)
                {
                    itemList[i].SetActive(true);
                }
            }
        }

        //dovakhiin helmet
        if (itemID == 13 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 4)
                {
                    itemList[i].SetActive(true);
                }
            }
        }

        //leather chest
        if (itemID == 14 && transform.childCount > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == 0)
                {
                    itemList[i].SetActive(true);
                }
            }
        }

    }
}
