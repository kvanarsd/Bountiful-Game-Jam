using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    // clock
    public int hour = 5;
    public int end = 10;
    public int minute = 0;
    [SerializeField] private int increment = 10;
    public Coroutine time;

    [SerializeField] private TMP_Text clock;
    [SerializeField] private TMP_Text candyCounter;
    [SerializeField] private PlayerScript player;

    // check dialog
    private bool dialog = false;
    public Canvas inGameCanvas;
    public Canvas titleCanvas;
    public Canvas pauseCanvas;
    private bool menu = false;

    // game end
    public bool gameover = false;
    public Canvas endMenu;
    public GameObject Death;
    public GameObject Love;
    public GameObject Kicked;
    public GameObject King;
    public GameObject Rejected;
    public TMP_Text endCandy;
    public TMP_Text date;

    // ending choice variables
    public int highCandy;
    public int midCandy;
    public List<DialogueManager> parents;
    public List<GameObject> images;
    public List<string> names; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OneMinute());
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            // pause game
            if (titleCanvas.enabled || pauseCanvas.enabled)
            {
                menu = true;
                Time.timeScale = 0;
            }

            // check for dialog pause
            if (Time.timeScale == 0 && !dialog && !menu)
            {
                dialog = true;
                inGameCanvas.enabled = false;

            }
            else if (dialog && Time.timeScale != 0)
            {
                minute += 30;
                AddClock();
                dialog = false;
                inGameCanvas.enabled = true;
            }

            if (menu && Time.timeScale != 1 && !titleCanvas.enabled && !pauseCanvas.enabled)
            {
                menu = false;
                Time.timeScale = 1;
            }

            // add candy
            candyCounter.text = player.candyHeld + " Candy";
        }
    }

    private IEnumerator OneMinute()
    {
        yield return new WaitForSeconds(5f);
        
        minute += increment;

        AddClock();

        StartCoroutine(OneMinute());
    }

    private void AddClock()
    {
        if (minute >= 60)
        {
            hour++;
            minute = minute - 60;
        }

        if (minute == 0)
        {
            clock.text = hour + ":00 pm";
        }
        else
        {
            clock.text = hour + ":" + minute + " pm";
        }

        if (hour >= end)
        {
            gameover = true;
            Time.timeScale = 0;
            EndGame();
        }
    }

    private void EndGame()
    {
        endMenu.enabled = true;
        endCandy.text = candyCounter.text;

        // check hearts
        bool love = false;
        bool attemptedLove = false;
        for (int i = 0; i < parents.Count; i++)
        {
            if (parents[i].currentHearts >= 3)
            {
                Debug.Log("love");
                love = true;
                images[i].SetActive(true);
                date.text = "You're cured! Enjoy a lovely night with " + names[i];
                break;
            }
            if (parents[i].currentHearts > 0)
            {
                Debug.Log("attempt");
                attemptedLove = true;
            }
        }
        if (love)
        {
            Love.SetActive(true);
        } else if(player.candyHeld < midCandy)
        {
            Death.SetActive(true);
        }else if (player.candyHeld < midCandy && player.candyHeld < highCandy && attemptedLove)
        {
            Rejected.SetActive(true);
        }else if(player.candyHeld < highCandy)
        {
            Kicked.SetActive(true);
        }else if(player.candyHeld >= highCandy)
        {
            King.SetActive(true);
        }
        
        
    }

    public void Reset()
    {
        SceneManager.LoadScene("GameScene");
    }
}
