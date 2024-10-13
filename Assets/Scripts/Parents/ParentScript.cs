using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParentScript : MonoBehaviour
{
    //collider and renderer
    public BoxCollider2D openDoorCollider;
    public SpriteRenderer spriteRenderer;

    //open and close door sprites
    public Sprite openSprite;
    public Sprite closeSprite;

    private bool doorOpen = false;
    public int childCount = 0;
    public bool playerNear;

    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text info;
    [SerializeField] private TMP_Text kick;

    //number of children until door opens
    [SerializeField] private int CHILDREN_ALLOWED;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //opens door when the player is at the door or when children are at the door
    void OnTriggerEnter2D(Collider2D person)
    {
        if (person.tag == "Player"){
            playerNear = true;
            if(!doorOpen){
                doorOpen = true;
                spriteRenderer.sprite = openSprite;
            }
        }
        if(person.tag == "Child"){
            childCount++;
            text.enabled = true;
            if (!doorOpen && childCount >= CHILDREN_ALLOWED) {
                doorOpen = true;
                spriteRenderer.sprite = openSprite; 
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D person)
    {
        if (person.tag == "Player")
        {
            // info text
            kick.enabled = false;
            info.enabled = true;
            if (childCount > 0)
            {
                info.text = "Too many kids around to talk";
            }
            else if (person.gameObject.GetComponent<PlayerScript>().candyHeld < 250)
            {
                info.text = "Not enough candy to talk (Cost 250)";
            }
            else
            {
                info.text = "Press [E] to Talk (Cost 250)";
            }
        }
    }

    //closes door when player leaves, or when all the children leave
    void OnTriggerExit2D(Collider2D person)
    {
        if(person.tag == "Player"){
            playerNear = false;
            if(childCount == 0){
                doorOpen = false;
                spriteRenderer.sprite = closeSprite;
            }
            info.enabled = false;
        } 
        if(person.tag == "Child"){
            childCount--;
            if(childCount < CHILDREN_ALLOWED){
                if(!playerNear){
                    doorOpen = false;
                    spriteRenderer.sprite = closeSprite;
                }
                text.enabled = false;
            }
        }
    }
}
