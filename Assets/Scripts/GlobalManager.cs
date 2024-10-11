using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // game end
    public bool gameover = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTime()
    {
        time = StartCoroutine(OneMinute());
    }
    private IEnumerator OneMinute()
    {
        yield return new WaitForSeconds(60);
        minute += increment;
        if (minute >= 60)
        {
            hour++;
            minute = 0;

            if (hour >= end)
            {
                gameover = true;
            }
        }
    }
}
