using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    public SpriteRenderer sRen;
    //private Rigidbody2D rb;
    [SerializeField] private TMP_Text text;

    public bool idle = true;
    public bool vertWalking = false;
    public bool horWalking = false;
    public bool following = false;
    public bool hurt = false;
    public bool treat = false;

    public GameObject candy;

    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        sRen = GetComponent<SpriteRenderer>();

        text.enabled = false;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.enabled = false;
        }
    }
}
