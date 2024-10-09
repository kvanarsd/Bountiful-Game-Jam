using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int candyHeld;
    public int candyOnGround;

    private Rigidbody2D rb;
    public SpriteRenderer sRen;
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
