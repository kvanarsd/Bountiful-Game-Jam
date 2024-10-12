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

    // check dialog
    private bool dialog = false;

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
        if (Time.timeScale == 0)
        {
            dialog = true;
        } else if (dialog)
        {
            minute += 30;
            AddClock();
            dialog = false;
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
