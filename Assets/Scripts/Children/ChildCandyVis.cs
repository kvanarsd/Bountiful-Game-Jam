using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCandyVis : MonoBehaviour
{
    private ChildScript parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<ChildScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Candy")
        {
            Debug.Log("following");
            parent.candy = collision.gameObject;
            parent.following = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Candy")
        {
            parent.following = false;
        }
    }
}
