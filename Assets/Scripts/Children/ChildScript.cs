using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    public SpriteRenderer sRen;
    public Animator anim;
    public string kidType;
    public Color ogColor;
    

    public bool idle = true;
    public bool vertWalking = false;
    public bool horWalking = false;
    public bool following = false;
    public bool hurt = false;
    public bool treat = false;

    // variables to store coroutines
    public Coroutine IdleCo;
    public Coroutine WalkCo;
    public Coroutine TreatCo;

    public GameObject candy;
    
    public int horDirection = 1;
    public int vertDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        sRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ogColor = sRen.color;

        int directionChoice = Random.Range(0, 1);
        if(directionChoice == 0 )
        {
            horDirection = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(horDirection == 1 )
        {
            sRen.flipX = true;
        } else
        {
            sRen.flipX = false;
        }
    }

    
}
