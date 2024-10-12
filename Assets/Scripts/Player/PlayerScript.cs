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
    public Animator anim;

    public bool seeParent;
    // Start is called before the first frame update
    void Start()
    {
        sRen = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        seeParent = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        seeParent = false;
        if (collision.tag == "HealthyDoor" || collision.tag == "ChocolateDoor" || collision.tag == "SweetDoor" || collision.tag == "SourDoor")
        {
            seeParent = true;
        }
    }
}
