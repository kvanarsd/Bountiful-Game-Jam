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
        StartCoroutine(teleport());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Vector2.Distance(transform.position, targetPos) <= stopPoint)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
            
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Player")))
        {
            Destroy(gameObject);
        }
        
    }*/

    private bool IsOutOfBounds()
    {
        if(transform.position.x < -12.2 || transform.position.x > 12.2 || transform.position.y < -4.85 || transform.position.y > -2.86)
        {
            return true;
        }
        return false;
    }

    private IEnumerator teleport()
    {
        Debug.Log("before yield");
        yield return new WaitForSeconds(5f);
        Debug.Log("after yield");
        gameObject.transform.position = new Vector3(1000, 1000, 1000);
        Debug.Log(transform.position);
        Destroy(gameObject);
        Debug.Log("after teleport");
    }
    

    
}
