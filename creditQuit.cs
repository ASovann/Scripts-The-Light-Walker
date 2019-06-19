using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditQuit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
        }

    }
    void end()
    {
        Application.Quit();
    }
}
