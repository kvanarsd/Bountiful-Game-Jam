using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildKick : MonoBehaviour
{
    private ChildrenManager man;
    private TMP_Text text;
    private TMP_Text info;
    // Start is called before the first frame update
    void Start()
    {
        man = FindObjectOfType<ChildrenManager>();
        text = man.text;
        info = man.info;
        text.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && text && !info.enabled)
        {
            text.enabled = true;
        }
        if (collision.tag == "Kick")
        {
            transform.parent.GetComponent<ChildScript>().hurt = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && text)
        {
            text.enabled = false;
        }
    }
}
