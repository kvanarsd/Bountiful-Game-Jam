using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentScript : MonoBehaviour
{
    //collider and renderer
    public BoxCollider2D openDoorCollider;
    public SpriteRenderer spriteRenderer;

    //open and close door sprites
    public Sprite openSprite;
    public Sprite closeSprite;

    bool doorOpen = false;
    int childCount = 0;

    //number of children until door opens
    public int CHILDREN_ALLOWED;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //opens door when the player is at the door
    void OnTriggerEnter2D(Collider2D person)
    {
        if(person.tag == "Player"){
            doorOpen = true;
            spriteRenderer.sprite = openSprite;
        }
        if(person.tag == "child"){
            if(childCount < CHILDREN_ALLOWED){
                childCount++;
            } else if (!doorOpen) {
                doorOpen = true;
                spriteRenderer.sprite = openSprite;
            }
        }
    }

    //closes door when player leaves
    void OnTriggerExit2D(Collider2D person)
    {
        if(person.tag == "Player" && childCount == 0){
            doorOpen = false;
            spriteRenderer.sprite = closeSprite;
        } else if(person.tag == "child"){
            childCount--;
            if(childCount < CHILDREN_ALLOWED){
                doorOpen = false;
                spriteRenderer.sprite = closeSprite;
            }
        }
    }
}
