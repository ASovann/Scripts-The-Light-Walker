using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    private float Distance;
    public float distanceToEnter;
    public Text interaction;
    public Scene cave;
    // Start is called before the first frame update
    void Start()
    {
        cave = SceneManager.GetSceneByName("Cave kit Demo");
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (Distance <= distanceToEnter)
        {
            
            interaction.text = "Press E";
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction.text = "";
                
                SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), cave);
            }
            else if (distanceToEnter < Distance)
            {
                interaction.text = "";
            }
            else if (distanceToEnter < Distance && Distance <= distanceToEnter + 2)
            {
                interaction.text = "";
            }
        }
    }
}