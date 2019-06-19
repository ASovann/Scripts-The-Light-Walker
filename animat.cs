using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animat : MonoBehaviour
{
    public GameObject glow;
    public GameObject glow2;
    public GameObject player;
    public AudioClip sound;
    public AudioClip sound2;
    private AudioSource source;
    private AudioSource source2;
    public Animator animator1;
    // Start is called before the first frame update
    void Start()
    {
        source = glow.GetComponent<AudioSource>();

        source2 = glow2.GetComponent<AudioSource>();


        StartCoroutine(Animations());
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(NextScene());
        }
    }

    IEnumerator Animations()
    {
        yield return new WaitForSeconds(1);
        glow.SetActive(true);
        source.PlayOneShot(sound);
        yield return new WaitForSeconds(5);
        glow.SetActive(false);
        glow2.SetActive(true);
        source2.PlayOneShot(sound2);

        yield return new WaitForSeconds(1);
        player.SetActive(true);
        yield return new WaitForSeconds(3);
        animator1.SetTrigger("Fade_out");
        yield return new WaitForSeconds(2);
        StartCoroutine(NextScene());

    }

    IEnumerator NextScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("loadingScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
