using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentScript : MonoBehaviour
{
    public BoxCollider2D openDoorCollider;
    public SpriteRenderer spriteRenderer;

    public Sprite openSprite;
    public Sprite closeSprite;
    bool doorOpen = false;

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
    }

    //closes door when player leaves
    void OnTriggerExit2D(Collider2D person)
    {
        if(person.tag == "Player"){
            doorOpen = false;
            spriteRenderer.sprite = closeSprite;
        }
    }
}
