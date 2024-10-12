using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // the parent dialogue script
    public DScript parentDialogue;

    // Visual novel UI aspects
    public Image parentSprite;
    public GameObject dialoguePanel;
    public TMP_Text dialogueTextBox;
    public GameObject exitButton;

    // parent script, used for playerNear
    public ParentScript pS;

    // the delay and buttons for choice 1 and choice 2, and their respective text boxes
    public float choiceDelay = 1f;
    public GameObject choice1;
    public TMP_Text choiceText1;
    public GameObject choice2;
    public TMP_Text choiceText2;

    // parent variables
    int currentIndex;
    public int currentHearts;
    bool lastLine = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTextBox.text = "";
        currentIndex = 0;
        currentHearts = 0;
        lastLine = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && pS.playerNear && pS.childCount == 0){
            if(!lastLine && currentIndex < parentDialogue.lines.Length-1 && parentDialogue.lines.Length != 0){
                if(dialoguePanel.activeInHierarchy) {
                    clearText();
                } else {
                    dialoguePanel.SetActive(true);
                    Time.timeScale=0;
                    showLine();
                }
            }
        }
    }

    void showLine()
    {
        insertText();
        if(lastLine)
        {
            //show last line, then end
            exitButton.SetActive(true);
        } else {
            StartCoroutine(ShowChoices());
        }
    }

    IEnumerator ShowChoices()
    {
        yield return new WaitForSeconds(choiceDelay);
        choice1.SetActive(true);
        choice2.SetActive(true);
    }

    // inserts parent dialogue text and choice text if applicable
    public void insertText()
    {
        dialogueTextBox.text = parentDialogue.lines[currentIndex].dialogueText;

        // inputs choice text if choices exist
        if(parentDialogue.lines[currentIndex].choices.Length != 0)
        {
            choiceText1.text = parentDialogue.lines[currentIndex].choices[0].choiceText;
            choiceText2.text = parentDialogue.lines[currentIndex].choices[1].choiceText;
        } else {
            lastLine = true;
        }
    }

    // choice 1
    public void selectChoice1()
    {
        currentHearts += parentDialogue.lines[currentIndex].choices[0].hearts;
        if(!lastLine){
            currentIndex = parentDialogue.lines[currentIndex].choices[0].nextIndex;
            clearText();
            showLine();
        }
    }

    // choice 2
    public void selectChoice2()
    {
        currentHearts += parentDialogue.lines[currentIndex].choices[1].hearts;
        if(!lastLine){
            currentIndex = parentDialogue.lines[currentIndex].choices[1].nextIndex;
            clearText();
            showLine();
        }
    }

    //clears parent text, choice text, and choice buttons
    public void clearText()
    {
        dialogueTextBox.text = "";
        choiceText1.text = "";
        choiceText2.text = "";
        choice1.SetActive(false);
        choice2.SetActive(false);
    }

    // closes dialogue panel and resumes game
    public void CloseText()
    {
        clearText();
        //Debug.Log(currentHearts);
        if(currentIndex < parentDialogue.lines.Length-1){
            currentIndex++;
            lastLine = false;
        } else {
            lastLine = true;
        }
        exitButton.SetActive(false);
        dialoguePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
