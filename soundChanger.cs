using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundChanger : MonoBehaviour
{
    private GameObject cam;
    public AudioClip dungeonSound;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        cam.GetComponent<AudioSource>().clip = dungeonSound;
        cam.GetComponent<AudioSource>().Play();
        cam.GetComponent<AudioSource>().loop = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
