using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private int childCount = 0;
    public bool playerNear;

    [SerializeField] private TMP_Text text;

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
        if (person.tag == "Player" && !doorOpen){
            playerNear = true;
            doorOpen = true;
            spriteRenderer.sprite = openSprite;
        }
        if(person.tag == "Child"){
            childCount++;
            if (!doorOpen && childCount >= CHILDREN_ALLOWED) {
                doorOpen = true;
                spriteRenderer.sprite = openSprite;
                text.enabled = true;
            }
        }
        
    }

    //closes door when player leaves, or when all the children leave
    void OnTriggerExit2D(Collider2D person)
    {
        if(person.tag == "Player" && childCount == 0){
            playerNear = false;
            doorOpen = false;
            spriteRenderer.sprite = closeSprite;
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
