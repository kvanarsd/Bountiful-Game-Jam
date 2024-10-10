using System.Collections;
using System.Collections.Generic;
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

    //UI
    public GameObject textBox;
    public bool playerNear;
    public GameObject exitButton;

    private bool doorOpen = false;
    private int childCount = 0;

    //number of children until door opens
    [SerializeField] private int CHILDREN_ALLOWED;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerNear){
            if(textBox.activeInHierarchy){

            } else {
                textBox.SetActive(true);
                exitButton.SetActive(true);
                Time.timeScale=0;
            }
        }
    }

    public void CloseText()
    {
        textBox.SetActive(false);
        Time.timeScale = 1;
    }

    //opens door when the player is at the door or when children are at the door
    void OnTriggerEnter2D(Collider2D person)
    {
        if(person.tag == "Player"){
            playerNear = true;
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

    //closes door when player leaves, or when all the children leave
    void OnTriggerExit2D(Collider2D person)
    {
        if(person.tag == "Player"){
            playerNear = false;
            if(childCount == 0){
                doorOpen = false;
                spriteRenderer.sprite = closeSprite;
            }
            
        } else if(person.tag == "child"){
            childCount--;
            if(childCount < CHILDREN_ALLOWED){
                doorOpen = false;
                spriteRenderer.sprite = closeSprite;
            }
        }
    }
}
