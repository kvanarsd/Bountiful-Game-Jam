using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildKick : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.enabled = false;
        }
    }
}
