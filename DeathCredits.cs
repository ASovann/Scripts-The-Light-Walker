using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCredits : MonoBehaviour
{
    private ennemyAI ennemy;
    // Start is called before the first frame update
    void Start()
    {
        ennemy = gameObject.GetComponent<ennemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ennemy.isDead)
        {
            StartCoroutine(Credits());
        }
    }

    IEnumerator Credits()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Generic");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
