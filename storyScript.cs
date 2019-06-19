using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storyScript : MonoBehaviour
{
    public float letterPause = 0.2f;
    string message;
    Text textComp;
    public AudioClip typing;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText());
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TypeText()
    {
        foreach(char letter in message.ToCharArray())
        {
            textComp.text += letter;
            
            yield return new WaitForSeconds(letterPause);
            source.PlayOneShot(typing);

        }
    }
}
