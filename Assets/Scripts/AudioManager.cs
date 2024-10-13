using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource aud;

    public PlayerManager PlayMan { get; set; }

    public AudioClip kick;
    public AudioClip scream;

    bool instate;
    bool togglePlay;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayMan.kicking)
        {
            aud.clip = kick;
            aud.loop = false;
            instate = true;
        }
        else
        {
            instate = false;
        }

        if(instate == true && togglePlay == false)
        {
            aud.Play();
            togglePlay = false;
        }else if(instate == false)
        {
            aud.Stop();
            togglePlay = false;
        }
    }
}
