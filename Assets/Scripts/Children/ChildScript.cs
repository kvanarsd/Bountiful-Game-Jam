using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    public int candy;
    public SpriteRenderer sRen;
    private Rigidbody2D rb;

    public bool idle = true;
    public bool vertWalking = false;
    public bool horWalking = false;
    public bool following = false;
    public bool hurt = false;
    public bool treat = false;

    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        sRen = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
