using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    public SpriteRenderer sRen;
    //private Rigidbody2D rb;
    

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
    public Coroutine FollowCo;

    public GameObject candy;

    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        sRen = GetComponent<SpriteRenderer>();

        int directionChoice = Random.Range(0, 1);
        if(directionChoice == 0 )
        {
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
