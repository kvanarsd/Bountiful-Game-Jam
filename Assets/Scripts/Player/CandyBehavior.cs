using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 targetPos;
    private float stopPoint = 0.1f;

    public void Initialize(Rigidbody2D cRB, Vector3 target)
    {
        rb = cRB;
        targetPos = target;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, targetPos) <= stopPoint)
        {
            rb.velocity = Vector2.zero;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Player")))
        {
            Destroy(gameObject);
        }
        
    }*/

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
