using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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

    // game end
    public bool gameover = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OneMinute());
    }

    // Update is called once per frame
    void Update()
    {
        candyCounter.text = player.candyHeld + " Candy";

        if (Time.timeScale == 0 && !dialog)
        {
            dialog = true;
            inGameCanvas.enabled = false;

        } else if (dialog && Time.timeScale != 0)
        {
            minute += 30;
            AddClock();
            dialog = false;
            inGameCanvas.enabled = true;
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

        clock.text = hour + ":" + minute + " pm";

        if (hour >= end)
        {
            gameover = true;
        }
    }
}
