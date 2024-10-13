using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource aud;

    public PlayerManager PlayMan { get; set; }

    public AudioClip music;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
