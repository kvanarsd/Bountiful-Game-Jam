using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Dialogue line class
    [System.Serializable]
    public class DLine
    {
        [TextArea] public string dialogueText; //text
        public Sprite parentEmote; //associated sprite
        public DChoice[] choices; //choices, usually array length 2
    }

    // choices class
    [System.Serializable]
    public class DChoice
    {
        [TextArea] public string choiceText; //text
        public int hearts; //how many hearts the choice is worth
        public int nextIndex; //the index of the next dialogue line
    }

    public Image parentSprite;
    public GameObject textBox;
    public GameObject exitButton;
    public ParentScript pS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && pS.playerNear){
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
}
