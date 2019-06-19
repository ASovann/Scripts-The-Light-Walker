using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambient : MonoBehaviour
{
    public AudioClip Ambient;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        source.PlayOneShot(Ambient);
        source.loop = true;
        
    }
}
