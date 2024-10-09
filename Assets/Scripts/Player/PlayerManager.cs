using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class PlayerManager : MonoBehaviour
{
    public PlayerScript Player;
    [SerializeField] private PlayerFSM PlayerSM;

    private Rigidbody2D rb;

    // other refs
    [SerializeField] private ParentManager ParentMan;
    [SerializeField] private ChildrenManager ChildMan;

    public bool idle = true;
    public bool walking = false;
    public bool kicking = false;
    public bool throwing = false;

    //shooting stuff
    public GameObject CandyPrefab;
    public Transform offset;
    public float candySpeed = 7f;



    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKey == false)
        {
            idle = true;
        }

        if (throwing == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Throw();
            }
        }

        if (kicking == false && throwing == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                walking = true;
                idle = false;
            }
            else
            {
                idle = true;
                walking = false;
            }
        }

        

    }

    void Throw()
    {
        GameObject candy = Instantiate(CandyPrefab, offset.position, Player.transform.rotation);

        Rigidbody2D cRB = candy.GetComponent<Rigidbody2D>();

        if (Player.transform.localRotation.y == 0)
        {
            cRB.velocity = new Vector2(candySpeed, 0);
        }
        else
        {
            cRB.velocity = new Vector2(-candySpeed, 0);
        }
        

    }

}
