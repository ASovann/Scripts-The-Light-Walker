using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public Text interaction;
    public float distanceToOpen;
    private float distance;
    bool show;
    public string scene;
    private GameObject go;
    private GameObject g02;
  

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("Player");
        g02 = GameObject.Find("Inventory's 1");
        
        interaction = GameObject.Find("Interaction").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position);
        if (distance <= distanceToOpen)
        {
            switch(show)
            {
                case false:
                    interaction.text = "Press E";
                    break;
                case true:
                    interaction.text = "";
                    break;

            }
            if(Input.GetKeyDown(KeyCode.E) && !show)
            {
                show = true;
                //if (scene != "SampleScene")
                //{
                //    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //    cube.transform.position = go.transform.position; 
                //}

                StartCoroutine(LoadYourAsyncScene());
                
                
            }

        }
        else if (distanceToOpen < distance && show)
        {
            show = false;
        }
        else if (distanceToOpen < distance && distance <= distanceToOpen + 1)
        {
            interaction.text = "";
        }
    }
    IEnumerator LoadYourAsyncScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(scene));
        SceneManager.MoveGameObjectToScene(g02, SceneManager.GetSceneByName(scene));
        go.GetComponent<CharacterMotor>().basePosition = new Vector3(154,1,135);
        go.transform.position = new Vector3(154, 1, 135);
        



    }

}
