using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    public int candy;
    public SpriteRenderer sRen;
    private Rigidbody2D rb;

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
